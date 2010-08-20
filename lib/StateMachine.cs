using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NodeCS
{
    /// <summary>
    /// A state machine.
    /// 
    /// Set up your different states, and then transition to them
    /// at various stages.
    /// </summary>
    public class StateMachine
        : EventEmitter
    {
        /// <summary>
        /// The list of states.
        /// </summary>
        protected Dictionary<string, Event> states = new Dictionary<string, Event>();

        /// <summary>
        /// First callback to run.
        /// </summary>
        protected Callback initialCallback;

        /// <summary>
        /// The next requested state.
        /// </summary>
        protected string nextState = null;

        public StateMachine() { }

        /// <summary>
        /// Create a new State Machine.
        /// </summary>
        /// <param name="startCallback">Initial function to call.</param>
        public StateMachine(Callback initialCallback)
        {
            this.initialCallback = initialCallback;
        }

        /// <summary>
        /// Add a state.
        /// </summary>
        /// <param name="state">State name.</param>
        /// <param name="callback">State handler.</param>
        public void addState(string state, Event callback)
        {
            this.states[state] = callback;
        }

        /// <summary>
        /// Transition to the given state.
        /// </summary>
        /// <param name="state">State to transition to.</param>
        /// <param name="data">Any data to pass to the state handler.</param>
        public void transitionTo(string state, params object[] data)
        {
            if (false == this.states.ContainsKey(state))
            {
                this.emit("error", new MissingStateException(state));
            }
            else
            {
                this.states[state](data);
            }
        }

        public void transition(EventEmitter ev, params object[] data)
        {
            this.transition(ev, "success", "error", data);
        }
        public void transition(EventEmitter ev, string success, params object[] data)
        {
            this.transition(ev, success, "error", data);
        }
        public void transition(EventEmitter ev, string success, string error, params object[] data)
        {
            ev.addListener("success", delegate(object[] p) { this.transitionTo(success, p); });
            ev.addListener("error", delegate(object[] p) { this.transitionTo(error, p); });

            if (typeof (StateMachine) == ev.GetType()) {
                (ev as StateMachine).start();
            }
        }

        /// <summary>
        /// Emit a success result.
        /// </summary>
        /// <param name="data"></param>
        public void emitSuccess(params object[] data)
        {
            this.emit("success", data);
        }

        /// <summary>
        /// Emit an error result.
        /// </summary>
        /// <param name="data"></param>
        public void emitError(params object[] data)
        {
            this.emit("error", data);
        }

        /// <summary>
        /// Start the EventEmitter.
        /// </summary>
        public void start()
        {
            Async.run(() => initialCallback());
        }

        protected override void _Spec()
        {
            this._addSpec("StateMachine", delegate(SpecBack.Spec.SpecDone done)
            {
                StateMachine action = null;
                action = new StateMachine(delegate()
                 {
                     action.transitionTo("test state", 1);
                 });
                action.addState("test state", delegate(object[] data)
                {
                    done(data.Length > 0 && (int)data[0] == 1);
                });
                action.start();
            });
        }
    }

    /// <summary>
    /// Tried to transition to a state that had not been defined.
    /// </summary>
    public class MissingStateException : Exception
    {
        public MissingStateException(string state)
            : base("State not defined: " + state)
        {
        }
    }
}

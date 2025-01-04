using Sirenix.OdinInspector;
using UnityEngine;
using System.Collections.Generic;

namespace GD.FSM
{
    /// <summary>
    /// Controls the FSM behavior by managing the current state, executing actions,
    /// and handling state transitions based on defined conditions.
    /// </summary>
    public class FSMController : MonoBehaviour
    {
        [FoldoutGroup("FSM Settings"), InlineEditor]
        public FSMState initialState;

        [FoldoutGroup("FSM Settings"), InlineEditor]
        public List<FSMTransition> globalTransitions;

        [FoldoutGroup("Runtime Info")]
        public FSMState currentState;

        private void Start()
        {
            currentState = initialState;
        }

        private void Update()
        {
            if (!CheckGlobalTransitions())
            {
                ExecuteActions(currentState.actions);
                CheckTransitions();
            }
        }

        /// <summary>
        /// Execute each action.
        /// Ben - Added parameter so enter and exit actions may run
        /// </summary>
        private void ExecuteActions(List<FSMAction> stateActions)
        {
            foreach (FSMAction action in stateActions)
            {
                action.Execute();
            }
        }

        /// <summary>
        /// Check transitions possible in this state. If condition met, change states.
        /// </summary>
        private void CheckTransitions()
        {
            foreach (FSMTransition transition in currentState.transitions)
            {
                if (transition.condition.Evaluate())
                {
                    // Previous state's exit action
                    ExecuteActions(currentState.exitActions);

                    currentState = transition.targetState;

                    // This state's entrance action
                    ExecuteActions(currentState.enterActions);
                    break;
                }
            }
        }

        /// <summary>
        /// Check transitions that apply across all states (regardless of current state).
        /// </summary>
        /// <returns></returns>
        private bool CheckGlobalTransitions()
        {

            foreach (var transition in globalTransitions)
            {
                // The condition is true
                if (transition.condition.Evaluate())
                {
                    currentState = transition.targetState;
                    return true;
                }
            }
            return false;
        }
    }
}
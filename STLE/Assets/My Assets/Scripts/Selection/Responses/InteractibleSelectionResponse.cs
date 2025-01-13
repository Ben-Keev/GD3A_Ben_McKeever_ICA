using UnityEngine;
using GD.Items;
using GD.FSM;

namespace GD.Selection
{   
    /// <summary>
    /// Interacts with interactible objects
    /// </summary>
    public class InteractibleSelectionResponse : SelectionResponse
    {
        public override void OnSelect(Transform currentTransform)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player.GetComponent<FSMController>().currentState.name == "Explore") // Player can interact
            {
                currentTransform.GetComponent<IInteractable>().OnHover();

                //Debug.Log(currentTransform.gameObject.name);

                if (player.GetComponent<PlayerExploreInputHandler>().interact.WasPressedThisFrame())
                {
                    currentTransform.GetComponent<IInteractable>().Interact(player);
                }
            }
        }

        public override void OnDeselect(Transform currentTransform)
        {
            currentTransform.GetComponent<IInteractable>().OnDehover();
        }
    }
}
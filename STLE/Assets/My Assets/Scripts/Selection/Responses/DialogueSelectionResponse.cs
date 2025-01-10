using UnityEngine;
using GD.Items;
using GD.FSM;

namespace GD.Selection
{   
    /// <summary>
    /// Opens a dialogue box. The script loaded is according to the name of the selection
    /// </summary>
    public class DialogueSelectionResponse : SelectionResponse
    {
        public override void OnSelect(Transform currentTransform)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player.GetComponent<FSMController>().currentState.name == "Explore") // Player can interact
            {
                currentTransform.GetComponent<IInteractable>().OnHover();

                //Debug.Log(currentTransform.gameObject.name);

                if (GetComponent<PlayerExploreInputHandler>().interact.WasPressedThisFrame())
                    currentTransform.GetComponent<IInteractable>().Interact(player);
            }
        }

        public override void OnDeselect(Transform currentTransform)
        {
            currentTransform.GetComponent<IInteractable>().OnDehover();
        }
    }
}
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
                if (currentTransform.GetComponent<IInteractable>() is IInteractable interactable)
                {
                    interactable.OnHover();

                    if (player.GetComponent<PlayerExploreInputHandler>().move.WasPressedThisFrame() && currentTransform.GetComponent<Item>() is Item item)
                        item.Interact(player);

                    if (player.GetComponent<PlayerExploreInputHandler>().interact.WasPressedThisFrame())
                        interactable.Interact(player);
                }

            }
        }

        public override void OnDeselect(Transform currentTransform)
        {
            currentTransform.GetComponent<IInteractable>().OnDehover();
        }
    }
}
using UnityEngine;
using GD.Items;
using GD.FSM;

namespace GD.Selection
{   
    /// <summary>
    /// Triggers response on interaction with an Iinteractible
    /// </summary>
    public class InteractableSelectionResponse : SelectionResponse
    {
        /// <summary>
        /// Triggers hover and interacts if interact pressed
        /// </summary>
        /// <param name="currentTransform">Object being hovered over</param>
        public override void OnSelect(Transform currentTransform)
        {
            // Player reference
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player.GetComponent<FSMController>().currentState.name == "Explore") // Player can interact
            {
                if (currentTransform.GetComponent<IInteractable>() is IInteractable interactable) // Raycast hit an interactable object
                {
                    interactable.OnHover(); // Mouse is over the object

                    if (player.GetComponent<PlayerExploreInputHandler>().Move.WasPressedThisFrame() && currentTransform.GetComponent<Item>() is Item item)
                        item.Interact(player); // The object is an item. Allow for it to be interacted using "move" left click input.

                    if (player.GetComponent<PlayerExploreInputHandler>().Interact.WasPressedThisFrame())
                        interactable.Interact(player); // The object is some other interactible item. It's interacted with only using right click.
                }

            }
        }

        /// <summary>
        /// Disables hover
        /// </summary>
        /// <param name="currentTransform">Object previously hovered over</param>
        public override void OnDeselect(Transform currentTransform)
        {
            currentTransform.GetComponent<IInteractable>().OnDehover();
        }
    }
}
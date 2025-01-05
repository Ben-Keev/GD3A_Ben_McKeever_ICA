using UnityEngine;
using GD.Items;

namespace GD.Selection
{   
    /// <summary>
    /// Opens a dialogue box. The script loaded is according to the name of the selection
    /// </summary>
    public class DialogueSelectionResponse : SelectionResponse
    {
        public override void OnSelect(Transform currentTransform)
        {
            if (GetComponent<PlayerExploreInputHandler>().interact.WasPressedThisFrame())
            {
                // https://docs.unity3d.com/6000.0/Documentation/ScriptReference/GameObject-layer.html
                if (currentTransform.gameObject.layer == LayerMask.NameToLayer("NPC"))
                    currentTransform.GetComponent<NPC>().Interact(GameObject.FindGameObjectWithTag("Player"));

                if (currentTransform.gameObject.layer == LayerMask.NameToLayer("Item"))
                    // Call interaction event
                    currentTransform.GetComponent<Item>().Interact(GameObject.FindGameObjectWithTag("Player"));
            }
        }
    }
}
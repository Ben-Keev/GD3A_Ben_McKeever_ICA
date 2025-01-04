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
                DialogueBoxManager.Instance.LoadDialogue(currentTransform.name);

                // https://docs.unity3d.com/6000.0/Documentation/ScriptReference/GameObject-layer.html
                if (currentTransform.gameObject.layer == 6) // 6 is the item layer
                    // Call interaction event
                    currentTransform.GetComponent<Item>().Interact(GameObject.FindGameObjectWithTag("Player"));
            }
        }
    }
}
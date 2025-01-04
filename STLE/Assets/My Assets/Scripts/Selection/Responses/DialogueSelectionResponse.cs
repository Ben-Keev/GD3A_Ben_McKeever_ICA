using UnityEngine;

namespace GD.Selection
{   
    /// <summary>
    /// Opens a dialogue box. Regardless of what was selected.
    /// </summary>
    public class DialogueSelectionResponse : SelectionResponse
    {
        public override void OnSelect(Transform currentTransform)
        {
            if (GetComponent<PlayerExploreInputHandler>().interact.WasPressedThisFrame())
            {
                if(currentTransform.name.Contains("student"))
                DialogueBoxManager.Instance.LoadDialogue(currentTransform.name);
            }
        }
    }
}
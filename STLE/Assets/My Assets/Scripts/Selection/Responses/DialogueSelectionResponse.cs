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
                DialogueBoxManager.Instance.LoadDialogue("student_a");
            }

            //Debug.Log(currentTransform.gameObject.name);
        }
    }
}
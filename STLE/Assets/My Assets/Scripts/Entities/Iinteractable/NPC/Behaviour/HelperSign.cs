using GD.Items;
using GD.Types;
using System;
using UnityEngine;

public class HelperSign : MonoBehaviour
{
    [SerializeField]
    private NPCData NPCData;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = transform.Find("Sign/Logo").GetComponent<MeshRenderer>();
    }

    public void OnFeedbackReceived(Tuple<ItemCategoryType, ItemData, FeedbackType> data)
    {
        ItemCategoryType expected = data.Item1;
        ItemData actual = data.Item2;

        DisplayFeedback(data.Item3);

        ItemCategoryType actualType = (ItemCategoryType)actual.ItemType;

        string[] dialogues = new string[2];

        dialogues[0] = $"You put {actual.Name} into the {expected.ToString()} bin.";

        // The items match up
        if (expected == actualType)
            dialogues[1] = $"Good job! {actual.Name} are supposed to go into the {expected.ToString()} bin.";

        // Cases where different process were mixed up
        else if (expected == ItemCategoryType.Trash && actualType == ItemCategoryType.Recycle)
            dialogues[1] = $"{actual.Name} deserves a bin that'll give it a second chance!";

        else if (expected == ItemCategoryType.Trash && actualType == ItemCategoryType.Compost)
            dialogues[1] = $"{actual.Name} won't help anything grow in the trash.";

        else if (expected == ItemCategoryType.Recycle && actualType == ItemCategoryType.Trash)
            dialogues[1] = $"I admire your attempt to recycle {actual.Name} but they're unsalvagable.";

        else if (expected == ItemCategoryType.Recycle && actualType == ItemCategoryType.Compost)
            dialogues[1] = $"{actual.Name} should find their own place to decompose naturally.";

        else if (expected == ItemCategoryType.Compost && actualType == ItemCategoryType.Trash)
            dialogues[1] = $"{actual.Name} are more trashy than fruity.";

        else if (expected == ItemCategoryType.Compost && actualType == ItemCategoryType.Recycle)
            dialogues[1] = $"{actual.Name} are reusable but not like that.";

        else
            dialogues[1] = $"I didn't even realise it was possible to put {actual.Name} into {expected.ToString()}";

        // Overwrite previous date to avoid needless use of storage.
        NPCData.OverwriteDialogue(dialogues);
    }
    private void DisplayFeedback(FeedbackType feedback)
    {
        meshRenderer.material = Resources.Load<Material>("Materials/" + feedback.ToString());
    }
}

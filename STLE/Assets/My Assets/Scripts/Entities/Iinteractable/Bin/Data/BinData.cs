using GD.Items;
using GD.Types;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// All data related to NPCs
/// </summary>
[CreateAssetMenu(fileName = "BinData", menuName = "GD/Data/Bin")]
public class BinData : SerializedScriptableObject
{
    #region Fields

    [FoldoutGroup("inventory", expanded: true)]
    [Tooltip("The bin's type")]
    [SerializeField]
    private ItemCategoryType binType;

    [FoldoutGroup("inventory", expanded: true)]
    [Tooltip("The bin's inventory")]
    [SerializeField]
    private Inventory binContents;

    [FoldoutGroup("inventory", expanded: true)]
    [Tooltip("The item this bin accepts")]
    [SerializeField]
    private ItemData acceptedItem;

    #endregion Fields

    #region Properties

    public ItemCategoryType BinType { get => binType; set => binType = value; }

    public Inventory BinContents { get => binContents; set => binContents = value; }

    public ItemData AcceptedItem { get => acceptedItem; set => acceptedItem = value; }


    #endregion Properties
}

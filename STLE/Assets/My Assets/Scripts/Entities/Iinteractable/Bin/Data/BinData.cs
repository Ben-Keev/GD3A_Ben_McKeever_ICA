using GD.Items;
using GD.Types;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stores all data for a bin.
/// </summary>
/// <see cref="ItemData"/>
/// <see cref="ItemCategoryType"/>
/// <see cref="Inventory"/>
/// <see cref="InventoryCollection"/>
[CreateAssetMenu(fileName = "BinData", menuName = "GD/Data/Bin")]
public class BinData : SerializedScriptableObject
{
    #region Fields

    [FoldoutGroup("inventory", expanded: true)]
    [Tooltip("What sort of rubbish is the bin for")]
    [SerializeField]
    private ItemCategoryType binType;

    [FoldoutGroup("inventory", expanded: true)]
    [Tooltip("The rubbish contained in the bin")]
    [SerializeField]
    private Inventory binContents;

    [FoldoutGroup("inventory", expanded: true)]
    [Tooltip("The item that's supposed to go in this bin")]
    [SerializeField]
    private ItemData correctItem;

    #endregion Fields

    #region Properties

    public ItemCategoryType BinType { get => binType; set => binType = value; }

    public Inventory BinContents { get => binContents; set => binContents = value; }

    public ItemData AcceptedItem { get => correctItem; set => correctItem = value; }


    #endregion Properties
}

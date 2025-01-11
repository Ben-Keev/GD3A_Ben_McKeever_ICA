using GD.Events;
using GD.Types;
using NUnit.Framework.Interfaces;
using Sirenix.OdinInspector;
using System;
using System.Xml.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace GD.Items
{
    /// <summary>
    /// Manages the players inventory, listens for events, etc.
    /// </summary>
    /// <see cref="Inventory"/>
    /// <see cref="ItemData"/>
    public class InventoryManager : MonoBehaviour
    {
        [SerializeField]
        [InlineEditor]
        [Tooltip("The player's inventory collection (e.g. a saddlebag")]
        private InventoryCollection inventoryCollection;

        [SerializeField]
        [InlineEditor]
        [Tooltip("The event to raise on succesful/unsuccesful inventory insertion")]
        private IntGameEvent onScoreEvent;

        [SerializeField]
        [InlineEditor]
        [Tooltip("Trigger particles on an inventory insertion")]
        private ParticleGameEvent onParticleEvent;

        [FoldoutGroup("SelectedInventory")]
        [SerializeField]
        [Tooltip("Currently selected inventory")]
        private int selectedInventory;

        [FoldoutGroup("SelectedInventory")]
        [SerializeField]
        [Tooltip("All possible items")]
        private ItemData[] possibleItems = new ItemData[3];

        [FoldoutGroup("UI")]
        [SerializeField]
        [InlineEditor]
        [Tooltip("The UI Component indicating the selected inventory")]
        private Image UISelectedInventory;

        [FoldoutGroup("UI")]
        [SerializeField]
        [InlineEditor]
        [Tooltip("The UI Component indicating how much is left in the selected inventory")]
        private TextMeshProUGUI UIItemsLeft;

        private void Awake()
        {
            //check if the inventory collection has been added
            if (inventoryCollection == null)
                throw new NullReferenceException("No inventory collection has been added");

            selectedInventory = 0;
            UpdateUI();

            inventoryCollection.ClearInventories();
        }

        public void CycleInventoryUp(InputAction.CallbackContext context)
        {
            selectedInventory++;

            if (selectedInventory >= 3)
                selectedInventory = 0;

            UpdateUI();
        }

        public void CycleInventoryDown(InputAction.CallbackContext context)
        {
            selectedInventory--;

            if (selectedInventory <= -1)
                selectedInventory = 2;
        
            UpdateUI();
        }

        /// <summary>
        /// Called on InventoryChange or CycleInventory
        /// </summary>
        public void UpdateUI()
        {
            ItemCategoryType target = (ItemCategoryType)selectedInventory;

            UISelectedInventory.sprite = inventoryCollection.Get(target).uiIcon;
            UIItemsLeft.text = inventoryCollection.Get(target).Count(possibleItems[(int) target]).ToString();
        }

        private void UpdateSelectedInventoryUI()
        {
            ItemCategoryType target = (ItemCategoryType)selectedInventory;
        }

        /// <summary>
        /// Adds the item to the inventory, taking InventoryCollection as a parameter
        /// </summary>
        /// <param name="data"></param>
        public void OnBinDeposit(Tuple<Transform, BinData> bin)
        {
            Tuple<Transform, Enum> particleData;

            if (inventoryCollection.Get((ItemCategoryType) selectedInventory).isEmpty())
            {
                Debug.Log("No Item");

                particleData = new Tuple<Transform, Enum>(bin.Item1, FeedbackType.NoItem);
                onParticleEvent?.Raise(particleData);
            }
            else if (selectedInventory != (int)bin.Item2.BinType)
            {
                Debug.Log("Wrong Bin!");

                ItemData selectedItem = possibleItems[(int)selectedInventory];

                onScoreEvent?.Raise(-1);
                inventoryCollection.Get((ItemCategoryType) selectedInventory).Remove(selectedItem, 1);

                particleData = new Tuple<Transform, Enum>(bin.Item1, FeedbackType.Wrong);
                onParticleEvent?.Raise(particleData);
            }
            else
            {
                Debug.Log("Correct Bin!");

                onScoreEvent?.Raise(1);
                inventoryCollection.Get(bin.Item2.BinType).Remove(bin.Item2.AcceptedItem, 1);

                particleData = new Tuple<Transform, Enum>(bin.Item1, FeedbackType.Correct);
                onParticleEvent?.Raise(particleData);
            }
        }

        /// <summary>
        /// Adds the item to the inventory.
        /// </summary>
        /// <param name="data"></param>
        public void OnInventoryAdd(ItemData data)
        {
            inventoryCollection.Add(data);
        }
    }
}
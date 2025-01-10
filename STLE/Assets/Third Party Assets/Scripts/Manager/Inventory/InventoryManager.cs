using GD.Events;
using GD.Types;
using Sirenix.OdinInspector;
using System;
using System.Xml.Linq;
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
        [Tooltip("Currently selected inventory")]
        private int selectedInventory;

        [SerializeField]
        [Tooltip("All possible items")]
        private ItemData[] possibleItems = new ItemData[3];

        [SerializeField]
        [InlineEditor]
        [Tooltip("The UI Component indicating the selected inventory")]
        private Image UIIndicator;

        private void Awake()
        {
            //check if the inventory collection has been added
            if (inventoryCollection == null)
                throw new NullReferenceException("No inventory collection has been added");

            selectedInventory = 0;

            UIIndicator.sprite = inventoryCollection.Get((ItemCategoryType) selectedInventory).uiIcon;

            inventoryCollection.ClearInventories();
        }

        public void CycleInventoryUp(InputAction.CallbackContext context)
        {
            selectedInventory++;

            if (selectedInventory >= 3)
                selectedInventory = 0;

            updateUI();
        }

        public void CycleInventoryDown(InputAction.CallbackContext context)
        {
            selectedInventory--;

            if (selectedInventory <= -1)
                selectedInventory = 2;
        
            updateUI();
        }

        private void updateUI()
        {
            UIIndicator.sprite = inventoryCollection.Get((ItemCategoryType)selectedInventory).uiIcon;
        }

        /// <summary>
        /// Adds the item to the inventory, taking InventoryCollection as a parameter
        /// </summary>
        /// <param name="data"></param>
        public void OnBinDeposit(BinData bin)
        {
            if(inventoryCollection.Get((ItemCategoryType) selectedInventory).isEmpty())
            {
                Debug.Log("No Item");
                onScoreEvent?.Raise(-1);
            }
            else if (selectedInventory != (int)bin.BinType)
            {
                Debug.Log("Wrong Bin!");

                Debug.Log(possibleItems[(int)selectedInventory].Name);

                ItemData selectedItem = possibleItems[(int)selectedInventory];

                onScoreEvent?.Raise(-1);
                inventoryCollection.Get((ItemCategoryType) selectedInventory).Remove(selectedItem, 1);
            }
            else
            {
                Debug.Log("Correct Bin!");

                onScoreEvent?.Raise(1);
                inventoryCollection.Get(bin.BinType).Remove(bin.AcceptedItem, 1);
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
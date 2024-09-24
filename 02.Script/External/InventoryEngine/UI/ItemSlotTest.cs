using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using MoreMountains.InventoryEngine;
using TMPro;

namespace SurviveCoding.External.InventoryEngine
{
    public class ItemSlotTest : InventorySlot
    {
        public  TMP_Text QuantityText2;

        protected override void Awake()
        {
            base.Awake();
            // Initialize the TMP_Text component
            QuantityText2 = this.gameObject.GetComponentInChildren<TMP_Text>();
        }

        public override void SetQuantity(int quantity)
        {
            if (quantity > 1)
            {
                QuantityText2.gameObject.SetActive(true);
                QuantityText2.text = quantity.ToString();
            }
            else
            {
                QuantityText2.gameObject.SetActive(false);
            }
        }

        public override void DrawIcon(InventoryItem item, int index)
        {
            if (ParentInventoryDisplay != null)
            {
                if (!InventoryItem.IsNull(item))
                {
                    SetIcon(item.Icon);
                    SetQuantity(item.Quantity);
                }
                else
                {
                    DisableIconAndQuantity();
                }
            }
        }

        public override void DisableIconAndQuantity()
        {
            base.DisableIconAndQuantity();
            QuantityText2.gameObject.SetActive(false);
        }

        public virtual bool TMP_EquipUseButtonShouldShow()
        {
            if (InventoryItem.IsNull(ParentInventoryDisplay.TargetInventory.Content[Index])) { return false; }
            return ParentInventoryDisplay.TargetInventory.Content[Index].DisplayProperties.DisplayEquipUseButton;
        }

        public virtual bool TMP_MoveButtonShouldShow()
        {
            if (InventoryItem.IsNull(ParentInventoryDisplay.TargetInventory.Content[Index])) { return false; }
            return ParentInventoryDisplay.TargetInventory.Content[Index].DisplayProperties.DisplayMoveButton;
        }

        public virtual bool TMP_DropButtonShouldShow()
        {
            if (InventoryItem.IsNull(ParentInventoryDisplay.TargetInventory.Content[Index])) { return false; }
            return ParentInventoryDisplay.TargetInventory.Content[Index].DisplayProperties.DisplayDropButton;
        }

        public virtual bool TMP_EquipButtonShouldShow()
        {
            if (InventoryItem.IsNull(ParentInventoryDisplay.TargetInventory.Content[Index])) { return false; }
            return ParentInventoryDisplay.TargetInventory.Content[Index].DisplayProperties.DisplayEquipButton;
        }

        public virtual bool TMP_UseButtonShouldShow()
        {
            if (InventoryItem.IsNull(ParentInventoryDisplay.TargetInventory.Content[Index])) { return false; }
            return ParentInventoryDisplay.TargetInventory.Content[Index].DisplayProperties.DisplayUseButton;
        }

        public virtual bool TMP_UnequipButtonShouldShow()
        {
            if (InventoryItem.IsNull(ParentInventoryDisplay.TargetInventory.Content[Index])) { return false; }
            return ParentInventoryDisplay.TargetInventory.Content[Index].DisplayProperties.DisplayUnequipButton;
        }
    }
}

using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System;
using MoreMountains.TopDownEngine;
using MoreMountains.InventoryEngine;

namespace SurviveCoding.External.InventoryEngine.Item
{
    [CreateAssetMenu(fileName = "ConsumableItem", menuName = "MoreMountains/InventoryEngine/ConsumableItem", order = 0)]
    [Serializable]
    /// <summary>
    /// Consumable Item range to EMP Tools
    /// </summary>
    public class ConsumableItem : InventoryItem
    {
        public override bool Use(string playerID)
        {
            base.Use(playerID);
            Debug.Log("Ha...");
            return true;
        }
    }
}

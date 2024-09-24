using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System;
using MoreMountains.TopDownEngine;
using MoreMountains.InventoryEngine;

namespace SurviveCoding.External.InventoryEngine.Item
{
    [CreateAssetMenu(fileName = "KeyItem", menuName = "MoreMountains/InventoryEngine/KeyItem", order = 0)]
    [Serializable]
    /// <summary>
    /// Consumable Item range to EMP Tools
    /// </summary>
    public class KeyItem : InventoryItem
    {

        public override bool Use(string playerID)
        {
            base.Use(playerID);
            Debug.Log("Using Key Item");
            return true;
        }
    }
}

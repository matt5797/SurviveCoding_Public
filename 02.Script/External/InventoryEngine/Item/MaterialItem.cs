using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System;
using MoreMountains.InventoryEngine;

namespace SurviveCoding.External.InventoryEngine.Item
{
    [CreateAssetMenu(fileName = "MaterialItem", menuName = "MoreMountains/InventoryEngine/MaterialItem", order = 0)]
    [Serializable]
    /// <summary>
    /// Base item class, to use when your object doesn't do anything special
    /// </summary>
    public class MaterialItem : InventoryItem
    {

    }
}

using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System;
using MoreMountains.TopDownEngine;
using MoreMountains.InventoryEngine;

namespace SurviveCoding.External.InventoryEngine.Item
{
	[CreateAssetMenu(fileName = "RepairItem", menuName = "MoreMountains/InventoryEngine/RepairItem", order = 0)]
	public class RepairItem : ConsumableItem
    {
		public float repairAmount;
		public override bool Use(string playerID)
		{
			base.Use(playerID);

			Debug.Log(repairAmount);
			return true;
		}
	}
}
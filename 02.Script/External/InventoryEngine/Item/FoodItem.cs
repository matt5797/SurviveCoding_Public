using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System;
using MoreMountains.TopDownEngine;
using MoreMountains.InventoryEngine;
using SurviveCoding.Player;

namespace SurviveCoding.External.InventoryEngine.Item
{
	[CreateAssetMenu(fileName = "FoodItem", menuName = "MoreMountains/InventoryEngine/FoodItem", order = 0)]
	public class FoodItem : ConsumableItem
    {
		public float fullness;
		PlayerStatComponent statComponent;
		PlayerStatType statType;
		

		public override bool Use(string playerID)
		{
			base.Use(playerID);

			//Status.Instance.DecreaseHunger(fullness);
			if (TargetInventory(playerID).Owner != null)
			{
				var statComponent = TargetInventory(playerID).Owner.GetComponent<PlayerStatComponent>();
				if(statComponent != null)
                {
					statComponent.GetStat(PlayerStatType.Hunger).CurrentValue -= fullness;
                }
				Debug.Log("run");
				return true;
			}
			else return false;
		}
	}
}

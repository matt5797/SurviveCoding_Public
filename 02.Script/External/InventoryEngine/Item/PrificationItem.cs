using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System;
using MoreMountains.TopDownEngine;
using MoreMountains.InventoryEngine;
using SurviveCoding.Player;

namespace SurviveCoding.External.InventoryEngine.Item
{
	[CreateAssetMenu(fileName = "PrificationItem", menuName = "MoreMountains/InventoryEngine/PrificationItem", order = 0)]
	public class PrificationItem : ConsumableItem
    {
		public float sanity;
		PlayerStatComponent statComponent;
		PlayerStatType statType;


		public override bool Use(string playerID)
		{
			base.Use(playerID);

			//Status.Instance.DecreaseHunger(fullness);
			if (TargetInventory(playerID).Owner != null)
			{
				var statComponent = TargetInventory(playerID).Owner.GetComponent<PlayerStatComponent>();
				if (statComponent != null)
				{
					statComponent.GetStat(PlayerStatType.Pollution).CurrentValue -= sanity;
				}
				Debug.Log("run");
				return true;
			}
			else return false;
		}
	}
}

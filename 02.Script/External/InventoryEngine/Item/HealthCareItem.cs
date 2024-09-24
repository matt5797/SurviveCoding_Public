using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using System;
using MoreMountains.InventoryEngine;
using MoreMountains.TopDownEngine;
using SurviveCoding.Player;

namespace SurviveCoding.External.InventoryEngine.Item
{
	[CreateAssetMenu(fileName = "HealthCareItem", menuName = "MoreMountains/InventoryEngine/HealthCareItem", order = 1)]
	[Serializable]
	/// <summary>
	/// Pickable health item
	/// </summary>
	public class HealthCareItem : ConsumableItem
    {
		[Header("Health")]
		[MMInformation("Here you need specify the amount of health gained when using this item.", MMInformationAttribute.InformationType.Info, false)]
		/// the amount of health to add to the player when the item is used
		[Tooltip("the amount of health to add to the player when the item is used")]
		public float HealthBonus;

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
					statComponent.GetStat(PlayerStatType.Health).CurrentValue += HealthBonus;
				}
				Debug.Log("run");
				return true;
			}
			else return false;
		}
		/*
		if (TargetInventory(playerID).Owner == null)
		{
			Debug.Log("Fail");
			return false;
		}
		Health characterHealth = TargetInventory(playerID).Owner.GetComponent<Health>();
		if (characterHealth != null)
		{
			characterHealth.ReceiveHealth(HealthBonus, TargetInventory(playerID).gameObject);
			Debug.Log("Heal!!!");
			return true;
		}

		else
		{

			return false;
		}
		*/
	}
}
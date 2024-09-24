using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using MoreMountains.InventoryEngine;

namespace SurviveCoding.External.InventoryEngine.UI
{
	/// <summary>
	/// An example of a game manager, the only significant part being how we trigger in a single place the load of all inventories, in the Start method.
	/// </summary>
	public class InventorySaveManager : MMSingleton<InventorySaveManager>
	{ 
		public virtual TopDownController3D Player { get; protected set; }

		/// <summary>
		/// Statics initialization to support enter play modes
		/// </summary>
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		protected static void InitializeStatics()
		{
			_instance = null;
		}

		protected override void Awake()
		{
			base.Awake();
			//Player = GameObject.FindGameObjectWithTag("Player").GetComponent<TopDownController3D>();
		}

		/// <summary>
		/// On start, we trigger our load event, which will be caught by inventories so they try to load saved content
		/// </summary>
		protected virtual void Start()
		{
			MMEventManager.TriggerEvent(new MMGameEvent("Load"));
			MMGameEvent.Trigger("Load");
		}

		void OnApplicationQuit()
		{
			Debug.Log("Ending");
			MMGameEvent.Trigger("Save");

		}
	}
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PowerUpManager : CollectionManager
{
	[SerializeField]
	private List<ConfigPowerUp> powerUpConfigs;
	
	public PowerUp GeneratePowerUp(EPowerUpId pId, int pAmount)
	{
		ConfigPowerUp config = powerUpConfigs.Find(a => a.id == pId);
		//UsableItem usable = itemUsables.Find(a => a.id == pId);
		PowerUp powerUp = new PowerUp(config, pAmount);
		return powerUp;
	}
	
	protected override List<ConfigCollectionItem> GetBaseConfigs()
	{
		List<ConfigCollectionItem> configs = new List<ConfigCollectionItem>();
		foreach(ConfigPowerUp c in powerUpConfigs)
		{
			configs.Add(c);
		}
		return configs;
	}

	internal void EvaluatePowerUp(Snake snake)
	{
		//throw new NotImplementedException();
	}
}

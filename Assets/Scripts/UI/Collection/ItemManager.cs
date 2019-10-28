using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemManager : CollectionManager
{
	[SerializeField]
	private List<ConfigItem> itemConfigs;

	[SerializeField]
	private List<UsableItem> itemUsables;

	[SerializeField]
	private List<Projectile> projectiles;

	/*public Item GenerateItem(EItemId pId, int pAmount)
	{
		CollectionItem item = GenerateItem(pId, pAmount);
		return Item.Convert(item);
	}*/

	protected override List<ConfigCollectionItem> GetBaseConfigs()
	{
		List<ConfigCollectionItem> configs = new List<ConfigCollectionItem>();
		foreach(ConfigItem c in itemConfigs)
		{
			configs.Add(c);
		}
		return configs;
	}

	public Item GenerateItem(EItemId pId, int pAmount)
	{
		ConfigItem config = itemConfigs.Find(a => a.id == pId);
		UsableItem usable = itemUsables.Find(a => a.id == pId);
		Projectile projectile = projectiles.Find(a => a.id == pId);
		UsableItem usableInstance = Instantiate(usable, transform);

		usableInstance.Init(this, projectile);
		Item item = new Item(config, pAmount, usableInstance);
		return item;
	}
}

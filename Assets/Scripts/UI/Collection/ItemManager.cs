using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemManager : CollectionManager
{
	[SerializeField]
	private List<ConfigItem> itemConfigs;

	public Item GenerateItem(EItemId pId, int pAmount)
	{
		CollectionItem item = GenerateItem((int)pId, pAmount);
		return Item.Convert(item);
	}

	protected override List<ConfigCollectionItem> GetBaseConfigs()
	{
		List<ConfigCollectionItem> configs = new List<ConfigCollectionItem>();
		foreach(ConfigItem c in itemConfigs)
		{
			configs.Add(c);
		}
		return configs;
	}
}

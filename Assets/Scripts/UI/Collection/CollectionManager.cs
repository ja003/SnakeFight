using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class CollectionManager : MonoBehaviour
{
	private List<ConfigCollectionItem> configs;

	private void Awake()
	{
		configs = GetBaseConfigs();
	}

	abstract protected List<ConfigCollectionItem> GetBaseConfigs();
	
	protected CollectionItem GenerateItem(int pId, int pAmount)
	{
		ConfigCollectionItem config = configs.Find(a => a.GetId() == pId);
		CollectionItem item = new CollectionItem(pId, pAmount, config.icon);
		return item;
	}
}

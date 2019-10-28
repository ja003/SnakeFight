using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : CollectionItem
{
	protected UsableItem usable;

	public Item(ConfigItem pConfig, int pAmount, UsableItem pUsable) : base((int)pConfig.id, pAmount, pConfig.icon)
	{
		usable = pUsable;
	}

	internal override void Evaluate()
	{
		usable.Evaluate();
		//throw new System.NotImplementedException();
	}

	internal override void SetSelected(bool pValue)
	{
		usable.SetSelected(pValue);
		//throw new System.NotImplementedException();
	}

	internal override void SetTarget(PlaygroundField pTargetField)
	{
		usable.SetTarget(pTargetField);
	}

	internal void SetOwner(Snake pSnake)
	{
		usable.SetOwner(pSnake);
	}


	//public static Item Convert(CollectionItem pItem)
	//{
	//	return new Item((EItemId)pItem.id, pItem.amount, pItem.icon);
	//}
}

public enum EItemId
{
	None,
	Gun,
	Knife,
}

public enum EItemType
{

}
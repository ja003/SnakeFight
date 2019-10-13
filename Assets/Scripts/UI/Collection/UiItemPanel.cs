using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UiItemPanel : UiCollectionPanel
{
	public void Refresh(Item pItem)
	{
		UiCollectionItem item = GetItem((int)pItem.id);
		if(item == null)
			item = AddItem(pItem);

		item.Refresh(pItem.amount);
	}
}


public class Item : CollectionItem
{
	/*public EItemId id;
	public int amount;
	internal Sprite icon;

	public Item(EItemId id, int amount, Sprite icon)
	{
		this.id = id;
		this.amount = amount;
		this.icon = icon;
	}

	internal void AddAmount(int pAmount)
	{
		amount += pAmount;
	}*/
	public Item(EItemId id, int amount, Sprite icon) : base((int)id, amount, icon)
	{
	}

	public static Item Convert(CollectionItem pItem)
	{
		return new Item((EItemId)pItem.id, pItem.amount, pItem.icon);
	}
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

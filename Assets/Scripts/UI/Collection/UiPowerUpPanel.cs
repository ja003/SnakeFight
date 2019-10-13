using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UiPowerUpPanel : UiCollectionPanel
{
	public void Refresh(PowerUp pItem)
	{
		UiCollectionItem item = GetItem((int)pItem.id);
		if(item == null)
			item = AddItem(pItem);

		item.Refresh(pItem.amount);
	}
}



public class PowerUp : CollectionItem
{

	public PowerUp(EPowerUpId id, int amount, Sprite icon):  base((int)id, amount, icon)
	{
	}
	
	public static PowerUp Convert(CollectionItem pItem)
	{
		return new PowerUp((EPowerUpId)pItem.id, pItem.amount, pItem.icon);
	}
}

public enum EPowerUpId
{
	None,
	Specal,
	Specal2,
}

public enum EPowerUpType
{

}
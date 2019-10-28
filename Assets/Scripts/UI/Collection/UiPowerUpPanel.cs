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




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




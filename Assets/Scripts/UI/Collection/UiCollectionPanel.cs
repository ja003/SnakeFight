using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UiCollectionPanel : MonoBehaviour
{
	[SerializeField]
	private Transform itemsHolder;

	[SerializeField]
	private UiCollectionItem prefab;

	[SerializeField]
	Image blockRaycast;

	private Dictionary<int, UiCollectionItem> powerUps =
		new Dictionary<int, UiCollectionItem>();

	internal void Init(UnityAction<int> pOnPowerUpSelected)
	{
		OnItemSelected = pOnPowerUpSelected;
		SetInteractable(true);
	}

	public void Refresh(CollectionItem pItem)
	{
		UiCollectionItem item = GetItem(pItem.id);
		if(item == null)
			item = AddItem(pItem);

		item.Refresh(pItem.amount);
	}

	UnityAction<int> OnItemSelected;
	   
	protected UiCollectionItem AddItem(CollectionItem pItem)
	{
		UiCollectionItem newItem = Instantiate(prefab, itemsHolder);
		newItem.Init(pItem.icon, pItem.amount, pItem.id, OnItemSelected);
		powerUps.Add(pItem.id, newItem);
		Refresh(pItem);
		return newItem;
	}

	UiCollectionItem selectedItem;
	internal void SelectItem(int pItem)
	{
		selectedItem?.SetSelected(false);
		selectedItem = GetItem(pItem);
		selectedItem?.SetSelected(true);
	}


	protected UiCollectionItem GetItem(int pItem)
	{
		UiCollectionItem item = null;
		powerUps.TryGetValue(pItem, out item);
		return item;
	}

	public void SetInteractable(bool pValue)
	{
		blockRaycast.enabled = !pValue;
		blockRaycast.raycastTarget = !pValue;
	}
}



public class CollectionItem
{
	public int id;
	public int amount;
	internal Sprite icon;

	public CollectionItem(int id, int amount, Sprite icon)
	{
		this.id = id;
		this.amount = amount;
		this.icon = icon;
	}

	internal void AddAmount(int pAmount)
	{
		amount += pAmount;
	}
}

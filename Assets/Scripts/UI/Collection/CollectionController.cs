using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class CollectionController
{
	private Dictionary<int, CollectionItem> items = new Dictionary<int, CollectionItem>();

	protected UiCollectionPanel ui;
	protected UiPanelAction uiAction;

	private CollectionItem activeItem;
	public CollectionItem ActiveItem => activeItem;

	protected Snake owner;

	public CollectionController(Snake pOwner, UiCollectionPanel pUiPanel, UiPanelAction pUiAction)
	{
		owner = pOwner;
		if(!owner.IsLocal)
			return;

		ui = pUiPanel;
		ui.Init(OnItemSelected);

		uiAction = pUiAction;
	}
	
	public virtual void DeselectItem()
	{
		if(!owner.IsLocal)
			return;

		OnItemSelected(0);
		Debug.Log($"{owner} DeselectItem");
	}

	protected virtual void OnItemSelected(int pItemId)
	{
		items.TryGetValue(pItemId, out activeItem);
		ui.SelectItem(pItemId);
	}

	protected void AddItem(CollectionItem pItem)
	{
		if(items.ContainsKey(pItem.id))
		{
			items[pItem.id].AddAmount(pItem.amount);
		}
		else
		{
			items.Add(pItem.id, pItem);
		}

		if(owner.IsLocal)
			ui.Refresh(items[pItem.id]);
	}

	abstract internal void OnCancelAction(EAction pAction);

	public abstract void Evaluate();

}

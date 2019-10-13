using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemController : CollectionController
{
	private PlaygroundField target;

	public ItemController(Snake pOwner, UiCollectionPanel pUiPanel, UiPanelAction pUiAction) : base(pOwner, pUiPanel, pUiAction)
	{
	}

	public void AddItem(Item pItem)
	{
		base.AddItem(pItem);
	}

	internal override void OnCancelAction(EAction pAction)
	{
		switch(pAction)
		{
			case EAction.Item:
				DeselectItem();
				break;
		}
	}

	public override void Evaluate()
	{
		Debug.Log("Item Evaluate");
		if(target == null)
		{
			//Debug.Log($"{id} doesnt attack");
			return;
		}
		Debug.Log($"{owner.Id} EvaluateItem");
		target.bodyPart?.OnAttacked(10);

		DeselectItem();
	}

	internal void EvaluateItem(Snake pOwner, PlaygroundField pTarget)
	{
		if(pTarget == null)
		{
			//Debug.Log($"{id} doesnt attack");
			return;
		}
		Debug.Log($"{pOwner.Id} EvaluateItem");
		pTarget.bodyPart?.OnAttacked(10);
		pTarget = null;
	}

	protected override void OnItemSelected(int pItemId)
	{
		base.OnItemSelected(pItemId);
		SetTarget(null);
		Debug.Log($"{owner} OnItemSelected {(EItemId)pItemId}");

	}

	public void SetTarget(PlaygroundField pTargetField)
	{
		bool isItemActive = ActiveItem != null;

		Debug.Log($"{owner} SetTarget {pTargetField} | {isItemActive}");

		target?.SetSelected(false);
		target = pTargetField;
		target?.SetSelected(isItemActive);

		uiAction.OnSetTarget((Item)ActiveItem, pTargetField);
	}
}

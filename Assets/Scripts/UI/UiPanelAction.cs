using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//todo: rename UiActionpanel ? UNIFY
public class UiPanelAction : MonoBehaviour
{
	[SerializeField]
	private Sprite moveUp;
	[SerializeField]
	private Sprite moveRight;
	[SerializeField]
	private Sprite moveDown;
	[SerializeField]
	private Sprite moveLeft;

	[SerializeField]
	private UiPanelActionItem actionMove;
	[SerializeField]
	private UiPanelActionItem actionItem;
	[SerializeField]
	private UiPanelActionItem actionPowerUp;

	public void Init()
	{
		actionMove.SetActive(false);
		actionItem.SetActive(false);
		actionPowerUp.SetActive(false);
	}


	internal void OnSetTarget(Item pItem, PlaygroundField pTarget)
	{
		bool isValidTarget = pTarget != null && pItem != null;
		actionItem.SetTexture(isValidTarget ? pItem.icon : null);
		actionItem.SetActive(isValidTarget);
	}

	internal void SetInteractable(bool pValue)
	{
		actionMove.SetInteractable(pValue);
		actionItem.SetInteractable(pValue);
		actionPowerUp.SetInteractable(pValue);
	}

	//todo: join with OnSetTarget and OnSetDirection
	internal void OnSetPowerUp(PowerUp pPowerUp)
	{
		bool isValid = pPowerUp != null;
		GetActionPanel(EAction.PowerUp).SetTexture(isValid ? pPowerUp.icon : null);
		GetActionPanel(EAction.PowerUp).SetActive(isValid);
	}

	public void OnSetDirection(EDirection pDirection)
	{
		actionMove.SetTexture(GetMoveTexture(pDirection));
		actionMove.SetActive(true);
	}

	private Sprite GetMoveTexture(EDirection pDirection)
	{
		switch(pDirection)
		{
			case EDirection.Up:
				return moveUp;
			case EDirection.Right:
				return moveRight;
			case EDirection.Down:
				return moveDown;
			case EDirection.Left:
				return moveLeft;
		}
		Debug.LogError("No direction set");
		return null;
	}

	internal void Init(UnityAction<EAction> pOnCancelAction)
	{
		actionMove.Init(() => pOnCancelAction(EAction.Move));
		actionItem.Init(() => pOnCancelAction(EAction.Item));
		actionPowerUp.Init(() => pOnCancelAction(EAction.PowerUp));
	}

	internal void OnCancelAction(EAction pAction)
	{
		GetActionPanel(pAction).SetActive(false);
	}

	private UiPanelActionItem GetActionPanel(EAction pAction)
	{
		switch(pAction)
		{
			case EAction.Move:
				return actionMove;
			case EAction.Item:
				return actionItem;
			case EAction.PowerUp:
				return actionPowerUp;
		}
		return null;
	}

	internal void OnEvaluateTurn()
	{
		//no need to deactivate actionMove
		actionItem.SetActive(false);
		actionPowerUp.SetActive(false);
	}

	
}

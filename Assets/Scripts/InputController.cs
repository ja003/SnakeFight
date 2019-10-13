using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputController : MonoBehaviour
{
	public UnityAction<EDirection> OnSetDirection;
	private UnityAction OnConfirmActions;
	public UnityAction<PlaygroundField> OnSetTarget;
	private UnityAction OnSwapLocalSnake;
	public UnityAction<EAction> OnCancelAction;

	//private ItemManager itemManager;
	//private PowerUpManager powerUpManager;

	public bool IsEvaluating;
	
	//public void SetIsEvaluating(bool value)
	//{
	//	Debug.Log("SetIsEvaluating " + value);
	//	isEvaluating = value;
	//	if(value)
	//	{
	//		OnConfirmActionsBcup = OnConfirmActions;
	//		OnConfirmActions = null;
	//	}
	//	else
	//	{
	//		OnConfirmActions = OnConfirmActionsBcup;
	//	}
	//}

	public void Init(
		UnityAction<EDirection> setMovementToLocalSnake,
		UnityAction setConfirmToLocalSnake,
		UnityAction<PlaygroundField> pSetTargetToLocalSnake,
		UnityAction pSwapLocalSnake,
		UnityAction<EAction> pCancelAction

		/*ItemManager pItemManager,
		PowerUpManager pPowerUpManager*/)
	{
		OnSetDirection = setMovementToLocalSnake;
		OnConfirmActions = setConfirmToLocalSnake;
		OnSetTarget = pSetTargetToLocalSnake;
		OnSwapLocalSnake = pSwapLocalSnake;
		OnCancelAction = pCancelAction;

		//itemManager = pItemManager;
		//powerUpManager = pPowerUpManager;

	}

	public void ConfirmActions()
	{
		if(IsEvaluating)
			return;
		OnConfirmActions();
	}

	void Update()
	{
		if(IsEvaluating)
			return;

		EDirection dir = GetSnakeDirection();

		if(dir != EDirection.None)
		{
			OnSetDirection(dir);
		}

		PlaygroundField targetField = GetTarget();
		if(targetField != null)
		{
			OnSetTarget(targetField);
		}

		bool confirm = GetActionsConfirmed();
		if(confirm)
			ConfirmActions();

		if(Input.GetKeyDown(KeyCode.Tab))
		{
			OnSwapLocalSnake();
		}
	}

	private PlaygroundField GetTarget()
	{		

		if(Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			//Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red, 1);

			bool hitField = Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, Playground.layerMask);
			if(hitField)
			{
				PlaygroundField field = hitInfo.collider.GetComponent<PlaygroundField>();
				return field;
			}

		}
		return null;
	}

	private static EDirection GetSnakeDirection()
	{
		EDirection dir = EDirection.None;
		if(Input.GetKeyDown(KeyCode.W))
		{
			dir = EDirection.Up;
		}
		else if(Input.GetKeyDown(KeyCode.D))
		{
			dir = EDirection.Right;
		}
		else if(Input.GetKeyDown(KeyCode.S))
		{
			dir = EDirection.Down;
		}
		else if(Input.GetKeyDown(KeyCode.A))
		{
			dir = EDirection.Left;
		}

		return dir;
	}

	private static bool GetActionsConfirmed()
	{
		if(Input.GetKeyDown(KeyCode.E))
		{
			//Debug.Log($"player {pPlayer} confirmed");
			return true;
		}
		return false;
	}
}

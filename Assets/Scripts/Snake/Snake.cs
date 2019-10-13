using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
	[SerializeField]
	private BodyPart bodyPartPrefab;
	[SerializeField]
	private int bodyPartsCount;

	private List<BodyPart> bodyParts;
	private BodyPart head => bodyParts[0];
	private BodyPart tail => bodyParts.Count > 0 ? bodyParts[bodyParts.Count - 1] : null;

	public int Id { get; private set; }
	public bool IsLocal { get; internal set; }

	private NetworkMessanger networkMessanger;

	public bool ActionsConfirmed;// { private set; get; }

	private ItemController itemController;
	private PowerUpController powerUpController;

	public void Heal(int pHealth)
	{
		head.AddHealth(pHealth);
	}

	public void TakeDamage(int pDamage)
	{
		head.AddHealth(-pDamage);
	}

	

	public void CancelAction(EAction pAction)
	{
		switch(pAction)
		{
			case EAction.Move:
				SetDirection(EDirection.None);
				break;
			case EAction.Item:
				itemController.OnCancelAction(pAction);
				break;
			case EAction.PowerUp:
				powerUpController.OnCancelAction(pAction);
				break;
		}
	}

	internal void Kill()
	{
		head.Destroy();
		Debug.Log($"{this} got killed");
	}

	internal void OnReceiveConfirmMessage(SnakeConfirmMessage pMessage)
	{
		SetDirection(pMessage.direction);
		ConfirmActions(false);
	}

	internal void AddItem(Item pItem)
	{
		itemController.AddItem(pItem);
	}

	internal void AddPowerUp(PowerUp pPowerUp)
	{
		powerUpController.AddPowerUp(pPowerUp);
	}

	public void Init(int pId, bool pIsLocal, NetworkMessanger pNetworkMessanger, PlaygroundField playgroundField, GameUI pUi)
	{
		Id = pId;
		IsLocal = pIsLocal;
		networkMessanger = pNetworkMessanger;
		bodyParts = new List<BodyPart>();
		for(int i = 0; i < bodyPartsCount; i++)
		{
			AddBodyPart(100 - i * 10);
		}

		PlaceAt(playgroundField);

		itemController = new ItemController(this, pUi.ItemPanel, pUi.ActionPanel);
		powerUpController = new PowerUpController(this, pUi.PowerUpPanel, pUi.ActionPanel);
	}


	internal void EvaluateMove()
	{
		Move();
		Draw();
		ActionsConfirmed = false;
	}

	public void EvaluateItem()
	{
		itemController.Evaluate();
	}

	public void EvaluatePowerUp()
	{
		powerUpController.Evaluate();
	}


	public void OnEvaluationStart()
	{
		ActionsConfirmed = false;
	}

	public void OnEvaluationFinished()
	{

	}



	private void ConfirmActions(bool pSendMessage)
	{
		//Debug.Log($"{this} ConfirmActions {pSendMessage}");
		ActionsConfirmed = true;
		if(pSendMessage)
			networkMessanger.SendConfirmMessage(Id, currentDirection);
	}

	public void AddBodyPart()
	{
		AddBodyPart(tail.health);
	}

	public void AddBodyPart(int pHealth)
	{
		BodyPart newPart = Instantiate(bodyPartPrefab, transform);
		newPart.SetHealth(pHealth);
		newPart.owner = this;
		if(tail != null)
		{
			newPart.previousPart = tail;
			tail.nextPart = newPart;
			newPart.field = tail.field; //todo: maybe previous field?
		}
		bodyParts.Add(newPart);
	}

	internal void SetTarget(PlaygroundField pTargetField)
	{
		itemController.SetTarget(pTargetField);
	}

	private EDirection currentDirection = EDirection.Right;
	private EDirection lastDirection = EDirection.Right;

	public void SetDirection(EDirection direction)
	{
		currentDirection = direction;
	}

	private void Move()
	{
		if(currentDirection == EDirection.None)
		{
			currentDirection = lastDirection;
			/*Debug.LogError("Direction not set");
			return;*/
		}
		PlaygroundField target = head.field.GetNeighbour(currentDirection);

		if(target?.bodyPart != null)
		{
			target.bodyPart.nextPart?.Destroy();
		}
		head.Move(target);
		lastDirection = currentDirection;
	}

	private void PlaceAt(PlaygroundField playgroundField)
	{
		foreach(BodyPart part in bodyParts)
		{
			part.field = playgroundField;
			playgroundField.bodyPart = head;
		}
		Draw();
	}

	private void Draw()
	{
		foreach(BodyPart part in bodyParts)
		{
			part.Draw();
		}
	}

	public override string ToString()
	{
		return $"Snake[{Id}]";
	}
}


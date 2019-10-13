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

	private int id;
	private NetworkMessanger networkMessanger;
	private PlaygroundField target;

	void Update()
	{
		if(NetworkMessanger.IsMultiplayer())
		{
			if(id == 1 && !networkMessanger.isServer)
				return;
			if(id == 2 && networkMessanger.isServer)
				return;
		}

		EDirection dir = GetSnakeDirection(id);

		if(dir != EDirection.None)
		{
			SetDirection(dir);
		}

		PlaygroundField targetField = GetTarget();
		if(targetField != null)
		{
			SetTarget(targetField);
		}

		bool confirm = GetActionsConfirmed(id);
		if(confirm)
			ConfirmActions(true);
	}

	public void Heal(int pHealth)
	{
		head.AddHealth(pHealth);
	}

	public void TakeDamage(int pDamage)
	{
		head.AddHealth(-pDamage);
	}

	private void SetTarget(PlaygroundField targetField)
	{
		Debug.Log($"{id} SetTarget {targetField}");
		target = targetField;
	}

	private PlaygroundField GetTarget()
	{
		if(Input.GetMouseButtonDown(id == 1 ? 0 : 1))
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

	internal void Kill()
	{
		head.Destroy();
		Debug.Log($"{this} got killed");
	}

	private static bool GetActionsConfirmed(int pSnakeId)
	{
		if(Input.GetKeyDown(pSnakeId == 1 || Debugger.OnePlayer ? KeyCode.E : KeyCode.Keypad9))
		{
			//Debug.Log($"player {pPlayer} confirmed");
			return true;
		}
		return false;
	}

	private static EDirection GetSnakeDirection(int pSnake)
	{
		EDirection dir = EDirection.None;
		if(Input.GetKeyDown(pSnake == 1 ? KeyCode.W : KeyCode.Keypad8))
		{
			dir = EDirection.Up;
		}
		else if(Input.GetKeyDown(pSnake == 1 ? KeyCode.D : KeyCode.Keypad6))
		{
			dir = EDirection.Right;
		}
		else if(Input.GetKeyDown(pSnake == 1 ? KeyCode.S : KeyCode.Keypad5))
		{
			dir = EDirection.Down;
		}
		else if(Input.GetKeyDown(pSnake == 1 ? KeyCode.A : KeyCode.Keypad4))
		{
			dir = EDirection.Left;
		}

		return dir;
	}

	internal void OnReceiveConfirmMessage(SnakeConfirmMessage pMessage)
	{
		SetDirection(pMessage.direction);
		ConfirmActions(false);
	}

	public void Init(int pId, NetworkMessanger pNetworkMessanger, PlaygroundField playgroundField)
	{
		id = pId;
		networkMessanger = pNetworkMessanger;
		bodyParts = new List<BodyPart>();
		for(int i = 0; i < bodyPartsCount; i++)
		{
			AddBodyPart(100 - i * 10);
		}

		PlaceAt(playgroundField);
	}

	internal void Evaluate()
	{
		Move();
		Draw();
		Attack();
		ActionsConfirmed = false;
	}

	private void Attack()
	{
		if(target == null)
		{
			//Debug.Log($"{id} doesnt attack");
			return;
		}
		Debug.Log($"{id} attack");
		target.bodyPart?.OnAttacked(10);
		target = null;
	}

	public bool ActionsConfirmed { private set; get; }

	private void ConfirmActions(bool pSendMessage)
	{
		//Debug.Log($"{this} ConfirmActions {pSendMessage}");
		ActionsConfirmed = true;
		if(pSendMessage)
			networkMessanger.SendConfirmMessage(id, currentDirection);
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

	private EDirection currentDirection = EDirection.Right;

	private void SetDirection(EDirection direction)
	{
		currentDirection = direction;
	}

	private void Move()
	{
		if(currentDirection == EDirection.None)
		{
			Debug.LogError("Direction not set");
			return;
		}
		PlaygroundField target = head.field.GetNeighbour(currentDirection);

		if(target?.bodyPart != null)
		{
			target.bodyPart.nextPart?.Destroy();
		}
		head.Move(target);
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
		return $"Snake[{id}]";
	}
}


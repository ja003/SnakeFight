using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickable : MapObject
{
	public override bool OnSnakeContact(Snake pSnake)
	{
		OnPickUp(pSnake);
		gameObject.SetActive(false);
		return true;
	}

	protected abstract void OnPickUp(Snake pSnake);
}

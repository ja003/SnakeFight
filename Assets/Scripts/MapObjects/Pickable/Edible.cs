using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edible : Pickable
{
	[SerializeField]
	private int health;
	[SerializeField]
	private int bodyParts;
	
	protected override void OnPickUp(Snake pSnake)
	{
		if(pSnake == null)
		{
			Debug.LogError("snake is null");
			return;
		}
		Debug.Log($"{pSnake} OnPickUp {this}");
		pSnake.Heal(health);
		for(int i = 0; i < bodyParts; i++)
		{
			pSnake.AddBodyPart();
		}
	}
}

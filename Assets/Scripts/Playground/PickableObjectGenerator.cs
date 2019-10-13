using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = System.Random;

public class PickableObjectGenerator : GameBehaviour
{
	[SerializeField]
	private Playground playground;

	[SerializeField]
	private Apple applePrefab;

	private int turnsLeftToGenerate = 1;
	
	public void GenerateObjects()
	{
		turnsLeftToGenerate--;
		if(turnsLeftToGenerate > 0)
			return;

		turnsLeftToGenerate = new Random().Next(1, 3);

		//Debug.Log($"GenerateObjects. next in {turnsLeftToGenerate} turns");

		int objectCount = 3;
		List<PlaygroundField> fields = playground.GetEmtyFields(objectCount);

		foreach(PlaygroundField field in fields)
		{
			field.PlaceMapObject(GetObject());
		}
	}

	private Pickable GetObject()
	{
		Pickable newObj = Instantiate(applePrefab, transform);
		return newObj;
	}	
}

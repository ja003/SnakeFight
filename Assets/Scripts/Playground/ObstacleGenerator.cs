using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
	[SerializeField]
	private Playground playground;
	[SerializeField]
	private Border borderPrefab;

	internal void Init()
	{
		List<PlaygroundField> borderFields = playground.GetBorderFields();

		foreach(PlaygroundField field in borderFields)
		{
			Border borderObj = Instantiate(borderPrefab);
			field.PlaceMapObject(borderObj);
		}
	}
}

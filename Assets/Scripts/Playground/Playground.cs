using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Playground : MonoBehaviour
{
	[SerializeField]
	public PlaygroundField fieldPrefab;
	[SerializeField]
	private  int width;
	[SerializeField]
	private int height;

	private PlaygroundField[,] fields;	
	private Vector3 topLeftCorner;

	private const int STEP_SIZE = 1;
	public static int layerMask;

	private void Awake()
	{
		layerMask = 1 << LayerMask.NameToLayer("playground");
	}


	internal List<PlaygroundField> GetBorderFields()
	{
		List<PlaygroundField> borderFields = new List<PlaygroundField>();
		for(int x = 0; x < width; x++)
		{
			borderFields.Add(fields[x, 0]);
			borderFields.Add(fields[x, height - 1]);
		}

		for(int y = 1; y < height - 1; y++)
		{
			borderFields.Add(fields[0, y]);
			borderFields.Add(fields[width - 1, y]);
		}
		return borderFields;
	}

	internal PlaygroundField GetField(int pX, int pY)
	{
		if(IsOOB(pX, pY))
			return null;
		return fields[pX, pY];
	}

	public List<PlaygroundField> GetEmtyFields(int pCount)
	{
		List<PlaygroundField> fields = new List<PlaygroundField>();
		for(int i = 0; i < pCount; i++)
		{
			bool selected = false;
			while(!selected)
			{
				PlaygroundField randomField = GetRandomField();
				if(randomField.IsEmty() && !fields.Contains(randomField))
				{
					fields.Add(randomField);
					selected = true;
				}
			}
		}
		return fields;
	}

	private PlaygroundField GetRandomField()
	{
		int x = new Random().Next(0, width - 1);
		int y = new Random().Next(0, height - 1);
		return fields[x, y];
	}

	private bool IsOOB(int pX, int pY)
	{
		return pX < 0 || pY < 0 || pX >= width || pY >= height;
	}

	public void Init()
	{

		if(width % 2 == 0)
			width += 1;
		if(height % 2 == 0)
			height += 1;

		topLeftCorner = new Vector3(-width / 2 * STEP_SIZE, 0, height / 2 * STEP_SIZE);

		fields = new PlaygroundField[width, height];
		for(int x = 0; x < width; x++)
		{
			for(int y = 0; y < height; y++)
			{
				Vector3 position = topLeftCorner + new Vector3(x * STEP_SIZE, 0, -y * STEP_SIZE);

				PlaygroundField newField = Instantiate(fieldPrefab, position, fieldPrefab.transform.rotation, transform);
				newField.Init(x, y, position);
				fields[x, y] = newField;
			}
		}

		for(int x = 0; x < width; x++)
		{
			for(int y = 0; y < height; y++)
			{
				fields[x, y].SetNeighbours(GetField(x, y - 1), GetField(x + 1, y), GetField(x, y + 1), GetField(x - 1, y));
			}
		}

		//calculated 'good' size
		//not exact for higher numbers => todo: precalculate 
		//sizes for multiple widths and interpolate
		Camera.main.orthographicSize = width - 2;
	}

}



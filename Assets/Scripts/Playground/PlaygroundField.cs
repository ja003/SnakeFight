using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaygroundField : GameBehaviour
{
	private int x;
	private int y;
	public Vector3 Position { get; private set; }

	private PlaygroundField up;
	private PlaygroundField right;
	private PlaygroundField down;
	private PlaygroundField left;

	public BodyPart bodyPart;
	public MapObject mapObject;

	[SerializeField]
	private Texture texture;
	[SerializeField]
	private Texture textureSelected;

	public void Init(int pX, int pY, Vector3 pPosition)
	{
		this.x = pX;
		this.y = pY;
		this.Position = pPosition;
	}

	public void SetNeighbours(PlaygroundField pUp, PlaygroundField pRight, 
		PlaygroundField pDown, PlaygroundField pLeft)
	{
		up = pUp;
		right = pRight;
		down = pDown;
		left = pLeft;
	}

	internal void TryPickUpObject(Snake pSnake)
	{
		if(mapObject == null)
			return;
		bool pickedUp = mapObject.OnSnakeContact(pSnake);
		if(pickedUp)
		{
			mapObject = null;
		}
	}

	internal void PlaceMapObject(MapObject pObject)
	{
		pObject.transform.parent = transform;
		pObject.transform.localPosition = new Vector3(0, 0, -0.1f);
		mapObject = pObject;
	}

	internal PlaygroundField GetNeighbour(EDirection pDirection)
	{
		switch(pDirection)
		{
			case EDirection.Up:
				return up;
			case EDirection.Right:
				return right;
			case EDirection.Down:
				return down;
			case EDirection.Left:
				return left;
		}
		return null;
	}

	internal bool IsEmty()
	{
		return bodyPart == null && mapObject == null;
	}

	internal void SetSelected(bool pValue)
	{
		rend.material.mainTexture = pValue ? textureSelected : texture;
	}

	public override string ToString()
	{
		return $"Field[{x},{y}]";
	}
}
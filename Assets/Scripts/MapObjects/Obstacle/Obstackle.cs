using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstackle : MapObject
{

	public override bool OnSnakeContact(Snake pSnake)
	{
		pSnake.Kill();
		return false;
	}
}

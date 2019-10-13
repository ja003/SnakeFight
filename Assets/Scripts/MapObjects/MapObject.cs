using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MapObject : MonoBehaviour
{
	public abstract bool OnSnakeContact(Snake pSnake);
}

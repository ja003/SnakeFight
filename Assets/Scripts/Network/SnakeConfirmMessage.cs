using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

#pragma warning disable CS0618 // Type or member is obsolete
public class SnakeConfirmMessage : MessageBase
#pragma warning restore CS0618 // Type or member is obsolete
{
	public int snakeId;
	public EDirection direction;
}
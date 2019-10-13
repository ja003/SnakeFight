using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehaviour : MonoBehaviour
{
	protected void DoInTime(Action pEvent, float pTime)
	{
		LeanTween.value(0, 1, pTime).setOnComplete(pEvent);
	}
}

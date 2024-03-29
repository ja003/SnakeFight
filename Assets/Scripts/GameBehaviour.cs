﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehaviour : MonoBehaviour
{
	protected Renderer rend;

	private void Awake()
	{
		rend = GetComponent<Renderer>();
	}

	protected void DoInTime(Action pEvent, float pTime)
	{
		LeanTween.value(0, 1, pTime).setOnComplete(pEvent);
	}
}

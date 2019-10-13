using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PanelMovement : MonoBehaviour
{
	[SerializeField]
	private Button btnUp;
	[SerializeField]
	private Button btnRight;
	[SerializeField]
	private Button btnDown;
	[SerializeField]
	private Button btnLeft;
	
	internal void Init(UnityAction<EDirection> pOnSetDirection)
	{
		btnUp.onClick.AddListener(() => pOnSetDirection(EDirection.Up));
		btnRight.onClick.AddListener(() => pOnSetDirection(EDirection.Right));
		btnDown.onClick.AddListener(() => pOnSetDirection(EDirection.Down));
		btnLeft.onClick.AddListener(() => pOnSetDirection(EDirection.Left));
	}

	internal void SetInteractable(bool pValue)
	{
		btnUp.interactable = pValue;
		btnRight.interactable = pValue;
		btnDown.interactable = pValue;
		btnLeft.interactable = pValue;
	}
}

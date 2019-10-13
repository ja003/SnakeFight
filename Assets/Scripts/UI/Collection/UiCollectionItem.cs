using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UiCollectionItem : UiBehaviour
{
	[SerializeField]
	private Image icon;

	[SerializeField]
	private Image select;

	[SerializeField]
	private Text amount;

	public void Init(Sprite pIcon, int pAmount, int pId, UnityAction<int> pOnClick)
	{
		icon.sprite = pIcon;
		SetAmount(pAmount);
		button.onClick.AddListener(() => pOnClick(pId));
		SetSelected(false);
	}

	private void SetAmount(int pAmount)
	{
		amount.text = pAmount.ToString();
	}

	internal void Refresh(int pAmount)
	{
		SetAmount(pAmount);
	}

	internal void SetSelected(bool pValue)
	{
		select.enabled = pValue;
	}
}

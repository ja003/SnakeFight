using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UiPanelActionItem : UiBehaviour
{
	private LayoutElement element;

	protected override void Awake()
	{
		base.Awake();
		element = GetComponent<LayoutElement>();
	}

	public void Init(UnityAction pOnClick)
	{
		button.onClick.AddListener(pOnClick);
	}

	public void SetTexture(Sprite pSprite)
	{
		image.sprite = pSprite;
	}

	public void SetActive(bool pActive)
	{
		image.enabled = pActive;
		element.enabled = pActive;
	}
}

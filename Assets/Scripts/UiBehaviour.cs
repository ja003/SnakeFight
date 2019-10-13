using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiBehaviour : MonoBehaviour
{
	protected Button button;
	protected Image image;

	protected virtual void Awake()
	{
		button = GetComponent<Button>();
		image = GetComponent<Image>();
	}

	public void SetInteractable(bool pValue)
	{
		button.interactable = pValue;
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
#pragma warning disable 0649

public class GameUI : MonoBehaviour
{
	[SerializeField]
	private PanelMovement movement;

	[SerializeField]
	private Button btnConfirm;

	[SerializeField]
	private InputController input;

	[SerializeField]
	private UiItemPanel itemPanel;
	public UiItemPanel ItemPanel => itemPanel;

	[SerializeField]
	private UiPowerUpPanel powerUpPanel;
	public UiPowerUpPanel PowerUpPanel => powerUpPanel;

	[SerializeField]
	private UiPanelAction actionPanel;
	public UiPanelAction ActionPanel => actionPanel;


	public void Init()
	{
		movement.Init(input.OnSetDirection);
		actionPanel.Init(input.OnCancelAction);
		btnConfirm.onClick.AddListener(input.ConfirmActions);
		actionPanel.Init();
	}

	public void OnEvaluationStart()
	{
		btnConfirm.enabled = false;
		movement.SetInteractable(false);
		actionPanel.SetInteractable(false);
		ItemPanel.SetInteractable(false);
		powerUpPanel.SetInteractable(false);
	}

	public void OnEvaluationFinished()
	{
		btnConfirm.enabled = true;
		movement.SetInteractable(true);
		actionPanel.SetInteractable(true);
		ItemPanel.SetInteractable(true);
		powerUpPanel.SetInteractable(true);
	}
}

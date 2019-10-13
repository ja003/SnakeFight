using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PowerUpController : CollectionController
{
	public PowerUpController(Snake pOwner, UiCollectionPanel pUiPanel, UiPanelAction pUiAction) : base(pOwner, pUiPanel, pUiAction)
	{
	}

	public void AddPowerUp(PowerUp pPowerUp)
	{
		AddItem(pPowerUp);
	}

	public override void Evaluate()
	{
		Debug.Log("TODO: powerup Evaluate");

		DeselectItem();
	}

	protected override void OnItemSelected(int pItemId)
	{
		base.OnItemSelected(pItemId);
		uiAction.OnSetPowerUp((PowerUp)ActiveItem);
	}

	internal override void OnCancelAction(EAction pAction)
	{
		switch(pAction)
		{
			case EAction.PowerUp:
				DeselectItem();
				break;
		}
	}
}

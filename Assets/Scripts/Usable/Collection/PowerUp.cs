using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : CollectionItem
{

	public PowerUp(ConfigPowerUp config, int pAmount) : base((int)config.id, pAmount, config.icon)
	{
	}
	
	internal override void Evaluate()
	{
		throw new System.NotImplementedException();
	}

	internal override void SetSelected(bool pValue)
	{
		throw new System.NotImplementedException();
	}

	internal override void SetTarget(PlaygroundField pTargetField)
	{
		throw new System.NotImplementedException();
	}

	//	public static PowerUp Convert(CollectionItem pItem)
	//	{
	//		return new PowerUp((EPowerUpId)pItem.id, pItem.amount, pItem.icon);
	//	}
}

public enum EPowerUpId
{
	None,
	Specal,
	Specal2,
}

public enum EPowerUpType
{

}


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class UsableItem : Usable
{
	[SerializeField]
	public EItemId id;

	public abstract void SetTarget(PlaygroundField pTargetField);
}

public abstract class UsablePowerUp : Usable
{
	[SerializeField]
	public EPowerUpId id;
}


public abstract class Weapon : UsableItem
{
	[SerializeField]
	int damage;


}

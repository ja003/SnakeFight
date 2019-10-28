using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ranged : Weapon
{
	[SerializeField]
	public int range;

	//[SerializeField]
	//Projectile projectile;


	private void Update()
	{
		//Debug.Log("Update " + transform.position);
	}

	protected override void Use()
	{
		//DoInTime(SpawnProjectile, 0.2f);
		SpawnProjectile();
	}

	private void SpawnProjectile()
	{
		if(target == null)
		{
			return;
		}

		Debug.Log($"{this} SpawnProjectile at {transform.position}");
		projectile = Instantiate(projectile, transform.position, 
			Quaternion.identity, itemManager.transform);
		projectile.Initiate(target);
	}

	public override void SetSelected(bool pValue)
	{
		base.SetSelected(pValue);
		target?.SetSelected(pValue);
	}


	PlaygroundField target;
	public override void SetTarget(PlaygroundField pField)
	{
		Debug.Log($"{this} SetTarget {pField}");
		target?.SetSelected(false);
		target = pField;
		target?.SetSelected(true);
	}

}

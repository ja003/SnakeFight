using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Usable : GameBehaviour
{
	//protected Snake owner;
	protected ItemManager itemManager;
	protected Projectile projectile;
	protected Snake owner;

	public void Init(ItemManager pItemManager, Projectile pProjectile)
	{
		itemManager = pItemManager;
		projectile = pProjectile;
		SetSelected(false);
	}

	public void SetOwner(Snake pOwner)
	{
		owner = pOwner;

	}

	public virtual void SetSelected(bool pValue)
	{
		Debug.Log($"{this} SetSelected {pValue}");
		transform.position = owner == null ? Vector3.zero : owner.GetHeadPosition();
		gameObject.SetActive(pValue);
	}

	public void Evaluate()
	{
		/*Vector3 headPosition = pOwner.GetHeadPosition();
		Debug.Log($"{pOwner} - {this} spawn at {headPosition}");

		Usable instance = Instantiate(this, headPosition, Quaternion.identity, itemManager.transform);
		instance.Use();*/

		Use();
	}

	protected abstract void Use();
}



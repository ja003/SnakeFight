using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : GameBehaviour
{
	[SerializeField]
	public float speed;

	PlaygroundField target;

	[SerializeField]
	public EItemId id;

	public void Initiate(PlaygroundField pTarget)
	{
		target = pTarget;
		Evaluate();
	}

	public void Evaluate()
	{
		Vector3 start = transform.position;
		Vector3 dir = target.transform.position - start;
		float length = dir.magnitude;
		Vector3 end = length > 1 ? start + dir.normalized * speed : target.transform.position;

		LeanTween.move(gameObject, end, 1).setOnComplete(OnMoveToDestination);
	}

	internal void Deselect()
	{
		target?.SetSelected(false);
	}

	private void OnMoveToDestination()
	{
		if(Vector3.Distance(target.transform.position, transform.position) < 1)
		{
			OnReachedTarget();
		}
	}

	private void OnReachedTarget()
	{
		Debug.Log($"Projectile reached target {target}");
	}
}

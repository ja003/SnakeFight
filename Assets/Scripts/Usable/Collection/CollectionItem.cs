using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollectionItem
{
	public int id;
	public int amount;
	internal Sprite icon;

	public CollectionItem(int id, int amount, Sprite icon)
	{
		this.id = id;
		this.amount = amount;
		this.icon = icon;
	}

	internal void AddAmount(int pAmount)
	{
		amount += pAmount;
	}

	abstract internal void SetSelected(bool pValue);
	internal abstract void Evaluate();
	internal abstract void SetTarget(PlaygroundField pTargetField);
}


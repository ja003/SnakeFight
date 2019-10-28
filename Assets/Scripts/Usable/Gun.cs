using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Ranged
{
	protected override void Use()
	{
		Debug.Log($"{this} - Use");
		base.Use();
	}


}

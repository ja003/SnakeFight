using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BodyPart : MonoBehaviour
{
	public int health;
	public PlaygroundField field;
	public BodyPart previousPart;
	public BodyPart nextPart;
	public Snake owner;

	public void Draw()
	{
		transform.position = field.Position;
	}

	internal void Move(PlaygroundField pToField)
	{
		field.bodyPart = null;
		PlaygroundField newField = pToField;
		if(newField == null)
		{
			field.bodyPart = this;
			Debug.Log("OOB!");
			return;
		}
		if(nextPart != null)
			nextPart.Move(field);

		field = newField;
		field.bodyPart = this;
		field.TryPickUpObject(owner);
	}

	internal void AddHealth(int pHealth)
	{
		SetHealth(health + pHealth);
		nextPart?.AddHealth(pHealth / 2);
	}

	internal void SetHealth(int pHealth)
	{
		health = pHealth;
		OnHealthChanged();
	}

	private void OnHealthChanged()
	{
		transform.localScale = Vector3.one * (health / 100f);
		if(health <= 0)
			Destroy();
	}

	public void Destroy()
	{
		//Debug.Log("Destroy");
		if(previousPart != null)
			previousPart.nextPart = null;
		gameObject.SetActive(false);
		nextPart?.Destroy();
	}

	public void OnAttacked(int pDamage)
	{
		//Debug.Log("OnAttacked");
		AddHealth(-pDamage);
	}
}

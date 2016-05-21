using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/*
 * 
 */

[System.Serializable]
public class DamageBase
{

	// Delegates for how to reduce damage.
	public delegate float DelTreatDamage(float damage);
	public DelTreatDamage GetDamage;

	// Constructor.
	public DamageBase(DelTreatDamage redu)
	{
		GetDamage = redu;
	}

	// To get the name of the damagebase.
	public string typeName {
		get
		{
			return this.GetType().Name;
		}
		private set { }
	}
}

[System.Serializable]
public class FlameEntity_Health {

	// All types of damages.
	[SerializeField]
	private List<DamageBase> damageTypes = new List<DamageBase>();

	// HP points.
	[SerializeField]
	float health;

	// To register a damge type.
	public void RegisterType(DamageBase typ)
	{
		damageTypes.Insert(damageTypes.Count, typ);
	}

	// To apply damage.
	public void ApplyDamage(string typeName, float damage)
	{
		DamageBase damageType = FindDamageType(typeName);
		if (damageType.GetDamage != null)
		{
			damage = damageType.GetDamage(damage);
		}
		health -= damage;
	}

	// To find damage types again.
	public DamageBase FindDamageType(string typeName)
	{
		foreach (var entry in damageTypes)
		{
			if (entry.typeName == typeName)
			{
				return entry;
			}
		}
		return null;
	}
}

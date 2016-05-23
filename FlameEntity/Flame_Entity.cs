using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

/*
 * This is a class that can hold information about enteties
 */
public class Flame_Entity : MonoBehaviour {//NetworkBehaviour {

	public Flame_Attr stats = new Flame_Attr();

	public FlameEntity_Health health = new FlameEntity_Health();

	public FlameInventory_Container container = null;

	public float FallDamageReduce(float damage)
	{
		if (damage < 5)
			return 0;
		return damage * 0.5f;
	}
	void Start()
	{

		//if (isServer)
		{
			container = GetComponent<FlameInventory_Container>() ?? gameObject.AddComponent<FlameInventory_Container>();
			health.RegisterType(new FallDamage(FallDamageReduce));
		}
	}

	void OnCollisionEnter(Collision hit)
	{
		health.ApplyDamage("FallDamage", hit.relativeVelocity.magnitude);
	}
	public FlameInventory_Container GetContainer()
	{
		if (container == null)
			container = gameObject.AddComponent<FlameInventory_Container>();
		return container;

	}
}

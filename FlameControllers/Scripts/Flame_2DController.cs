using UnityEngine;
using System.Collections;
using FlameCore.Controllers;

public class Flame_2DController : Flame_3DBaseController {

	public bool allowX = true;
	public bool allowY = true;
	void Update()
	{

		Vector3 vr = transform.position.from();

		if (allowX)
		{
			float x = Input.GetAxisRaw(bindings.horizontalAxis);
			vr.x += x * movementSpeed;
			
		}
		if (allowY)
		{
			float y = Input.GetAxisRaw(bindings.verticalAxis);
			vr.y += y * movementSpeed;
		}

		avatar_rigidbody.AddForce(vr);
		//transform.position = vr;
	}
}

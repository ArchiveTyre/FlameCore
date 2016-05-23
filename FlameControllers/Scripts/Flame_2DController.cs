using UnityEngine;
using System.Collections;
using FlameCore.Controllers;

public class Flame_2DController : Flame_2DBaseController {

	public bool allowX = true;
	public bool allowY = true;
	public bool allowHalt = true;
	public float maxVelocity = 2f;
	void Update()
	{

		
		var vr = avatar_rigidbody.velocity.from();
		
		if (vr.x > maxVelocity)
		{
			vr.x = maxVelocity;
		}
		else if (vr.x < -maxVelocity)
		{
			vr.x = -maxVelocity;
		}
		if (vr.y > maxVelocity)
		{
			vr.y = maxVelocity;
		}
		else if (vr.y < -maxVelocity)
		{
			vr.y = -maxVelocity;
		}

		if (allowX)
		{
			float x = Input.GetAxisRaw(bindings.horizontalAxis);
			if (x == 0 && allowHalt)
				vr.x = 0;
			vr.x += x * movementSpeed;
			
		}
		if (allowY)
		{
			float y = Input.GetAxisRaw(bindings.verticalAxis);
			if (y == 0 && allowHalt)
				vr.y = 0;
			vr.y += y * movementSpeed;
		}

		if (Input.GetButtonDown("Run"))
		{
			vr = Vector2.zero;
		}

		avatar_rigidbody.velocity = vr;
		//transform.position = vr;
	}
}

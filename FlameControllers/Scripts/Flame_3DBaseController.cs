using UnityEngine;
using System.Collections;
using FlameCore.Controllers;

namespace FlameCore.Controllers
{
	public class Flame_3DBaseController : Flame_BaseController
	{

		private Rigidbody cachedRigidbody;

		protected override void SetupBase()
		{
			base.SetupBase();
			cachedRigidbody = GetComponent<Rigidbody>();
		}
		// Rigidbody for the avatar.
		protected Rigidbody avatar_rigidbody
		{
			get
			{
				if (cachedAvatar != Avatar)
				{
					cachedRigidbody = GetComponent<Rigidbody>();
					cachedAvatar = Avatar;
				}
				return cachedRigidbody;
			}
		}
	}
}

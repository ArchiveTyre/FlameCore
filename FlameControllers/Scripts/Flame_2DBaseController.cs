using UnityEngine;
using System.Collections;

namespace FlameCore.Controllers
{
	public class Flame_2DBaseController : Flame_BaseController
	{
		private Rigidbody2D cachedRigidbody;

		protected override void SetupBase()
		{
			base.SetupBase();
			cachedRigidbody = GetComponent<Rigidbody2D>();
		}
		// Rigidbody for the avatar.
		protected Rigidbody2D avatar_rigidbody
		{
			get
			{
				if (cachedAvatar != Avatar)
				{
					cachedRigidbody = GetComponent<Rigidbody2D>();
					cachedAvatar = Avatar;
				}
				return cachedRigidbody;
			}
		}
	}
}

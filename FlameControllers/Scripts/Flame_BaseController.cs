using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

/*
 * Controller_ClickType
 * CopyRight 2016 (c) All rights reserved by Flame___
 * Description:
 * - This class is the base of all classes. 
 * - It can be used for global configurations 
 * - like 2 different types of controlls with the 
 * - same avatar and speed.
 * History:
 * - Created on April 2 2016 By Alexander Björkman
 */
namespace FlameCore.Controllers
{
	[AddComponentMenu("")]
	public class Flame_BaseController : NetworkBehaviour
	{

		// the keybindings to use the correct keys
		public Flame_KeyBindings bindings;

		[SerializeField]
		private GameObject avatar;

		[HideInInspector]
		protected GameObject cachedAvatar;

		// Slightly more effiecient then lowercase avatar since it automatically updates the 
		public GameObject Avatar
		{
			get
			{
				return avatar;
			}

			set
			{
				avatar = value;
				cachedAvatar = value;
			}
		}


		// the speed of movement.
		public float movementSpeed = 1f;

		// Camera of avatar.
		public Camera avatarCamera = null;

		// The speed of rotation
		public float rotationSpeed = 1f;

		/// <summary>
		/// This function will set some objects
		/// and disable self if not local player.
		///  </summary>
		virtual protected void SetupBase()
		{
			if (Avatar == null) {
				Debug.LogError("Avatar not set, using gameObject as Avatar");
				Avatar = gameObject;
			}

			if (bindings == null)
				bindings = gameObject.AddComponent<Flame_KeyBindings>();

		}

		virtual protected void MoveUpdate()
		{
			
		}

		void Update()
		{
			if (isLocalPlayer)
			{
				MoveUpdate();
			}	
		}

		// THIS METHOD MUST BE CALLED AT THE START METHOD OF ANY CONTROLLER IF IT WANTS TO ACCES Avatar or avatar_rigidbody
		void OnEnable()
		{
			SetupBase();
		}

	}
}

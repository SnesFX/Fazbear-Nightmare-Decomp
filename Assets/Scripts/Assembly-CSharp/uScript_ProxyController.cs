using System;
using UnityEngine;

[NodePath("Events/Physics Events")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Controller_Collision")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Fires an event signal when a CharacterController collides with an object.")]
[FriendlyName("Controller Collision", "Fires an event signal when a CharacterController collides with an object.")]
public class uScript_ProxyController : uScriptEvent
{
	public class ProxyControllerCollisionEventArgs : EventArgs
	{
		private ControllerColliderHit m_Hit;

		[FriendlyName("Instigator", "The GameObject that was hit by Character Controller.")]
		public GameObject GameObject
		{
			get
			{
				return m_Hit.gameObject;
			}
		}

		[FriendlyName("Character Controller", "The controller that hit Collider.")]
		[SocketState(false, false)]
		public CharacterController Controller
		{
			get
			{
				return m_Hit.controller;
			}
		}

		[SocketState(false, false)]
		[FriendlyName("Collider", "The collider that was hit by Character Controller.")]
		public Collider Collider
		{
			get
			{
				return m_Hit.collider;
			}
		}

		[SocketState(false, false)]
		[FriendlyName("Rigid Body", "The rigidbody that was hit by Character Controller.")]
		public Rigidbody RigidBody
		{
			get
			{
				return m_Hit.rigidbody;
			}
		}

		[SocketState(false, false)]
		[FriendlyName("Transform", "The transform that was hit by Character Controller.")]
		public Transform Transform
		{
			get
			{
				return m_Hit.transform;
			}
		}

		[FriendlyName("Point", "The impact point in world space.")]
		[SocketState(false, false)]
		public Vector3 Point
		{
			get
			{
				return m_Hit.point;
			}
		}

		[SocketState(false, false)]
		[FriendlyName("Normal", "The normal of the surface we collided with in world space.")]
		public Vector3 Normal
		{
			get
			{
				return m_Hit.normal;
			}
		}

		[SocketState(false, false)]
		[FriendlyName("Move Direction", "Approximately the direction from the center of the capsule to the point we touch.")]
		public Vector3 MoveDirection
		{
			get
			{
				return m_Hit.moveDirection;
			}
		}

		[SocketState(false, false)]
		[FriendlyName("Move Length", "How far the character has travelled until it hit the collider.")]
		public float MoveLength
		{
			get
			{
				return m_Hit.moveLength;
			}
		}

		public ProxyControllerCollisionEventArgs(ControllerColliderHit hit)
		{
			m_Hit = hit;
		}
	}

	public delegate void uScriptEventHandler(object sender, ProxyControllerCollisionEventArgs args);

	[FriendlyName("On Controller Collider Hit")]
	public event uScriptEventHandler OnHit;

	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (this.OnHit != null)
		{
			this.OnHit(this, new ProxyControllerCollisionEventArgs(hit));
		}
	}
}

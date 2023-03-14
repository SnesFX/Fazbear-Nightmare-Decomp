using System;
using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("Particle Collision", "Fires an event signal when a particle collides with a GameObject.")]
[NodePath("Events/Particles")]
[NodeToolTip("Fires an event signal when a particle collides with a GameObject.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Particle_Collision")]
public class uScript_Particle : uScriptEvent
{
	public class ParticleCollisionEventArgs : EventArgs
	{
		private GameObject m_GameObject;

		[FriendlyName("Instigator", "The GameObject that was collided with.")]
		public GameObject GameObject
		{
			get
			{
				return m_GameObject;
			}
		}

		public ParticleCollisionEventArgs(GameObject hit)
		{
			m_GameObject = hit;
		}
	}

	public delegate void uScriptEventHandler(object sender, ParticleCollisionEventArgs args);

	[FriendlyName("On Particle Collision")]
	public event uScriptEventHandler Collision;

	private void OnParticleCollision(GameObject gameObject)
	{
		if (this.Collision != null)
		{
			this.Collision(this, new ParticleCollisionEventArgs(gameObject));
		}
	}
}

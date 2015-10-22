using UnityEngine;
using System.Collections;

namespace FoF
{
	public abstract class Actor : MonoBehaviour
	{
		protected float movementSpeed = 6f;

		protected int _health;
		public int health
		{
			get { return _health; }
			set
			{
				_health = health;
				if (_health <= 0) { this.die(); }
			}
		}

		protected int moveSpeed;
		protected int attackStrength;

		public abstract void move (float forwardback, float leftright, float vertical);

		public void attack (Actor target)
		{
			target.health -= attackStrength;
		}

		public abstract void die ();
	}
}

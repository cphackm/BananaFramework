using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

using BananaFramework.Chunks.Structural;

namespace BananaFramework.Chunks.Logical
{
	/// <summary>
	/// The AbstractGameObject class acts as the base class for all logical objects that act as 
	/// part of the game.
	/// </summary>
	public abstract class AbstractGameObject
	{
		protected bool isDestroyed;
		public bool IsDestroyed
		{
			get
			{
				return isDestroyed;
			}
		}
		public int id { get; set; }
		public AbstractGameLevel level { get; set; }

		public virtual void OnRegisteredByLevel() { }
		public abstract void Update();
		public abstract void Render();
	}
}

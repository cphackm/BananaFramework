using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

using BananaFramework.Managers;

namespace BananaFramework.Chunks.Logical
{
	/// <summary>
	/// The AbstractSpriteObject class provides some basic fields for objects that will be rendered 
	/// to the screen.
	/// </summary>
	public abstract class AbstractSpriteObject : AbstractGameObject
	{
		protected Vector2 position { set; get; }
		protected float angle { set; get; }
		protected Vector2 scale { set; get; }
		protected Color color { set; get; }
		protected float depth { set; get; }
		protected RenderManager.BaseOriginKeys[] origin { set; get; }
	}
}

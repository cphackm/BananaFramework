using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

using BananaFramework.GameManagers;

namespace BananaFramework.GameObjects.GraphicalObjects
{
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

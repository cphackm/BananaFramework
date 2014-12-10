using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

using BananaFramework.Chunks.Structural;

namespace BananaFramework.Chunks.Logical
{
	public abstract class AbstractGameObject
	{
		public int id { get; set; }
		public AbstractGameLevel level { get; set; }

		public abstract void Update();
		public abstract void Render();
	}
}

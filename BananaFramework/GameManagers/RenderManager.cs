using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using BananaFramework.GameChunks;

namespace BananaFramework.GameManagers
{
	public class RenderManager
	{
		public enum BaseOriginKeys
		{
			top,
			left,
			right,
			bottom,
			center
		};

		public struct OriginKeys
		{
			public static BaseOriginKeys[] top = { BaseOriginKeys.top };
			public static BaseOriginKeys[] bottom = { BaseOriginKeys.bottom };
			public static BaseOriginKeys[] left = { BaseOriginKeys.left };
			public static BaseOriginKeys[] right = { BaseOriginKeys.right };
			public static BaseOriginKeys[] center = { BaseOriginKeys.center };
			public static BaseOriginKeys[] topLeft = { BaseOriginKeys.top, BaseOriginKeys.left };
			public static BaseOriginKeys[] topRight = { BaseOriginKeys.top, BaseOriginKeys.right };
			public static BaseOriginKeys[] botLeft = { BaseOriginKeys.bottom, BaseOriginKeys.left };
			public static BaseOriginKeys[] botRight = { BaseOriginKeys.bottom, BaseOriginKeys.right };
		}

		private static ContentManager cm;
		private static GraphicsDevice gd;
		private static SpriteBatch spriteBatch;
		private static Dictionary<string, Texture2D> textures;
		private static Dictionary<string, Animation> animations;

		public static void Initialize(GraphicsDevice Gd, ContentManager Cm)
		{
			gd = Gd;
			cm = Cm;
			spriteBatch = new SpriteBatch(gd);
			textures = new Dictionary<string, Texture2D>();
			animations = new Dictionary<string, Animation>();
		}

		public static void LoadTexture(string TextureKey, string TexturePath)
		{
			if (null != cm)
			{
				textures.Add(TextureKey, cm.Load<Texture2D>(TexturePath));
			}
		}

		public static void LoadAnimations(string AnimationsPath)
		{
			List<Animation> tempAnimations = Animation.LoadAnimationsFromXml(AnimationsPath);
			foreach (Animation a in tempAnimations)
			{
				animations.Add(a.name, a);
			}
		}

		public static Texture2D GetTexture(string TextureKey)
		{
			return textures[TextureKey];
		}

		public static Animation GetAnimation(string AnimationKey)
		{
			return animations[AnimationKey];
		}

		private static Vector2 CalculateOrigin(Texture2D Texture, BaseOriginKeys[] Origin)
		{
			Vector2 origin = Vector2.Zero;

			if (Origin.Contains(BaseOriginKeys.top))
			{
				origin.Y = 0;
			}
			if (Origin.Contains(BaseOriginKeys.bottom))
			{
				origin.Y = Texture.Height;
			}
			if (Origin.Contains(BaseOriginKeys.left))
			{
				origin.X = 0;
			}
			if (Origin.Contains(BaseOriginKeys.right))
			{
				origin.X = Texture.Width;
			}
			if (Origin.Contains(BaseOriginKeys.center))
			{
				origin.X = Texture.Width / 2;
				origin.Y = Texture.Height / 2;
			}

			return origin;
		}

		private static Vector2 CalculateOrigin(Rectangle SourceRect, BaseOriginKeys[] Origin)
		{
			Vector2 origin = Vector2.Zero;

			if (Origin.Contains(BaseOriginKeys.top))
			{
				origin.Y = 0;
			}
			if (Origin.Contains(BaseOriginKeys.bottom))
			{
				origin.Y = SourceRect.Height;
			}
			if (Origin.Contains(BaseOriginKeys.left))
			{
				origin.X = 0;
			}
			if (Origin.Contains(BaseOriginKeys.right))
			{
				origin.X = SourceRect.Width;
			}
			if (Origin.Contains(BaseOriginKeys.center))
			{
				origin.X = SourceRect.Width / 2;
				origin.Y = SourceRect.Height / 2;
			}

			return origin;
		}

		public static void BeginRender()
		{
			spriteBatch.Begin();
		}

		public static void EndRender()
		{
			spriteBatch.End();
		}

		public static void DrawQuad(string TextureKey, Vector2 Position, Vector2 Scale, BaseOriginKeys[] Origin)
		{
			Texture2D texture = GetTexture(TextureKey);
			Vector2 origin = CalculateOrigin(texture, Origin);

			spriteBatch.Draw(
				texture, 
				position: Position, 
				sourceRectangle: new Rectangle(0, 0, texture.Width, texture.Height), 
				color: Color.White, 
				scale: Scale, 
				origin: origin);
		}

		public static void DrawQuad(string TextureKey, Vector2 Position, Rectangle SourceRect, Vector2 Scale, BaseOriginKeys[] Origin)
		{
			Texture2D texture = GetTexture(TextureKey);
			Vector2 origin = CalculateOrigin(SourceRect, Origin);

			spriteBatch.Draw(
				texture,
				position: Position,
				sourceRectangle: SourceRect,
				color: Color.White,
				scale: Scale,
				origin: origin);
		}
	}
}

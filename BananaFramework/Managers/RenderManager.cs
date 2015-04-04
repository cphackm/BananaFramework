using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using BananaFramework.Chunks.Support;

namespace BananaFramework.Managers
{
	/// <summary>
	/// The RenderManager class provides helper methods for rendering sprites, managing textures 
	/// and animations, and managing render targets.  It also keeps track of the SpriteBatch object 
	/// used for all rendering operations
	/// </summary>
	public class RenderManager
	{
		/// <summary>
		/// Defines the most basic origin keys.
		/// </summary>
		public enum BaseOriginKeys
		{
			top,
			left,
			right,
			bottom,
			center,
			centerX,
			centerY
		};

		/// <summary>
		/// Defines compound origin keys for sprites.
		/// </summary>
		public struct OriginKeys
		{
			public static BaseOriginKeys[] top = { BaseOriginKeys.top, BaseOriginKeys.centerX };
			public static BaseOriginKeys[] bottom = { BaseOriginKeys.bottom, BaseOriginKeys.centerX };
			public static BaseOriginKeys[] left = { BaseOriginKeys.left, BaseOriginKeys.centerY };
			public static BaseOriginKeys[] right = { BaseOriginKeys.right, BaseOriginKeys.centerY };
			public static BaseOriginKeys[] center = { BaseOriginKeys.center };
			public static BaseOriginKeys[] topLeft = { BaseOriginKeys.top, BaseOriginKeys.left };
			public static BaseOriginKeys[] topRight = { BaseOriginKeys.top, BaseOriginKeys.right };
			public static BaseOriginKeys[] botLeft = { BaseOriginKeys.bottom, BaseOriginKeys.left };
			public static BaseOriginKeys[] botRight = { BaseOriginKeys.bottom, BaseOriginKeys.right };
		}

		public static Vector2 scroll;
		private static ContentManager cm;
		private static GraphicsDevice gd;
		private static SpriteBatch spriteBatch;
		private static Dictionary<string, Texture2D> textures;
		private static Dictionary<string, Animation> animations;
		private static Dictionary<string, SpriteFont> fonts;
		private static Dictionary<string, RenderTarget2D> renderTargets;
		private static Texture2D nextTexture;

		/// <summary>
		/// Initializes the RenderManager by setting up important internal components.
		/// </summary>
		/// <param name="Gd">Reference to the GraphicsDevice.</param>
		/// <param name="Cm">Reference to the ContentManager.</param>
		public static void Initialize(GraphicsDevice Gd, ContentManager Cm)
		{
			gd = Gd;
			cm = Cm;
			spriteBatch = new SpriteBatch(gd);
			textures = new Dictionary<string, Texture2D>();
			animations = new Dictionary<string, Animation>();
			fonts = new Dictionary<string, SpriteFont>();
			renderTargets = new Dictionary<string, RenderTarget2D>();
			nextTexture = null;
		}

		/// <summary>
		/// Loads a texture and associates it with a string key.
		/// </summary>
		/// <param name="TextureKey">The key to associate with the new texture.</param>
		/// <param name="TexturePath">The path of the texture to load.</param>
		public static void LoadTexture(string TextureKey, string TexturePath)
		{
			if (null != cm)
			{
				textures.Add(TextureKey, cm.Load<Texture2D>(TexturePath));
			}
		}

		/// <summary>
		/// Loads a list of animations.
		/// </summary>
		/// <param name="AnimationsPath">The path to the file containing the list of animations to load.</param>
		public static void LoadAnimations(string AnimationsPath)
		{
			List<Animation> tempAnimations = Animation.LoadAnimationsFromXml(AnimationsPath);
			foreach (Animation a in tempAnimations)
			{
				animations.Add(a.name, a);
			}
		}

		/// <summary>
		/// Loads a font and associates it with a string key.
		/// </summary>
		/// <param name="FontKey">The key to associate with the new font.</param>
		/// <param name="FontPath">The path of the font to load.</param>
		public static void LoadFont(string FontKey, string FontPath)
		{
			if (null != cm)
			{
				fonts.Add(FontKey, cm.Load<SpriteFont>(FontPath));
			}
		}

		/// <summary>
		/// Sets the currently active render target.
		/// </summary>
		/// <param name="TargetKey">The key of the render target to activate.</param>
		public static void SetRenderTarget(string TargetKey)
		{
			gd.SetRenderTarget(TargetKey == null ? null : GetRenderTarget(TargetKey));
		}

		/// <summary>
		/// Sets a texture to draw if one isn't specified on the next draw call.
		/// </summary>
		/// <param name="TextureKey">The key of the texture to draw.</param>
		public static void SetTexture(string TextureKey)
		{
			nextTexture = GetTexture(TextureKey);
		}

		/// <summary>
		/// Sets a texture to draw from one of the tracked render targets.
		/// </summary>
		/// <param name="TargetKey">The key of the render target to draw.</param>
		public static void SetTextureFromRenderTarget(string TargetKey)
		{
			nextTexture = GetRenderTarget(TargetKey);
		}

		/// <summary>
		/// Gets the texture associated with the given string key.
		/// </summary>
		/// <param name="TextureKey">The key of the texture to return.</param>
		/// <returns>The texture associated with TextureKey.</returns>
		public static Texture2D GetTexture(string TextureKey)
		{
			return textures[TextureKey];
		}

		public static SpriteFont GetFont(string FontKey)
		{
			return fonts[FontKey];
		}

		/// <summary>
		/// Gets the render target associated with the given string key.
		/// </summary>
		/// <param name="TargetKey">The key of the render target to return.</param>
		/// <returns>The render target associated with TargetKey.</returns>
		public static RenderTarget2D GetRenderTarget(string TargetKey)
		{
			return renderTargets[TargetKey];
		}

		/// <summary>
		/// Returns the animation associated with the given string key.
		/// </summary>
		/// <param name="AnimationKey">The key of the animation to return.</param>
		/// <returns>The animation associated with AnimationKey.</returns>
		public static Animation GetAnimation(string AnimationKey)
		{
			return animations[AnimationKey];
		}

		/// <summary>
		/// Calculates the origin of a texture as a Vector2 based on a given origin key.
		/// </summary>
		/// <param name="Texture">The key of the texture to calculate an origin for.</param>
		/// <param name="Origin">The origin key to calculate an origin position from.</param>
		/// <returns>The Vector2 defining the desired origin on Texture.</returns>
		public static Vector2 CalculateOrigin(Texture2D Texture, BaseOriginKeys[] Origin)
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
			if (Origin.Contains(BaseOriginKeys.centerX))
			{
				origin.X = Texture.Width / 2;
			}
			if (Origin.Contains(BaseOriginKeys.centerY))
			{
				origin.Y = Texture.Height / 2;
			}

			return origin;
		}

		/// <summary>
		/// Calculates the origin of a source rectangle as a Vector2 based on a given origin key.
		/// </summary>
		/// <param name="SourceRect">The source rectangle to calculate an origin for.</param>
		/// <param name="Origin">The origin key to calculate an origin position from.</param>
		/// <returns>The Vector2 defining the desired origin on SourceRect.</returns>
		public static Vector2 CalculateOrigin(Rectangle SourceRect, BaseOriginKeys[] Origin)
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
			if (Origin.Contains(BaseOriginKeys.centerX))
			{
				origin.X = SourceRect.Width / 2;
			}
			if (Origin.Contains(BaseOriginKeys.centerY))
			{
				origin.Y = SourceRect.Height / 2;
			}

			return origin;
		}

		/// <summary>
		/// Calculates the origin of a Vector2 as a Vector2 based on a given origin key.
		/// </summary>
		/// <param name="SourceRect">The source rectangle to calculate an origin for.</param>
		/// <param name="Origin">The origin key to calculate an origin position from.</param>
		/// <returns>The Vector2 defining the desired origin on SourceRect.</returns>
		public static Vector2 CalculateOrigin(Vector2 SourceVector, BaseOriginKeys[] Origin)
		{
			Vector2 origin = Vector2.Zero;

			if (Origin.Contains(BaseOriginKeys.top))
			{
				origin.Y = 0;
			}
			if (Origin.Contains(BaseOriginKeys.bottom))
			{
				origin.Y = SourceVector.Y;
			}
			if (Origin.Contains(BaseOriginKeys.left))
			{
				origin.X = 0;
			}
			if (Origin.Contains(BaseOriginKeys.right))
			{
				origin.X = SourceVector.X;
			}
			if (Origin.Contains(BaseOriginKeys.center))
			{
				origin.X = SourceVector.X / 2;
				origin.Y = SourceVector.Y / 2;
			}
			if (Origin.Contains(BaseOriginKeys.centerX))
			{
				origin.X = SourceVector.X / 2;
			}
			if (Origin.Contains(BaseOriginKeys.centerY))
			{
				origin.Y = SourceVector.Y / 2;
			}

			return origin;
		}

		/// <summary>
		/// Creates a render target of a given size and associates it with a string key.
		/// </summary>
		/// <param name="Key">The key to assicate with the new target.</param>
		/// <param name="Width">The width in pixels of the new target.</param>
		/// <param name="Height">The height in pixels of the new target.</param>
		public static void CreateRenderTarget(String Key, int Width, int Height)
		{
			RenderTarget2D temp = new RenderTarget2D(gd, Width, Height);
			renderTargets.Add(Key, temp);
		}

		/// <summary>
		/// Begins a SpriteBatch process to enable drawing of sprites.
		/// </summary>
		public static void BeginRender(BlendState BS = null)
		{
			spriteBatch.Begin(SpriteSortMode.FrontToBack, BS, SamplerState.PointClamp, null, null);
		}

		/// <summary>
		/// Ends the current SpriteBatch process.
		/// </summary>
		public static void EndRender()
		{
			spriteBatch.End();
		}

		public static void ClearCurrentTarget(Color ClearColor)
		{
			gd.Clear(ClearColor);
		}

		/// <summary>
		/// Draws a quad utilizing the entire specified texture.
		/// </summary>
		/// <param name="TextureKey">The associated key of the texture to draw.</param>
		/// <param name="Position">The position to draw at.</param>
		/// <param name="Scale">The scale to draw at, 1 being natural size.</param>
		/// <param name="Depth">The Z depth to draw at.</param>
		/// <param name="CColor">The color filter to apply.</param>
		/// <param name="Origin">The origin to draw with.</param>
		public static void DrawQuad(string TextureKey, Vector2 Position, Vector2 Scale, float Depth, Color CColor, BaseOriginKeys[] Origin, bool Mirror = false, bool Scroll = true)
		{
			Texture2D texture = TextureKey == null ? nextTexture : GetTexture(TextureKey);
			Vector2 origin = CalculateOrigin(texture, Origin);

			spriteBatch.Draw(
				texture, 
				position: Position - (Scroll ? scroll : Vector2.Zero), 
				sourceRectangle: new Rectangle(0, 0, texture.Width, texture.Height), 
				color: CColor, 
				scale: Scale, 
				origin: origin,
				depth: Depth,
				effect: Mirror ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
		}

		/// <summary>
		/// Draws a quad utilizing the portion of the specified texture specified.
		/// </summary>
		/// <param name="TextureKey">The associated key of the texture to draw from.</param>
		/// <param name="Position">The position to draw at.</param>
		/// <param name="SourceRect">The source rectangle of the portion of the texture to draw from.</param>
		/// <param name="Scale">The scale to draw at, 1 being natural size.</param>
		/// <param name="Depth">The Z depth to draw at.</param>
		/// <param name="CColor">The color filter to apply.</param>
		/// <param name="Origin">The origin to draw with.</param>
		public static void DrawQuad(string TextureKey, Vector2 Position, Rectangle SourceRect, Vector2 Scale, float Depth, Color CColor, BaseOriginKeys[] Origin, bool Mirror = false, bool Scroll = true)
		{
			Texture2D texture = TextureKey == null ? nextTexture : GetTexture(TextureKey);
			Vector2 origin = CalculateOrigin(SourceRect, Origin);

			spriteBatch.Draw(
				texture,
				position: Position - (Scroll ? scroll : Vector2.Zero),
				sourceRectangle: SourceRect,
				color: CColor,
				scale: Scale,
				origin: origin,
				depth: Depth,
				effect: Mirror ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
		}

		/// <summary>
		/// Draws a string
		/// </summary>
		/// /// <param name="SString">String to draw.</param>
		/// <param name="FontKey">Key associated with the desired font.</param>
		/// <param name="Position">Position to draw the string at.</param>
		/// <param name="Depth">The Z depth to draw at.</param>
		/// <param name="CColor">Color to draw the font with.</param>
		/// <param name="Origin">Origin to draw the string at.</param>
		/// <param name="Scroll">Enables or disables scrolling for this string.</param>
		public static void DrawString(string SString, string FontKey, Vector2 Position, float Depth, Color CColor, BaseOriginKeys[] Origin, bool Scroll = true)
		{
			SpriteFont font = GetFont(FontKey);
			Vector2 stringSize = font.MeasureString(SString);
			spriteBatch.DrawString(font, SString, Position, CColor, 0.0f, CalculateOrigin(stringSize, Origin), 1.0f, SpriteEffects.None, Depth);
		}
	}
}

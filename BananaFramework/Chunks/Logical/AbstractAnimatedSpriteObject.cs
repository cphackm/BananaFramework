using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using BananaFramework.Managers;
using BananaFramework.Chunks.Support;

namespace BananaFramework.Chunks.Logical
{
	/// <summary>
	/// The AbstractAnimatedSpriteObject class represents an object that is equipped to 
	/// automatically update and draw an animated sprite from loaded Animation class data.
	/// </summary>
	public abstract class AbstractAnimatedSpriteObject : AbstractSpriteObject
	{
		protected Animation currentAnimation;
		protected int currentFrame;
		protected float frameTimer;
		protected float timerTarget;
		protected bool isPaused;
		protected bool isFinished;
		protected bool isMirrored;
		protected float speedScale;

		protected bool hasShadow;
		protected float shadowDepth;
		protected float shadowOpacity;

		/// <summary>
		/// Constructs a new AbstractAnimatedSpriteObject with default values.
		/// </summary>
		public AbstractAnimatedSpriteObject()
		{
			currentAnimation = null;
			currentFrame = 0;
			frameTimer = 0.0f;
			timerTarget = 0.0f;
			isPaused = false;
			isMirrored = false;
			speedScale = 1.0f;
			hasShadow = false;
			shadowDepth = 0.0f;
			shadowOpacity = 0.2f;
			isFinished = false;
		}

		protected void SetAnimation(string Key, bool Restart)
		{
			Animation newAnim = RenderManager.GetAnimation(Key);
			if (Restart || newAnim != currentAnimation)
			{
				currentAnimation = newAnim;
				currentFrame = 0;
				frameTimer = 0.0f;
				timerTarget = currentAnimation.frameSpeeds[0] * 10.0f;
				isFinished = false;
			}
		}

		/// <summary>
		/// Updates the state of the currently playing animation, if applicable. This method should 
		/// be called at the beginning of the Update() method in any deriving classes that choose 
		/// to override it.
		/// </summary>
		public override void Update()
		{
			if (null != currentAnimation && !isPaused)
			{
				float dt = GameManager.GetGlobalState<float>("DT");

				frameTimer += 1000.0f * dt * speedScale;
				if (frameTimer >= timerTarget)
				{
					if (currentFrame == (currentAnimation.frameCount - 1) && !currentAnimation.isLooping)
					{
						isFinished = true;
					}

					if (!isFinished)
					{
						frameTimer -= timerTarget;
						currentFrame = (currentFrame == (currentAnimation.frameCount - 1)) ? 0 : (currentFrame + 1);
						timerTarget = currentAnimation.frameSpeeds[currentFrame] * 10.0f;
					}
				}
			}
		}

		/// <summary>
		/// Renders the currently playing animation, if applicable. This method should be called at
		/// the beginning of the Render() method in any deriving classes that choose to override it.
		/// </summary>
		public override void Render()
		{
			// Only render if there is an animation set
			if (currentAnimation != null)
			{
				Texture2D tex = RenderManager.GetTexture(currentAnimation.sheetKey);
				Rectangle sourceRect = new Rectangle((currentFrame * currentAnimation.frameWidth) % tex.Width, (int)Math.Floor(currentFrame * currentAnimation.frameWidth / (double)tex.Width) * currentAnimation.frameHeight, currentAnimation.frameWidth, currentAnimation.frameHeight);
				RenderManager.DrawQuad(currentAnimation.sheetKey, position, sourceRect, scale, depth, color, origin, isMirrored);

				if (hasShadow)
				{
					for (int i = 0; i < currentAnimation.frameHeight; i++)
					{
						Rectangle shadowRect = new Rectangle(currentFrame * currentAnimation.frameWidth, currentAnimation.frameHeight - i - 1, currentAnimation.frameWidth, 1);
						RenderManager.DrawQuad(currentAnimation.sheetKey, position - new Vector2(i + 1), shadowRect, scale, shadowDepth, Color.Black * shadowOpacity, origin);
					}
				}
			}
		}
	}
}

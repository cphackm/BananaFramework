using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

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
		protected float speedScale;
		protected bool isStarted; // This is only false when the animation is first created

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
			speedScale = 1.0f;
			isStarted = false;
			hasShadow = false;
			shadowDepth = 0.0f;
			shadowOpacity = 0.2f;
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

				if (!isStarted)
				{
					isStarted = true;
					timerTarget = currentAnimation.frameSpeeds[currentFrame];
				}

				frameTimer += 1000.0f * dt * speedScale;
				if (frameTimer >= timerTarget)
				{
					currentFrame = (currentFrame == (currentAnimation.frameCount - 1)) ? 0 : (currentFrame + 1);
					timerTarget = currentAnimation.frameSpeeds[currentFrame];
					frameTimer = 0.0f;
				}
			}
		}

		/// <summary>
		/// Renders the currently playing animation, if applicable. This method should be called at
		/// the beginning of the Render() method in any deriving classes that choose to override it.
		/// </summary>
		public override void Render()
		{
			Rectangle sourceRect = new Rectangle(currentFrame * currentAnimation.frameWidth, 0, currentAnimation.frameWidth, currentAnimation.frameHeight);
			RenderManager.DrawQuad(currentAnimation.sheetKey, position, sourceRect, scale, depth, color, origin);

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

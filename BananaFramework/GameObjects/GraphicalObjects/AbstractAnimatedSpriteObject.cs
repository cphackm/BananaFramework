using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

using BananaFramework.GameManagers;
using BananaFramework.GameChunks;

namespace BananaFramework.GameObjects.GraphicalObjects
{
	public abstract class AbstractAnimatedSpriteObject : AbstractSpriteObject
	{
		protected Animation currentAnimation;
		protected int currentFrame;
		protected float frameTimer;
		protected float timerTarget;
		protected bool isPaused;
		protected float speedScale;
		protected bool isStarted; // This is only false when the animation is first created

		public AbstractAnimatedSpriteObject()
		{
			currentAnimation = null;
			currentFrame = 0;
			frameTimer = 0.0f;
			timerTarget = 0.0f;
			isPaused = false;
			speedScale = 1.0f;
			isStarted = false;
		}

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

		public override void Render()
		{
			Rectangle sourceRect = new Rectangle(currentFrame * currentAnimation.frameWidth, 0, currentAnimation.frameWidth, currentAnimation.frameHeight);
			RenderManager.DrawQuad(currentAnimation.sheetKey, position, sourceRect, scale, origin);
		}
	}
}

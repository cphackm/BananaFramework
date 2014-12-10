using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BananaFramework.Managers;
using BananaFramework.Chunks.Logical;

namespace BananaFramework.Chunks.Support
{
	public class Timer : AbstractGameObject
	{
		protected float progress;
		public float Progress
		{
			get
			{
				return progress;
			}
		}
		public float NormalizedProgress
		{
			get
			{
				return progress / length;
			}
		}
		protected float length;
		public float Length
		{
			get
			{
				return length;
			}
		}
		protected bool isRunning;
		public bool IsRunning
		{
			get
			{
				return isRunning;
			}
		}
		protected bool isFinished;
		public bool IsFinished
		{
			get
			{
				return isFinished;
			}
		}

		public Timer(float Length, bool Start)
		{
			progress = 0.0f;
			length = Length;
			isRunning = Start;
			isFinished = false;
		}

		public override void Update()
		{
			// isFinished should only be true on the frame that the timer finishes
			isFinished = false;
			float dt = GameManager.GetGlobalState<float>("DT");
			if (isRunning)
			{
				progress += dt;
			}
			if (progress >= length)
			{
				progress = length;
				isFinished = true;
			}
		}

		public override void Render()
		{
		}
	}
}

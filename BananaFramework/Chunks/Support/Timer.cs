using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BananaFramework.Managers;
using BananaFramework.Chunks.Logical;

namespace BananaFramework.Chunks.Support
{
	/// <summary>
	/// The Timer class represents a device to measure elapsed time over measured intervals.
	/// </summary>
	public class Timer : AbstractGameObject
	{
		protected float progress;
		/// <summary>
		/// Progress toward completion in seconds
		/// </summary>
		public float Progress
		{
			get
			{
				return progress;
			}
		}
		/// <summary>
		/// Progress toward completion as a value between 0 and 1
		/// </summary>
		public float NormalizedProgress
		{
			get
			{
				return progress / length;
			}
		}
		protected float length;
		/// <summary>
		/// The length of the timer in seconds
		/// </summary>
		public float Length
		{
			get
			{
				return length;
			}
		}
		protected bool isRunning;
		/// <summary>
		/// True if the timer is currently running
		/// </summary>
		public bool IsRunning
		{
			get
			{
				return isRunning;
			}
		}
		protected bool isFinished;
		/// <summary>
		/// True on the frame when the timer finishes, false otherwise
		/// </summary>
		public bool IsFinished
		{
			get
			{
				return isFinished;
			}
		}

		/// <summary>
		/// Constructs a new timer with designated length and start state
		/// </summary>
		/// <param name="Length">Length of the timer in seconds</param>
		/// <param name="Start">Indicates whether or not the timer is started at creation</param>
		public Timer(float Length, bool Start)
		{
			progress = 0.0f;
			length = Length;
			isRunning = Start;
			isFinished = false;
		}

		/// <summary>
		/// Updates the timer
		/// </summary>
		public override void Update()
		{
			// isFinished should only be true on the frame that the timer finishes
			isFinished = false;
			float dt = GameManager.GetGlobalState<float>("DT");
			if (isRunning)
			{
				progress += dt;

				if (progress >= length)
				{
					progress = length;
					isFinished = true;
					isRunning = false;
				}
			}
		}

		public void Restart(bool Start)
		{
			progress = 0.0f;
			isRunning = Start;
			isFinished = false;
		}

		public void Start()
		{
			isRunning = true;
		}

		public void Stop()
		{
			isRunning = false;
		}

		public override void Render() { }
	}
}

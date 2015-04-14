using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BananaFramework.Chunks.Structural;
using Microsoft.Xna.Framework;

namespace BananaFramework.Managers
{
	public class LevelManager
	{
		private static Dictionary<string, AbstractGameLevel> levels;
		private static AbstractGameLevel currentLevel;
		public static AbstractGameLevel CurrentLevel
		{
			get
			{
				return currentLevel;
			}
		}

		public static void Initialize()
		{
			levels = new Dictionary<string,AbstractGameLevel>();
		}

		public static void SetLevel(string Key)
		{
			if (levels.ContainsKey(Key))
			{
				currentLevel = levels[Key];
			}
		}

		public static void RegisterLevel(string Key, AbstractGameLevel Level)
		{
			levels.Add(Key, Level);
		}

		public static void UpdateCurrentLevel()
		{
			if (currentLevel != null)
			{
				currentLevel.Update();
			}
		}

		public static void RenderCurrentLevel()
		{
			if (currentLevel != null)
			{
				currentLevel.Render();
			}
		}
	}
}

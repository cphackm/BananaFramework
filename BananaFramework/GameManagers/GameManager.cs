using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace BananaFramework.GameManagers
{
	public class GameManager
	{
		private static Dictionary<string, object> globalStates;
		private static int nextObjectId;

		public static void Initialize()
		{
			globalStates = new Dictionary<string, object>();
			nextObjectId = 0;
		}

		public static void SetGlobalState(string Key, object Value)
		{
			if (globalStates.ContainsKey(Key))
			{
				globalStates[Key] = Value;
			}
			else
			{
				globalStates.Add(Key, Value);
			}
		}

		public static T GetGlobalState<T>(string Key)
		{
			return (T)globalStates[Key];
		}

		public static int GetNextObjectId()
		{
			return nextObjectId++;
		}

		public static void Update(GameTime gameTime)
		{
			CalculateDeltaTime(gameTime);
		}

		private static void CalculateDeltaTime(GameTime gameTime)
		{
			float elapsedTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
			float dt = elapsedTime / 1000.0f;
			SetGlobalState("DT", dt);
		}
	}
}

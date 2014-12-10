using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace BananaFramework.Managers
{
	/// <summary>
	/// The GameManager provides help with global state control for the entire game.
	/// </summary>
	public class GameManager
	{
		private static Dictionary<string, object> globalStates;
		private static int nextObjectId;

		/// <summary>
		/// Initializes the GameManager to prepare it for tracking of global states.
		/// </summary>
		public static void Initialize()
		{
			globalStates = new Dictionary<string, object>();
			nextObjectId = 0;
		}

		/// <summary>
		/// Adds -- or sets, if it already exists -- a global state as a sort of key value pair.
		/// </summary>
		/// <param name="Key">The key for the associated value.</param>
		/// <param name="Value">The object to store associated with the given key.</param>
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

		/// <summary>
		/// Returns the global state object associated with the given key as an object of type T.
		/// </summary>
		/// <typeparam name="T">The type to cast the retrieved value to upon return.</typeparam>
		/// <param name="Key">The key of the associated value to return.</param>
		/// <returns></returns>
		public static T GetGlobalState<T>(string Key)
		{
			return (T)globalStates[Key];
		}

		/// <summary>
		/// Returns a unique object ID. IDs are distributed sequentially starting from 1.
		/// </summary>
		/// <returns></returns>
		public static int GetNextObjectId()
		{
			return nextObjectId++;
		}

		/// <summary>
		/// Updates parts of the game that involve global state or widely used values.
		/// </summary>
		/// <param name="gameTime"></param>
		public static void Update(GameTime gameTime)
		{
			CalculateDeltaTime(gameTime);
		}

		/// <summary>
		/// Calcultes the delta time for this frame from the current game time.
		/// </summary>
		/// <param name="gameTime">A game time object to determine time elapsed since the last frame.</param>
		private static void CalculateDeltaTime(GameTime gameTime)
		{
			float elapsedTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
			float dt = elapsedTime / 1000.0f;
			SetGlobalState("DT", dt);
		}
	}
}

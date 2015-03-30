using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

using BananaFramework.Managers;
using BananaFramework.Chunks.Logical;
using BananaFramework.Chunks.Support;

namespace BananaFramework.Chunks.Structural
{
	/// <summary>
	/// The AbstractGameLevel class represents a game level that stores and updates information 
	/// associated with the level.
	/// </summary>
	public abstract class AbstractGameLevel
	{
		protected Dictionary<string, object> levelStates;

		protected int width, height;
		protected Vector2 scroll;
		protected List<Timer> timers;
		protected List<AbstractGameObject> objects;
		protected Dictionary<Type, List<AbstractGameObject>> objectsByType;

		/// <summary>
		/// Constructs a new AbstractGameLevel object while setting up various object control 
		/// structures. This constructor should be called at construction time by deriving classes.
		/// </summary>
		public AbstractGameLevel()
		{
			InitializeBase();
		}

		/// <summary>
		/// Used to initialize all properties of the base AbstractGameClass.
		/// </summary>
		protected void InitializeBase()
		{
			levelStates = new Dictionary<string, object>();
			timers = new List<Timer>();
			objects = new List<AbstractGameObject>();
			objectsByType = new Dictionary<Type, List<AbstractGameObject>>();
		}

		/// <summary>
		/// Adds -- or sets, if it already exists -- a state as a sort of key value pair.
		/// </summary>
		/// <param name="Key">The key for the associated value.</param>
		/// <param name="Value">The object to store associated with the given key.</param>
		public void SetLevelState(string Key, object Value)
		{
			if (levelStates.ContainsKey(Key))
			{
				levelStates[Key] = Value;
			}
			else
			{
				levelStates.Add(Key, Value);
			}
		}

		/// <summary>
		/// Returns the level state object associated with the given key as an object of type T.
		/// </summary>
		/// <typeparam name="T">The type to cast the retrieved value to upon return.</typeparam>
		/// <param name="Key">The key of the associated value to return.</param>
		/// <returns></returns>
		public T GetLevelState<T>(string Key)
		{
			return (T)levelStates[Key];
		}

		/// <summary>
		/// Registers a timer object with the level.
		/// </summary>
		/// <param name="RTimer">The timer object to register.</param>
		public virtual void RegisterTimer(Timer RTimer)
		{
			timers.Add(RTimer);
		}

		/// <summary>
		/// Registers a game object with the level for updating and later retrieval.
		/// </summary>
		/// <param name="GameObject">The game object to register.</param>
		/// <param name="DeepType">If true, the associated AbstractGameObject will be added 
		/// separately to lists for each base type of the given AbstractGameObject above the 
		/// types provided by the framework.</param>
		/// <returns>True if no errors occur.</returns>
		public virtual bool RegisterGameObject(AbstractGameObject GameObject, bool DeepType = false)
		{
			GameObject.id = Managers.GameManager.GetNextObjectId();
			GameObject.level = this;
			objects.Add(GameObject);

			Type objType = GameObject.GetType();

			if (DeepType)
			{
				while (objType != typeof(AbstractGameObject) && objType != typeof(AbstractAnimatedSpriteObject) && objType != typeof(AbstractSpriteObject))
				{
					if (!objectsByType.ContainsKey(objType))
					{
						objectsByType.Add(objType, new List<AbstractGameObject>());
					}

					objectsByType[objType].Add(GameObject);

					objType = objType.BaseType;
				}
			}
			else
			{
				if (!objectsByType.ContainsKey(objType))
				{
					objectsByType.Add(objType, new List<AbstractGameObject>());
				}

				objectsByType[objType].Add(GameObject);
			}

			return true;
		}

		public virtual void UnregisterGameObject(AbstractGameObject GameObject)
		{
			objects.Remove(GameObject);

			Type objType = GameObject.GetType();

			while (objType != typeof(AbstractGameObject) && objType != typeof(AbstractAnimatedSpriteObject) && objType != typeof(AbstractSpriteObject))
			{
				if (objectsByType.ContainsKey(objType))
				{
					objectsByType[objType].Remove(GameObject);
				}

				objType = objType.BaseType;
			}
		}

		/// <summary>
		/// Searches for an AbstractGameObject with ID registered with this level.
		/// </summary>
		/// <typeparam name="T">The top type of the AbstractGameObject to be retrieved.</typeparam>
		/// <param name="Id">The ID to search for.</param>
		/// <returns>The object associated with the given ID, cast to type T.</returns>
		public virtual T GetGameObjectById<T>(int Id) where T : AbstractGameObject
		{
			return (T)objects.Find(ago => ago.id == Id );
		}

		/// <summary>
		/// Searches for the list of all registered objects of type T.
		/// </summary>
		/// <typeparam name="T">The type of the list of objects to retrieve.</typeparam>
		/// <returns>A list of objects of type T.</returns>
		public virtual List<T> GetGameObjectsByType<T>() where T : AbstractGameObject
		{
			return objectsByType.ContainsKey(typeof(T)) ? objectsByType[typeof(T)].Cast<T>().ToList() : new List<T>();
		}

		/// <summary>
		/// Updates the level, including associated timers and AbstractGameObjects.
		/// </summary>
		public virtual void Update()
		{
			foreach (Timer t in timers)
			{
				t.Update();
			}
			List<AbstractGameObject> destroyedObjects = new List<AbstractGameObject>();
			foreach (AbstractGameObject ago in objects)
			{
				ago.Update();

				if (ago.IsDestroyed)
				{
					destroyedObjects.Add(ago);
				}
			}
			foreach (AbstractGameObject ago in destroyedObjects)
			{
				UnregisterGameObject(ago);
			}
		}

		/// <summary>
		/// Renders the level, including associated AbstractGameObjects.
		/// </summary>
		public virtual void Render()
		{
			foreach (AbstractGameObject ago in objects)
			{
				ago.Render();
			}
		}
	}
}

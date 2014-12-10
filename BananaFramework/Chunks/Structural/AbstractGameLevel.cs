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
	public abstract class AbstractGameLevel
	{
		protected Dictionary<string, object> levelStates;

		protected List<Timer> timers;
		protected List<AbstractGameObject> objects;
		protected Dictionary<Type, List<AbstractGameObject>> objectsByType;

		public AbstractGameLevel()
		{
			levelStates = new Dictionary<string, object>();
			timers = new List<Timer>();
			objects = new List<AbstractGameObject>();
			objectsByType = new Dictionary<Type, List<AbstractGameObject>>();
		}

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

		public T GetLevelState<T>(string Key)
		{
			return (T)levelStates[Key];
		}

		public virtual void RegisterTimer(Timer RTimer)
		{
			timers.Add(RTimer);
		}

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

		public virtual T GetGameObjectById<T>(int Id) where T : AbstractGameObject
		{
			return (T)objects.Find(ago => ago.id == Id );
		}

		public virtual List<T> GetGameObjectsByType<T>() where T : AbstractGameObject
		{
			return objectsByType.ContainsKey(typeof(T)) ? objectsByType[typeof(T)].Cast<T>().ToList() : new List<T>();
		}

		public virtual void Update()
		{
			foreach (Timer t in timers)
			{
				t.Update();
			}
			foreach (AbstractGameObject ago in objects)
			{
				ago.Update();
			}
		}

		public virtual void Render()
		{
			foreach (AbstractGameObject ago in objects)
			{
				ago.Render();
			}
		}
	}
}

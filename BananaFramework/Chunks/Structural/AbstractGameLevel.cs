using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

using BananaFramework.Managers;
using BananaFramework.Chunks.Logical;

namespace BananaFramework.Chunks.Structural
{
	public abstract class AbstractGameLevel
	{
		protected Dictionary<string, object> levelStates;

		protected List<AbstractGameObject> objects;
		protected Dictionary<Type, List<AbstractGameObject>> objectsByType;

		public AbstractGameLevel()
		{
			levelStates = new Dictionary<string, object>();
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

		public virtual bool RegisterGameObject(AbstractGameObject GameObject)
		{
			GameObject.id = Managers.GameManager.GetNextObjectId();
			GameObject.level = this;
			objects.Add(GameObject);

			Type objType = GameObject.GetType();

			if (!objectsByType.ContainsKey(objType))
			{
				objectsByType.Add(objType, new List<AbstractGameObject>());
			}

			objectsByType[objType].Add(GameObject);

			return true;
		}

		public virtual T GetGameObjectById<T>(int Id) where T : AbstractGameObject
		{
			return (T)objects.Find(ago => ago.id == Id );
		}

		public virtual List<T> GetGameObjectsByType<T>() where T : AbstractGameObject
		{
			return objectsByType[typeof(T)].Cast<T>().ToList();
		}

		public virtual void Update()
		{
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

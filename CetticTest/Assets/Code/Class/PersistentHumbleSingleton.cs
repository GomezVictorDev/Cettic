using UnityEngine;
using System;


	/// <summary>
	/// basicamente es un singleton clasico pero buscara y  destruira, en el awake, cualquier otro componente anterior del mismo tipo
	/// </summary>
	public class PersistentHumbleSingleton<T> : MonoBehaviour	where T : Component
	{
		protected static T _instance;
		public float InitializationTime;

		/// <summary>
		/// Patron de diseño Singleton
		/// </summary>
		/// <value>The instance.</value>
		public static T Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = FindObjectOfType<T> ();
					if (_instance == null)
					{
						GameObject obj = new GameObject ();
						obj.hideFlags = HideFlags.HideAndDontSave;
						_instance = obj.AddComponent<T> ();
					}
				}
				return _instance;
			}
		}

		/// <summary>
		/// Durante el awake, Buscara si hay otra copia de este objeto en la escena. Si Lo hay lo destruirá
		/// </summary>
		protected virtual void Awake ()
		{
			InitializationTime=Time.time;

			DontDestroyOnLoad (this.gameObject);
			// Buscamos por objetos excistentes del mismo tipo
			T[] check = FindObjectsOfType<T>();
			foreach (T searched in check)
			{
				if (searched!=this)
				{
					
					// si encontramos cualquier otro objeto del mismo tipo que este y es más antiguo en realación a este lo destruimos. 
					if (searched.GetComponent<PersistentHumbleSingleton<T>>().InitializationTime<InitializationTime)
					{
						Destroy (searched.gameObject);
					}
				}
			}

			if (_instance == null)
			{
				_instance = this as T;
			}
		}
	}


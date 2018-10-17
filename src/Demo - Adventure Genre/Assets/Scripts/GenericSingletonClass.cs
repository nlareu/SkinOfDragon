using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSingletonClass<T> : MonoBehaviour where T : Component{

	//Simplemente una clase singleton generica.
	//Cada vez que tengo que crear un singleton, heredo de esta para
	//evitar repetir codigo

	private static T instance;
	public static T Instance {
		get {
			if (instance == null) {
				instance = FindObjectOfType<T> ();
				if (instance == null) {
					GameObject obj = new GameObject ();
					obj.name = typeof(T).Name;
					instance = obj.AddComponent<T> ();
				}
			}
			return instance;
		}
	}

	public virtual void Awake () {
		if (instance == null) {
			instance = this as T;
			DontDestroyOnLoad (this.gameObject);
		} else {
			Destroy (gameObject);
		}
	}
}

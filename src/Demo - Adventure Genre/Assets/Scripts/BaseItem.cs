using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem {

	//Un template generico para items. Se pueden agregar mas variables o clases de items mas complejas que hereden los datos basicos de esta

	public string id, name;
	public Sprite icon;
	public int cant;

	public virtual void Use(){
	}
}

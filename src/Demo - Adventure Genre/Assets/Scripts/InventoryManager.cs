using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : GenericSingletonClass<InventoryManager> {

	//Este es un singleton que funciona como manager de inventario.
	//Nunca hagan al player un singleton, hagan los managers como game objects separados


	//Creo un array de imagenes serializable que hace referencia a los slots del canvas
	[SerializeField]
	Image[] canvasSlots;

	BaseItem[] inventoryArray;

	JSONLoader jsonDataBase;

	void Awake () {
		base.Awake ();
	}

	void Start () {
		jsonDataBase = new JSONLoader ();

		//Hago que el tamaño del inventario sea igual a la cantidad de slots que puse en el canvas
		inventoryArray = new BaseItem[canvasSlots.Length];
	}

	//Busco dentro de la base de datos de items un item con esta id y lo agrego al inventario
	public void AddItem(string id){
		if (SearchForItem (id) <= inventoryArray.Length) {
			inventoryArray [SearchForItem (id)].cant++;
			PutCanvasIcons (SearchForItem (id));
		} else {
			for (int i = 0; i < inventoryArray.Length; i++) {
				if (inventoryArray [i] != null) {
				} else {
					inventoryArray [i] = jsonDataBase.GetItem (id);
					inventoryArray [i].cant = 1;
					PutCanvasIcons (i);
					return;
				}
			}
		}
	}

	//Busco dentro del inventario un item con esta id y lo quito
	public void RemoveItem(string id){
		for (int i = 0; i < inventoryArray.Length; i++) {
			if (inventoryArray [i] != null) {
				if (inventoryArray [i].id == id) {
					inventoryArray [i].cant--;
					if (inventoryArray [i].cant >= 0) {
						inventoryArray [i] = null;
						PutCanvasIcons (i);
					}
				}
			}
		}
	}


	//Busco si tengo cierto item dentro del inventario y devuelvo el indice donde este
	public int SearchForItem(string id){
		for (int i = 0; i < inventoryArray.Length; i++) {
			if (inventoryArray [i] != null) {
				if (inventoryArray [i].id == id) {
					return i;
				}
			}
		}
		return (inventoryArray.Length + 1);
	}

	//Hago que los iconos de los items que tengo en el inventario aparezcan en los slots del canvas
	void PutCanvasIcons(int index){
		if (inventoryArray [index] != null) {
			canvasSlots [index].sprite = inventoryArray [index].icon;
			canvasSlots [index].GetComponentInChildren<Text> ().text = inventoryArray [index].cant.ToString ();
			canvasSlots [index].gameObject.SetActive (true);

		} else {
			canvasSlots [index].gameObject.SetActive (false);
		}
	}
}

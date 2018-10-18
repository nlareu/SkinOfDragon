using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JSONLoader {

	//Un loader para armar una base de datos a partir de archivos json.
	//Es una muy mala practica cargar datos desde el inspector, les recomiendo
	//acostumbrarse a cargar items, dialogos y cosas por el estilo desde json
	//u otra clase de archivos de texto

	string jsonItem;

	//Desde aca creo un array que va a contener todos los items del juego a modo de base de datos
	BaseItem[] itemArray;

	public JSONLoader () {

		//Indico desde que archivo de texto se van a cargar los datos
		jsonItem = LoadText ("/Scripts/Items.json");
		itemArray = LoadJsonItem (jsonItem);
	}

	string LoadText (string _path){
		string text = string.Empty;
		text = File.ReadAllText (Application.dataPath + _path);
		return text;
	}

	//--------------------Getters--------------------

	//Este es un getter que uso para buscar un item dentro de la base de datos creada y retornarlo
	public BaseItem GetItem (string id) {
		for (int i = 0; i < itemArray.Length; i++) {
			if (itemArray [i].id == id) {
				return itemArray [i];
			}
		}
		return null;
	}

	//--------------------Carga de items--------------------

	BaseItem[] LoadJsonItem (string _json) {
		JSONObject jsonObj = new JSONObject (_json);
		JSONObject jsonObjAux = jsonObj;

		BaseItem[] baseArray;
		ConsumableItems[] consumablesArray;

		int itemCount = 0, consumablesCount = 0;

		//Me fijo cuantos items hay dentro del archivo json
		if (jsonObjAux.HasField ("Items")) {
			itemCount = jsonObjAux.GetField ("Items").Count;
		}

		if (jsonObjAux.HasField ("Consumables")) {
			consumablesCount = jsonObjAux.GetField ("Consumables").Count;
		}

		baseArray = new BaseItem[itemCount];
		consumablesArray = new ConsumableItems[consumablesCount];
		itemArray = new BaseItem[itemCount + consumablesCount];

		//Por cada item del array voy cargando los items del json dato por dato
		for (int i = 0; i < itemCount; i++) {
			jsonObjAux = jsonObj.GetField ("Items");
			baseArray [i] = new BaseItem ();
			baseArray [i].id = jsonObjAux [i].GetField ("id").str;
			baseArray [i].name = (jsonObjAux [i].HasField ("name")) ? jsonObjAux [i].GetField ("name").str : string.Empty;
			baseArray [i].icon = Resources.Load<Sprite> ("Art/" + jsonObjAux [i].GetField ("icon").str);
		}

		//Carga los items consumibles
		for (int i = 0; i < itemCount; i++) {
			jsonObjAux = jsonObj.GetField ("Consumables");
			consumablesArray [i] = new ConsumableItems ();
			consumablesArray [i].id = jsonObjAux [i].GetField ("id").str;
			consumablesArray [i].name = (jsonObjAux [i].HasField ("name")) ? jsonObjAux [i].GetField ("name").str : string.Empty;
			consumablesArray [i].icon = Resources.Load<Sprite> ("Art/" + jsonObjAux [i].GetField ("icon").str);
		}


		//Hace un merge de todos los array de items en uno solo y lo devuelve
		int index = 0;
		for (int i = 0; i < baseArray.Length; i++) {
			itemArray [index] = baseArray [i];
			index++;
		}

		for (int i = 0; i < consumablesArray.Length; i++) {
			itemArray [index] = consumablesArray [i];
			index++;
		}

		return itemArray;
	}
}

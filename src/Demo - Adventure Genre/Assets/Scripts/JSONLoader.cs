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

		int itemCount = 0;

		//Me fijo cuantos items hay dentro del archivo json
		if (jsonObjAux.HasField ("Items")) {
			itemCount = jsonObjAux.GetField ("Items").Count;
		}

		itemArray = new BaseItem[itemCount];

		//Por cada item del array voy cargando los items del json dato por dato
		for (int i = 0; i < itemCount; i++) {
			jsonObjAux = jsonObj.GetField ("Items");
			itemArray [i] = new BaseItem ();
			itemArray [i].id = jsonObjAux [i].GetField ("id").str;
			itemArray [i].name = (jsonObjAux [i].HasField ("name")) ? jsonObjAux [i].GetField ("name").str : string.Empty;
			itemArray [i].icon = Resources.Load<Sprite> ("Art/" + jsonObjAux [i].GetField ("icon").str);
		}

		return itemArray;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoListener : MonoBehaviour {

	//Esta clase generica es para objetos que estan esperando al Player
	//Para que el jugador interactue con estos, debera llevar un objeto iditemRequerido en su inventario
	//Seria bueno que las acciones para todos los objetos sean con un Animador y un trigger "Interactuar"

	public string iditemRequerido;

	public void OnCollisionEnter2D(Collision2D col){

		bool hasItem = InventoryManager.Instance.hasItem(iditemRequerido);

		//Al enfrentar el objeto, verificar si lo tengo en el inventario
		if(hasItem){
			//Interactuar
			GetComponent<Animator>().SetTrigger("Interactuar");
			//Remover item del inventario
			InventoryManager.Instance.RemoveItem(iditemRequerido);

		}

	}


}

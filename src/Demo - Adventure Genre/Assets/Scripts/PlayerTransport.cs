using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransport : BaseInteraction {

	//Script ultra basico para objetos que transporten al personaje.
	//Obviamente va a hacer falta retocarlo para agregar eventos y animaciones una vez tengan claro el diseño del juego

	//Hay que interactuar con el objeto o funciona por contacto?
	[SerializeField]
	bool isInteractable = true;

	//La escena a cargar
	[SerializeField]
	string sceneToLoad;


	//En caso de no ser interactuable, al tocar el objeto transporta al personaje
	void OnCollisionEnter2D(){
		if (!isInteractable && sceneToLoad != string.Empty) {
			StartTransport ();
		}
	}


	//En caso de ser interactuable al pulsar el boton transporta al personaje
	public override void StartInteraction(){
		if (isInteractable && sceneToLoad != string.Empty) {
			StartTransport ();
		}
	}

	//Llama al manager de escenas para iniciar el transporte
	void StartTransport(){
		SceneManager.Instance.LoadScene (sceneToLoad);
	}
}

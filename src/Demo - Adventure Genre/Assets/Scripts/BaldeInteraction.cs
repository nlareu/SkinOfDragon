﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaldeInteraction : BaseInteraction {

	public string idObjeto;

	//Este script esta medio harcodeado y hecho a modo de ejemplo. Normalmente haria algo mas generico que se pueda
	//usar con varios tipos de objetos, pero para ejemplificar el funcionamiento lo hice exculsivo para el balde

	public override void StartInteraction(){
		InventoryManager.Instance.AddItem (idObjeto);
		this.gameObject.SetActive (false);
	}
}

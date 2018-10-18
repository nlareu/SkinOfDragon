using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableItems : BaseItem {

	//Template para items consumibles.
	//Hace falta aclarar que hacen los items consumibles.
	//¿Devuelven vida? ¿Aumentan algun estatus? ¿El personaje jugador tiene alguna clase de stats?
	//Aclaren esos detalles por favor.

	/*
	 * Aca ejemplifico como funcionaria si fueran como los consumibles de un juego rpg
	 * public baseStats itemStats;
	 * 
	 * public struct baseStats{
	 * 	int hp, str, dex;
	 * }
	 */

	//Esta funcion se llamaria desde el inventory manager, pero primero organicense y diseñen bien lo que quieren
	public override void Use(/*Aca se tendria que pasar una clase que de acceso a los stats del personaje*/){
		/*
		 * Para afectar al personaje se tendria que crear una clase que almacene los stats de este
		 * character.characterStats.hp += itemStats.hp;
		 * character.characterStats.str += itemStats.str;
		 * character.characterStats.dex += itemStats.dex;
		 */
		InventoryManager.Instance.RemoveItem (id);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	//Creo las variables serializables para la velocidad del personaje caminando, corriendo y el radio de interaccion
	[SerializeField]
	float speed = 5f, runSpeed = 10f, interactionRange = 1f;

	//Un game object sin render que se coloca frente al personaje para interactuar con objetos o personajes
	//CUIDADO al cambiar las animaciones del personaje porque las modifique para que la posicion
	//de este objeto cambie segun la animacion.
	//Si se cambian las animaciones, hay que volver a modificar la posicion manualmente o no va a haber interaccion
	[SerializeField]
	GameObject interactionPos;

	//La intensidad de movimiento horizontal y vertical
	float h, v;

	//La direccion de movimiento actual y la direccion de la ultima vez que se movio
	Vector2 direction, lastDirection;

	Rigidbody2D playerRB;

	Animator playerAnim;

	public GameObject miniMapa;

	void Start () {
		playerRB = GetComponent<Rigidbody2D> ();
		playerAnim = GetComponent<Animator> ();
	}

	void Update () {
		SetSpeed ();
		PlayerInteraction ();
	}

	void FixedUpdate(){
		Movement ();

		MiniMapa ();

	}

	void MiniMapa(){
	
		//Prender o apagar con la letra M
		if(Input.GetKeyDown(KeyCode.M)){

			if(miniMapa.activeSelf)
				miniMapa.SetActive (false);
			else
				miniMapa.SetActive (true);

		}
	
	}

	void SetSpeed(){
		direction = Vector2.zero;
		h = Input.GetAxisRaw ("Horizontal");
		v = Input.GetAxisRaw ("Vertical");

		//Elimino los valores verticales en el movimiento del personaje
		if (h != 0) {
			v = 0;
			lastDirection = new Vector2 (h, v);
			direction += (Vector2.right * h);
		} else if (v != 0) {
			h = 0;
			lastDirection = new Vector2 (h, v);
			direction += (Vector2.up * v);
		}

		//Seteo la direccion del ultimo movimiento para que el player siempre mire hacia donde se movio por ultima vez
		playerAnim.SetFloat ("lastH", lastDirection.x);
		playerAnim.SetFloat ("lastV", lastDirection.y);
	}

	void Movement(){
		//Chequea si el player se esta moviendo o no para activar su animacion
		if (direction != Vector2.zero) {
			playerAnim.SetBool ("isMoving", true);
			playerAnim.SetFloat ("h", direction.x);
			playerAnim.SetFloat ("v", direction.y);
		} else {
			playerAnim.SetBool ("isMoving", false);
		}

		//Si el player esta presionando el boton de correr, corre, si no simplemente camina
		if (Input.GetButton ("Run")) {
			playerRB.velocity = direction * runSpeed;
		} else {
			playerRB.velocity = direction * speed;
		}
	}

	//Acciona el script de interaccion de cualquier objeto con el que interactue
	void PlayerInteraction(){
		if (Input.GetButtonDown ("Interaction")) {
			Collider2D interact = Physics2D.OverlapCircle (interactionPos.transform.position, interactionRange);
			if (interact.GetComponent<BaseInteraction> () != null) {
				interact.GetComponent<BaseInteraction> ().StartInteraction ();
			}
		}
	}

	//Permite ver en la escena el radio del game object de interaccion
	void OnDrawGizmosSelected(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (interactionPos.transform.position, interactionRange);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BloquearDireccionesMovimiento{
    public bool bloquearY, bloquearX, bloquearArriba, bloquearAbajo, bloquearDerecha, bloquearIzquierda;

}

public class ObjetoEmpujable : MonoBehaviour {

    public BloquearDireccionesMovimiento bloqueos;
    private Rigidbody2D rb;
    private RigidbodyConstraints2D freezeRb;

	// Use this for initialization
	void Start () {
    
        rb = GetComponent<Rigidbody2D>();

        //Bloquear los ejes del Rigidbody
        if (bloqueos.bloquearX)
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        if (bloqueos.bloquearY)
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;

	}
	
	// Update is called once per frame
	void Update () {
    
        rb.constraints = freezeRb;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject player = collision.gameObject;

        if (player.tag == "Player")
        {
            freezeRb = RigidbodyConstraints2D.FreezeRotation;

            //Si el player esta empujando hacia la derecha
            if (player.GetComponent<PlayerController>().UltimaDireccion().x != 0)
            {
                if (bloqueos.bloquearDerecha)
                {
                    if (collision.gameObject.transform.position.x < transform.position.x)
                        freezeRb = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                    //else
                        //freezeRb = RigidbodyConstraints2D.FreezeRotation;
                }
                //Si el player  empujando hacia la izquierda
                if (bloqueos.bloquearIzquierda)
                {
                    if (collision.gameObject.transform.position.x > transform.position.x)
                        freezeRb = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                    //else
                        //freezeRb = RigidbodyConstraints2D.FreezeRotation;
                }
            }
            else
            {

                //Si el player esta empujando hacia arriba
                if (bloqueos.bloquearArriba)
                {
                    if (collision.gameObject.transform.position.y > transform.position.y)
                        freezeRb = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                    //else
                        //freezeRb = RigidbodyConstraints2D.FreezeRotation;
                }
                //Si el player esta empujando hacia abajo
                if (bloqueos.bloquearAbajo)
                {
                    if (collision.gameObject.transform.position.y < transform.position.y)
                        freezeRb = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                    //else
                        //freezeRb = RigidbodyConstraints2D.FreezeRotation;
                }
            }
        }
    }
}

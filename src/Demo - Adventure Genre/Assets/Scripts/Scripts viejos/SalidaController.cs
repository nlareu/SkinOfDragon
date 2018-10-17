using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalidaController : MonoBehaviour {

    public GameObject GameOverPoster;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        this.GameOverPoster.SetActive(true);
    }
}

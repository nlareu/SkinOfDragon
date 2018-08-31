using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    private void Awake()
    {
        this.tag = "Checkpoint";
    }

    //private void OnTriggerEnter2D(Collision2D col)
    //{
    //    CollisionsManager.ResolveCollision(col.gameObject, this.gameObject, col);
    //}
}

using UnityEngine;

public class AvatarCheckpointCollision : GameCollision
{
    public override string Collider1Tag { get { return "Player"; } }
    public override string Collider2Tag { get { return "Checkpoint"; } }

    public override void Resolve(GameObject collider1, GameObject collider2, Collider2D collider)
    {
        Transform cp = collider2.GetComponent<Transform>();

        AppController.SetLastCheckpoint(cp);
    }
    public override void Resolve(GameObject collider1, GameObject collider2, Collision2D collision)
    {
    }
}
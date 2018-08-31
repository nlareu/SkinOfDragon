using UnityEngine;

public class AvatarEnemyCollision : GameCollision
{
    public override string Collider1Tag { get { return "Player"; } }
    public override string Collider2Tag { get { return "Enemy"; } }

    public override void Resolve(GameObject collider1, GameObject collider2, Collider2D collider)
    {
    }
    public override void Resolve(GameObject collider1, GameObject collider2, Collision2D collision)
    {
        AvatarController ac = collider1.GetComponent<AvatarController>();

        var cp = AppController.GetLastCheckpoint();

        ac.transform.position = new Vector2(cp.position.x, ac.transform.position.y);
    }
}
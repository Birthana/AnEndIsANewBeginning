using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Transform respawnPoint;

    private void Start()
    {
        Explode.OnDeath += MoveBackToSpawn;
    }

    public void MoveBackToSpawn()
    {
        transform.position = respawnPoint.position;
    }
}

using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform spawnPoint;
    private Transform player;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        Explode.OnDeath += MoveToSpawnPoint;
    }
    private void MoveToSpawnPoint()
    {
        player.position = spawnPoint.position;
    }
}

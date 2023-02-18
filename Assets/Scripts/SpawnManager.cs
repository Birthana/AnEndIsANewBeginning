using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<Transform> spawnPoint = new List<Transform>();
    private List<Transform> playerTransforms = new List<Transform>();

    void Start()
    {
        FindAllPlayerTransforms();
        Explode.OnDeath += MoveToSpawnPoint;
    }

    private void FindAllPlayerTransforms()
    {
        PlayerMovement[] players = FindObjectsOfType<PlayerMovement>();
        foreach (var player in players)
        {
            playerTransforms.Add(player.transform);
        }
    }

    private void MoveToSpawnPoint()
    {
        for (int i = 0; i < playerTransforms.Count; i++)
        {
            playerTransforms[i].position = spawnPoint[i].position;
        }
    }
}

using System;
using UnityEngine;

[RequireComponent(typeof(Explode))]
public class Respawn : MonoBehaviour
{
    public event Action OnRespawn;
    public Transform respawnPoint;

    private void Start()
    {
        var explode = GetComponent<Explode>();
        explode.OnDeath += MoveBackToSpawn;
    }

    public void MoveBackToSpawn()
    {
        transform.position = respawnPoint.position;
        OnRespawn?.Invoke();
    }

    public void SetRespawn(Transform newRespawnPoint)
    {
        respawnPoint = newRespawnPoint;
        OnRespawn?.Invoke();
    }
}

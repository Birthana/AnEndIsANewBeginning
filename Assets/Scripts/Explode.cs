using System;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public static event Action OnDeath;
    public Transform spawnPoint;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private float RADIUS;

    void Update()
    {
        if (PlayerPressesSpace())
        {
            DestroyObjectsInRange();
        }
    }

    private bool PlayerPressesSpace()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    private void DestroyObjectsInRange()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, RADIUS, Vector2.zero, 0, obstacleLayer);
        foreach (var hit in hits)
        {
            Destroy(hit.transform.gameObject);
        }
        OnDeath?.Invoke();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, RADIUS);
    }
}

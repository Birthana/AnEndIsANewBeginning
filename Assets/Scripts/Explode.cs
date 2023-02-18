using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class Explode : MonoBehaviour
{
    public static event Action OnDeath;
    public float chargeTime;
    [SerializeField] private TextMeshPro countdownText;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private float RADIUS;
    private bool IsActivated = false;

    private void Start()
    {
        IsActivated = false;
    }

    void Update()
    {
        if (PlayerPressesSpace() && !IsActivated)
        {
            StartCoroutine(Activating());
        }
    }

    private bool PlayerPressesSpace()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    IEnumerator Activating()
    {
        IsActivated = true;
        Debug.Log($"Charging.");
        yield return new WaitForSeconds(chargeTime);
        DestroyObjectsInRange();
        IsActivated = false;
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

using System;
using System.Collections;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public event Action OnDeath;
    public event Action OnReset;
    public event Action OnExplode;
    public event Action<float> OnCharging;
    public float chargeTime;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private LayerMask dandelionLayer;
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
        yield return StartCoroutine(TickingDown());
        yield return StartCoroutine(Exploding());
        SpawnNewDandelions();
        DestroyObjectsInRange();
        IsActivated = false;
    }

    IEnumerator TickingDown()
    {
        float cooldown = chargeTime;
        while (cooldown > 0)
        {
            OnCharging?.Invoke(cooldown);
            float increment = Time.deltaTime;
            yield return new WaitForSeconds(increment);
            cooldown -= increment;
        }
        OnReset?.Invoke();
    }

    IEnumerator Exploding()
    {
        OnExplode?.Invoke();
        yield return new WaitForSeconds(0.1f);
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

    private void SpawnNewDandelions()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, RADIUS, Vector2.zero, 0, dandelionLayer);
        foreach (var hit in hits)
        {
            var dandelion = hit.transform.GetComponent<Dandelion>();
            dandelion.Reveal();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, RADIUS);
    }
}

using UnityEngine;

public class Dandelion : MonoBehaviour
{
    public GameObject playerPrefab;
    public bool IsMain = false;
    private bool IsRevealed = false;

    private void Start()
    {
        if (!IsMain)
        {
            return;
        }
        Reveal();
    }

    public void Reveal()
    {
        if (IsRevealed)
        {
            return;
        }
        var spriteRender = GetComponent<SpriteRenderer>();
        spriteRender.enabled = true;
        SpawnNewSpore();
        IsRevealed = true;
    }

    private void SpawnNewSpore()
    {
        var newSpore = Instantiate(playerPrefab);
        newSpore.transform.position = transform.position;
        var respawn = newSpore.GetComponent<Respawn>();
        respawn.SetRespawn(transform);
    }
}

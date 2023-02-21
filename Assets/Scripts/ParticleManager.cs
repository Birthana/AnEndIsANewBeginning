using UnityEngine;

[RequireComponent(typeof(Explode))]
public class ParticleManager : MonoBehaviour
{
    public ParticleSystem particlePrefab;

    private void Start()
    {
        var explode = GetComponent<Explode>();
        explode.OnExplode += PlayParticle;
    }

    public void PlayParticle()
    {
        var ps = Instantiate(particlePrefab);
        ps.transform.position = transform.position;
        ps.Play();
        Destroy(ps.gameObject, 1.0f);
    }
}

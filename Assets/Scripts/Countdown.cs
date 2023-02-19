using UnityEngine;
using TMPro;

[RequireComponent(typeof(Explode))]
[RequireComponent(typeof(Respawn))]
public class Countdown : MonoBehaviour
{
    public TextMeshPro countdownText;

    private void Start()
    {
        ResetDisplay();
        var explode = GetComponent<Explode>();
        explode.OnCharging += Display;
        var respawn = GetComponent<Respawn>();
        respawn.OnRespawn += ResetDisplay;
    }

    public void ResetDisplay()
    {
        countdownText.text = $"";
    }

    public void Display(float countdown)
    {
        countdownText.text = string.Format("{0:0.00}", countdown);
    }
}

using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    public GameObject screen;
    public TextMeshPro timeText;

    private void Start()
    {
        screen.SetActive(false);
    }

    public void SetTime(int time) { timeText.text = $"{time}"; }

    public void Reveal()
    {
        screen.SetActive(true);
        Time.timeScale = 0.0f;
    }
}

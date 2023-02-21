using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshPro timeText;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        StartClock();
    }

    private void Update()
    {
        time += Time.deltaTime;
        timeText.text = $"{(int)time}";
    }

    public void StartClock() { time = 0.0f; }

    public int GetTime() { return (int)time; }
}

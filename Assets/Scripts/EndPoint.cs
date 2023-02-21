using UnityEngine;

[RequireComponent(typeof(GameOver))]
public class EndPoint : MonoBehaviour
{
    public AudioSource victorySound;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        victorySound.Play();
        var timer = FindObjectOfType<Timer>();
        var gameOverScreen = GetComponent<GameOver>();
        gameOverScreen.SetTime(timer.GetTime());
        gameOverScreen.Reveal();
    }
}

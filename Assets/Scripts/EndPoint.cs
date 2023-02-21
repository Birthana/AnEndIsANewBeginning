using UnityEngine;

[RequireComponent(typeof(GameOver))]
public class EndPoint : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        var timer = FindObjectOfType<Timer>();
        var gameOverScreen = GetComponent<GameOver>();
        gameOverScreen.SetTime(timer.GetTime());
        gameOverScreen.Reveal();
    }
}

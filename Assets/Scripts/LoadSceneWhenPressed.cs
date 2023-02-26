using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;


public class LoadSceneWhenPressed : MonoBehaviour
{
    public enum SceneName { MainMenu, Puzzle1 }
    public SceneName scene;

    void Update()
    {
        if (PlayerPressesButton())
        {
            LoadScene();
        }
    }

    private void LoadScene()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(scene.ToString());
    }

    private bool PlayerPressesLeftMouse() { return Input.GetMouseButtonDown(0); }

    private bool PlayerPressesButton()
    {
        if (PlayerPressesLeftMouse())
        {
            RaycastHit2D hit = GetHit();
            if (hit)
            {
                if (IsHitItself(hit))
                {
                    return true;
                }
            }
        }
        return false;
    }

    private RaycastHit2D GetHit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics2D.Raycast(ray.origin, Vector2.zero);
    }

    private bool IsHitItself(RaycastHit2D hit)
    {
        return hit.collider.Equals(transform.GetComponent<BoxCollider2D>());
    }
}

using UnityEditor;
using UnityEngine;

public class SpawnAtLocation : EditorWindow
{
    private GameObject prefab;

    [MenuItem("Tools/Spawn Prefab At Location")]
    public static void ShowWindow()
    {
        GetWindow(typeof(SpawnAtLocation));
    }

    private void OnEnable() { SceneView.duringSceneGui += Spawn; }
    private void OnDisable() { SceneView.duringSceneGui -= Spawn; }

    private void OnGUI()
    {
        GUILayout.Label("Spawn Prefab At Location", EditorStyles.boldLabel);

        prefab = EditorGUILayout.ObjectField("Prefab To Spawn", prefab, typeof(GameObject), false) as GameObject;
    }

    private void Spawn(SceneView sceneView)
    {
        if (prefab == null)
        {
            Debug.LogError("No Prefab to spawn.");
            return;
        }

        if (Event.current.type == EventType.MouseDown)
        {
            var newObject = Instantiate(prefab);
            Vector2 mousePositionInEditor = Event.current.mousePosition;
            Ray ray = HandleUtility.GUIPointToWorldRay(mousePositionInEditor);
            Vector2 mousePositionInScene = ray.origin;
            newObject.transform.position = Clamp(mousePositionInScene);
        }
    }

    private Vector2 Clamp(Vector2 mousePosition)
    {
        return new Vector2(Mathf.Round(mousePosition.x * 2)/2, Mathf.Round(mousePosition.y * 2) / 2);
    }
}

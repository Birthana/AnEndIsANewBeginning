using UnityEditor;
using UnityEngine;

public class SpawnAtLocation : EditorWindow
{
    private int selectedOption;
    private GameObject[] prefabs;

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

        prefabs = Resources.LoadAll<GameObject>("Prefabs");
        string[] options = new string[prefabs.Length];
        for (int i = 0; i < prefabs.Length; i++)
        {
            options[i] = prefabs[i].name;
        }

        selectedOption = EditorGUILayout.Popup("Prefab To Spawn", selectedOption, options);

        EditorGUILayout.Space();

        EditorGUILayout.HelpBox("Click in the scene, and the selected prefab with spawn in the scene in the box.", MessageType.None);
    }

    private void Spawn(SceneView sceneView)
    {
        Vector2 mousePositionInEditor = Event.current.mousePosition;
        Ray ray = HandleUtility.GUIPointToWorldRay(mousePositionInEditor);
        Vector2 mousePositionInScene = ray.origin;

        Handles.DrawWireCube(Clamp(mousePositionInScene), new Vector3(1.0f, 1.0f));
        if (Event.current.type == EventType.MouseMove)
        {
            SceneView.RepaintAll();
        }

        if (Event.current.type == EventType.MouseDown)
        {
            var newObject = Instantiate(prefabs[selectedOption]);
            newObject.transform.position = Clamp(mousePositionInScene);
        }
    }

    private Vector2 Clamp(Vector2 mousePosition)
    {
        return new Vector2(Mathf.Round(mousePosition.x * 2)/2, Mathf.Round(mousePosition.y * 2) / 2);
    }
}

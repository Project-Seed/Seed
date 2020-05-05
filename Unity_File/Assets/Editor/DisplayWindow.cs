using UnityEditor;
using UnityEngine;

class DisplayWindow : EditorWindow
{
    [MenuItem("Window/Display")]
    static void Init()
    {
        DisplayWindow window = (DisplayWindow)GetWindow(typeof(DisplayWindow));
        window.Show();
    }

    Transform mousePos;
    GameObject obj;
    GameObject origin;
    float density = 10.0f;

    private void OnGUI()
    {
            //배치할 오브젝트 선택
        origin = (GameObject)EditorGUILayout.ObjectField("Select Origin", origin, typeof(GameObject), true);
        obj = (GameObject)EditorGUILayout.ObjectField("Select Object", obj, typeof(GameObject), true);
        density = GUILayout.HorizontalSlider(density, 1f, 100f);

        if (obj != null)
        {
            if (GUILayout.Button("Create!"))
                Create(origin,obj, density);
        }
        else
            System.Console.WriteLine("OBJ NULL");
        
        if (GUILayout.Button("Clear"))
        {
            obj = null;
        }
    }

    void Create(GameObject origin, GameObject obj, float density)
    {
        for (int i = 0; i < density; ++i)
            Instantiate(obj, origin.transform.position, Quaternion.Euler(0, 0, 0), origin.transform);
        Debug.Log("Create done");

    }

}


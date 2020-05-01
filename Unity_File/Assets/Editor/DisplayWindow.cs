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
    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        {
            //배치할 오브젝트 선택
            obj = (GameObject)EditorGUILayout.ObjectField("Select Object", obj, typeof(GameObject), true);

            if (Input.GetMouseButtonDown(0))
            {
                mousePos.position = Input.mousePosition;
                if (obj != null)
                    Instantiate(obj, mousePos);
                else
                    System.Console.WriteLine("OBJ NULL");
            }

            if (GUILayout.Button("Clear"))
            {
                obj = null;
            }
        }
        GUILayout.EndHorizontal();

    }
}


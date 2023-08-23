#if (UNITY_EDITOR)
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class SceneSwitcher : MonoBehaviour
{
    [MenuItem("Scenes/Main Menu")]
    public static void OpenScene1()
    {
        EditorSceneManager.OpenScene("Assets/Developer/Scenes/MainMenu.unity");
    } 
    [MenuItem("Scenes/Game Play")]
    public static void OpenScene2()
    {
        EditorSceneManager.OpenScene("Assets/Developer/Scenes/GamePlayScreen.unity");
    }
}
#endif

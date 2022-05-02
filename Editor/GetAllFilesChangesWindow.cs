using UnityEditor;
using UnityEngine;

namespace UnityEditor.Threads.GitPlugin
{
    public class GetAllFilesChangesWindow : EditorWindow
    {
        private static string _title = "GetAllFilesChanges";
        private static string[] _outputs;

        [MenuItem("Git/GetAllFilesChanges")]
        public static void Init()
        {
            _outputs = GITMethods.GetAllFilesChanges();
            GetAllFilesChangesWindow window =
                (GetAllFilesChangesWindow) EditorWindow.GetWindow(typeof(GetAllFilesChangesWindow));
            window.Show();
        }

        void OnGUI()
        {
            GUILayout.Label(_title, EditorStyles.boldLabel);
            GUILayout.Space(10);
            foreach (var output in _outputs)
            {
                GUILayout.Label(output);
            }
        }
    }
}

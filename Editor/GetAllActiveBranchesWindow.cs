using UnityEditor;
using UnityEngine;

namespace UnityEditor.Threads.GitPlugin
{
    public class GetAllActiveBranchesWindow : EditorWindow
    {
        private static string _title = "GetAllActiveBranches";
        private static string[] _outputs;

        [MenuItem("Assets/Git/GetAllActiveBranches")]
        public static void Init()
        {
            _outputs = GITMethods.GetAllActiveBranches();
            GetAllActiveBranchesWindow window =
                (GetAllActiveBranchesWindow) EditorWindow.GetWindow(typeof(GetAllActiveBranchesWindow));
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

using UnityEditor;
using UnityEngine;

namespace UnityEditor.Threads.GitPlugin
{
    public class GetAllRemoteBranchesWindow : EditorWindow
    {
        private static string _title = "GetAllRemoteBranches";
        private static string[] _outputs;

        [MenuItem("Assets/Git/GetAllRemoteBranches")]
        public static void Init()
        {
            _outputs = GITMethods.GetAllRemoteBranches();
            GetAllRemoteBranchesWindow window =
                (GetAllRemoteBranchesWindow) EditorWindow.GetWindow(typeof(GetAllRemoteBranchesWindow));
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

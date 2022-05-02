using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace UnityEditor.Threads.GitPlugin
{
    public class GetAllActiveBranchesWithThisFileChanged : EditorWindow
    {
        private static string _title = "GetAllActiveBranchesWithThisFileChanged";
        private static string[] _outputs;

        [MenuItem("Assets/Git/GetAllActiveBranchesWithThisFileChanged")]
        public static void Init()
        {
            var selectedObjects = Selection.objects.Select((Object o) => (o.name)).ToArray();
            var changedFiles = GITMethods.GetAllFilesChanges();
            List<string> shared = new List<string>();
            foreach (var selected in selectedObjects)
            {
                foreach (var changed in changedFiles)
                {
                    if (Regex.IsMatch(changed,selected,RegexOptions.IgnoreCase))
                    {
                        shared.Add(changed);
                    }
                }
            }
            _outputs = shared.ToArray();
            GetAllActiveBranchesWithThisFileChanged window =
                (GetAllActiveBranchesWithThisFileChanged) EditorWindow.GetWindow(typeof(GetAllActiveBranchesWithThisFileChanged));
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

using System;
using UnityEngine;


namespace UnityEditor.Threads.GitPlugin
{
    [CreateAssetMenu(menuName = "Create GitSettings", fileName = "GitSettings", order = 0)]
    public class GitSettings : ScriptableObject
    {
        public string mainBranch = "";
        public string[] activeBranches = new string[] { };
        public string activeFolder = "";
    }
}

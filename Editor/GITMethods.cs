using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

namespace UnityEditor.Threads.GitPlugin
{
    public static class GITMethods
    {
        private static GitSettings _settings =>
            (GitSettings) AssetDatabase.LoadAssetAtPath("Packages/com.threads.gitplugin/GitSettings.asset", typeof(GitSettings));

        private static string _path = "";

        private static void SetPath()
        {
            if (_path != "")
            {
                return;
            }

            var path = Application.dataPath;
            while (!path.EndsWith("/"))
            {
                path = path.Remove(path.Length - 1);
            }

            _path = path.Remove(path.Length - 1);
        }

        public static string[] GetAllRemoteBranches()
        {
            SetPath();
            var startInfo = new ProcessStartInfo
            {
                WorkingDirectory = _path,
                WindowStyle = ProcessWindowStyle.Normal,
                FileName = "git.exe",
                Arguments = "branch -r",
                UseShellExecute = false,
                RedirectStandardOutput = true
            };

            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();

            string output = process.StandardOutput.ReadToEnd();
            output = output.Replace(" ", "");
            if (output.EndsWith("\n"))
            {
                output = output.Substring(0, output.Length - 1);
            }

            output = output.Replace("\n", Environment.NewLine);

            string[] branches = output.Split(
                new string[] {Environment.NewLine},
                StringSplitOptions.None
            );
            return branches;
        }

        public static string[] GetAllFilesChanges()
        {
            string baseBranch = "HEAD";
            string[] otherBranches = _settings.activeBranches;

            SetPath();
            List<string> allFiles = new List<string>();
            foreach (var currentBranch in otherBranches)
            {
                var startInfo = new System.Diagnostics.ProcessStartInfo
                {
                    WorkingDirectory = _path,
                    WindowStyle = ProcessWindowStyle.Normal,
                    FileName = "git.exe",
                    Arguments = "diff --name-only " + baseBranch + " " + currentBranch,
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                };

                Process process = new Process();
                process.StartInfo = startInfo;
                process.Start();

                string output = process.StandardOutput.ReadToEnd();
                if (output.Length == 0)
                {
                    continue;
                }

                output = output.Replace(" ", "");
                if (output.EndsWith("\n"))
                {
                    output = output.Substring(0, output.Length - 1);
                }

                output = output.Replace("\n", Environment.NewLine);

                var files = output.Split(
                    new string[] {Environment.NewLine},
                    StringSplitOptions.None
                ).ToList();
                for(int i =0; i < files.Count;i++)
                {
                    if (files[i].EndsWith(".meta"))
                    {
                        continue;
                    }
                    int start =  files[i].LastIndexOf('/') + 1;
                    if (start < 0)
                    {
                        start = 0;
                    }
                    int count = files[i].LastIndexOf('.') - start;
                    if (count < 0)
                    {
                        count = files[i].Length - start;
                    }
                    allFiles.Add(files[i].Substring(start, count));
                }
            }
            return allFiles.Distinct().ToArray();
        }

        [MenuItem("Git/UpdatedActiveBranches")]
        public static void SetActiveBranches()
        {
            _settings.activeBranches = GetAllActiveBranches();
        }

        public static string[] GetAllActiveBranches()
        {
            var allBranches = GetAllRemoteBranches();
            return allBranches.Where((string branch) => branch.StartsWith(_settings.activeFolder)).ToArray();
        }
    }
}

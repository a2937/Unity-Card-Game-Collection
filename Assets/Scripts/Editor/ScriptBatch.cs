//Enhancement of Unity`s sample scriptbatch class
using UnityEditor;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public static class ScriptBatch
{
    [MenuItem("MyTools/Windows Build With Postprocess")]
    public static void BuildGame()
    {
        // Get filename.
        string path = EditorUtility.SaveFolderPanel("Choose Location of Built Game", "", "");

        //Get all scene files in the project loaded
        DirectoryInfo info = new DirectoryInfo(Application.dataPath);
        FileInfo[] fileInfo = info.GetFiles("*.unity*", SearchOption.AllDirectories);

        List<string> levels = new List<string>();
        foreach (FileInfo file in fileInfo)
        {
            if (!file.Extension.Equals(".meta"))
            {
                int startIndex = file.DirectoryName.IndexOf("Assets");
                int endIndex = file.DirectoryName.Length - startIndex;
                levels.Add((file.DirectoryName.Substring(startIndex, endIndex).Replace('\\', '/')
                   + '/' + file.Name));
            }
        }
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = levels.ToArray();
        buildPlayerOptions.locationPathName = path + "/BuiltGame.exe";
        buildPlayerOptions.target = BuildTarget.StandaloneWindows;
        buildPlayerOptions.options = BuildOptions.None;
        // Build player.
        BuildPipeline.BuildPlayer(buildPlayerOptions);

        UnityEngine.Debug.Log(Application.dataPath.Replace('/', '\\') + "\\Templates\\Readme.txt");
        //UnityEngine.Debug.Log(File.Exists(Application.dataPath.Replace('/', '\\') + "\\ Templates \\ Readme.txt"));
        // Copy a file from the project folder to the build folder, alongside the built game.
        if (File.Exists(Application.dataPath.Replace('/', '\\') + "\\Templates\\Readme.txt"))
        {
            UnityEngine.Debug.Log("Readme.txt found");
            FileUtil.CopyFileOrDirectory("Assets/Templates/Readme.txt", path + "/Readme.txt");
        }
        // Run the game (Process class from System.Diagnostics).
        Process proc = new Process();
        proc.StartInfo.FileName = path + "/BuiltGame.exe";
        proc.Start();
    }
}
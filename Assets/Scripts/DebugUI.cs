using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Schema;
using UnityEngine.SceneManagement;

public class DebugUI : MonoBehaviour
{
    private List<string> _scenes;
    private bool _enabled;

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this);

        _scenes = new List<string>();

        string folderName = Application.dataPath + "/Scenes";
        var dirInfo = new DirectoryInfo(folderName);
        var allFileInfos = dirInfo.GetFiles("*.unity", SearchOption.AllDirectories);
        foreach (var fileInfo in allFileInfos)
        {
            string name = SeneNameFromPath(@fileInfo.FullName);

            if (Application.CanStreamedLevelBeLoaded(name))
            {
                Debug.Log("Found a scene file: " + name);
                _scenes.Add(name);
            }
        }

    }

    private string SeneNameFromPath(string value)
    {
        value = value.Substring(value.LastIndexOf('\\') + 1);
        return value.Split('.')[0];
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _enabled = !_enabled;
        }

    }

    void OnGUI()
    {
        if (_enabled)
        {
            DrawButtons();
        }
    }

    private void DrawButtons()
    {
        foreach (string s in _scenes)
        {
            if (GUILayout.Button(s))
            {
                SceneManager.LoadScene(s);
            }
        }
    }
}

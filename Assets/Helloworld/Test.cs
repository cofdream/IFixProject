using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DA.DownloadTool;
using IFix.Core;
using System.IO;

public class Test : MonoBehaviour
{
    public Button button;
    public Text content;
    public Button loadNetIFix;
    public Button restLoadPatch;

    public static Text Content { get; set; }
    void Start()
    {
        Content = content;

        loadNetIFix.onClick.AddListener(() =>
        {
            Download.AsyncDownLoadInGitHub("Assembly-CSharp.patch.bytes", (handler) =>
            {
                var data = handler.data;
                string path;
                if (Application.platform == RuntimePlatform.Android)
                {
                    path = Application.streamingAssetsPath + "/Assembly-CSharp.patch.bytes";
                }
                else
                {
                    path = Path.Combine(Application.dataPath, "../Temp/Assembly-CSharp.patch.bytes");
                }

                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                var fs = new FileStream(path, FileMode.CreateNew);
                fs.Write(data, 0, data.Length);
                fs.Close();
            });

        });

        restLoadPatch.onClick.AddListener(() =>
        {
            string path;
            if (Application.platform == RuntimePlatform.Android)
            {
                path = Application.streamingAssetsPath + "/Assembly-CSharp.patch.bytes";
            }
            else
            {
                path = Path.Combine(@"E:\Git\IFixProject\Assembly-CSharp.patch.bytes");
            }
            if (File.Exists(path))
            {
                PatchManager.Load(path);
            }
            else
            {
                Log("加载本地Patch失败，文件不存在");
            }

        });



        button.onClick.AddListener(() =>
        {
            TestFunc();
        });
    }

    //[IFix.Patch]
    public void TestFunc()
    {
        Log("v0.0.1");
    }

    public static void Log(string log)
    {
        Content.text += Time.time + "  " + log + "\n";
    }
}

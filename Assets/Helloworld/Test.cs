using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DA.DownloadTool;
using IFix.Core;
using System.IO;

public class Test : Root
{
    public Button button;
    public Text content;

    public static Text Content { get; set; }
    void Start()
    {
        Content = content;

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

            PatchManager.Load(path);

            fs.Close();
        });

        Log("Application.dataPath; " + Application.dataPath);
        Log("Application.streamingAssetsPath; " + Application.streamingAssetsPath);
        Log("Application.persistentDataPath; " + Application.persistentDataPath);
        Log("Application.temporaryCachePath; " + Application.temporaryCachePath);



        button.onClick.AddListener(() =>
        {
            TestFunc();
            new AA().Func();
        });
    }

    [IFix.Patch]
    public void TestFunc()
    {
        Log("Self TestFunc v0.0.1 and");
    }

    public static void Log(string log)
    {
        Content.text += Time.time + "  " + log + "\n";
    }
}

/*
 
git rm Assembly-CSharp.patch.bytes -r -f
git commit -m"remove"
git push




git add Assembly-CSharp.patch.bytes
git commit -m"add"
git push


 */

public class Root : MonoBehaviour
{

}

public class AA
{
    [IFix.Patch]
    public void Func()
    {
        return;
        Test.Log("AA.Func");

        var t = Type.GetType("Test3");

        if (t != null)
        {
            (Activator.CreateInstance(t) as ABC).Func();
        }
        t = Type.GetType("Test4");

        if (t != null)
        {
            (Activator.CreateInstance(t) as ABC).Func();
        }
    }
}


public interface ABC
{
    void Func();

}

[IFix.Interpret]

public class Test3 : ABC
{
    [IFix.Patch]
    public void Func()
    {
        Test.Log("fix Test3");
        Func2();
        Func3();
        Func5();
        Func4();
    }
    [IFix.Interpret]
    public void Func2()
    {
        Test.Log("fix Test3 Func2");
    }
    [IFix.Interpret]
    public void Func3()
    {
        Test.Log("fix Test3 Func3");
    }
    [IFix.Interpret]
    public void Func4()
    {
        Test.Log("fix Test3 Func4");
    }
    [IFix.Interpret]
    public void Func5()
    {
        Test.Log("fix Test3 Func5");
    }
}

[IFix.Interpret]

public class Test4 : ABC
{
    [IFix.Patch]
    public void Func()
    {
        Test.Log("fix Test3");
        Func2();
        Func3();
        Func5();
        Func4();
    }
    [IFix.Interpret]
    public void Func2()
    {
        Test.Log("fix Test3 Func2");
    }
    [IFix.Interpret]
    public void Func3()
    {
        Test.Log("fix Test3 Func3");
    }
    [IFix.Interpret]
    public void Func4()
    {
        Test.Log("fix Test3 Func4");
    }
    [IFix.Interpret]
    public void Func5()
    {
        Test.Log("fix Test3 Func5");
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace DA.DownloadTool
{
    public static class Download
    {
        private static DownloadMono downloadMono;

        public static string GithubPathRoot = "https://github.com/cofdream/Resources/";

        public static Coroutine Run(IEnumerator routine)
        {
            if (downloadMono == null)
            {
                var go = new GameObject(typeof(DownloadMono).Name);
                UnityEngine.Object.DontDestroyOnLoad(go);
                downloadMono = go.AddComponent<DownloadMono>();
            }
            return downloadMono.StartCoroutine(routine);
        }

        public static IEnumerator AsyncDownLoad(string url, Action<DownloadHandler> callback, DownloadHandler downloadHandler = null)
        {
            UnityWebRequest webRequest = new UnityWebRequest(url);

            if (downloadHandler == null) downloadHandler = new DownloadHandlerBuffer();

            webRequest.downloadHandler = downloadHandler;

            yield return webRequest.SendWebRequest();

            callback?.Invoke(webRequest.downloadHandler);

            webRequest.Dispose();
        }

        public static void AsyncDownLoadInGitHub(string url, Action<DownloadHandler> callback)
        {
            Run(AsyncDownLoad(GithubPathRoot + url, callback));
        }

        private sealed class DownloadMono : MonoBehaviour
        {
        }
    }
}
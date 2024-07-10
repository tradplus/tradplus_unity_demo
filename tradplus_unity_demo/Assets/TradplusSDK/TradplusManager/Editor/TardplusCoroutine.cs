
using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace Tardplus.TradplusEditorManager.Editor
{
    public class TardplusCoroutine
    {
        private readonly IEnumerator enumerator;

        private TardplusCoroutine(IEnumerator enumerator)
        {
            this.enumerator = enumerator;
        }

        static IEnumerator NetworkStart(string urlStr, WWWForm form, Action<string> callback)
        {
            UnityWebRequest request = UnityWebRequest.Post(urlStr, form);
            var operation = request.SendWebRequest();
            while (!operation.isDone) yield return new WaitForSeconds(0.1f);
#if UNITY_2020_1_OR_NEWER
            if (request.result != UnityWebRequest.Result.Success)
#else
            if (request.isNetworkError || request.isHttpError)
#endif
            {
                Debug.LogError("error"+ request.error);
                callback(null);
            }
            else
            {
                callback(request.downloadHandler.text);
            }
        }

        public static TardplusCoroutine StartCoroutine(string urlStr, WWWForm form, Action<string> callback)
        {

            var coroutine = new TardplusCoroutine(NetworkStart(urlStr, form, callback));
            coroutine.Start();
            return coroutine;
        }

        private void Start()
        {
            EditorApplication.update += OnEditorUpdate;
        }

        public void Stop()
        {
            if (EditorApplication.update == null) return;

            EditorApplication.update -= OnEditorUpdate;
        }

        private void OnEditorUpdate()
        {
            if (!enumerator.MoveNext())
            {
                Stop();
            }
        }
    }
}

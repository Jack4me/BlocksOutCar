using CodeBase.Scene;
using UnityEngine;

namespace CodeBase.ScreenManager {
    public class LoaderCallBack : MonoBehaviour {
        private bool IsFirstUpdate = true;

        private void Update() {
            if (IsFirstUpdate) {
                IsFirstUpdate = false;
                Invoke("InvokeLoaderCallback", 1f);
            }
        }

        private void InvokeLoaderCallback() {
            SceneHandler.LoaderCallBack();
        }
    }
}
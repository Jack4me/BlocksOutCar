using CodeBase.ScreenManager;
using UnityEngine;

namespace CodeBase.Scene {
    public class SceneLoader : MonoBehaviour {
        
        public void LoadScene(SceneHandler.SceneType scene, bool useLoadingScreen = true) {
            SceneHandler.Load(scene, useLoadingScreen);
        }
    }
}
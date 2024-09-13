using CodeBase.Scene;
using UnityEngine;

namespace CodeBase.ScreenManager {
    public class GameSceneUILoad : MonoBehaviour {
        public void ReloadScene() {
            SceneHandler.Load(SceneHandler.SceneType.Preferences);
        }
    }
}
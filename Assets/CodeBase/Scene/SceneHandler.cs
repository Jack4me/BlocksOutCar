using System;

namespace CodeBase.Scene {
    public static class SceneHandler {
        public enum SceneType { 
            Loading,
            MainMenu,
            Preferences,
            ChooseLevel,
            Shop,
            FirstLevel,
            SecondLevel,

        }
        
        private static SceneType _previousSceneType; 
        private static Action onLoaderCallBack;
        public static void Load(SceneType sceneType, bool useLoadingScreen = true) {
            _previousSceneType = (SceneType)Enum.Parse(typeof(SceneType), UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
            onLoaderCallBack = () => { UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneType.ToString()); };

            if (useLoadingScreen) {
                UnityEngine.SceneManagement.SceneManager.LoadScene(SceneType.Loading.ToString());
            } else {
                LoaderCallBack();
            }
        }

        public static void LoaderCallBack() {
            if (onLoaderCallBack != null) {
                onLoaderCallBack();
                onLoaderCallBack = null;
            }
        }
        public static void LoadPreviousScene(bool useLoadingScreen = true) {
            Load(_previousSceneType, useLoadingScreen);
        }
    }
}
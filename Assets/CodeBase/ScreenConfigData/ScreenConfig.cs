using UnityEngine;

namespace CodeBase.ScreenConfigData {
    [CreateAssetMenu(fileName = "ScreenConfig", menuName = "ScreenManager/ScreenConfig", order = 1)]
    public class ScreenConfig : ScriptableObject {
        // public string SceneName;
        // public GameObject[] screens;
        //
        // [System.Serializable]
        // public class ScreenInfo {
        //     public GameObject screenPrefab; // Префаб экрана
        //     public bool isActiveByDefault; // Включён ли экран по умолчанию
        // }
        public string SceneName;
        public ScreenInfo[] screens;

        [System.Serializable]
        public class ScreenInfo {
            public GameObject screenPrefab; // Префаб экрана
            public bool isActiveByDefault; // Включён ли экран по умолчанию
        }
    }
}
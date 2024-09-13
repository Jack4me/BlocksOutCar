using System.Collections.Generic;
using CodeBase.ScreenConfigData;
using UnityEngine;

namespace CodeBase.Screen {
    using UnityEngine.SceneManagement;

    public class ScreenManager : MonoBehaviour {
        public static ScreenManager instance;
        public Stack<GameObject> screenHistory = new Stack<GameObject>();
        public Dictionary<string, GameObject> screenRegistry = new Dictionary<string, GameObject>();

        public GameObject[] screensTest;

        // Список конфигураций для каждой сцены
        [SerializeField] private ScreenConfig[] screenConfigs;

        private GameObject HUDInstance;

        void Awake() {
            if (instance != null) {
                Destroy(gameObject);
            }
            else {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }

            LoadHUD();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnDestroy() {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
            LoadScreensForScene(scene.name);
        }

        private void LoadHUD() {
            if (HUDInstance == null) {
                GameObject hudPrefab = Resources.Load<GameObject>("UI/HUD/HUD");
                HUDInstance = Instantiate(hudPrefab);
                DontDestroyOnLoad(HUDInstance);
            }
        }

        private void LoadScreensForScene(string sceneName) {
            RemoveOldScreens();
            foreach (var config in screenConfigs) {
                if (config.SceneName == sceneName) {
                    InitializeScreens(config.screens);
                    return;
                }
            }

            Debug.LogError("Конфигурация экранов для сцены " + sceneName + " не найдена!");
        }

        private void RemoveOldScreens() {
            // Удаляем все экраны предыдущей сцены
            foreach (var screen in screenRegistry.Values) {
                Destroy(screen); // Удаляем объект экрана из сцены
            }

            screenRegistry.Clear(); // Очищаем словарь
            screenHistory.Clear();
        }

        private void InitializeScreens(ScreenConfig.ScreenInfo[] screensInfo) {
            screenHistory.Clear(); // На всякий случай очищаем историю
            screenRegistry.Clear(); // Очищаем реестр

            // Инициализация экранов для данной сцены
            foreach (ScreenConfig.ScreenInfo screenInfo in screensInfo) {
                GameObject screen = Instantiate(screenInfo.screenPrefab, HUDInstance.transform);

                // Регистрация экрана
                RegisterScreen(screen.name, screen); // Регистрируем экран в словаре

                // Устанавливаем активность по умолчанию
                screen.SetActive(screenInfo.isActiveByDefault);

                // Если экран активен по умолчанию, добавляем его в историю
                if (screenInfo.isActiveByDefault) {
                    screenHistory.Push(screen);
                }
            }
        }


        public void ShowScreen(string screenName) {
            if (screenRegistry.ContainsKey(screenName)) {
                HideAllScreens();
                GameObject screenToOpen = screenRegistry[screenName];
                screenToOpen.SetActive(true);
                if (screenHistory.Count > 0) {
                    screenHistory.Peek().SetActive(false);
                }

                // screenToOpen.SetActive(true);
                screenHistory.Push(screenToOpen);
                Debug.Log(screenHistory.Count + "screenHistory");
            }
            else {
                Debug.LogError("Screen not found: " + screenName);
            }
        }

        public void GoBack() {
            if (screenHistory.Count > 1) {
                screenHistory.Pop().SetActive(false);
                screenHistory.Peek().SetActive(true);
            }
        }

        public void RegisterScreen(string screenName, GameObject screenObject) {
            if (!screenRegistry.ContainsKey(screenName)) {
                screenRegistry.Add(screenName, screenObject);
            }
        }

        private void HideAllScreens() {
            foreach (var screen in screenRegistry.Values) {
                screen.SetActive(false);
            }
        }

        public void DebugRegisteredScreens() {
            if (screenRegistry.Count > 0) {
                foreach (var entry in screenRegistry) {
                    Debug.Log("Registered screen: " + entry.Key + ", Object: " + entry.Value.name);
                }
            }
            else {
                Debug.Log("No screens registered in the registry.");
            }
        }
    }
}
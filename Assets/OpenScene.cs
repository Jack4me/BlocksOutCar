using CodeBase.Scene;
using UnityEngine;
using UnityEngine.UI;

public class OpenScene : MonoBehaviour {
    [SerializeField] private SceneHandler.SceneType sceneToLoad;
    [SerializeField] private Button openWindowButton;


    // Ссылка на SceneLoader
    private SceneLoader sceneLoader;

    private void Start() {
        // Находим SceneLoader на сцене
        sceneLoader = FindObjectOfType<SceneLoader>();

        if (sceneLoader == null) {
            Debug.LogError("SceneLoader не найден на сцене!");
            return;
        }

        // Добавляем слушатель для кнопки, который будет вызывать метод из SceneLoader
        openWindowButton.onClick.AddListener(() => sceneLoader.LoadScene(sceneToLoad));
    }
}
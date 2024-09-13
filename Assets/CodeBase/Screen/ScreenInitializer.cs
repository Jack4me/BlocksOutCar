using UnityEngine;

namespace CodeBase.ScreenManager {
    public class ScreenInitializer : MonoBehaviour
    {
       
            [SerializeField] private string screenName;

            void Awake() {
                // Регистрация экрана в реестре ScreenManager
                CodeBase.Screen.ScreenManager.instance.RegisterScreen(screenName, gameObject);
                if (gameObject.CompareTag("Menu") ) {
                    return;
                }
                gameObject.SetActive(false);
            }
        
    }
}

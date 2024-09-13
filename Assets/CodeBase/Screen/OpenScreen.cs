using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Screen {
    public class OpenScreen : MonoBehaviour {
        [SerializeField] private string screenName;
        [SerializeField] private Button OpenWindowButton;

        private void Start() {
            OpenWindowButton.onClick.AddListener(OnOpenHUDButtonClick);
        }

        private void OnOpenHUDButtonClick() {
            ScreenManager.instance.ShowScreen(screenName);
        }
    }
}
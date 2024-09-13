using System;
using UnityEngine;
using UnityEngine.UI;
namespace CodeBase.ScreenManager {
   public class CloseButton : MonoBehaviour {
      public GameObject window; 
      public Button CloseHUDButton;


      private void Start() {
         CloseHUDButton.onClick.AddListener(CodeBase.Screen.ScreenManager.instance.GoBack);
      }

      public void CloseWindow() {
         window.SetActive(false);
      }
   }
}

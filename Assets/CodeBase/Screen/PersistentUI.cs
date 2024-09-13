using UnityEngine;

namespace CodeBase.Screen {
    public class PersistentUI : MonoBehaviour
    {
        public static PersistentUI instanceUIManager;

        void Awake()
        {
            if (instanceUIManager != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instanceUIManager = this;
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}
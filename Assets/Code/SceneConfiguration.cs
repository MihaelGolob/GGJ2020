using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code {

    public class SceneConfiguration : MonoBehaviour {

        public bool EnableSoundManager;

        public SoundManager SoundManager { get; private set; }

        private void Awake() {
            DontDestroyOnLoad(this);
            ApplyExistingSceneConfiguration();
            InitializeComponents();
        }

        private void ApplyExistingSceneConfiguration() {
            var configs = FindObjectsOfType<SceneConfiguration>();
            if (configs.Length > 2) {
//                Debug.LogError($"You have to many {nameof(SceneConfiguration)} components in the {SceneManager.GetActiveScene().name} scene");
            }
            foreach (var config in configs) {
                if (config == this) continue;
                ConfigureScene(config);
                Destroy(config.gameObject);
            }

            void ConfigureScene(SceneConfiguration config) {
                if (config.EnableSoundManager && config.SoundManager != null) {
                    SoundManager = config.SoundManager;
                }
            }
        }

        void InitializeComponents() {
            if (EnableSoundManager && SoundManager == null) {
                SoundManager = new SoundManager();
            }
        }

        private void Update() {
            SoundManager?.OnUpdate();
        }

    }

}

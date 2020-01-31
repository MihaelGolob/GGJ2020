using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Utilities {
    public class ChangeSceneUtility : MonoBehaviour {
        public  bool   DontDestroyOnLoad;
        public  bool   ChangeSceneTrigger;
        public  string SceneName;
        private bool   NotSetYet = true;

        void Update()
        {
            if (ChangeSceneTrigger) {
                SceneManager.LoadScene(SceneName);
                ChangeSceneTrigger = false;
            }
        
            if (DontDestroyOnLoad && NotSetYet) {
                DontDestroyOnLoad(this);
                NotSetYet = false;
            }
        }
    }
}

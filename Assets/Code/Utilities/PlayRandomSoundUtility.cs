using UnityEngine;

namespace Code.Utilities {
    public class PlayRandomSoundUtility : MonoBehaviour {
        public  bool               DontDestroyOnLoad;
        public  SceneConfiguration SceneConfiguration;
        public  bool               PlaySoundTrigger;
        private bool               NotSetYet = true;

        void Update() {
            if (PlaySoundTrigger) {
                var sm    = SceneConfiguration.SoundManager;
                var clips = SceneConfiguration.SoundManager.GetAllAudioClipNames();
                sm.PlayClip(clips[new System.Random().Next(0, clips.Length)], 1);
                PlaySoundTrigger = false;
            }

            if (DontDestroyOnLoad && NotSetYet) {
                DontDestroyOnLoad(this);
                NotSetYet = false;
            }
        }
    }
}
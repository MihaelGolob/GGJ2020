using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code {
    public class SoundManager {
        private const    string          AUDIO_SOURCE_NAME = "AudioSource";
        private readonly AudioSource[]   _pool             = new AudioSource[10];
        private readonly List<AudioClip> _clips = new List<AudioClip>();

        public SoundManager() {
            FindAllClips();
            InitializePool();
        }

        private void FindAllClips() {
            var loadedClips = Resources.LoadAll<AudioClip>("Audio/");
            foreach (var clip in loadedClips) {
                _clips.Add(clip);
                Debug.Log($"AudioClip {clip.name} found.");
            }
        }

        private void InitializePool() {
            var parent = new GameObject("SoundPool");
            Object.DontDestroyOnLoad(parent);
            for (var i = 0; i < _pool.Length; i++) {
                var @object = new GameObject(AUDIO_SOURCE_NAME, typeof(AudioSource));
                @object.transform.SetParent(parent.transform);
                _pool[i] = @object.GetComponent<AudioSource>();
            }
        }

        public void OnUpdate() {
            ResetAudioSourcesWhenTheyStop();
        }

        private void ResetAudioSourcesWhenTheyStop() {
            foreach (var source in _pool) {
                if (!source.isPlaying) {
                    source.clip            = null;
                    source.gameObject.name = AUDIO_SOURCE_NAME;
                }
            }
        }

        public void PlayClip(string name, float volume, bool loop = false) {
            foreach (var clip in _clips) {
                if (clip.name == name) {
                    var source = GetAudioSource();
                    if (source == null) {
                        Debug.LogWarning("Audio pool is full, cannot play the sound!");
                        return;
                    }

                    source.clip   = clip;
                    source.volume = volume;
                    source.loop   = loop;
                    source.Play();
                    source.gameObject.name = name;
                    return;
                }
            }
        }
        private AudioSource GetAudioSource() {
            return _pool.SkipWhile(s => s.isPlaying).FirstOrDefault();
        }

        public void StopClip(string name) {
            for (var i = 0; i < _pool.Length; i++) {
                var source = _pool[i].GetComponent<AudioSource>();
                if (source.clip.name == name) {
                    source.Stop();
                    source.gameObject.name = AUDIO_SOURCE_NAME;
                    return;
                }
            }
        }

        public string[] GetAllAudioClipNames() {
            return _clips.Select(c => c.name).ToArray();
        }
    }
}
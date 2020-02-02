using Code;
using UnityEngine;

public class SoundTriggerObject : MonoBehaviour {

    [SerializeField] private string SoundName;
    [SerializeField] private float Volume;
    [SerializeField] private bool Loop;
    private bool PlayOnlyOnFirstEnter = true;
    private SoundManager SoundManager;

    private void Start() {
        SoundManager = FindObjectOfType<SceneConfiguration>().SoundManager;
    }

    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Player") || !PlayOnlyOnFirstEnter) return;
        SoundManager.PlayClip(SoundName, Volume, Loop);
        PlayOnlyOnFirstEnter = false;
    }

}

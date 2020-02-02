using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class TimelineManager : MonoBehaviour {
    [SerializeField] private PlayableTrack toLevels;
    
    private PlayableDirector _timeline;

    private void Start() {
        _timeline = GetComponent<PlayableDirector>();
        _timeline.playableAsset = toLevels;
        _timeline.Play();
    }
    
}

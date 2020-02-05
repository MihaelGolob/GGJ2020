using System;
using System.Collections;
using Code;
using UnityEngine;


public class Bridge : MonoBehaviour {

    private bool _cancelLow;
    private bool _cancelLift;
    
    private int _currAngle;

    private SoundManager _sound;

    private void Start() {
        _cancelLift = false;
        _cancelLow = false;

        _currAngle = 90;
        _sound = FindObjectOfType<SceneConfiguration>().SoundManager;
    }    

    public IEnumerator lower() {
        _cancelLift = true;
        _cancelLow = false;
        /*_sound.PlayClip("bridge", 0.5f);*/
        for (; _currAngle > 0; _currAngle--) {
            if (_cancelLow) {
                break;
            }
            transform.localRotation *= Quaternion.Euler(1, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }
        
    }
    
    public IEnumerator lift() {
        _cancelLift = false;
        _cancelLow = true;
        for (; _currAngle < 90; _currAngle++) {
            if (_cancelLift) {
                break;
            }
            transform.localRotation *= Quaternion.Euler(-1, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }

}

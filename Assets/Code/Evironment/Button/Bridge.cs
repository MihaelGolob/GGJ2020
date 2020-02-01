using System;
using System.Collections;
using UnityEngine;


public class Bridge : MonoBehaviour {

    private bool _cancelLow;
    private bool _cancelLift;
    
    private int _currAngle;

    private void Start() {
        _cancelLift = false;
        _cancelLow = false;

        _currAngle = 90;
    }    

    public IEnumerator lower() {
        _cancelLift = true;
        _cancelLow = false;
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

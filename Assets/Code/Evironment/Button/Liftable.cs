using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liftable : MonoBehaviour {
    [SerializeField] private float travelDistance = 20f;

    private Vector3 _startPos;

    private bool _cancelLift;
    private bool _cancelLow;

    private void Start() {
        _startPos = transform.position;
        _cancelLift = false;
        _cancelLow = false;
    }

    public IEnumerator lift() {
        _cancelLift = false;
        _cancelLow = true;
        
        float step = 0.01f;
        float numSteps = travelDistance / step;
        print("resetting button");
        for (float f = Mathf.Abs(transform.position.y - _startPos.y); f < travelDistance; f += step) {
            if (_cancelLift)
                break;
            transform.position = new Vector3(_startPos.x, _startPos.y + f, _startPos.z);
            yield return new WaitForSeconds(0.5f/numSteps);
        }

        _cancelLift = false;
    }

    public IEnumerator lower() {
        _cancelLift = true;
        _cancelLow = false;
        
        float step = 0.01f;
        float numSteps = travelDistance / step;
        print("resetting button");
        for (float f = Mathf.Abs(transform.position.y - _startPos.y); f > 0; f -= step) {
            if (_cancelLow)
                break;
            transform.position = new Vector3(_startPos.x, _startPos.y + f, _startPos.z);
            yield return new WaitForSeconds(0.5f/numSteps);
        }

        _cancelLow = false;
    }
}

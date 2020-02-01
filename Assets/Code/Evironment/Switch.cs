using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] private float travelDistance;
    [SerializeField] private float transitionTime;
    
    private Vector3 _startPos;
    private bool _pressed;

    private void Start() {
        _startPos = transform.position;
        _pressed = false;
    }

    private void Update() {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.up, out hit, 0.7f, 1)) {
            if (!_pressed) {
                _pressed = true;
                StartCoroutine(buttonDown());
            }
        }
        else {
            if (_pressed) {
                _pressed = false;
                StartCoroutine(buttonUp());
            }
        }
    }

    private IEnumerator buttonDown() {
        float step = 0.01f;
        float numSteps = travelDistance / step;

        for (float f = 0; f < travelDistance; f += step) {
            transform.position = new Vector3(_startPos.x, _startPos.y - f, _startPos.z);
            
            yield return new WaitForSeconds(transitionTime/numSteps);
        }
    }

    private IEnumerator buttonUp() {
        float step = 0.01f;
        float numSteps = travelDistance / step;
        print("resetting button");
        for (float f = travelDistance; f > 0; f -= step) {
            transform.position = new Vector3(_startPos.x, _startPos.y + f, _startPos.z);
            
            yield return new WaitForSeconds(transitionTime/numSteps);
        }
    }

    private IEnumerator resetButton() {
        yield return new WaitForSeconds(0.2f);
        _pressed = false;
        StartCoroutine(buttonUp());
    }
}

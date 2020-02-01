using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {
    [SerializeField] private float travelDistance;
    [SerializeField] private float transitionTime;
    
    private Vector3 _startPos;

    private bool _pressed;
    
    private void Start() {
        _startPos = transform.position;
        _pressed = false;
    }

    private void OnCollisionEnter(Collision other) {
        Rigidbody collision = other.rigidbody;
        if (collision.CompareTag("Player") && !_pressed) {
            StartCoroutine(buttonDown());
        }
    }

    private IEnumerator buttonDown() {
        float step = 0.01f;
        float numSteps = travelDistance / step;

        for (float f = 0; f < travelDistance; f += step) {
            transform.position = new Vector3(_startPos.x, _startPos.y - f, _startPos.z);
            
            yield return new WaitForSeconds(transitionTime/numSteps);
        }

        _pressed = true;
    }
}

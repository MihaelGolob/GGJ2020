using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {
    [SerializeField] private GameObject door;
    [SerializeField] private float travelDistance = 0.1f;

    private Vector3 _startPos;
    private bool _pressed;

    private void Start() {
        _startPos = transform.position;
        _pressed = false;
    }

    private void OnTriggerEnter(Collider other) {
        if (!_pressed && (other.CompareTag("Player") || other.CompareTag("Pushable"))) {
            StartCoroutine(buttonDown());
            
            var liftable = door.GetComponent<Liftable>();
            if (liftable != null) {
                StartCoroutine(liftable.lift());   
            }
            else {
                var bridge = door.GetComponent<Bridge>();
                StartCoroutine(bridge.lower());
            }

            _pressed = true;
        }
            
    }

    private IEnumerator buttonDown() {
        float step = 0.01f;
        float numSteps = travelDistance / step;

        for (float f = Mathf.Abs(transform.position.y - _startPos.y); f < travelDistance; f += step) {
            transform.position = new Vector3(_startPos.x, _startPos.y - f, _startPos.z);
            
            yield return new WaitForSeconds(0.1f/numSteps);
        }
    }
}

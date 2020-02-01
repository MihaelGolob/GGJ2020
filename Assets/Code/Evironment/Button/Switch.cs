using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {
    [SerializeField] private GameObject door;
    [SerializeField] private float travelDistance;

    private Vector3 _startPos;
    private bool _pressed;
    private Liftable _liftable;

    private void Start() {
        _startPos = transform.position;
        _pressed = false;
        _liftable = door.GetComponent<Liftable>();
    }

    private void Update() {
        /*RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.up, out hit, 0.7f, 1)) {
            if (!_pressed) {
                _pressed = true;
                StartCoroutine(buttonDown());
                StartCoroutine(_liftable.lift());
            }
        }
        else {
            if (_pressed) {
                _pressed = false;
                StartCoroutine(buttonUp());
                StartCoroutine(_liftable.lower());
            }
        }*/
    }

    private void OnTriggerEnter(Collider other) {
        if (!_pressed && other.CompareTag("Player")) {
            _pressed = true;
            StartCoroutine(buttonDown());
            StartCoroutine(_liftable.lift());
        }
    }

    private void OnTriggerExit(Collider other) {
        if (_pressed && other.CompareTag("Player")) {
            _pressed = false;
            StartCoroutine(buttonUp());
            StartCoroutine(_liftable.lower());
        }
    }

    private IEnumerator buttonDown() {
        float step = 0.01f;
        float numSteps = travelDistance / step;

        for (float f = 0; f < travelDistance; f += step) {
            transform.position = new Vector3(_startPos.x, _startPos.y - f, _startPos.z);
            yield return new WaitForSeconds(0.1f/numSteps);
        }
    }

    private IEnumerator buttonUp() {
        float step = 0.01f;
        float numSteps = travelDistance / step;
        print("resetting button");
        for (float f = travelDistance; f > 0; f -= step) {
            transform.position = new Vector3(_startPos.x, _startPos.y + f, _startPos.z);
            yield return new WaitForSeconds(0.1f/numSteps);
        }
    }
}

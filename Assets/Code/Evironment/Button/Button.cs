using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {
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

        if(!_pressed && Physics.Raycast(transform.position, Vector3.up, out hit, 0.5f, 1)) {
            _pressed = true;
            StartCoroutine(buttonDown());
            StartCoroutine(_liftable.lift());
        }*/
    }

    private void OnTriggerEnter(Collider other) {
        if (!_pressed && other.CompareTag("Player")) {
            StartCoroutine(buttonDown());
            StartCoroutine(_liftable.lift());
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
}

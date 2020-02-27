using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {
    [SerializeField] private GameObject door;
    [SerializeField] private float travelDistance;

    private bool _pressed;
    private Vector3 _startPos;
    
    private void Start() {
        _startPos = transform.position;
        _pressed = false;
    }

    private void OnTriggerEnter(Collider other) {
        if (!_pressed && other.CompareTag("Bullet")) {
            _pressed = true;

            var liftable = door.GetComponent<Liftable>();
            if (liftable != null) {
                StartCoroutine(liftable.lift());   
            }
            else {
                var bridge = door.GetComponent<Bridge>();
                StartCoroutine(bridge.lower());
            }
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            GetComponent<Collider>().enabled = false;
        }
    }
}

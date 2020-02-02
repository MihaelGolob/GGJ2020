using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Key : MonoBehaviour {
    [SerializeField] private float hoverHeight;
    [SerializeField] private float speed;

    private bool _move;
    
    private Vector3 _startPos;
    private float _startTime;
    private float _distance;

    private Vector3 _target;

    private void Start() {
        _move = false;
        
        _target = new Vector3(transform.position.x, transform.position.y + hoverHeight, transform.position.z);
        
        _startPos = transform.position;
        _distance = Vector3.Distance(transform.position, _target);
    }

    private void Update() {
        if (_move) {
            float distCovered = (Time.time - _startTime) * speed;
            float fractOfJourney = distCovered / _distance;
            
            transform.position = Vector3.Lerp(_startPos, _target, fractOfJourney);   
        }
        
        // TODO destroy game object when at the top
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            var pl = other.GetComponent<Player>();
            pl.Keys++;
            _startTime = Time.time;
            _move = true;
        }
    }
}

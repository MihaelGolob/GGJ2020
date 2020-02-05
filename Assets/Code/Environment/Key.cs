using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Key : MonoBehaviour {
    [SerializeField] private float hoverHeight = 20;
    [SerializeField] private float speed = 15;

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

        if (transform.position.y > _target.y) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && !_move) {
            var pl = other.GetComponent<AnimationController>();
            pl.Coins++;
            _startTime = Time.time;
            _move = true;
        }
    }
}

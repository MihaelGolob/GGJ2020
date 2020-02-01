using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour {
    private CharacterController _controller;
    private Animator _animator;

    private void Start() {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    private void Update() {
        move();
    }

    private void move() {
        Vector2 Movement = new Vector2(1, 0);
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Security.Cryptography;
using UnityEngine;

public class disappearing_platform : MonoBehaviour {
    [SerializeField] private float transitionTime;

    private Renderer _renderer;
    private bool _triggered;
    private float _startScaleX;
    
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _triggered = false;
        _startScaleX = transform.localScale.x;
    }

    private void OnTriggerEnter(Collider other) {
        if(!_triggered)
            StartCoroutine(disappear());
    }

    IEnumerator disappear() {
        _triggered = true;
        for (float i = 1f; i > 0f; i -= 0.01f) {
            transform.localScale = new Vector3(_startScaleX*i, transform.localScale.y, transform.localScale.z);

            print(i);

            yield return new WaitForSeconds(transitionTime / 100.0f);
        }
        Destroy(gameObject);
    }
}

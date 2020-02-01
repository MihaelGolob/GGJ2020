using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Numerics;
using System.Security.Cryptography;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class disappearing_platform : MonoBehaviour {
    [SerializeField] private float transitionTime;
    [SerializeField] private bool xAxis;
    [SerializeField] private bool yAxis;
    [SerializeField] private bool zAxis;
    
    private bool _triggered;
    private Vector3 _startScale;
    
    // Start is called before the first frame update
    void Start() {
        _triggered = false;
        _startScale = transform.localScale;
    }

    private void OnCollisionEnter(Collision other) {
        if(!_triggered)
            StartCoroutine(disappear());
    }

    IEnumerator disappear() {
        _triggered = true;
        for (float i = 1f; i > 0f; i -= 0.01f) {
            if(xAxis)
                transform.localScale = new Vector3(_startScale.x*i, transform.localScale.y, transform.localScale.z);
            if(yAxis)
                transform.localScale = new Vector3(transform.localScale.x, _startScale.y*i, transform.localScale.z);
            if(zAxis)
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, _startScale.z*i);

            print(i);

            yield return new WaitForSeconds(transitionTime / 100.0f);
        }
        Destroy(gameObject);
    }
}

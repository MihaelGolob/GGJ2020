using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving_platform : MonoBehaviour {
    
    // positions:
    [SerializeField] Transform startPos;
    [SerializeField] Transform endPos;
    private float _totalDistance;
    
    // speed:
    [SerializeField] private float speed;
    [SerializeField] private int waitTime;

    private float _startTime;
    private bool _waiting;
    
    // Start is called before the first frame update
    void Start() {
        _startTime = Time.time;
        _totalDistance = Vector3.Distance(startPos.position, endPos.position);
        _waiting = false;
        
        transform.position = startPos.position;
    }

    // Update is called once per frame
    void Update() {
        if (!_waiting) {
            float distanceCovered = (Time.time - _startTime) * speed;

            float fractionOfDistance = distanceCovered / _totalDistance;
            transform.position = Vector3.Lerp(startPos.position, endPos.position, fractionOfDistance);   
        }

        if (transform.position == endPos.position && !_waiting) {
            StartCoroutine(wait());
        }
    }

    IEnumerator wait() {
        _waiting = true;
        yield return new WaitForSeconds(waitTime);
        // change direction
        Transform tmp = endPos;
        endPos = startPos;
        startPos = tmp;
        _startTime = Time.time;
        
        _waiting = false;
    }
}

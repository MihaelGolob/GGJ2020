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

    private float _startTime;
    
    
    // Start is called before the first frame update
    void Start() {
        _startTime = Time.time;
        _totalDistance = Vector3.Distance(startPos.position, endPos.position);
        transform.position = startPos.position;
    }

    // Update is called once per frame
    void Update() {
        float distanceCovered = (Time.time - _startTime) * speed;

        float fractionOfDistance = distanceCovered / _totalDistance;
        transform.position = Vector3.Lerp(startPos.position, endPos.position, fractionOfDistance);

        if (transform.position == endPos.position) {
            // change direction
            Transform tmp = endPos;
            endPos = startPos;
            startPos = tmp;
            _startTime = Time.time;
        }
    }
}

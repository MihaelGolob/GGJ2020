using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenOnAwake : MonoBehaviour {
    [SerializeField] private float transitionTime;
    
    private Vector3 _endScale;

    void Start() {
        _endScale = transform.localScale;
        transform.localScale = new Vector3(0f, 0f, 0f);

        StartCoroutine(resize());
    }

    IEnumerator resize(){
        for (float i = 0f; i < 1f; i += 0.01f) {
            transform.localScale = new Vector3(_endScale.x * i, _endScale.y * i, _endScale.z * i);
            
            yield return new WaitForSeconds(transitionTime / 100.0f);
        }
    }
}

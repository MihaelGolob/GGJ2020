using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rainfall : MonoBehaviour {
    
    [SerializeField] private GameObject rain;
    [SerializeField] private Transform begin;
    [SerializeField] private Transform end;
    [SerializeField] private int amount;

    private float _time;
    
    void Start() {
        _time = Time.time;   
    }

    void Update() {
        if (Time.time - _time >= 1f/amount) {
            float x = Random.Range(begin.position.x, end.position.x);
            float y = begin.position.y;
            float z = Random.Range(begin.position.z, end.position.z);
            
            Vector3 pos = new Vector3(x,y,z);
            
            GameObject tmp = Instantiate(rain, pos, Quaternion.identity);
            var destroySelf = tmp.GetComponent<destroySelf>();
            StartCoroutine(destroySelf.destroy(5));
            
            _time = Time.time;
        }
    }
}

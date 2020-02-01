using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour {
    [SerializeField] float offset;

    private Transform _startPos;

    // Start is called before the first frame update
    void Start() {
        _startPos = transform;
    }

    public IEnumerator click() {
        // call functions here
        
        
        // animation
        for (float i = 0f; i < offset; i += 0.001f) {
            
            transform.position = new Vector3(transform.position.x, transform.position.y, _startPos.position.z + offset);
            
            yield return new WaitForSeconds(0.01f);   
        }

        StartCoroutine(returnToPos());
    }
    
    public IEnumerator returnToPos() {
        for (float i = offset; i > 0; i -= 0.001f) {
            
            transform.position = new Vector3(transform.position.x, transform.position.y, _startPos.position.z - offset);
            
            yield return new WaitForSeconds(0.01f);   
        }
    }
}

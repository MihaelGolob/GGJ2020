using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour {
    [SerializeField] float offset = 0.015f;
    [SerializeField] private bool xAxis;
    [SerializeField] private bool yAxis;
    [SerializeField] private bool zAxis;

    private Transform _startPos;

    // Start is called before the first frame update
    void Start() {
        _startPos = transform;
    }

    public IEnumerator click() {
        // call functions here
    
        if (transform.name == "start") {
            // start methods
            Debug.Log("start game");
        }
        else if (transform.name == "upgrades") {
            // upgrade methods
            Debug.Log("upgrades");
        }
        else if (transform.name == "credits") {
            // credit methods
            Debug.Log("show credits");
        }
        else if (transform.name.Substring(0,5) == "level") {
            Debug.Log("launch level");
            SceneManager.LoadScene("level" + transform.name[5]);
        }
        
        
        // animation
        for (float i = 0f; i < offset; i += 0.001f) {
            if(zAxis)
                transform.position = new Vector3(transform.position.x, transform.position.y, _startPos.position.z + offset);
            if(yAxis)
                transform.position = new Vector3(transform.position.x, transform.position.y + offset, _startPos.position.z);
            if(xAxis)
                transform.position = new Vector3(transform.position.x - offset, transform.position.y, _startPos.position.z);
            
            yield return new WaitForSeconds(0.01f);   
        }

        StartCoroutine(returnToPos());
    }
    
    private IEnumerator returnToPos() {
        for (float i = offset; i > 0; i -= 0.001f) {
            if(zAxis)
                transform.position = new Vector3(transform.position.x, transform.position.y, _startPos.position.z - offset);
            if(yAxis)
                transform.position = new Vector3(transform.position.x, transform.position.y - offset, _startPos.position.z);
            if(xAxis)
                transform.position = new Vector3(transform.position.x + offset, transform.position.y, _startPos.position.z);
            
            yield return new WaitForSeconds(0.01f);   
        }
    }
}

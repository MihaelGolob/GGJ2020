using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour {
    [SerializeField] float offset = 0.015f;
    [SerializeField] private bool xAxis;
    [SerializeField] private bool yAxis;
    [SerializeField] private bool zAxis;

    private Transform _startPos;
    private PlayableDirector _timeline;

    // Start is called before the first frame update
    void Start() {
        _startPos = transform;
        _timeline = GetComponent<PlayableDirector>();
    }

    public IEnumerator click() {
        // call functions here
    
        if (transform.name == "start") {
            // start methods
            Debug.Log("start game");
            _timeline.Play();
        }
        else if (transform.name == "upgrades") {
            // upgrade methods
            Debug.Log("upgrades");
            _timeline.Play();
        }
        else if (transform.name == "quit") {
            // credit methods
            Debug.Log("quit");
            //_timeline.Play();
        }
        else if (transform.name == "back") {
            Debug.Log("show credits");
            _timeline.Play();
        }
        else if (transform.name == "Tutorial") {
            SceneManager.LoadScene("Tutorial");
        }
        else if (transform.name.Substring(0, 5) == "Level") {
            Debug.Log("launch level");
            SceneManager.LoadScene("Level" + transform.name[5]);
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

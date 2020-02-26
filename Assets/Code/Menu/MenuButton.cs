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
    [SerializeField] private bool stayIn = false;

    private Transform _startPos;
    private PlayableDirector _timeline;

    private GameObject _player;
    private AnimationController _playerCont;

    private bool _clicked;
    private bool _block;

    // Start is called before the first frame update
    void Start() {
        _startPos = transform;
        _timeline = GetComponent<PlayableDirector>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerCont = _player.GetComponent<AnimationController>();
        _clicked = false;
        _block = false;
    }

    public IEnumerator click() {
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
            Application.Quit();
            //_timeline.Play();
        }
        else if (transform.name == "back") {
            Debug.Log("show credits");
            _timeline.Play();
        }
        else if (transform.name == "Tutorial") {
            SceneManager.LoadScene("Tutorial");
        }
        else if (transform.name == "upRun") {
            if (_playerCont.Coins >= 5 && !_clicked) {
                _playerCont.Coins -= 5;
                _playerCont.LevelUp(Level.Run);
            }
            else {
                _block = true;
            }
        }
        else if (transform.name == "upJump") {
            if (_playerCont.Coins >= 15 && !_clicked) {
                _playerCont.Coins -= 15;
                _playerCont.LevelUp(Level.Jump);   
            }
            else {
                _block = true;
            }
        }
        else if (transform.name == "upPush") {
            if (_playerCont.Coins >= 40 && !_clicked) {
                _playerCont.Coins -= 40;
                _playerCont.LevelUp(Level.Push);
            }
            else {
                _block = true;
            }
        }
        else if (transform.name == "upShoot") {
            if (_playerCont.Coins >= 100 && !_clicked) {
                _playerCont.Coins -= 100;
                _playerCont.LevelUp(Level.Shoot);
            }
            else {
                _block = true;
            }
        }
        else if (transform.name.Substring(0, 5) == "Level") {
            Debug.Log("launch level");
            SceneManager.LoadScene("Level" + transform.name[5]);
        }
        
        
        // animation
        if (!(stayIn && _clicked)) {
            if (!_block) {
                for (float i = 0f; i < offset; i += 0.001f) {
                    if(zAxis)
                        transform.position = new Vector3(transform.position.x, transform.position.y, _startPos.position.z + offset);
                    if(yAxis)
                        transform.position = new Vector3(transform.position.x, transform.position.y + offset, _startPos.position.z);
                    if(xAxis)
                        transform.position = new Vector3(transform.position.x - offset, transform.position.y, _startPos.position.z);
            
                    yield return new WaitForSeconds(0.01f);   
                }     
            }
        }
        
        _clicked = true;
        
        if(!stayIn)
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

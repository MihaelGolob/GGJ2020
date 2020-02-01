using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour {

    [SerializeField] private int distance;
    [SerializeField] private float _speed;
    
    private Vector3 _target;
    private bool _move;

    private ParticleSystem _particles;
    
    // Start is called before the first frame update
    void Start() {
        _target = new Vector3(transform.position.x, transform.position.y + distance, transform.position.z);
        _move = false;

        _particles = GetComponent<ParticleSystem>();
        _particles.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (_move) {
            makeStep();
        }
    }

    void OnTriggerEnter(Collider collider) {
        if (collider.CompareTag("Player")) {
            _move = true;
            _particles.Play();
        }
    }

    void makeStep() {
        float step = _speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _target, step);
    }
}

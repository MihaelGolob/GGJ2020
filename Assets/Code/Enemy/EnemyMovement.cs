using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(CharacterController))]
public class EnemyMovement : MonoBehaviour {
    [SerializeField] private float speed;
    [SerializeField] private Transform start;
    [SerializeField] private Transform target;
    
    private CharacterController _controller;
    private Animator _animator;

    private int _gravityForce;
    Vector3 _direction = new Vector3(0, 0, 1);

    private void Start() {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        _gravityForce = 3;

        transform.position = start.position;
    }

    private void Update() {
        move();
    }

    private void move() {
        Vector3 gravity = Vector3.zero;

        if (!_controller.isGrounded) {
            gravity.y -= _gravityForce;
        }
        
        _controller.Move(_direction * speed * Time.deltaTime);
        _controller.Move(gravity * Time.deltaTime);

        if (Mathf.Abs(transform.position.z - target.position.z) <= 0.1f) {
            _direction = -_direction;
            Vector3 tmp = start.position;
            start.position = target.position;
            target.position = tmp;
            transform.localRotation *= Quaternion.Euler(0, 180, 0);
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (other.collider.CompareTag("Bullet")) {
            Destroy(gameObject);
        }
    }
}

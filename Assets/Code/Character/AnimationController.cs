using System;
using UnityEngine;

public enum LocomotionLevel {

    Walk = 1,
    OldRun = 2,
    Run = 3

}

public class AnimationController : MonoBehaviour {

    [SerializeField] private LocomotionLevel LocomotionLevel = LocomotionLevel.Walk;
    [SerializeField] private float RaycastGroundDistance = 1f;
    [SerializeField] private bool IsGroundedFlag;
    [SerializeField] private Vector3 JumpForce;
    private readonly int PushHash = Animator.StringToHash("Push");
    private readonly int JumpHash = Animator.StringToHash("Jump");
    private readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    private Animator Animator;
    private Rigidbody Rigidbody;
    private int Locomotion = 1;
    private int Orientation = 1;
    private int LastOrientation = 1;
    private LayerMask Ground;

    private void Awake() {
        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody>();
        Ground = LayerMask.GetMask("Ground");
    }

    private void Update() {
        ApplyLocomotionInput();
        ApplyOrientation();
        ApplyLocomotion();
        Jump();
    }

    private void Jump() {
        if (!IsGrounded()) return;
        if (!Input.GetButtonDown("Jump")) return;
        var force = transform.forward;
        force.y = JumpForce.y;
        force.z *= JumpForce.z;
        Rigidbody.AddForce(force);
    }

    bool IsGrounded() {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 1.0f;
    
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null) {
            return true;
        }
    
        return false;
        
        var position = transform.position + transform.TransformDirection(Vector3.up) / 2;
        Debug.DrawLine(position, position + Vector3.down * RaycastGroundDistance, Color.red);
        IsGroundedFlag = Physics.Raycast(position, Vector3.down, out var hit, RaycastGroundDistance);
        return IsGroundedFlag;
    }

    private void ApplyLocomotionInput() {
        Locomotion = 0;
        Animator.SetBool(PushHash, false);
        if (Input.GetButton("Push")) {
            Animator.SetBool(PushHash, true);
        } else if (Input.GetButtonDown("Jump")) {
            Animator.SetTrigger(JumpHash);
        }

        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical")) {
            Locomotion = (int) LocomotionLevel;
        }
        Animator.SetInteger(LocomotionHash, Locomotion);
    }

    private void ApplyOrientation() {
        int newOrientation = Input.GetAxisRaw("Vertical") > 0 ? 1 : -1;
        if (newOrientation != LastOrientation && Math.Abs(Input.GetAxisRaw("Vertical")) > 0.1f && IsGroundedFlag) {
            Orientation = newOrientation;
        }
        var rotation = transform.rotation;
        rotation = Quaternion.Euler(0, Orientation == -1 ? 180 : 0, 0);
        transform.rotation = rotation;
        LastOrientation = Orientation;
    }

    private void ApplyLocomotion() {
        var position = transform.position;
        position.x = 0;
        position.z += GetLocomotionMultiplyer() * Time.deltaTime * Input.GetAxis("Vertical");
        transform.position = position;

        float GetLocomotionMultiplyer() {
            switch (LocomotionLevel) {
                case LocomotionLevel.Walk:
                    return 1.8f;
                case LocomotionLevel.OldRun:
                    return 4.5f;
                case LocomotionLevel.Run:
                    return 7.4f;
            }
            return 1f;
        }
    }

}

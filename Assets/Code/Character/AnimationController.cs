using System;
using UnityEngine;

public enum LocomotionLevel {

    Walk = 1,
    OldRun = 2,
    Run = 3

}

public class AnimationController : MonoBehaviour {

    [SerializeField] private LocomotionLevel LocomotionLevel = LocomotionLevel.Walk;
    [SerializeField] private bool IsGroundedFlag;
    [SerializeField] private bool IsPushingFlag;
    [SerializeField] private Vector3 JumpForce;
    private readonly int PushHash = Animator.StringToHash("Push");
    private readonly int JumpHash = Animator.StringToHash("Jump");
    private readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    private Animator Animator;
    private Rigidbody Rigidbody;
    private Grounded Grounded;
    private int Locomotion = 1;
    private int Orientation = 1;
    private int LastOrientation = 1;
    private Rigidbody LastPushObject;

    private void Awake() {
        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody>();
        Grounded = GetComponentInChildren<Grounded>();
    }

    private void Update() {
        CheckGround();
        ApplyLocomotionInput();
        ApplyOrientation();
        ApplyLocomotion();
        Jump();
        UnsetPushing();
    }

    private void CheckGround() {
        IsGroundedFlag = Grounded.IsGrounded;
    }

    private void ApplyLocomotionInput() {
        Locomotion = 0;
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
        transform.rotation = Quaternion.Euler(0, Orientation == -1 ? 180 : 0, 0);
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

    private void Jump() {
        if (!IsGroundedFlag) return;
        if (!Input.GetButtonDown("Jump")) return;
        var force = new Vector3(0, 1, Orientation);
        force.y *= JumpForce.y;
        force.z *= JumpForce.z;
        Rigidbody.AddForce(force, ForceMode.Impulse);
        Animator.SetTrigger(JumpHash);
    }

    void Push(Collider other) {
        if (Input.GetButton("Push") && other.CompareTag("Pushable")) {
            IsPushingFlag = true;
            Animator.SetBool(PushHash, true);
            other.GetComponent<Rigidbody>().mass = 0.01f;
            LastPushObject = other.GetComponent<Rigidbody>();
        } else {
            IsPushingFlag = false;
            Animator.SetBool(PushHash, false);
        }
    }

    private void UnsetPushing() {
        if (LastPushObject != null && !IsPushingFlag) {
            LastPushObject.mass = 100_000;
        }
    }

    private void OnTriggerEnter(Collider other) {
        Push(other);
    }

    private void OnTriggerStay(Collider other) {
        Push(other);
    }

    private void OnTriggerExit(Collider other) {
        Animator.SetBool(PushHash, false);
        if (LastPushObject != null) LastPushObject.mass = 100_000;
    }

}

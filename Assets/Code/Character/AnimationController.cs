using System;
using System.Collections;
using System.Linq;
using Code;
using UnityEngine;

public enum Level {

    Walk,
    Run,
    Jump,
    Push,
    Shoot

}

public enum LocomotionLevel {

    Walk = 1,
    OldRun = 2,
    Run = 3

}

public enum DanceMove {

    Belly = 1

}

public class AnimationController : MonoBehaviour {

    [SerializeField] private Level Level;
    [SerializeField] private LocomotionLevel LocomotionLevel;
    [SerializeField] private DanceMove DanceMove = DanceMove.Belly;
    [SerializeField] private bool IsGroundedFlag;
    [SerializeField] private bool IsPushingFlag;
    [SerializeField] private Vector3 JumpForce;
    [SerializeField] private bool Dance;
    [SerializeField] private GameObject Bullets;
    [SerializeField] private Vector3 BulletOffset;
    [SerializeField] private float BulletForce;
    [SerializeField] private bool DisableLocomotion;
    [SerializeField] private float FOVMax = 100;
    [SerializeField] private float FOVMin = 30;
    private readonly int PushHash = Animator.StringToHash("Push");
    private readonly int JumpHash = Animator.StringToHash("Jump");
    private readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    private readonly int DanceHash = Animator.StringToHash("Dance");
    private readonly int ShootHash = Animator.StringToHash("Shoot");
    private Animator Animator;
    private Rigidbody Rigidbody;
    private Rigidbody LastPushObject;
    private Grounded Grounded;
    private SoundManager SoundManager;
    private Transform[] AllArmour;
    private Camera Camera;
    private int Locomotion = 1;
    private int Orientation = 1;
    private int LastOrientation = 1;
    private bool PushButtonDown = false;
    
    public int Coins {
        get { return PlayerPrefs.GetInt("Coins"); }
        set { PlayerPrefs.SetInt("Coins", value); }
    }

    private void Awake() {
        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody>();
        Grounded = GetComponentInChildren<Grounded>();
        SoundManager = FindObjectOfType<SceneConfiguration>().SoundManager;
        FindAllArmour();
        HideAllArmour();
        SetMinZoom();
        LoadLevelStatus();

        Coins = 0;
        PlayerPrefs.SetInt("Coins", 0);
    }

    void FindAllArmour() {
        AllArmour = GetComponentsInChildren<Transform>().Where(t => t.CompareTag("Armour")).ToArray();
    }

    void HideAllArmour() {
        foreach (var armour in AllArmour) {
            armour.GetChild(0).localScale = Vector3.zero;
        }
    }

    void SetMinZoom() {
        Camera = FindObjectOfType<Camera>();
        Camera.fieldOfView = FOVMin;
    }

    private void Update() {
        CheckGround();
        if (!DisableLocomotion) {
            ApplyLocomotionInput();
            ApplyOrientation();
            ApplyLocomotion();
            Jump();
            SetPushButtonDown();
            UnsetPushing();
            Shoot();
        }
        MakeOneDanceMove();
        ZoomInOut();
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
        if (!IsGroundedFlag) return;
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
        if (Level < Level.Jump) return;
        if (!IsGroundedFlag) return;
        if (!Input.GetButtonDown("Jump")) return;
        var force = new Vector3(0, 1, Orientation);
        force.y *= JumpForce.y;
        force.z *= JumpForce.z;
        Rigidbody.AddForce(force, ForceMode.Impulse);
        Animator.SetTrigger(JumpHash);
    }

    private void SetPushButtonDown() {
        PushButtonDown = false;
        if (Input.GetButton("Push")) {
            PushButtonDown = true;
        }
    }

    private void UnsetPushing() {
        if (LastPushObject != null && !IsPushingFlag) {
            LastPushObject.mass = 100_000;
        }
    }

    private void MakeOneDanceMove() {
        Animator.SetInteger(DanceHash, 0);
        if (!Dance) return;

        Animator.SetInteger(DanceHash, (int) DanceMove);
        Dance = false;
    }

    private void ZoomInOut() {
        if (Input.GetKey(KeyCode.Q)) {
            if (Camera.fieldOfView < FOVMax) {
                Camera.fieldOfView += Time.deltaTime * 70;
            }
        } else {
            if (Camera.fieldOfView > FOVMin) {
                Camera.fieldOfView -= Time.deltaTime * 70;
            }
        }
    }

    private void Shoot() {
        if (Level < Level.Shoot) return;
        if (Input.GetButtonDown("Fire1")) {
            Animator.SetTrigger(ShootHash);
        }
    }

#region Collsions

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

    void Push(Collider other) {
        if ((PushButtonDown || Input.GetButton("Push")) && other.CompareTag("Pushable") && Level >= Level.Push) {
            IsPushingFlag = true;
            Animator.SetBool(PushHash, true);
            other.GetComponent<Rigidbody>().mass = 0.01f;
            LastPushObject = other.GetComponent<Rigidbody>();
        } else {
            IsPushingFlag = false;
            Animator.SetBool(PushHash, false);
        }
    }

#endregion

#region AnimationEvents

    void ThrowApple() {
        var bullets = Instantiate(Bullets);
        foreach (var bullet in bullets.GetComponentsInChildren<Rigidbody>()) {
            if (bullet.gameObject.CompareTag("Bullet")) {
                var transform = this.transform;
                var forward = transform.forward;
                bullet.transform.position = transform.position + forward * BulletOffset.z + transform.up * BulletOffset.y;
                bullet.AddForce(forward * BulletForce, ForceMode.Impulse);
            }

        }
    }

    void FootSound() {
        SoundManager.PlayRandomThemeSound("grass", 0.1f);
    }

    void JumpSound() {
        SoundManager.PlayRandomThemeSound("jump", 0.3f);
        if (ProbabilityChance(12f)) SoundManager.PlayRandomThemeSound("fart", 1f);
    }

    void PushingSound() {
        SoundManager.PlayRandomThemeSound("pushing", 0.1f);
    }

#endregion

#region Leveling Up and Down

    public void LevelUp(Level level) {
        Level = level;
        switch (level) {
            case Level.Run:
                StartCoroutine(ShowArmour("Tight"));
                LocomotionLevel = LocomotionLevel.OldRun;
                break;
            case Level.Jump:
                StartCoroutine(ShowArmour("Tight"));
                StartCoroutine(ShowArmour("Leg"));
                LocomotionLevel = LocomotionLevel.Run;
                break;
            case Level.Push:
                StartCoroutine(ShowArmour("Tight"));
                StartCoroutine(ShowArmour("Leg"));
                StartCoroutine(ShowArmour("Arm"));
                StartCoroutine(ShowArmour("Shoulder"));
                LocomotionLevel = LocomotionLevel.Run;
                break;
            case Level.Shoot:
                StartCoroutine(ShowArmour("Tight"));
                StartCoroutine(ShowArmour("Leg"));
                StartCoroutine(ShowArmour("Arm"));
                StartCoroutine(ShowArmour("Shoulder"));
                StartCoroutine(ShowArmour("Chest"));
                LocomotionLevel = LocomotionLevel.Run;
                break;
        }
        SaveLevelStatus();
    }

    public void ResetAllLevels() {
        Level = Level.Walk;
        LocomotionLevel = LocomotionLevel.Walk;
        SaveLevelStatus();
    }

    private void SaveLevelStatus() {
        PlayerPrefs.SetInt("LevelStatus", (int) Level);
    }

    private void LoadLevelStatus() {
        var level = PlayerPrefs.GetInt("LevelStatus", 0);
        if (level == 0) return;
        LevelUp((Level) level);
    }

    private IEnumerator ShowArmour(string name) {
        var armourToShow = AllArmour.Where(t => t.name.Contains(name));
        if (!armourToShow.Any()) yield break;

        var scale = 0f;
        while (scale < 1) {
            scale += Time.deltaTime;
            foreach (var armour in armourToShow) {
                armour.GetChild(0).localScale = Vector3.one * scale;
            }
            yield return null;
        }
    }

#endregion

    private bool ProbabilityChance(float percentage) {
        return new System.Random().Next(0, 1_000_000) <= percentage * 10_000;
    }

}

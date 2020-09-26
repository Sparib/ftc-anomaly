using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Constants")]
    
    public float AccelerationMod;
    public float XAxisSensitivity;
    public float YAxisSensitivity;
    public float DecelerationMod;

    [Space]

    [Header("Pause")]
    public bool pause;

    [Space]

    [Range(0, 90)] public float MaxXAngle = 90f;

    [Space]

    public float MaximumMovementSpeed = 1f;

    private Vector3 _moveSpeed;

    [Space]

    private Vector3 _acceleration;

    private float _rotationX;

    private InputMaster _inputMaster;

    // Start is called before the first frame update
    void Awake() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _moveSpeed = Vector3.zero;
        _inputMaster = new InputMaster();
        _inputMaster.Player.Movement.performed += ctx => HandleMovementInput(ctx);
        _inputMaster.Player.Look.performed += ctx => HandleMouseInput(ctx);
    }

    // Update is called once per frame
    void Update() {
        if (!pause) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _moveSpeed += _acceleration;

            HandleDeceleration(_acceleration);

            if (_moveSpeed.magnitude > MaximumMovementSpeed) {
                _moveSpeed = _moveSpeed.normalized * MaximumMovementSpeed;
            }

            transform.Translate(_moveSpeed);
        } else {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void HandleMovementInput(InputAction.CallbackContext context){
        Debug.Log("Move");
        var acceleration = Vector3.zero;

        var contextVariable = context.ReadValue<Vector2>();
        acceleration.x = contextVariable.x;
        acceleration.z = contextVariable.y;

        _acceleration = acceleration * AccelerationMod;
    }

    private void HandleMouseInput(InputAction.CallbackContext context) {
        if (pause) { return; }
        
        var mouseDelta = context.ReadValue<Vector2>();

        var rotationHorizontal = XAxisSensitivity * mouseDelta.x;
        var rotationVertical = YAxisSensitivity * mouseDelta.y;

        transform.Rotate(Vector3.up, rotationHorizontal, Space.World);

        var rotationY = transform.localEulerAngles.y;

        _rotationX += rotationVertical;
        _rotationX = Mathf.Clamp(_rotationX, -MaxXAngle, MaxXAngle);

        transform.localEulerAngles = new Vector3(-_rotationX, rotationY, 0);
    }

    private void HandleDeceleration(Vector3 acceleration) {
        //deceleration functionality
        if (Mathf.Approximately(Mathf.Abs(acceleration.x), 0)) {
            if (Mathf.Abs(_moveSpeed.x) < DecelerationMod) {
                _moveSpeed.x = 0;
            }
            else {
                _moveSpeed.x -= DecelerationMod * Mathf.Sign(_moveSpeed.x);
            }
        }

        if (Mathf.Approximately(Mathf.Abs(acceleration.y), 0)) {
            if (Mathf.Abs(_moveSpeed.y) < DecelerationMod) {
                _moveSpeed.y = 0;
            }
            else {
                _moveSpeed.y -= DecelerationMod * Mathf.Sign(_moveSpeed.y);
            }
        }

        if (Mathf.Approximately(Mathf.Abs(acceleration.z), 0)) {
            if (Mathf.Abs(_moveSpeed.z) < DecelerationMod) {
                _moveSpeed.z = 0;
            }
            else {
                _moveSpeed.z -= DecelerationMod * Mathf.Sign(_moveSpeed.z);
            }
        }
    }

    private void OnEnable() {
        _inputMaster.Enable();
    }

    private void OnDisable() {
        _inputMaster.Disable();
    }
}

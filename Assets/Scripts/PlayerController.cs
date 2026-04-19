using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    public InputActionAsset actions;
    public Camera playerCamera;
    public Transform arm;
    public float mouseSpeed = 1;
    public Vector2 lookLimits = new Vector2(-60, 60);
    public float walkSpeed = 2;
    public float runSpeed = 6;
    public float jumpHeight = 2;
    public float jumpAngle = 45;
    public float maxInteractDistance = 1.5f;

    InputAction lookAction;
    InputAction moveAction;
    InputAction jumpAction;
    InputAction sprintAction;
    InputAction interactAction;
    float verticalLookAngle = 0;
    Rigidbody rigidBody;
    bool canJump;
    float JumpVelocity => Mathf.Sqrt(2 * Physics.gravity.magnitude * jumpHeight);
    Rigidbody ThingInArm = null;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        rigidBody = GetComponent<Rigidbody>();
        InputActionMap playerInputMap = actions.FindActionMap("Player");
        playerInputMap.Enable();
        lookAction = playerInputMap.FindAction("Look");
        moveAction = playerInputMap.FindAction("Move");
        jumpAction = playerInputMap.FindAction("Jump");
        sprintAction = playerInputMap.FindAction("Sprint");
        interactAction = playerInputMap.FindAction("Interact");
    }

    void Update()
    {
        Vector2 lookValue = lookAction.ReadValue<Vector2>();
        lookValue = lookValue * mouseSpeed;
        verticalLookAngle = Mathf.Clamp(verticalLookAngle + lookValue.y, lookLimits.x, lookLimits.y);
        playerCamera.transform.localRotation = Quaternion.AngleAxis(verticalLookAngle, Vector3.left);
        transform.rotation = Quaternion.AngleAxis(lookValue.x, Vector3.up) * transform.rotation;

        if (interactAction.WasPressedThisFrame())
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hitInfo, maxInteractDistance))
            {
                Interactinator interactinator;
                if (hitInfo.collider.TryGetComponent<Interactinator>(out interactinator))
                    interactinator.Interact(this);
            }
        }
    }

    void FixedUpdate()
    {
        rigidBody.useGravity = !canJump;

        Vector3 resultVelocity = Vector3.up * rigidBody.linearVelocity.y;

        float speed = sprintAction.IsPressed() ? runSpeed : walkSpeed;
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        moveValue *= speed;

        Vector3 forwardVector = transform.rotation * Vector3.forward;
        Vector3 rightVector = transform.rotation * Vector3.right;
        resultVelocity += forwardVector * moveValue.y + rightVector * moveValue.x;

        if (canJump && jumpAction.IsPressed())
            resultVelocity.y = JumpVelocity;

        rigidBody.linearVelocity = resultVelocity;
        canJump = false;
    }

    void OnCollisionStay(Collision collision)
    {
        foreach (var contact in collision.contacts)
            if (Vector3.Dot(contact.normal, Vector3.up) > Mathf.Cos(jumpAngle * Mathf.Deg2Rad))
                canJump = true;
    }
}

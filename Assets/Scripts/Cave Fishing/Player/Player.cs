using Shears;
using Shears.Detection;
using Shears.Input;
using System.Collections;
using UnityEngine;

namespace CaveFishing.Players
{
    public class Player : MonoBehaviour
    {
        private const float JUMP_BUFFER_TIME = 0.25f;

        [Header("Player Components")]
        [SerializeField] private ManagedInputMap inputMap;

        [Header("Controller Components")]
        [SerializeField] private CharacterController controller;
        [SerializeField] private Transform relativeTransform;
        [SerializeField] private AreaDetector3D groundDetector;

        [Header("Settings")]
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float jumpForce = 10f;
        [SerializeField] private float gravity = -9.81f;

        private bool isJumping = false;
        private float yVelocity = 0;

        private readonly Timer jumpBufferTimer = new(JUMP_BUFFER_TIME);
        private IManagedInput movementInput;
        private IManagedInput jumpInput;

        private void Awake()
        {
            inputMap.GetInputs(
                ("Move", i => movementInput = i),
                ("Jump", i => jumpInput = i)
                );

            inputMap.EnableAllInputs();
        }

        private void OnEnable()
        {
            jumpInput.Started += OnJumpInput;
        }

        private void OnDisable()
        {
            jumpInput.Started -= OnJumpInput;
        }

        private void Update()
        {
            ApplyRotation();
            ApplyMovement(movementInput.ReadValue<Vector2>());
            ApplyGravity();
        }

        private void ApplyRotation()
        {
            var rotation = relativeTransform.rotation;
            rotation.x = 0f;
            rotation.z = 0f;

            controller.transform.rotation = rotation;
        }

        private void ApplyMovement(Vector2 input)
        {
            var forward = relativeTransform.forward;
            forward.y = 0f;
            forward.Normalize();

            var right = relativeTransform.right;
            right.y = 0f;
            right.Normalize();

            var movement = (forward * input.y) + (right * input.x);
            movement *= moveSpeed * Time.deltaTime;

            controller.Move(movement);
        }
    
        private void ApplyGravity()
        {
            if (!isJumping && jumpBufferTimer.IsDone)
            {
                if (groundDetector.Detect())
                    yVelocity = -0.5f * Time.deltaTime;
                else
                    yVelocity += (gravity * Time.deltaTime * Time.deltaTime);

                controller.Move(yVelocity * Vector3.up);
            }
        }

        private void OnJumpInput(ManagedInputInfo info)
        {
            if (isJumping || !groundDetector.Detect())
                return;

            Jump();
        }

        private void Jump()
        {
            StartCoroutine(IEJump());
        }

        private IEnumerator IEJump()
        {
            isJumping = true;
            jumpBufferTimer.Start();

            var force = jumpForce;

            do
            {
                if (jumpBufferTimer.IsDone && groundDetector.Detect())
                    break;

                yVelocity = force * Time.deltaTime;

                controller.Move(yVelocity * Vector3.up);
                force += gravity * Time.deltaTime;

                yield return null;
            } while (yVelocity > 0f);

            isJumping = false;
        }
    }
}

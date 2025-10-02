using Shears.Input;
using UnityEngine;

namespace CaveFishing.Games.PongGame
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private ManagedInputProvider inputProvider;
        [SerializeField] private Paddle paddle;
        [SerializeField] private float moveSpeed = 1;

        private IManagedInput moveUpInput;
        private IManagedInput moveDownInput;
        private bool isEnabled = false;

        private void Awake()
        {
            inputProvider.GetInputs(
                ("Move Up", input => moveUpInput = input),
                ("Move Down", input => moveDownInput = input)
            );
        }

        public void Enable()
        {
            if (isEnabled)
                return;

            paddle.SetPosition(0.5f);

            isEnabled = true;
        }

        public void Disable()
        {
            if (!isEnabled)
                return;

            isEnabled = false;
        }

        private void Update()
        {
            if (!isEnabled)
                return;

            if (moveUpInput.IsPressed())
                paddle.MoveUp(moveSpeed * Time.deltaTime);
            else if (moveDownInput.IsPressed())
                paddle.MoveDown(moveSpeed * Time.deltaTime);
        }
    }
}

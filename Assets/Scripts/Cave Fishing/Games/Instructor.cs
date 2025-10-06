using System;
using UnityEngine;

namespace CaveFishing.Games
{
    public class Instructor : MonoBehaviour
    {
        private Action completeCallback;

        public event Action InstructionRequested;

        public void Instruct(Action completeCallback)
        {
            this.completeCallback = completeCallback;

            InstructionRequested?.Invoke();
        }

        public void Complete()
        {
            if (completeCallback == null)
                return;

            completeCallback();
        }
    }
}

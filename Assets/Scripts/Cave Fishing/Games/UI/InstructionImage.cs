using Shears;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CaveFishing.Games.UI
{
    public class InstructionImage : MonoBehaviour
    {
        [SerializeField] private Instructor instructor;
        [SerializeField] private Image[] elements;
        [SerializeField] private float displayTime = 1;

        private void OnEnable()
        {
            instructor.InstructionRequested += OnInstructionRequested;
        }

        private void OnDisable()
        {
            instructor.InstructionRequested -= OnInstructionRequested;
        }

        private void OnInstructionRequested()
        {
            StartCoroutine(IEDisplay());
        }

        private IEnumerator IEDisplay()
        {
            foreach (var element in elements)
                element.enabled = true;

            yield return CoroutineUtil.WaitForSeconds(displayTime);

            foreach (var element in elements)
                element.enabled = false;

            instructor.Complete();
        }
    }
}

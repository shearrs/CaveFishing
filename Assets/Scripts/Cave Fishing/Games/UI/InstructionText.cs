using Shears;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CaveFishing.Games.UI
{
    public class InstructionText : MonoBehaviour
    {
        [SerializeField] private Instructor instructor;
        [SerializeField] private Image[] elements;
        [SerializeField] private TextMeshProUGUI textMesh;
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
            textMesh.enabled = true;

            foreach (var element in elements)
                element.enabled = true;

            yield return CoroutineUtil.WaitForSeconds(displayTime);

            textMesh.enabled = false;

            foreach (var element in elements)
                element.enabled = false;

            instructor.Complete();
        }
    }
}

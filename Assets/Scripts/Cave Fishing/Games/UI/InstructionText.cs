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
        [SerializeField] private Image[] elements;
        [SerializeField] private TextMeshProUGUI textMesh;
        [SerializeField] private float displayTime = 1;

        public void Display(Action completeCallback)
        {
            StartCoroutine(IEDisplay(completeCallback));
        }

        private IEnumerator IEDisplay(Action completeCallback)
        {
            textMesh.enabled = true;

            foreach (var element in elements)
                element.enabled = true;

            yield return CoroutineUtil.WaitForSeconds(displayTime);

            textMesh.enabled = false;

            foreach (var element in elements)
                element.enabled = false;

            completeCallback?.Invoke();
        }
    }
}

using TMPro;
using UnityEngine;

namespace CaveFishing.Games.FishTypingGame.UI
{
    public class KeyboardUI : MonoBehaviour
    {
        [SerializeField] private Keyboard keyboard;
        [SerializeField] private TextMeshProUGUI textMesh;

        private void OnEnable()
        {
            keyboard.InputLettersUpdated += OnInputLettersUpdated;
        }

        private void OnDisable()
        {
            keyboard.InputLettersUpdated -= OnInputLettersUpdated;
        }

        private void OnInputLettersUpdated(string inputLetters)
        {
            textMesh.text = inputLetters;
        }
    }
}

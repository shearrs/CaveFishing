using TMPro;
using UnityEngine;

namespace CaveFishing.Fishing.UI
{
    public class WordleLetter : MonoBehaviour
    {
        public enum FillColor { None, Gray, Yellow, Green }

        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private GameObject grayFill;
        [SerializeField] private GameObject yellowFill;
        [SerializeField] private GameObject greenFill;

        private GameObject currentFill;

        public void SetLetter(string text) => this.text.text = text;

        public void SetFill(FillColor color)
        {
            if (currentFill != null)
                currentFill.SetActive(false);

            switch (color)
            {
                case FillColor.Gray:
                    currentFill = grayFill;
                    break;
                case FillColor.Yellow:
                    currentFill = yellowFill;
                    break;
                case FillColor.Green:
                    currentFill = greenFill;
                    break;
                case FillColor.None:
                    currentFill = null;
                    break;
            }

            if (currentFill != null)
                currentFill.SetActive(true);
        }
    }
}

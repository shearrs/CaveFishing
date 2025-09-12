using System.Collections.Generic;
using UnityEngine;

namespace CaveFishing.Fishing.UI
{
    public class WordleWord : MonoBehaviour
    {
        [SerializeField] private List<WordleLetter> letters;

        public void SetLetter(int index, string letter)
        {
            letters[index].SetLetter(letter);
        }
    }
}

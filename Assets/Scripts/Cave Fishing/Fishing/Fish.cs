using CaveFishing.Games;
using UnityEngine;

namespace CaveFishing.Fishing
{
    public class Fish : MonoBehaviour
    {
        [SerializeField] private MinigameType type;

        public MinigameType MinigameType => type;
    }
}

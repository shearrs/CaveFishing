using CaveFishing.Games;
using UnityEngine;

namespace CaveFishing.Fishing
{
    public class Fish : MonoBehaviour
    {
        [SerializeField] private MinigameType minigameType;

        public MinigameType MinigameType => minigameType;
    }
}

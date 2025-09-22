using Shears;
using System.Collections;
using UnityEngine;

namespace CaveFishing.Games.QuickClickGame
{
    public class QuickClick : MonoBehaviour
    {
        [SerializeField] private ClickTarget targetPrefab;
        [SerializeField] private float gameDuration;
        [SerializeField] private float minSpawnTime;
        [SerializeField] private float maxSpawnTime;

        private readonly Timer gameTimer = new();
        private readonly Timer buttonTimer = new();

        public void Awake()
        {
            Enable();
        }

        public void Enable()
        {
            gameTimer.Start(gameDuration);
            
            StartCoroutine(IESpawnButtons());
        }

        public void Disable() 
        { 
            StopAllCoroutines();
        }

        private IEnumerator IESpawnButtons()
        {
            buttonTimer.Start(Random.Range(minSpawnTime, maxSpawnTime));

            while (!gameTimer.IsDone)
            {
                while(!buttonTimer.IsDone)
                    yield return null;

                SpawnButton();
                buttonTimer.Start(Random.Range(minSpawnTime, maxSpawnTime));

                yield return null;
            }
        }

        private void SpawnButton()
        {
            float x = Random.Range(.2f, .8f);
            float y = Random.Range(.2f, .8f);

            var button = Instantiate(targetPrefab, transform);
            button.Position = new Vector2(x,y);
        }
    }
}

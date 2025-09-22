using Shears;
using Shears.Signals;
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
        [SerializeField, ReadOnly] private int count;

        private readonly Timer gameTimer = new();
        private readonly Timer buttonTimer = new();

        private void OnValidate()
        {
            if(minSpawnTime > maxSpawnTime)
            {
                maxSpawnTime = minSpawnTime;
            }
        }

        public void Awake()
        {
            Enable();
            SignalShuttle.Register<TargetClickedSignal>(OnTargetClicked);
        }

        public void Enable()
        {
            count = 0;
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

        private void OnTargetClicked(TargetClickedSignal signal)
        {
            count++;
        }
    }
}

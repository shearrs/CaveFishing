using CaveFishing.Audio;
using Shears;
using Shears.Signals;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CaveFishing.Games.QuickClickGame
{
    public class QuickClick : Minigame
    {
        [SerializeField] private ClickTarget targetPrefab;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private float gameDuration;
        [SerializeField] private float minSpawnTime;
        [SerializeField] private float maxSpawnTime;
        [SerializeField, ReadOnly] private int count;

        private readonly Timer gameTimer = new();
        private readonly Timer buttonTimer = new();
        private readonly List<ClickTarget> targets = new();
        private bool won = false;

        public event Action Enabled;
        public event Action Disabled;

        private void OnValidate()
        {
            if(minSpawnTime > maxSpawnTime)
                maxSpawnTime = minSpawnTime;
        }

        public void Start()
        {
            SignalShuttle.Register<TargetClickedSignal>(OnTargetClicked);
        }

        public override void Enable()
        {
            won = false;

            Enabled?.Invoke();
            SignalShuttle.Emit(new GameEnabledSignal(MinigameType.QuickClick));
        }

        public override void Disable() 
        { 
            StopAllCoroutines();

            won = targets.Count == 0;

            foreach (var target in targets)
                Destroy(target.gameObject);

            targets.Clear();

            Disabled?.Invoke();
        }

        public void StartGame()
        {
            count = 0;
            gameTimer.Start(gameDuration);

            StartCoroutine(IESpawnButtons());
        }

        public void EndGame()
        {
            SignalShuttle.Emit(new GameDisabledSignal(MinigameType.QuickClick));

            if (won)
                SignalShuttle.Emit(new GameWonSignal());
            else
                SignalShuttle.Emit(new GameLostSignal());
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

            yield return CoroutineUtil.WaitForSeconds(1.5f);

            Disable();
        }

        private void SpawnButton()
        {
            float x = Random.Range(.2f, .8f);
            float y = Random.Range(.2f, .8f);

            var button = Instantiate(targetPrefab, transform);
            button.Position = new Vector2(x,y);

            targets.Add(button);
        }

        private void OnTargetClicked(TargetClickedSignal signal)
        {
            targets.Remove(signal.Target);

            audioSource.pitch = Random.Range(0.85f, 1.15f);
            audioSource.Play();
            count++;
        }
    }
}

using Shears.Signals;
using System;
using UnityEngine;

namespace CaveFishing.Games.QuickClickGame
{
    public class ClickTarget : MonoBehaviour
    {
        private Vector2 position;

        public Vector2 Position 
        { 
            get => position; 
            set
            {
                position = value;
                PositionSet?.Invoke();
            } 
        }

        public event Action PositionSet;

        public void OnClicked()
        {
            SignalShuttle.Emit(new TargetClickedSignal());
            Destroy(gameObject);
        }
    }
}
 
using Shears.StateMachineGraphs;
using UnityEngine;

namespace CaveFishing.Players
{
    [StateMenuItem("Player State/Player Control State")]
    public class PlayerControlState : PlayerState<PlayerCharacter>
    {
        [SerializeField] private PlayerCharacter character;

        protected override void Inject(PlayerCharacter dependency)
        {
            character = dependency;
        }

        protected override void OnEnter()
        {
        }

        protected override void OnExit()
        {
        }

        protected override void OnUpdate()
        {
            character.UpdateCharacter();
        }
    }
}

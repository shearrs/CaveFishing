using UnityEngine;

namespace CaveFishing.Fishing
{
    public class DisableState : FishingRodState
    {
        private readonly Bobber bobber;

        public DisableState(FishingRod fishingRod, Bobber bobber) : base(fishingRod)
        {
            Name = "Disable State";
            this.bobber = bobber;
        }

        protected override void OnEnter()
        {
            if (bobber.gameObject.activeSelf)
                bobber.gameObject.SetActive(false);

            FishingRod.gameObject.SetActive(false);
        }

        protected override void OnExit()
        {
        }

        protected override void OnUpdate()
        {
        }
    }
}

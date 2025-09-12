using Shears.StateMachineGraphs;
using UnityEngine;

namespace CaveFishing.Players
{
    public abstract class PlayerState : State {}
    public abstract class PlayerState<T> : State<T> {}
    public abstract class PlayerState<T1, T2> : State<T1, T2> {}
    public abstract class PlayerState<T1, T2, T3> : State<T1, T2, T3> {}
}

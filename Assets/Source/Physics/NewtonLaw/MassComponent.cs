using NB.ECS.Wrapper;

namespace NB.SpaceGame.Physics.NewtonLaw
{
    public class MassComponent: ComponentWrapper<Mass> {}

    [System.Serializable]
    public struct Mass
    {
        public float value;
    }
}

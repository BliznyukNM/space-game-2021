#if !ECS_DISABLE_GLOBAL_SPACE

namespace NB.ECS.Wrapper
{
    public class GlobalSpace: ISpace
    {
        public readonly static GlobalSpace Ref = new GlobalSpace();

        public World World { get; }
        public Systems Systems { get; }

        private GlobalSpace()
        {
            World = new World();
            Systems = new Systems(World);
        }
    }
}

#endif

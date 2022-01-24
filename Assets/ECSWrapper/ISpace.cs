namespace NB.ECS.Wrapper
{
    public interface ISpace
    {
        World World { get; }
        Systems Systems { get; }
    }
}

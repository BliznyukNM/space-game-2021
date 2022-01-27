namespace NB.ECS.Wrapper
{
    public interface IComponentRegistrator
    {
        public void Register(int entity, World world);
    }
}

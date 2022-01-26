using System.Collections.Generic;

namespace NB.ECS
{
    public interface ISystem { }

    public interface IStartSystem: ISystem
    {
        void OnStart(World world);
    }

    public interface IUpdateSystem: ISystem
    {
        void OnUpdate(World world, float delta);
    }

    public interface IPhysicsUpdateSystem: ISystem
    {
        void OnPhysicsUpdate(World world, float delta);
    }

    public interface IStopSystem: ISystem
    {
        void OnStop(World world);
    }

    public class Systems
    {
        public bool IsRunning { get; private set; }

        private World world;
        private readonly List<ISystem> systems;

        public Systems(World world)
        {
            this.world = world;
            systems = new List<ISystem>(64);
        }

        public Systems Register(ISystem system)
        {
            systems.Add(system);

            if (system is IStartSystem startSystem && IsRunning)
            {
                startSystem.OnStart(world);
            }

            return this;
        }

        public void Start()
        {
            systems.ForEach(system =>
            {
                if (system is IStartSystem startSystem)
                {
                    startSystem.OnStart(world);
                }
            });
            IsRunning = true;
        }

        public void Update(float delta)
        {
            systems.ForEach(system =>
            {
                if (system is IUpdateSystem updateSystem)
                {
                    updateSystem.OnUpdate(world, delta);
                }
            });
        }

        public void PhysicsUpdate(float delta)
        {
            systems.ForEach(system =>
            {
                if (system is IPhysicsUpdateSystem updateSystem)
                {
                    updateSystem.OnPhysicsUpdate(world, delta);
                }
            });
        }

        public void Stop()
        {
            systems.ForEach(system =>
            {
                if (system is IStopSystem stopSystem)
                {
                    stopSystem.OnStop(world);
                }
            });
            IsRunning = false;
        }
    }
}

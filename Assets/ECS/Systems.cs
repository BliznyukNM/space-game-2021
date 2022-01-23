using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NB.ECS
{
    public interface ISystem { }

    public interface IStartSystem: ISystem
    {
        void Start(World world);
    }

    public interface IUpdateSystem: ISystem
    {
        void Update(World world);
    }

    public interface IStopSystem: ISystem
    {
        void Stop(World world);
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
                startSystem.Start(world);
            }

            return this;
        }

        public void Start()
        {
            systems.ForEach(system =>
            {
                if (system is IStartSystem startSystem)
                {
                    startSystem.Start(world);
                }
            });
            IsRunning = true;
        }

        public void Update()
        {
            systems.ForEach(system =>
            {
                if (system is IUpdateSystem updateSystem)
                {
                    updateSystem.Update(world);
                }
            });
        }

        public void Stop()
        {
            systems.ForEach(system =>
            {
                if (system is IStopSystem stopSystem)
                {
                    stopSystem.Stop(world);
                }
            });
            IsRunning = false;
        }
    }
}

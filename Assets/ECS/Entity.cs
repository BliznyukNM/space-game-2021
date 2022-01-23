using UnityEngine;

namespace NB.ECS
{
    public static class Entity
    {
        public static void AddComponent<T>(this in int entity, in World world, in T component) where T: struct
        {
            var pool = world.GetPool<T>();
            pool.Add(entity, component);
        }

        public static ref T GetComponent<T>(this in int entity, in World world) where T: struct
        {
            var pool = world.GetPool<T>();
            return ref pool.Get(entity);
        }
        
        public static void RemoveComponent<T>(this in int entity, in World world) where T: struct
        {
            var pool = world.GetPool<T>();
            pool.Remove(entity);
        }

        public static bool HasComponent<T>(this in int entity, in World world) where T: struct
        {
            var pool = world.GetPool<T>();
            return pool.Has(entity);
        }
    }
}

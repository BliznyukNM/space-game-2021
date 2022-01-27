namespace NB.ECS
{
    public static class Entity
    {
        public static int AddComponent<T>(this int entity, World world, in T component) where T: struct
        {
            var pool = world.GetPool<T>();
            return pool.Add(entity, component);
        }

        public static ref T GetComponent<T>(this int entity, World world) where T: struct
        {
            var pool = world.GetPool<T>();
            return ref pool.Get(entity);
        }
        
        public static bool RemoveComponent<T>(this int entity, World world) where T: struct
        {
            var pool = world.GetPool<T>();
            return pool.Remove(entity);
        }

        public static bool HasComponent<T>(this int entity, World world) where T: struct
        {
            var pool = world.GetPool<T>();
            return pool.Has(entity);
        }

        public static bool HasAllComponents(this int entity, World world, params System.Type[] types)
        {
            foreach(var type in types)
            {
                var pool = world.GetPool(type);
                if (pool == null || !pool.Has(entity))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool HasAnyComponent(this int entity, World world, params System.Type[] types)
        {
            foreach(var type in types)
            {
                var pool = world.GetPool(type);
                if (pool.Has(entity))
                {
                    return true;
                }
            }

            return false;
        }
    }
}

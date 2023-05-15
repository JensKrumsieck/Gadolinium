namespace Gadolinium.ECS;

public class World
{
    private readonly List<Entity> _entities = new();
    private readonly Dictionary<Type, IStorage> _storages = new();
    public Entity CreateEntity()
    {
        var e = new Entity(_entities.Count);
        _entities.Add(e);
        return e;
    }

    public void AddComponent<T>(Entity entity, T component) where T : struct
    {
        if(!_storages.ContainsKey(typeof(T))) _storages.Add(typeof(T), new ComponentCache<T>());
        ((ComponentCache<T>)_storages[typeof(T)]).AddComponent(entity.Id, component);
    }

    public void RemoveComponent<T>(Entity entity) where T : struct
    {
        if (HasStorage<T>(out var cache)) 
            cache!.RemoveComponent(entity.Id);
    }

    private bool HasStorage<T>(out ComponentCache<T>? cache)
    {
        var result = _storages.TryGetValue(typeof(T), out var storage) && storage is ComponentCache<T>;
        cache = result ? (ComponentCache<T>)storage! : null;
        return result;
    }

    public ref T GetComponent<T>(Entity entity) where T : struct
    {
        if (HasStorage<T>(out var cache))
            return ref cache!.GetComponent(entity.Id);
        throw new ArgumentException($"Component does not exist on Entity {entity.Id}");
    }

    public bool TryGetComponent<T>(Entity entity, out T? component) where T : struct
    {
        component = null;
        if (!HasStorage<T>(out var cache) || cache!.HasComponent(entity.Id)) return false;
        component = cache.GetComponent(entity.Id);
        return true;
    }

    public Span<T> GetComponents<T>() where T : struct => HasStorage<T>(out var cache) ? cache!.GetComponents() : new Span<T>();
    public void DespawnEntity(Entity entity)
    {
        foreach (var storage in _storages.Values) storage.RemoveComponent(entity.Id);
        _entities.Remove(entity);
    }
}
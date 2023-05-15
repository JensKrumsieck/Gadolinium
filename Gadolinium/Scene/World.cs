using System.Collections;

namespace Gadolinium.Scene;

public sealed class World : IEnumerable<Entity>
{
    private readonly List<Entity> _entities = new();
    private readonly Dictionary<Type, IStorage> _storages = new();
    private readonly List<ISystem> _systems = new();

    public Entity CreateEntity()
    {
        var e = new Entity(_entities.Count);
        _entities.Add(e);
        return e;
    }

    public void DestroyEntity(Entity entity)
    {
        foreach (var storage in _storages.Values) storage.RemoveComponent(entity.Id);
        _entities.Remove(entity);
    }

    public void AddComponent<T>(Entity entity) where T : struct
    {
        var component = new T();
        AddComponent(entity, component);
    }

    public void AddComponent<T>(Entity entity, T component) where T : struct
    {
        if (!_storages.ContainsKey(typeof(T))) _storages.Add(typeof(T), new ComponentCache<T>());
        ((ComponentCache<T>) _storages[typeof(T)]).AddComponent(entity.Id, component);
    }

    public void RemoveComponent<T>(Entity entity) where T : struct
    {
        if (HasComponentStorage<T>(out var cache))
            cache!.RemoveComponent(entity.Id);
    }

    public ref T GetComponent<T>(Entity entity) where T : struct
    {
        if (HasComponentStorage<T>(out var cache))
            return ref cache!.GetComponent(entity.Id);
        throw new ArgumentException($"Component does not exist on Entity {entity.Id}");
    }

    public bool TryGetComponent<T>(Entity entity, out T? component) where T : struct
    {
        component = null;
        if (!HasComponentStorage<T>(out var cache) || cache!.HasComponent(entity.Id)) return false;
        component = cache.GetComponent(entity.Id);
        return true;
    }

    public Span<T> GetComponents<T>() where T : struct =>
        HasComponentStorage<T>(out var cache) ? cache!.GetComponents() : new Span<T>();

    public World AddSystem<TSystem>() where TSystem : ISystem, new()
    {
        var system = new TSystem();
        return AddSystem(system);
    }

    public World AddSystem(ISystem system)
    {
        _systems.Add(system);
        return this;
    }

    public void InitializeSystems()
    {
        foreach (var system in _systems)
            system.Init(this);
    }

    public void ExecuteSystems(float deltaTime = 0)
    {
        foreach (var system in _systems)
            system.Execute(this, deltaTime);
    }

    private bool HasComponentStorage<T>(out ComponentCache<T>? cache)
    {
        var result = _storages.TryGetValue(typeof(T), out var storage) && storage is ComponentCache<T>;
        cache = result ? (ComponentCache<T>) storage! : null;
        return result;
    }

    public IEnumerator<Entity> GetEnumerator() => _entities.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Gadolinium.ECS;

internal class ComponentCache<T> : IStorage
{
    private readonly Dictionary<int, int> _componentPositionalData = new();
    private readonly List<T> _componentData = new();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ref T GetComponent(int entityId) =>
        ref CollectionsMarshal.AsSpan(_componentData)[GetIndex(entityId)];

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AddComponent(int entityId, T component)
    {
        _componentData.Add(component);
        _componentPositionalData.Add(entityId, _componentData.Count - 1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void RemoveComponent(int entityId)
    {
        _componentData.RemoveAt(GetIndex(entityId));
        _componentPositionalData.Remove(entityId);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool HasComponent(int entityId) => _componentPositionalData.ContainsKey(entityId);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private int GetIndex(int entityId) => _componentPositionalData[entityId];
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Span<T> GetComponents() => CollectionsMarshal.AsSpan(_componentData);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Clear() => _componentData.Clear();
}
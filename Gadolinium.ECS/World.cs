namespace Gadolinium.ECS;

public class World
{
    private Dictionary<Type, List<IComponentData>> _components = new();
    private List<Entity> _entities = new();
}
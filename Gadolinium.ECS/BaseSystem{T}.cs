namespace Gadolinium.ECS;

public abstract class BaseSystem<T> where T : IComponentData
{
    public abstract void Execute(float deltaTime);
}
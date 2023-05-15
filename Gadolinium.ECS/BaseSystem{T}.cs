namespace Gadolinium.ECS;

public abstract class BaseSystem<T> where T : struct
{
    protected World World;
    protected BaseSystem(World w) => World = w;

    public abstract void Execute(float deltaTime);
}
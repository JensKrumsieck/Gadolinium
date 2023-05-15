namespace Gadolinium.Scene;

public abstract class BaseSystem<T> : ISystem where T : struct
{
    public virtual void Init(World w)
    {
    }
    public abstract void Execute(World w, float deltaTime = 0);
}

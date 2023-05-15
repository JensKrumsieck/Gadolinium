namespace Gadolinium.Scene;

public abstract class BaseSystem : ISystem
{
    public virtual void Init(World w){  }
    public abstract void Execute(World w, float deltaTime = 0);
}

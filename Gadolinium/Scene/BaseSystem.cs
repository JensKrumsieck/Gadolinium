namespace Gadolinium.Scene;

public abstract class BaseSystem : ISystem
{
    public virtual void Init(World w){  }
    public  virtual void Execute(World w, float deltaTime){}
    public virtual void Execute(World w) {}
}

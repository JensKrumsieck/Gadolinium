namespace Gadolinium.Scene;

public interface ISystem
{
    void Init(World w);
    void Execute(World w, float deltaTime);
    void Draw(World w);
}

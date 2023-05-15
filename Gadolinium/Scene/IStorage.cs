namespace Gadolinium.Scene;

internal interface IStorage
{
    void Clear();
    void RemoveComponent(int entityId);
}

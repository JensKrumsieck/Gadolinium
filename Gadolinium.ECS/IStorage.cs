namespace Gadolinium.ECS;

internal interface IStorage
{
    void Clear();
    void RemoveComponent(int entityId);
}
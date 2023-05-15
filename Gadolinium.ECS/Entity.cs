namespace Gadolinium.ECS;

public struct Entity
{
    public int Id { get; }
    public Entity(int id) => Id = id;
}
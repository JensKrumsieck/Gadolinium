using FluentAssertions;
using Gadolinium.Scene;

namespace Gadolinium.Test;

public class ECSTests
{
    private struct TestComponent
    {
        public required int Number;
    }

    private class TestSystem : BaseSystem<TestComponent>
    {
        public override void Execute(World w, float deltaTime = 0)
        {
            var components = w.GetComponents<TestComponent>();
            for (var i = 0; i < components.Length; i++)
            {
                components[i].Number *= 2;
            }
        }
    }

    [Fact]
    public void World_Can_Create_Entities()
    {
        var w = new World();
        var e = w.CreateEntity();
        e.Id.Should().Be(0);
        e.Should().BeOfType(typeof(Entity));
    }

    [Fact]
    public void World_Can_Add_Component_To_Entity()
    {
        var w = new World();
        var e = w.CreateEntity();
        w.AddComponent<TestComponent>(e);
        w.GetComponents<TestComponent>().Length.Should().Be(1);
        w.GetComponent<TestComponent>(e).Number.Should().Be(0);
    }

    [Fact]
    public void Entity_Throws_If_Component_Does_Not_Exist()
    {
        var w = new World();
        var e = w.CreateEntity();
        Assert.Throws<ArgumentException>(() => w.GetComponent<TestComponent>(e));
    }

    [Fact]
    public void Entity_TryGetComponent_Returns_False()
    {
        var w = new World();
        var e = w.CreateEntity();
        var result = w.TryGetComponent<TestComponent>(e, out var component);
        result.Should().BeFalse();
        component.Should().BeNull();
    }

    [Fact]
    public void Entity_Component_Can_Be_Altered()
    {
        var w = new World();
        var e = w.CreateEntity();
        var tc = new TestComponent {Number = 1};
        w.AddComponent(e, tc);
        ref var compo = ref w.GetComponent<TestComponent>(e);
        compo.Number = 2;
        w.GetComponent<TestComponent>(e).Number.Should().Be(2);
    }

    [Fact]
    public void Entity_Component_Can_Be_Removed()
    {
        var w = new World();
        var e = w.CreateEntity();
        var tc = new TestComponent {Number = 1};
        w.AddComponent(e, tc);
        w.GetComponents<TestComponent>().Length.Should().Be(1);
        w.RemoveComponent<TestComponent>(e);
        w.GetComponents<TestComponent>().Length.Should().Be(0);
    }

    [Fact]
    public void Entity_Component_IsUnique()
    {
        var w = new World();
        var e = w.CreateEntity();
        var tc1 = new TestComponent {Number = 1};
        var tc2 = new TestComponent {Number = 2};
        w.AddComponent(e, tc1);
        Assert.Throws<ArgumentException>(() => w.AddComponent(e, tc2));
    }

    [Fact]
    public void World_Can_DespawnEntity()
    {
        var w = new World();
        var e = w.CreateEntity();
        var tc1 = new TestComponent {Number = 1};
        w.AddComponent(e, tc1);
        w.GetComponents<TestComponent>().Length.Should().Be(1);
        w.DestroyEntity(e);
        w.GetComponents<TestComponent>().Length.Should().Be(0);
    }

    [Fact]
    public void World_Can_Spawn_Massive_Amounts()
    {
        var w = new World();
        for (var i = 0; i < 10000; i++)
        {
            var e = w.CreateEntity();
            w.AddComponent(e, new TestComponent {Number = new Random().Next(0, 1000)});
        }

        w.GetComponents<TestComponent>().Length.Should().Be(10000);
    }

    [Fact]
    public void World_Can_Execute_Systems()
    {
        var w = new World();
        var e = w.CreateEntity();
        w.AddSystem<TestSystem>().AddComponent(e, new TestComponent {Number = 1});
        w.InitializeSystems();
        w.ExecuteSystems();
        w.GetComponent<TestComponent>(e).Number.Should().Be(2);
    }
}

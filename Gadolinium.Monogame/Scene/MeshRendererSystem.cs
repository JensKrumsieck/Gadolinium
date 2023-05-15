using Gadolinium.Scene;

namespace Gadolinium.Monogame.Scene;

public class MeshRendererSystem : BaseSystem
{
    public override void Execute(World w, float deltaTime = 0)
    {
        var components = w.GetComponents<MeshComponent>();
        foreach (var component in components)
        {
            component.Mesh.Device.SetVertexBuffer(component.Mesh.VertexBuffer);
            component.Mesh.Device.Indices = component.Mesh.IndexBuffer;
        }
    }
}

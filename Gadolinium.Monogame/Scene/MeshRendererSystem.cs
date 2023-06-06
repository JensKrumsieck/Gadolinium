using Gadolinium.Scene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gadolinium.Monogame.Scene;

public class MeshRendererSystem : BaseSystem
{
    private Entity _cameraEntity;
    public MeshRendererSystem(Entity cameraEntity) => _cameraEntity = cameraEntity;
    
    public override void Draw(World w)
    {
        var components = w.GetComponents<MeshComponent>();
        foreach (var component in components)
        {
            component.Mesh.Device.SetVertexBuffer(component.Mesh.VertexBuffer);
            component.Mesh.Device.Indices = component.Mesh.IndexBuffer;
            var cameraComponent = w.GetComponent<CameraComponent>(_cameraEntity);
            var cameraTransform = w.GetComponent<TransformComponent>(_cameraEntity);
            component.Material.Effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(cameraComponent.FieldOfView), 
                cameraComponent.AspectRatio, cameraComponent.NearPlaneDistance, cameraComponent.FarPlaneDistance);
            
            component.Material.Effect.View = Matrix.CreateLookAt(cameraTransform.Position, Vector3.Zero, Vector3.Up);
            
            foreach (var pass in  component.Material.Effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                component.Mesh.Device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, component.Mesh.IndexBuffer.IndexCount / 3);
            }

        }
    }
}

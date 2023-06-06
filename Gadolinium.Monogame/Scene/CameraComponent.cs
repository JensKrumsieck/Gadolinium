namespace Gadolinium.Monogame.Scene;

public struct CameraComponent
{
    public float AspectRatio  = 1f;
    public float FieldOfView  = 60f;
    public float NearPlaneDistance  = .1f;
    public float FarPlaneDistance  = 1000f;
    public CameraComponent() {  }
}
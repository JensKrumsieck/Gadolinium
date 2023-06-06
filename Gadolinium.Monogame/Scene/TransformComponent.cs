using Microsoft.Xna.Framework;

namespace Gadolinium.Monogame.Scene;

public struct TransformComponent
{
    public Vector3 Position;
    public Quaternion Rotation;
    public Vector3 Scale;

    public Matrix WorldMatrix => Matrix.CreateScale(Scale)
                                 * Matrix.CreateFromQuaternion(Rotation) *
                                 Matrix.CreateTranslation(Position);
}
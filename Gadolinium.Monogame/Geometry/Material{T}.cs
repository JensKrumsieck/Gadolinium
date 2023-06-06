using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Gadolinium.Monogame.Geometry;

public class Material<T> where T : Effect
{
    public Material(T effect) => Effect = effect;
    public T Effect { get; set; }
}
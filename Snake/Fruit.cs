using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    class Fruit : Sprite
    {
        public Fruit(Texture2D texture, Vector2 position, Direction direction, Rectangle screen) : base
            (texture, position, direction, screen)
        {

        }
    }
}

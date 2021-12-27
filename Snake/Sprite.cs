using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Snake
{
    class Sprite
    {
        protected Texture2D texture;
        protected Rectangle screen;
        protected Vector2 position;

        public Vector2 Position 
        {
            get { return position;  }
            set { position = value;  }
        }

        protected Direction direction;

        public Direction Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        public Rectangle SpriteBox { get { return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height); } }
        
        public Sprite(Texture2D texture, Vector2 position, Direction direction, Rectangle screen)
        {
            this.texture = texture;
            this.position = position;
            this.direction = direction;
            this.screen = screen;
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}

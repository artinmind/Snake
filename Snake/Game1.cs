using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Snake 
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        Rectangle screen;
        Random rnd;
        Fruit fruit;
        //SnakePart snakePart;
        SnakePart head, tail;
        List<SnakePart> snakeParts;

        int screenWidth = 400;
        int screenHeight = 400;
        public static int snakePartSize = 20;

        float timer = 0;
        float delay = 120;

        bool isGameOver = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            screen = new Rectangle(0, 0, screenWidth, screenHeight);
            rnd = new Random();
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            fruit = new Fruit(Content.Load<Texture2D>("Fruit"), new Vector2(snakePartSize * rnd.Next(0, screenWidth / snakePartSize), snakePartSize * rnd.Next(0, screenHeight /snakePartSize)), Direction.None, screen);
            
            snakeParts = new List<SnakePart>();

            head = new SnakePart(Content.Load<Texture2D>("SnakePart"), new Vector2(snakePartSize * rnd.Next(0, (screenWidth / snakePartSize)), snakePartSize * rnd.Next(0, (screenHeight / snakePartSize))), Direction.Right, screen);

            snakeParts.Add(head);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (!isGameOver)
            {
                timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (timer > delay)
                {
                    timer = 0;

                    foreach (SnakePart s in snakeParts)
                    {
                        s.Update(gameTime);
                    }

                    for (int i = snakeParts.Count - 1; i > 0; i--)
                    {
                        snakeParts[i].Direction = snakeParts[i - 1].Direction;
                    }

                    snakeParts[0].InputKeyBoard();

                    if (snakeParts[0].SpriteBox.Intersects(fruit.SpriteBox))
                    {
                        fruit.Position = new Vector2(snakePartSize * rnd.Next(0, screenWidth / snakePartSize), snakePartSize * rnd.Next(0, screenHeight / snakePartSize));
                        for(int i = 1; i < snakeParts.Count; i++)
                        {
                            if(fruit.Position.X == snakeParts[i].Position.X && fruit.Position.Y == snakeParts[i].Position.Y)
                                fruit.Position = new Vector2(snakePartSize * rnd.Next(0, screenWidth / snakePartSize), snakePartSize * rnd.Next(0, screenHeight / snakePartSize));
                        }

                        tail = new SnakePart(Content.Load<Texture2D>("SnakePart"), new Vector2(snakeParts[snakeParts.Count - 1].Position.X, snakeParts[snakeParts.Count - 1].Position.Y), snakeParts[snakeParts.Count - 1].Direction, screen);

                        switch (snakeParts[snakeParts.Count - 1].Direction)
                        {
                            case Direction.Up:
                                tail.Position = new Vector2(tail.Position.X, tail.Position.Y + snakePartSize);
                                break;
                            case Direction.Down:
                                tail.Position = new Vector2(tail.Position.X, tail.Position.Y - snakePartSize);
                                break;
                            case Direction.Left:
                                tail.Position = new Vector2(tail.Position.X + snakePartSize, tail.Position.Y);
                                break;
                            case Direction.Right:
                                tail.Position = new Vector2(tail.Position.X - snakePartSize, tail.Position.Y);
                                break;
                            default:
                                break;
                        }

                        snakeParts.Add(tail);
                    }
                    for (int i = 1; i < snakeParts.Count; i++)
                    {
                        if (snakeParts[0].SpriteBox.Intersects(snakeParts[i].SpriteBox))
                            isGameOver = true;
                    }
                    if (isGameOver)
                        Exit();
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            fruit.Draw(spriteBatch);
            foreach (SnakePart s in snakeParts)
            {
                s.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

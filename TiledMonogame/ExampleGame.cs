using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;
using Squared.Tiled;


namespace TiledMonogame
{
    /// <summary>
    /// An example of a tilemap-based game using a map created in the Tiled editor
    /// Based on work done by Kevin Gadd, Zach Musgrave, and Esa Karjalainen 
    /// </summary>
    public class ExampleGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Map _map;
        private Vector2 _viewportPosition;

        /// <summary>
        /// Constructs a new tilemap ExampleGame
        /// </summary>
        public ExampleGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Initalizes the ExampleGame 
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// Loads the Content for the ExampleGame
        /// </summary>
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _map = Map.Load(Path.Combine(Content.RootDirectory, "MapTest.tmx"), Content);
            _map.ObjectGroups["events"].Objects["hero"].Texture = Content.Load<Texture2D>("hero");
        }

        /// <summary>
        /// Updates the ExampleGame
        /// </summary>
        /// <param name="gameTime">The current GameTime</param>
        protected override void Update(GameTime gameTime)
        {
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            KeyboardState keyState = Keyboard.GetState();
            float scrollx = 0, scrolly = 0;

            if (keyState.IsKeyDown(Keys.Left))
                scrollx = -1;
            if (keyState.IsKeyDown(Keys.Right))
                scrollx = 1;
            if (keyState.IsKeyDown(Keys.Up))
                scrolly = 1;
            if (keyState.IsKeyDown(Keys.Down))
                scrolly = -1;

            scrollx += gamePadState.ThumbSticks.Left.X;
            scrolly += gamePadState.ThumbSticks.Left.Y;

            if (gamePadState.IsButtonDown(Buttons.Back) || keyState.IsKeyDown(Keys.Escape))
                this.Exit();

            float scrollSpeed = 8.0f;

            _map.ObjectGroups["events"].Objects["hero"].X += (int)(scrollx * scrollSpeed);
            _map.ObjectGroups["events"].Objects["hero"].Y -= (int)(scrolly * scrollSpeed);
            _map.ObjectGroups["events"].Objects["hero"].Width = 100;
            _map.Layers["Layer 1"].Opacity = (float)(Math.Cos(Math.PI * (gameTime.TotalGameTime.Milliseconds * 4) / 10000));

            base.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the ExampleGame
        /// </summary>
        /// <param name="gameTime">The current GameTime</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _map.Draw(_spriteBatch, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), _viewportPosition);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

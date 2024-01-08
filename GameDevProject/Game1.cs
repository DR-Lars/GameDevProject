using GameDevProject.GameObject;
using GameDevProject.Input;
using GameDevProject.Level;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameDevProject
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _textureNinja;
        private Texture2D _textureShroom;
        private Rectangle _actualScreenRectangle;
        private RenderTarget2D _nativeRenderTarget;
        private int _screenWidth, _screenHeight;
        private Ninja _ninja;
        private Shroom _shroom;
        private Level1 _level1;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _screenHeight = 1080;
            _screenWidth = (_screenHeight / 9) * 16;
            _graphics.PreferredBackBufferWidth = _screenWidth;
            _graphics.PreferredBackBufferHeight = _screenHeight;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here 
            _nativeRenderTarget = new RenderTarget2D(GraphicsDevice, 256, 144);
            _actualScreenRectangle = new Rectangle(0,0,_screenWidth,_screenHeight);
            base.Initialize();
            _ninja = new Ninja(_textureNinja, new KeyboardReader());
            _shroom = new Shroom(_textureShroom, new StandardNPC(), _ninja);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _textureNinja = Content.Load<Texture2D>("ninjaWalk");
            _textureShroom = Content.Load<Texture2D>("shroomWalk");
            _level1 = new Level1(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic 
            _shroom.Update(gameTime);
            _ninja.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(_nativeRenderTarget);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _level1.Draw(_spriteBatch);
            _shroom.Draw(_spriteBatch);
            _ninja.Draw(_spriteBatch);
            _spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _spriteBatch.Draw(_nativeRenderTarget, _actualScreenRectangle, Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
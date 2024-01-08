using GameDevProject.GameObject;
using GameDevProject.Input;
using GameDevProject.Level;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GameDevProject
{
    public class Game1 : Game
    {
        private enum GameState { MainMenu, Playing, GameOver, Won, LevelSelect }

        private GameState _gameState = GameState.MainMenu;
        private int currentLevel = 1;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _textureNinja;
        private Texture2D _textureShroom;
        private Rectangle _actualScreenRectangle;
        private RenderTarget2D _nativeRenderTarget;
        private int _screenWidth, _screenHeight;
        private KeyboardState _keyboardOld;
        private Ninja _ninja;
        private Shroom _shroom;
        private Level1 _level1;
        private Level2 _level2;
        private SpriteFont font;
        private Song music;

        private const string gameName = "Ninja Evade";
        private const string startText = "Start";
        private const string selectText = "Select Level";
        private int select;
        private Color selectColor;

        public bool playing = false;

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

            selectColor = Color.Gold;

            _level1 = new Level1(Content);
            _level2 = new Level2(Content);

            _ninja = new Ninja(_textureNinja, new KeyboardReader());
            _shroom = new Shroom(_textureShroom, _ninja);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            font = Content.Load<SpriteFont>("Font");

            _textureNinja = Content.Load<Texture2D>("ninjaWalk");
            _textureShroom = Content.Load<Texture2D>("shroomWalk");

            music = Content.Load<Song>("NinjaEvadeThemeSong");
            MediaPlayer.Play(music);
            MediaPlayer.IsRepeating = true;
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardNew = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed /*|| Keyboard.GetState().IsKeyDown(Keys.Escape)*/)
                Exit();

            // TODO: Add your update logic
            _level1.Update(gameTime);
            _level2.Update(gameTime);
            _shroom.Update(gameTime);
            _ninja.Update(gameTime);
            base.Update(gameTime);
            switch (_gameState)
            {
                case GameState.MainMenu:
                    playing = false;
                    ActivateLevel(0);
                    if (keyboardNew.IsKeyDown(Keys.Down) & !_keyboardOld.IsKeyDown(Keys.Down)) { if (select == 0) { select++; } }
                    else if (keyboardNew.IsKeyDown(Keys.Up) & !_keyboardOld.IsKeyDown(Keys.Up)) { if (select == 1) { select--; } }
                    
                    if (keyboardNew.IsKeyDown(Keys.Enter) & !_keyboardOld.IsKeyDown(Keys.Enter))
                    {
                        if (select == 0)
                        {
                            _gameState = GameState.Playing;
                        }
                        else
                        {
                            select = 0;
                            _gameState = GameState.LevelSelect;
                        }
                    }
                    else if (keyboardNew.IsKeyDown(Keys.Escape) & _keyboardOld.IsKeyUp(Keys.Escape))
                    {
                        Exit();
                    }
                    break;
                case GameState.LevelSelect:
                    playing = false;
                    if (keyboardNew.IsKeyDown(Keys.Down) & !_keyboardOld.IsKeyDown(Keys.Down)) { if (select != 4) { select++; } }
                    else if (keyboardNew.IsKeyDown(Keys.Up) & !_keyboardOld.IsKeyDown(Keys.Up)) { if (select != 0) { select--; } }
                    
                    if (keyboardNew.IsKeyDown(Keys.Enter) & !_keyboardOld.IsKeyDown(Keys.Enter))
                    {
                        _gameState = GameState.Playing;
                        currentLevel = select + 1;
                    }
                    else if (keyboardNew.IsKeyDown(Keys.Escape) & !_keyboardOld.IsKeyDown(Keys.Escape))
                    {
                        select = 0;
                        _gameState = GameState.MainMenu;
                    }
                    break;
                case GameState.GameOver:
                    playing = false;
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape) && !_keyboardOld.IsKeyDown(Keys.Escape))
                        _gameState = GameState.MainMenu;
                    break;
                case GameState.Won:
                    playing = false;
                    break;
                case GameState.Playing:
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape) && !_keyboardOld.IsKeyDown(Keys.Escape))
                        _gameState = GameState.MainMenu;
                    if (!playing)
                    {
                        ActivateLevel(currentLevel);
                        playing = true;
                        _gameState = GameState.GameOver;
                    }
                    break;
            }
            _keyboardOld = keyboardNew;
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            GraphicsDevice.SetRenderTarget(_nativeRenderTarget);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _level1.Draw(_spriteBatch);
            _level2.Draw(_spriteBatch);
            _shroom.Draw(_spriteBatch);
            _ninja.Draw(_spriteBatch);
            _spriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _spriteBatch.Draw(_nativeRenderTarget, _actualScreenRectangle, Color.White);
            _spriteBatch.End();

            _spriteBatch.Begin();
            switch (_gameState)
            {
                case GameState.MainMenu:
                    _spriteBatch.DrawString(font, gameName, new Vector2(_screenWidth / 2, _screenHeight / 3), Color.FloralWhite, 0, font.MeasureString(gameName) / 2, 4, SpriteEffects.None, 0);
                    _spriteBatch.DrawString(font, startText, new Vector2(_screenWidth / 2, _screenHeight / 1.70f), select == 0 ? selectColor : Color.White, 0, font.MeasureString(startText) / 2, 2, SpriteEffects.None, 0);
                    _spriteBatch.DrawString(font, selectText, new Vector2(_screenWidth / 2, _screenHeight / 1.30f), select == 1 ? selectColor : Color.White, 0, font.MeasureString(selectText) / 2, 2, SpriteEffects.None, 0);
                    break;
                case GameState.LevelSelect:
                    _spriteBatch.DrawString(font, "Ninja", new Vector2(_screenWidth / 1.9f, _screenHeight * 0.4f), Color.FloralWhite, 0, font.MeasureString(gameName) / 2, 4, SpriteEffects.None, 0);
                    _spriteBatch.DrawString(font, "Evade", new Vector2(_screenWidth / 2, _screenHeight * 0.6f), Color.FloralWhite, 0, font.MeasureString(gameName) / 2, 4, SpriteEffects.None, 0);
                    for (int i = 0; i < 5; i++)
                        _spriteBatch.DrawString(font, "Level " + (i + 1).ToString(), new Vector2(_screenWidth / 1.3f, _screenHeight * (0.3f + 0.1f * i)), select == i ? selectColor : Color.White, 0, font.MeasureString("Level " + (i + 1).ToString()) / 2, 2, SpriteEffects.None, 0);
                    break;
                case GameState.GameOver:
                    break;
                case GameState.Won:
                    break;
                case GameState.Playing:
                    break;
            }
            _spriteBatch.End();
        }

        private void ActivateLevel(int level)
        {
            switch (level)
            {
                case 0:
                    _level1.active = false;
                    _ninja.active = false;
                    _shroom.active = false;
                    break;
                case 1:
                    _level1.active = true;
                    _ninja.active = true;
                    _shroom.active = true;
                    _shroom.Position = new Vector2(200, 20);
                    break;
                case 2:
                    _level2.active = true;
                    _ninja.active = true;
                    _shroom.active = true;
                    _shroom.Position = new Vector2(130, 70);
                    break;
                default: 
                    break;
            }
        }
    }
}
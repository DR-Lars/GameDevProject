using GameDevProject.GameObject;
using GameDevProject.GameObject.Block;
using GameDevProject.Input;
using GameDevProject.Level;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

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
        private Texture2D _textureBoobam;
        private Texture2D _textureOcto;
        private Texture2D _textureCoin;
        private Rectangle _actualScreenRectangle;
        private RenderTarget2D _nativeRenderTarget;
        private int _screenWidth, _screenHeight;
        private KeyboardState _keyboardOld;
        private Ninja _ninja;
        private List<Shroom> _shrooms;
        private List<Boobam> _boobams;
        private List<Octo> _octos;
        private Coin _coin1;
        private Coin _coin2;
        private Coin _coin3;
        private Level1 _level1;
        private Level2 _level2;
        private SpriteFont font;
        private Song music;

        private const string gameName = "Ninja Evade";
        private const string startText = "Start";
        private const string gameOverText = "Game Over";
        private const string winText = "You Won!";
        private const string mainMenuText = "Press 'Esc' to go to main menu";
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
            _shrooms = new List<Shroom>();
            _boobams = new List<Boobam>();
            _octos = new List<Octo>();

            _ninja = new Ninja(_textureNinja, new KeyboardReader());
            _shrooms.Add(new Shroom(_textureShroom, _ninja));
            _shrooms.Add(new Shroom(_textureShroom, _ninja));
            _shrooms.Add(new Shroom(_textureShroom, _ninja));
            _boobams.Add(new Boobam(_textureBoobam, _ninja));
            _boobams.Add(new Boobam(_textureBoobam, _ninja));
            _octos.Add(new Octo(_textureOcto, _ninja));
            _coin1 = new Coin(_textureCoin);
            _coin2 = new Coin(_textureCoin);
            _coin3 = new Coin(_textureCoin);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            font = Content.Load<SpriteFont>("Font");

            _textureNinja = Content.Load<Texture2D>("ninjaWalk");
            _textureShroom = Content.Load<Texture2D>("shroomWalk");
            _textureBoobam = Content.Load<Texture2D>("boobamWalk");
            _textureOcto = Content.Load<Texture2D>("octoWalk");
            _textureCoin = Content.Load<Texture2D>("coin");

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
            var previousPosition = _ninja.Position;
            _level1.Update(gameTime);
            _level2.Update(gameTime);
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
                        select = 0;
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
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape) && !_keyboardOld.IsKeyDown(Keys.Escape))
                        _gameState = GameState.MainMenu;
                    break;
                case GameState.Playing:
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape) && !_keyboardOld.IsKeyDown(Keys.Escape))
                        _gameState = GameState.MainMenu;
                    if (!playing)
                    {
                        ActivateLevel(currentLevel);
                        playing = true;
                    }

                    foreach (var shroom in _shrooms)
                    {
                        shroom.prevPosition = shroom.Position;
                        shroom.Update(gameTime);
                        if (_ninja.hitbox.IsTouching(shroom.hitbox) && shroom.active)
                        {
                            _gameState = GameState.GameOver;
                        }
                    }

                    foreach (var boobam in _boobams)
                    {
                        boobam.prevPosition = boobam.Position;
                        boobam.Update(gameTime);
                        if (_ninja.hitbox.IsTouching(boobam.hitbox) && boobam.active)
                        {
                            _gameState = GameState.GameOver;
                        }
                    }

                    foreach (var octo in _octos)
                    {
                        octo.prevPosition = octo.Position;
                        octo.Update(gameTime);
                        if (_ninja.hitbox.IsTouching(octo.hitbox) && octo.active)
                        {
                            _gameState = GameState.GameOver;
                        }
                    }

                    foreach (Block block in _level1.obstacles)
                    {
                        if (_ninja.hitbox.IsTouching(block.hitbox))
                        {
                            _ninja.Position = previousPosition;
                        }
                        foreach (var shroom in _shrooms)
                        {
                            if (shroom.hitbox.IsTouching(block.hitbox))
                            {
                                shroom.Position = shroom.prevPosition;
                            }
                        }
                        foreach (var boobam in _boobams)
                        {
                            if (boobam.hitbox.IsTouching(block.hitbox))
                            {
                                boobam.Position = boobam.prevPosition;
                            }
                        }
                    }
                    _coin1.Update(gameTime);
                    _coin2.Update(gameTime);
                    _coin3.Update(gameTime);
                    if (_ninja.hitbox.IsTouching(_coin1.hitbox))
                    {
                        _coin1.active = false;
                    }
                    if (_ninja.hitbox.IsTouching(_coin2.hitbox))
                    {
                        _coin2.active = false;
                    }
                    if (_ninja.hitbox.IsTouching(_coin3.hitbox))
                    {
                        _coin3.active = false;
                    }

                    if (!_coin1.active && !_coin2.active && !_coin3.active)
                    {
                        _gameState = GameState.Won;
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
            _ninja.Draw(_spriteBatch);
            foreach (var shroom in _shrooms)
            {
                shroom.Draw(_spriteBatch);
            }
            foreach (var boobam in _boobams)
            {
                boobam.Draw(_spriteBatch);
            }
            foreach (var octo in _octos)
            {
                octo.Draw(_spriteBatch);
            }

            _coin1.Draw(_spriteBatch);
            _coin2.Draw(_spriteBatch);
            _coin3.Draw(_spriteBatch);
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
                    _spriteBatch.DrawString(font, startText, new Vector2(_screenWidth / 2, _screenHeight / 1.65f), select == 0 ? selectColor : Color.White, 0, font.MeasureString(startText) / 2, 2, SpriteEffects.None, 0);
                    _spriteBatch.DrawString(font, selectText, new Vector2(_screenWidth / 2, _screenHeight / 1.4f), select == 1 ? selectColor : Color.White, 0, font.MeasureString(selectText) / 2, 2, SpriteEffects.None, 0);
                    break;
                case GameState.LevelSelect:
                    _spriteBatch.DrawString(font, "Ninja", new Vector2(_screenWidth / 1.9f, _screenHeight * 0.4f), Color.FloralWhite, 0, font.MeasureString(gameName) / 2, 4, SpriteEffects.None, 0);
                    _spriteBatch.DrawString(font, "Evade", new Vector2(_screenWidth / 2, _screenHeight * 0.6f), Color.FloralWhite, 0, font.MeasureString(gameName) / 2, 4, SpriteEffects.None, 0);
                    for (int i = 0; i < 5; i++)
                        _spriteBatch.DrawString(font, "Level " + (i + 1).ToString(), new Vector2(_screenWidth / 1.3f, _screenHeight * (0.3f + 0.1f * i)), select == i ? selectColor : Color.White, 0, font.MeasureString("Level " + (i + 1).ToString()) / 2, 2, SpriteEffects.None, 0);
                    break;
                case GameState.Won:
                    _spriteBatch.DrawString(font, winText, new Vector2(_screenWidth / 2, 0.45f * _screenHeight), Color.LightGreen, 0, font.MeasureString(winText) / 2, 4, SpriteEffects.None, 0);
                    _spriteBatch.DrawString(font, mainMenuText, new Vector2(_screenWidth / 2, 0.7f * _screenHeight), Color.Gold, 0, font.MeasureString(mainMenuText) / 2, 1, SpriteEffects.None, 0);
                    break;
                case GameState.GameOver:
                    _spriteBatch.DrawString(font, gameOverText, new Vector2(_screenWidth / 2, 0.45f * _screenHeight), Color.Red, 0, font.MeasureString(gameOverText) / 2, 4, SpriteEffects.None, 0);
                    _spriteBatch.DrawString(font, mainMenuText, new Vector2(_screenWidth / 2, 0.7f * _screenHeight), Color.Gold, 0, font.MeasureString(mainMenuText) / 2, 1, SpriteEffects.None, 0);
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
                    foreach (var shroom in _shrooms)
                    {
                        shroom.active = false;
                    }
                    foreach (var boobam in _boobams)
                    {
                        boobam.active = false;
                    }
                    foreach (var octo in _octos)
                    {
                        octo.active = false;
                    }
                    _coin1.active = false;
                    _coin2.active = false;
                    _coin3.active = false;
                    break;
                case 1:
                    _level1.active = true;
                    _ninja.active = true;
                    _ninja.Position = new Vector2(15, 15);
                    foreach (var shroom in _shrooms)
                    {
                        shroom.active = true;
                    }
                    _coin1.active = true;
                    _shrooms[0].Position = new Vector2(200, 20);
                    _coin1.Position = new Vector2(140, 80);
                    break;
                case 2:
                    _level1.active = true;
                    _ninja.active = true;
                    _ninja.Position = new Vector2(15, 15);
                    foreach (var shroom in _shrooms)
                    {
                        shroom.active = true;
                    }
                    _coin1.active = true;
                    _shrooms[0].Position = new Vector2(200, 20);
                    _shrooms[1].Position = new Vector2(100, 40);
                    _coin1.Position = new Vector2(140, 80);
                    break;
                case 3:
                    _level1.active = true;
                    _ninja.active = true;
                    _ninja.Position = new Vector2(15, 15);
                    foreach (var shroom in _shrooms)
                    {
                        shroom.active = true;
                    }
                    foreach (var boobam in _boobams)
                    {
                        boobam.active = true;
                    }
                    _coin1.active = true;
                    _coin2.active = true;
                    _shrooms[0].Position = new Vector2(200, 20);
                    _shrooms[1].Position = new Vector2(100, 40);
                    _coin1.Position = new Vector2(140, 80);
                    _coin2.Position = new Vector2(200, 30);
                    _boobams[1].Position = new Vector2(100, 100);
                    break;
                case 4:
                    _level1.active = true;
                    _ninja.active = true;
                    _ninja.Position = new Vector2(15, 15);
                    foreach (var shroom in _shrooms)
                    {
                        shroom.active = true;
                    }
                    foreach (var boobam in _boobams)
                    {
                        boobam.active = true;
                    }
                    foreach (var octo in _octos)
                    {
                        octo.active = true;
                    }
                    _coin1.active = true;
                    _coin2.active = true;
                    _shrooms[0].Position = new Vector2(200, 20);
                    _shrooms[1].Position = new Vector2(100, 40);
                    _coin1.Position = new Vector2(140, 80);
                    _coin2.Position = new Vector2(200, 30);
                    _boobams[1].Position = new Vector2(100, 100);
                    break;
                case 5:
                    _level1.active = true;
                    _ninja.active = true;
                    _ninja.Position = new Vector2(15, 15);
                    foreach (var shroom in _shrooms)
                    {
                        shroom.active = true;
                    }
                    foreach (var boobam in _boobams)
                    {
                        boobam.active = true;
                    }
                    foreach (var octo in _octos)
                    {
                        octo.active = true;
                    }
                    _coin1.active = true;
                    _coin2.active = true;
                    _coin3.active = true;
                    _shrooms[0].Position = new Vector2(200, 20);
                    _shrooms[1].Position = new Vector2(100, 40);
                    _shrooms[2].Position = new Vector2(50, 80);
                    _coin1.Position = new Vector2(140, 80);
                    _coin2.Position = new Vector2(200, 30);
                    _coin3.Position = new Vector2(140, 50);
                    _boobams[0].Position = new Vector2(90, 20);
                    _boobams[1].Position = new Vector2(100, 100);
                    break;
                default: 
                    break;
            }
        }
    }
}
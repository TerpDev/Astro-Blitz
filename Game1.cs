using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Project1
{
    public class Game1 : Game
    {
        // Game components
        private Player player;
        private List<Bullet> _bullets = new List<Bullet>();
        private EnemySpawner enemySpawner;

        //soundEffects
        private SoundEffect gameOverEffect;
        private SoundEffect gamePlayMusic;
        // Graphics
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Textures
        private Texture2D backgroundTexture, bulletTexture, enemyTexture, enemyTexture2, spaceShip;
        // Game state
        public bool gameStarted = false, gameOverScreen = false;
        bool isSpacePressedLastFrame = false;

        // Score
        public int score = 0;
        public int highScore = 0;
        public string scoreText = "Score:";
        public string highScoreText = "High Score:";

        // UI text
        public string mainText = "Welcome to Astro Blitz\n\nInfinite shooter game \n(get as many points as possible)\n\n\n\nPress Enter to start the game", gameOverText = "Game Over!", mainMovement = "A = Left \nS = Down \nD = Right\nW = Up\nSpace = Shooting";
        public Vector2 mainTextPosition = new Vector2(300, 100), gameOverTextPosition = new Vector2(350, 200);
        public SpriteFont mainTextFont;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            backgroundTexture = Content.Load<Texture2D>("background");
            bulletTexture = Content.Load<Texture2D>("Laser1");
            spaceShip = Content.Load<Texture2D>("yct20hubfk061");
            enemyTexture = Content.Load<Texture2D>("enemyspaceship-removebg-preview");
            enemyTexture2 = Content.Load<Texture2D>("enemyspaceship2");
            gameOverEffect = Content.Load<SoundEffect>("gameover");
            gamePlayMusic = Content.Load<SoundEffect>("gameplaysound");
            gamePlayMusic.Play(0.2f, 0f, 0f);

            player = new Player(spaceShip, new Vector2(350, 200), 200f, bulletTexture, isSpacePressedLastFrame, _bullets);
            enemySpawner = new EnemySpawner(enemyTexture, enemyTexture2, spawnInterval: 1f, GraphicsDevice);
            mainTextFont = Content.Load<SpriteFont>("Score");
        }
        protected override void Update(GameTime gameTime)
        {
            player.Update(gameTime, GraphicsDevice.Viewport.Width + 100, GraphicsDevice.Viewport.Height);

            var kstate = Keyboard.GetState();

            if (gameStarted)
            {
                for (int i = 0; i < _bullets.Count; i++)
                {
                    _bullets[i].Update(gameTime, enemySpawner.enemies, ref score, ref highScore);

                    if (!_bullets[i].isActive)
                    {
                        _bullets.RemoveAt(i);
                        i--; 
                    }
                }
                Rectangle playerRect = new Rectangle((int)player.shipPosition.X, (int)player.shipPosition.Y, player.shipTexture.Width, player.shipTexture.Height);

                foreach (var enemy in enemySpawner.enemies)
                {
                    Rectangle enemyRect = enemy.GetBoundingBox();

                    if (playerRect.Intersects(enemyRect))
                    {
                        PlayerDeath();
                        break;
                    }
                }
                enemySpawner.Update(gameTime, enemySpawner.enemies);
                isSpacePressedLastFrame = kstate.IsKeyDown(Keys.Space);

                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || kstate.IsKeyDown(Keys.Escape))
                {
                    Exit();
                }
                base.Update(gameTime);
            }
            else
            {
                if (kstate.IsKeyDown(Keys.Enter))
                {
                    gameStarted = true;
                }
            }
        }
        private void PlayerDeath()
        {
            gameOverEffect.Play();
            gameStarted = false;
            _bullets.Clear(); 
            enemySpawner.enemies.Clear(); 
            player.shipPosition = new Vector2(100, 100); 

            if (score > highScore)
            {
                highScore = score;
            }

            gameOverScreen = true;
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            _spriteBatch.Begin();

            DrawEnemy();
            DrawGameOver();
            _spriteBatch.DrawString(mainTextFont, $"{scoreText} {score}", new Vector2(10, 10), Color.White);

            _spriteBatch.DrawString(mainTextFont, $"{highScoreText} {highScore}\n\n\n{mainMovement}", new Vector2(10, 30), Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
        void DrawEnemy()
        {
            if (gameStarted)
            {
                _spriteBatch.Draw(backgroundTexture, Vector2.Zero, null, Color.White, 0f, Vector2.Zero,
                    new Vector2(GraphicsDevice.Viewport.Width / (float)backgroundTexture.Width,
                                GraphicsDevice.Viewport.Height / (float)backgroundTexture.Height), SpriteEffects.None, 0f);
                player.Draw(_spriteBatch);

                for (int i = 0; i < _bullets.Count; i++)
                {
                    _bullets[i].Draw(_spriteBatch);
                }
                enemySpawner.Draw(_spriteBatch);
            }
        }
        void DrawGameOver()
        {
            if (!gameStarted)
            {
                _spriteBatch.Draw(backgroundTexture, Vector2.Zero, null, Color.White, 0f, Vector2.Zero,
                    new Vector2(GraphicsDevice.Viewport.Width / (float)backgroundTexture.Width,
                                GraphicsDevice.Viewport.Height / (float)backgroundTexture.Height), SpriteEffects.None, 0f);
                if (gameOverScreen)
                {
                    mainText = "Press enter to play again!";
                    _spriteBatch.DrawString(mainTextFont, $"{gameOverText}", gameOverTextPosition, Color.Red);
                    _spriteBatch.DrawString(mainTextFont, $"{mainText}", mainTextPosition, Color.Red);
                }

                _spriteBatch.DrawString(mainTextFont, mainText, mainTextPosition, Color.White);

            }
        }
    }
}
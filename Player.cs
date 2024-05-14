using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;


namespace Project1
{
    public class Player
    {
        public Texture2D shipTexture;
        public Vector2 shipPosition;
        public Vector2 shipVelocity;
        public float shipSpeed;

        public Texture2D bulletTexture;
        bool isSpacePressedLastFrame = false;
        public List<Bullet> _bullets = new List<Bullet>();

        public Player(Texture2D texture, Vector2 position, float speed, Texture2D bulletTexture, bool isSpacePressedLastFrame, List<Bullet> bullets)
        {
            shipTexture = texture;
            shipPosition = position;
            shipSpeed = speed = 300f;
            this.bulletTexture = bulletTexture;
            this.isSpacePressedLastFrame = isSpacePressedLastFrame;
            _bullets = bullets;
        }

        public void Update(GameTime gameTime, int screenWidth, int screenHeight)
        {
            var kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.Space) && !isSpacePressedLastFrame)
            {
                Vector2 bulletSpawnPosition = shipPosition + new Vector2(shipTexture.Width * 0.3f / 2, shipTexture.Height * 0.3f / 2);
                _bullets.Add(new Bullet(bulletTexture, bulletSpawnPosition));
            }
            isSpacePressedLastFrame = kstate.IsKeyDown(Keys.Space);
            Movement(gameTime, screenWidth, screenHeight);
           

            shipPosition += shipVelocity;
        }
        private void Movement(GameTime gameTime, int screenWidth, int screenHeight)
        {
            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.W))
            {
                shipPosition.Y -= shipSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (shipPosition.Y < 0)
                {
                    shipPosition.Y = 0;
                }
            }

            if (kstate.IsKeyDown(Keys.S))
            {
                shipPosition.Y += shipSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (shipPosition.Y > screenHeight - shipTexture.Height)
                {
                    shipPosition.Y = screenHeight - shipTexture.Height;
                }
            }

            if (kstate.IsKeyDown(Keys.A))
            {
                shipPosition.X -= shipSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (shipPosition.X < 0)
                {
                    shipPosition.X = screenWidth - shipTexture.Width;
                }
            }

            if (kstate.IsKeyDown(Keys.D))
            {
                shipPosition.X += shipSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (shipPosition.X > screenWidth - shipTexture.Width)
                {
                    shipPosition.X = 0;
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(shipTexture, shipPosition, Color.White);
        }
    }
}

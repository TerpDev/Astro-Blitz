using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Project1
{
    public class Bullet
    {
        public Texture2D _texture;
        public Vector2 _position;
        private float speed = 500;
        public bool isActive = true;
        public Rectangle bulletRect;

        public Bullet(Texture2D texture, Vector2 spawnPosition)
        {
            _texture = texture;
            _position = spawnPosition;
        }
        public void Update(GameTime gameTime, List<Enemy> enemies, ref int score, ref int highScore)
        {
            _position += new Vector2(0, -speed * (float)gameTime.ElapsedGameTime.TotalSeconds);

            bulletRect = new Rectangle((int)_position.X, (int)_position.Y, _texture.Width, _texture.Height);

            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                Enemy enemy = enemies[i];
                Rectangle enemyRect = enemy.GetBoundingBox();

                if (bulletRect.Intersects(enemyRect))
                {
                    enemies.RemoveAt(i);
                    score++;
                    isActive = false;

                    if (score > highScore)
                    {
                        highScore = score;
                    }
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }
    }
}
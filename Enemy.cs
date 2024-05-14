using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;


namespace Project1
{
    public class Enemy
    {
        private Texture2D enemyTexture;
        public Vector2 enemyPosition;
        private Vector2 enemyVelocity;
        private float enemySpeed;
        public Rectangle GetBoundingBox()
        {
            return new Rectangle((int)enemyPosition.X, (int)enemyPosition.Y, enemyTexture.Width, enemyTexture.Height);
        }

        public Enemy(Texture2D texture, Vector2 position, float speed)
        {
            enemyTexture = texture;
            enemyPosition = position;
            enemySpeed = speed;
            enemyVelocity = new Vector2(0, enemySpeed);
        }
        public void Update(GameTime gameTime)
        {
            enemyPosition += enemyVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(enemyTexture, enemyPosition, Color.White);
        }
    }

    
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
namespace Project1;

public class EnemySpawner
{
    public List<Enemy> enemies;
    private Texture2D enemyTexture;
    private Texture2D enemyTexture2;
    private Random random;
    private float spawnTimer;
    private float spawnInterval;
    private GraphicsDevice graphicsDevice;
    public bool gameStarted = false;

    public EnemySpawner(Texture2D enemyTexture, Texture2D enemyTexture2, float spawnInterval, GraphicsDevice graphicsDevice)
    {
        enemies = new List<Enemy>();
        this.enemyTexture = enemyTexture;
        this.enemyTexture2 = enemyTexture2;
        this.spawnInterval = spawnInterval;
        random = new Random();
        spawnTimer = 0f;
        this.graphicsDevice = graphicsDevice;

    }

    public void Update(GameTime gameTime, List<Enemy> enemies)
    {
        foreach (var enemy in enemies)
        {
            enemy.Update(gameTime);
        }

        spawnTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (spawnTimer >= spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0f;
        }
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var enemy in enemies)
        {
            enemy.Draw(spriteBatch);
        }
    }
    private void SpawnEnemy()
    {
        int screenWidth = graphicsDevice.Viewport.Width;
        int enemyType = random.Next(1, 3);
        Vector2 spawnPosition;
        if (enemyType == 1)
        {
            spawnPosition = new Vector2(random.Next(0, screenWidth - enemyTexture.Width), 0);
            enemies.Add(new Enemy(enemyTexture, spawnPosition, 50f));
        }
        else
        {
            spawnPosition = new Vector2(random.Next(0, screenWidth - enemyTexture2.Width), 0);
            enemies.Add(new Enemy(enemyTexture2, spawnPosition, 50f));
        }
    }
}
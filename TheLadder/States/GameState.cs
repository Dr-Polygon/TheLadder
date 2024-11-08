﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TheLadder.Sprites;
using TheLadder.Models;
using TheLadder.States;
using TheLadder.Controls;

namespace TheLadder.States
{

    public class GameState : State
    {
        private SpriteFont font;

        private ScoreManager scoreManager;

        private List<Sprite> sprites;

        private List<Player> players;

        private float timer;

        private Texture2D powerUpTexture;

        public static int screenWidth = 1920;

        public static int screenHeight = 1080;

        public static Random random;

        private Texture2D gameBackGroundTexture;

        public GameState(Game1 game, ContentManager content) 
            : base(game,content)
        {
            random = new Random();
        }

        public override void LoadContent()
        {
            var carTexture = Content.Load<Texture2D>("Car_Right");
            powerUpTexture = Content.Load<Texture2D>("PowerUp");
            scoreManager = scoreManager.Load();
            gameBackGroundTexture = Content.Load<Texture2D>("Background/Track1");
            font = Content.Load<SpriteFont>("Font");
            sprites = new List<Sprite>
            {
                new Car(carTexture)
                {
                    Position = new Vector2(100, 100),
                    Bullet = new Bullet(Content.Load<Texture2D>("Bullet")),
                },
            };
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                _game.ChangeState(new MenuState(_game, _content));

            foreach (var sprite in sprites)
                sprite.Update(gameTime, sprites);

            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach (var sprite in sprites.ToArray())
                sprite.Update(gameTime, sprites);
        }

        private void SpawnPowerUp()
        {
            if (timer > 1)
            {
                timer = 0;

                var xPos = Random.Next(0, screenWidth - powerUpTexture.Width);
                var yPos = Random.Next(0, screenHeight - powerUpTextureHeight);

                sprites.Add(new Sprite(powerUpTexture)
                {
                    Position = new Vector2(xPos, yPos),
                });
            }
        }

        public override void PostUpdate(GameTime gameTime)
        {
            var collidableSprites = sprites.Where(c => c is ICollidable);

            foreach (var spriteA in collidableSprites)
            {
                foreach (var spriteB in collidableSprites)
                {
                    //dont do anything if they are the same sprite
                    if (spriteA == spriteB)
                        continue;
                    if (!spriteA.CollisionArea.Intersects(spriteB.Collisionarea))
                        continue;
                    if (spriteA.Intersects(spriteB))
                        ((ICollidable)spriteA.OnCollide(spriteB);
                }
            }

            for (int i = 0; i < sprites.Count; i++)
            {
                if (sprites[i].IsRemoved)
                {
                    sprites.RemoveAt(i);
                    i--;
                }
            }

            if (players.All(c => c.IsDead))
            {
                foreach(var player in players)
                    scoreManager.Add(player.Score);

                ScoreManager.Save(scoreManager);

                Game.ChangeState(new HighscoreState(_game, )content));
            }

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(gameBackGroundTexture, new Vector2(0, 0), Color.White);

            foreach (var sprite in sprites)
                sprite.Draw(spriteBatch);

            float x = 10f;
            var fontY = 10;
            var i = 0;

            spriteBatch.DrawString(font, "Player: " + Player.Score.playerName, new Vector2(x, 10f), Color.White);

            spriteBatch.End();
        }

    }
}
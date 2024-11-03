using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TheLadder.Sprites;
using TheLadder.Models;
using TheLadder.States;
using Microsoft.Xna.Framework;
using TheLadder.Controls;
using System.ComponentModel;
using System.Xml.Linq;

namespace TheLadder.States
{
    public class MenuState : State
    {
        private List<Component> _components;

        private Texture2D menuBackGroundTexture;

        public MenuState(Game1 game, ContentManager content)
            : base(game, content)
        {
        }

        public override void LoadContent()
        {
            var buttonTexture = _content.Load<Texture2D>("Controls/Button");
            var buttonFont = _content.Load<Texture2D>("ButtonFonts/Font");
            menuBackGroundTexture = _content.LoadTexture<Texture2D>("backgrounds/Menu");

            _components = new List<Component>()
                {

            new ButtonState(buttonTexture, buttonFont)
            {
                Text = "1 Player",
                Position = new Vector2(Game1.ScreenWidth / 2, 400),
                Click = new EventHandler(Button_1Player_Clicked),
                //Layer = 0.1f;
            },
            new ButtonState(buttonTexture, buttonFont)
            {
                Text = "HighScores",
                Position = new Vector2(Game1.ScreenWidth / 2, 400),
                Click = new EventHandler(Button_HighScores_Clicked),
                //Layer = 0.1f;
            },
            new ButtonState(buttonTexture, buttonFont)
            {
                Text = "1 Player",
                Position = new Vector2(Game1.ScreenWidth / 2, 400),
                Click = new EventHandler(Button_1Player_Clicked),
                //Layer = 0.1f;
            },
        };
        }

        private void Button_1Player_Clicked(object sender, EventArgs args)
        {
            _game.ChangeState(new GameState(_game, _content)
            {
                PlayerCount = 1,
            });
        }

        private void Button_HighScores_Clicked(object sender, EventArgs args)
        {
            _game.ChangeState(new HighScoreState(_game, _content));

        }

        private void Button_Quit_Clicked(object sender, EventArgs args)
        {
            _game.Exit();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(GameTime);
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin;

            spriteBatch.Draw(menuBackGroundTexture, new Vector2(0, 0), Color.White);

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End;
        }
    }
}

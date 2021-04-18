//Author: Taric Folkes
//Project Name: Abduction
//File Name: Settings Button
//Creation Date: Dec. 10, 2017
//Modified Date: Dec. 12, 2017
//Description: This class depicts how the settings button interacts with the mouse when over it.
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Abduction
{
    class Settings
    {
        //Settings Button
        Texture2D settingsButton;
        Rectangle oButtonRec;//o stands for - Options||Settings
        Vector2 oButtonPos;
        const int btnX = 1151;
        const int btnY = 0;
        const int btnW = 95;
        const int btnH = 63;

        public Vector2 buttonSize;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="newSettingsButton"></param>
        /// <param name="graphics"></param>
        public Settings(Texture2D newSettingsButton, GraphicsDevice graphics)
        {
            settingsButton = newSettingsButton;

            buttonSize = new Vector2(graphics.Viewport.Width / 13, graphics.Viewport.Height / 12);
        }
        //Determine if mouse clicked
        public bool isClicked;

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="mouse">Used to get the current location and the if the mouse button are pressed</param>
        public void Update(MouseState mouse)
        {
            oButtonRec = new Rectangle((int)oButtonPos.X, (int)oButtonPos.Y,
                (int)buttonSize.X, (int)buttonSize.Y);

            Rectangle mouseRec = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if (mouseRec.X > btnX && mouseRec.X < btnX + btnW && mouseRec.Y > btnY && mouseRec.Y < btnY + btnH)
            {
                if (mouse.LeftButton == ButtonState.Pressed) isClicked = true;
            }
            else
            {
                isClicked = false;
            }
        }
        /// <summary>
        /// Determines the position of the button
        /// </summary>
        /// <param name="newSettingsButtonPos"></param>
        public void setSettingsPosition(Vector2 newSettingsButtonPos)
        {
            oButtonPos = newSettingsButtonPos;
        }
        /// <summary>
        /// Draw
        /// </summary>
        /// <param name="spriteBatch">Used to draw in the texture</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(settingsButton, oButtonRec, Color.White);
        }
    }
}
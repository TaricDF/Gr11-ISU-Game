//Author: Taric Folkes
//Project Name: Abduction
//File Name: Exit Button
//Creation Date: Dec. 10, 2017
//Modified Date: Dec. 12, 2017
//Description: This class depicts how the exit button interacts with the mouse when over it.
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Abduction
{
    class Exit
    {
        //Exit Button
        Texture2D exitButton;
        Rectangle exitButtonRec;
        Vector2 eButtonPos;
        const int btnX = 484;
        const int btnY = 594;
        const int btnW = 130;
        const int btnH = 54;

        Color eButtonColor = new Color(255, 255, 255, 255);

        public Vector2 eButtonSize;

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="newExitButton"></param>
        /// <param name="graphics"></param>
        public Exit(Texture2D newExitButton, GraphicsDevice graphics)
        {
            exitButton = newExitButton;

            eButtonSize = new Vector2(graphics.Viewport.Width / 7, graphics.Viewport.Height / 8);
        }
        //If mouse if hovering or is clicking on button
        bool cover;
        public bool isClicked;

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="mouse">Used to get the current location and the if the mouse button are pressed</param>
        public void Update(MouseState mouse)
        {
            exitButtonRec = new Rectangle((int)eButtonPos.X, (int)eButtonPos.Y,
                (int)eButtonSize.X, (int)eButtonSize.Y);

            Rectangle mouseRec = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if (mouseRec.X > btnX && mouseRec.X < btnX + btnW && mouseRec.Y > btnY && mouseRec.Y < btnY + btnH)
            {
                if (eButtonColor.A == 255) cover = false;
                if (eButtonColor.A == 0) cover = true;
                if (cover) eButtonColor.A += 5; else eButtonColor.A -= 5;
                if (mouse.LeftButton == ButtonState.Pressed) isClicked = true;
            }
            else if (eButtonColor.A < 255)
            {
                eButtonColor.A += 5;
                isClicked = false;
            }
        }
        /// <summary>
        /// Determines the position of the button
        /// </summary>
        /// <param name="newButtonPos"></param>
        public void setExitPosition(Vector2 newButtonPos)
        {
            eButtonPos = newButtonPos;
        }
        /// <summary>
        /// Draw
        /// </summary>
        /// <param name="spriteBatch">Used to draw in the texture</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(exitButton, exitButtonRec, eButtonColor);
        }
    }
}
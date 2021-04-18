//Author: Taric Folkes
//Project Name: Abduction
//File Name: Controls Button
//Creation Date: Dec. 10, 2017
//Modified Date: Dec. 12, 2017
//Description: This class depicts how the controls button interacts with the mouse when over it.
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Abduction
{
    class Controls
    {
        //Controls Button
        Texture2D controlButton;
        Rectangle controlButtonRec;
        Vector2 cButtonPos;
        const int btnX = 480;
        const int btnY = 513;
        const int btnW = 134;
        const int btnH = 56;

        Color cButtonColor = new Color(255, 255, 255, 255);

        public Vector2 cButtonSize;

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="newControlButton"></param>
        /// <param name="graphics"></param>
        public Controls(Texture2D newControlButton, GraphicsDevice graphics)
        {
            controlButton = newControlButton;

            cButtonSize = new Vector2(graphics.Viewport.Width / 9, graphics.Viewport.Height / 12);
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
            controlButtonRec = new Rectangle((int)cButtonPos.X, (int)cButtonPos.Y,
                (int)cButtonSize.X, (int)cButtonSize.Y);

            Rectangle mouseRec = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if (mouseRec.X > btnX && mouseRec.X < btnX + btnW && mouseRec.Y > btnY && mouseRec.Y < btnY + btnH)
            {
                if (cButtonColor.A == 255) cover = false;
                if (cButtonColor.A == 0) cover = true;
                if (cover) cButtonColor.A += (int)5.5; else cButtonColor.A -= (int)5.5;
                if (mouse.LeftButton == ButtonState.Pressed) isClicked = true;
            }
            else if (cButtonColor.A < 255)
            {
                cButtonColor.A += (int)5.5;
                isClicked = false;
            }
        }
        /// <summary>
        /// Determines the position of the button
        /// </summary>
        /// <param name="newButtonPos"></param>
        public void setControlPosition(Vector2 newButtonPos)
        {
            cButtonPos = newButtonPos;
        }
        /// <summary>
        /// Draw
        /// </summary>
        /// <param name="spriteBatch">Used to draw in the texture</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(controlButton, controlButtonRec, cButtonColor);
        }
    }
}
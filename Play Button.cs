//Author: Taric Folkes
//Project Name: Abduction
//File Name: Play Button
//Creation Date: Dec. 10, 2017
//Modified Date: Dec. 12, 2017
//Description: This class depicts how the play button interacts with the mouse when over it.
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Abduction
{
    class Play
    {
        //Start Button
        Texture2D startButton;
        Rectangle startButtonRec;
        Vector2 sButtonPos;
        const int btnX = 477;
        const int btnY = 406;
        const int btnW = 137;
        const int btnH = 89;

        Color sButtonColor = new Color(255, 255, 255, 255);

        public Vector2 sButtonSize;

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="newStartButton"></param>
        /// <param name="graphics"></param>
        public Play(Texture2D newStartButton, GraphicsDevice graphics)
        {
            startButton = newStartButton;

            sButtonSize = new Vector2(graphics.Viewport.Width / 10, graphics.Viewport.Height / 10);
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
            startButtonRec = new Rectangle((int)sButtonPos.X, (int)sButtonPos.Y,
                (int)sButtonSize.X, (int)sButtonSize.Y);

            Rectangle mouseRec = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if (mouseRec.X > btnX && mouseRec.X < btnX + btnW && mouseRec.Y > btnY && mouseRec.Y < btnY + btnH)
            {
                if (sButtonColor.A == 255) cover = false;
                if (sButtonColor.A == 0) cover = true;
                if (cover) sButtonColor.A += (int)5.5; else sButtonColor.A -= (int)5.5;
                if (mouse.LeftButton == ButtonState.Pressed) isClicked = true;
            }
            else if (sButtonColor.A < 255)
            {
                sButtonColor.A += (int)5.5;
                isClicked = false;
            }
        }
        /// <summary>
        /// Determines the position of the button
        /// </summary>
        /// <param name="newButtonPos"></param>
        public void setStartPosition(Vector2 newButtonPos)
        {
            sButtonPos = newButtonPos;
        }
        /// <summary>
        /// Draw
        /// </summary>
        /// <param name="spriteBatch">Used to draw in the texture</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(startButton, startButtonRec, sButtonColor);
        }
    }
}
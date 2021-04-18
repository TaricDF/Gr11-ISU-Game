//Author: Taric Folkes
//Project Name: Abduction
//File Name: Return Button
//Creation Date: Dec. 10, 2017
//Modified Date: Dec. 12, 2017
//Description: This class depicts how the back/return button interacts with the mouse when over it.
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Abduction
{
    class Return
    {
        //Exit Button
        Texture2D returnButton;
        Rectangle returnButtonRec;
        Vector2 rbuttonPos;
        const int btnX = 29;
        const int btnY = 21;
        const int btnW = 124;
        const int btnH = 55;

        Color rbuttonColor = new Color(255, 255, 255, 255);

        public Vector2 rButtonSize;

        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="newReturnButton"></param>
        /// <param name="graphics"></param>
        public Return(Texture2D newReturnButton, GraphicsDevice graphics)
        {
            returnButton = newReturnButton;

            rButtonSize = new Vector2(graphics.Viewport.Width / 7, graphics.Viewport.Height / 8);
        }
        //Determine if mouse clicked
        public bool isClicked;

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="mouse">Used to get the current location and the if the mouse button are pressed</param>
        public void Update(MouseState mouse)
        {
            returnButtonRec = new Rectangle((int)rbuttonPos.X, (int)rbuttonPos.Y,
                (int)rButtonSize.X, (int)rButtonSize.Y);

            Rectangle mouseRec = new Rectangle(mouse.X, mouse.Y, 1, 1);

            if (mouseRec.X > btnX && mouseRec.X < (btnX + btnW) && mouseRec.Y > btnY && mouseRec.Y < (btnY + btnH))
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
        /// <param name="newButtonPos"></param>
        public void setReturnPosition(Vector2 newButtonPos)
        {
            rbuttonPos = newButtonPos;
        }
        /// <summary>
        /// Draw
        /// </summary>
        /// <param name="spriteBatch">Used to draw in the texture</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(returnButton, returnButtonRec, rbuttonColor);
        }
    }
}
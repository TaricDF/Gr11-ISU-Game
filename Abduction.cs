//Author: Taric Folkes
//Project Name: Abduction
//File Name: ISU
//Creation Date: Dec. 9, 2017
//Modified Date: 
//Description: Abduction, is a singleplayer level-based puzzler maze game with an alien twist.
//It involves a farmer trying to escape various mazes which increase in difficulty per level.
//Within the mazes are various obstacles that the player has to overcome in order to beat the level alongside those obstacles are power-ups that aid the player in defeating the level
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Abduction
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Abduction : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Screen Size
        int screenW = 1280, screenH = 800;

        #region GameStates
        //GameStates
        enum GameState
        {
            MainMenu,
            Options,
            Controls,
            About,
            LevelSelect,
            Level1,
            Level2,
            Level3,
            Paused,
            Lose,
            Win,
        }
        GameState currentGameState = GameState.MainMenu;
        #endregion

        #region Classes
        //Main Buttons
        Play btnPlay;
        Controls btnControl;
        Settings btnSettings;
        Exit btnExit;
        Return btnReturn;
        #endregion

        //Mouse State
        MouseState mouse;
        MouseState prevMouse;
        SpriteFont mouseFont;
        Vector2 mouseFontLoc;
        string font = "";

        //Keyboard State
        KeyboardState kb;
        KeyboardState prevKb;

        //Background
        Texture2D background;
        Rectangle backgroundRec;

        #region Game Audio
        //Audio
        Song backgroundTheme;
        Song levelTheme;
        SoundEffect YouWin;
        SoundEffectInstance YouWinInst;
        SoundEffect YouLose;
        SoundEffectInstance YouLoseInst;
        #endregion

        //On-Click Audio
        SoundEffect onClick;
        SoundEffectInstance onClickInst;

        //Game Title 
        SpriteFont gameTitle;
        Vector2 titleLoc;
        string title = "Abduction";

        //Level Selection Screen
        Texture2D levelSelectTitle;
        Rectangle lvlSelectRec;
        Texture2D selctionBg;
        Rectangle selectionRec;

        #region Options Screen
        //Options Screen
        Texture2D optionsBg;
        Rectangle optionsBgRec;
        Texture2D optionsTitle;
        Rectangle optionsTitleRec;
        Texture2D volSlider;
        Rectangle volSliderRec;
        Texture2D buttonSlider;
        Rectangle buttonSliderRec;
        Texture2D resetBtn;
        Rectangle resetRec;
        #endregion

        //Controls Screen
        Texture2D controlTitle;
        Rectangle controlTitleRec;
        SpriteFont controlFont;
        string controls = "Controls\nPlayer 1\n'W'- Move Forward\n'A'- Move Left\n'S'- Move Down\n'D'- Move Right" +
            "\n\nPlayer 2\n'Up Arrow'- Move Forward\n'Left Arrow'- Move Left\n'Down Arrow'- Move Down\n'Right Arrow'- Move Right";
        Vector2 controlLoc;

        #region About Screen
        //About Screen
        Texture2D about;
        Rectangle aboutRec;
        Texture2D aboutTitle;
        Rectangle aboutTitleRec;
        SpriteFont infoFont;
        //Game Description
        string info = "Description\nAbduction, is a singleplayer level-based puzzler maze game with an alien twist. It involves a farmer \ntrying to escape various mazes which increase in difficulty per level." +
            " Within the mazes are various \nobstacles that the player has to overcome in order to beat the level alongside those obstacles are \npower-ups that aid the player in defeating the level." +
            " The overall goal of the player is to beat all the \nlevels of the maze and escape the clutches of the aliens that abducted you. The player will also \nwant to try and achieve the highest possible score they can in each level. " +
            " This game has an \ninteresting style and audio, that incites excitement, suspense and thrill in the player.\n\nBackstory\nOne day a farmer was abducted by aliens while he was sleeping. He wakes up to see aliens all \naround him so he runs away." +
            "However what he doesn't know is that to escape the alien's clutches \nhe has to go through various mazes, and that the entire alien ship is rigged with booby traps and \nvarious obstacles. But that's not the only thing he has to avoid," +
            "the alien captures are chasing him, \nand if he is caught he will be killed.";
        Vector2 infoLoc;
        #endregion

        //Lose Screen
        Texture2D lose;
        Rectangle loseRec;

        //Win Screen
        Texture2D win;
        Rectangle winRec;

        #region Pause Screen
        //Pause
        Texture2D pauseSc;
        Rectangle pauseScRec;
        Song pauseMusic;
        #endregion

        #region Level Buttons
        //Level Numbers
        Texture2D[] levelnumber = new Texture2D[5];
        Rectangle[] lvlNumRec = new Rectangle[5];
        #endregion

        #region Levels
        //Level
        Texture2D level1;
        Rectangle level1Rec;
        Texture2D level2;
        Rectangle level2Rec;
        Texture2D level3;
        Rectangle level3Rec;
        Texture2D level4;
        Rectangle level4Rec;
        Texture2D level5;
        Rectangle level5Rec;
        //Color Data
        Color[] level1Colors;
        Color[] level2Colors;
        #endregion

        #region Level Status
        //Level Stats
        bool isCounting = false;
        float time = 90.0f;
        string timer = "Time Remaining: ";
        SpriteFont levelFont;
        Vector2 timerLoc;
        //Level Control
        //Determines what level player is on
        //If they have beaten the level
        bool isLevel1, isLevel2, isLevel3, isLevel4, isLevel5;
        bool hasBeatenLvl1, hasBeatenLvl2, hasBeatenLvl3, hasBeatenLvl4;
        #endregion

        #region Maze Elements
        //Power-Ups
        Texture2D[] hpPower = new Texture2D[3];
        Rectangle[] hpPowerRec = new Rectangle[3];
        Texture2D[] speedPower = new Texture2D[3];
        Rectangle[] speedPowerRec = new Rectangle[3];
        Texture2D[] invincPower = new Texture2D[3];
        Rectangle[] invincPowerRec =  new Rectangle[3];
        //Health Positions
        Vector2 healthLoc = new Vector2(868, 580);
        Vector2 healthLoc2 = new Vector2(535, 246);
        Vector2 healthLoc3 = new Vector2(26, 480);
        //Speed Positions
        Vector2 speedLoc = new Vector2(295, 336);
        Vector2 speedLoc2 = new Vector2(366, 234);
        Vector2 speedLoc3 = new Vector2(957, 179);
        //Controls if health is still active
        bool[] hpIsActive = new bool[3] {true,true,true};
        bool[] speedIsActive = new bool[3] { true, true, true };
        bool[] invincIsActive = new bool[3] { true, true, true };
        //Controls the speed power-up
        bool isFaster;
        float speedTimer = 3f;
        //Area of effect trap
        Texture2D aoeTrap;
        Rectangle[] trapRec = new Rectangle[4];
        //Trap Locations
        Vector2 trapLoc = new Vector2(845, 642);
        Vector2 trapLoc2 = new Vector2(84, 378);
        Vector2 trapLoc3 = new Vector2(435, 62);
        Vector2 trapLoc4 = new Vector2(595, 430);
        #endregion

        #region Character
        //Character||Player
        Texture2D rightWalk, leftWalk, upWalk, downWalk, currentAnim;
        Rectangle walkRec;
        Rectangle walkRec2; 
        Rectangle walkSrcRec;
        //Movement
        float leftSpeed, rightSpeed;
        float upSpeed, downSpeed;
        //Animation
        float elapsed = 0;
        float delay = 165f;
        int frames = 0;
        Color[] playerData;
        bool isLeft = true;
        bool isRight = true;
        bool isUp = true;
        bool isDown = true;
        //Health Bar
        SpriteFont healthFont;
        Vector2 healthPos;
        string health = "Health: ";
        int hp = 100;
        #endregion

        #region Enemy
        //Enemy
        Texture2D enemy;
        Rectangle enemyRec;
        float enemySpeedX, enemySpeedY;
        float spawnTime;
        Color[] enemyData;
        const int LEFT = 1;
        const int RIGHT = 2;
        const int UP = 3;
        const int DOWN = 4;
        int direction = UP;
        float rotation = 90f;
        Random randomDirection = new Random();
        #endregion

        public Abduction()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //Allows mouse to be visible
            this.IsMouseVisible = true;

            //Screen Adjustments
            //Changes resolution to 1280x800
            graphics.PreferredBackBufferWidth = screenW;
            graphics.PreferredBackBufferHeight = screenH;
            graphics.ApplyChanges();

            //Enemy Movement Speed
            enemySpeedX = 3.5f;
            enemySpeedY = 3.5f;

            //Level State
            //Makes sure that when player opens the game window all the levels can't be played
            isLevel1 = false;
            isLevel2 = false;
            isLevel3 = false;
            isLevel4 = false;
            isLevel5 = false;

            //To know if user has already beaten the level
            //Also allows them to leave level screen and come back to play(still has to have window open)
            hasBeatenLvl1 = false;
            hasBeatenLvl2 = false;
            hasBeatenLvl3 = false;
            hasBeatenLvl4 = false;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Background Audio
            backgroundTheme = Content.Load<Song>(@"Audio\abduction them song");

            //Plays background audio for when the user enters the window
            MediaPlayer.Play(backgroundTheme);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.0f;

            //On-Click Effect
            onClick = Content.Load<SoundEffect>(@"Audio\click_sound");
            onClickInst = onClick.CreateInstance();
            onClickInst.Volume = 0.5f;
            #region Level Audio
            //Level Audio/Theme
            levelTheme = Content.Load<Song>(@"Audio\level audio");
            //Victory/Failure Audio for levels
            YouWin = Content.Load<SoundEffect>(@"Audio\You_Win");
            YouWinInst = YouWin.CreateInstance();
            YouLose = Content.Load<SoundEffect>(@"Audio\You_Lose");
            YouLoseInst = YouLose.CreateInstance();
            #endregion
            //Coordinates
            mouseFont = Content.Load<SpriteFont>(@"Fonts\mouse");
            mouseFontLoc = new Vector2(15, 730);

            //Background
            background = Content.Load<Texture2D>(@"GameState Sprites\MainMenu\abduction background");
            backgroundRec = new Rectangle(0, 0, screenW, screenH);

            //Play button
            btnPlay = new Play(Content.Load<Texture2D>(@"GameState Sprites\MainMenu\start button"), graphics.GraphicsDevice);
            btnPlay.setStartPosition(new Vector2(466, 425));

            //Exit Button
            btnExit = new Exit(Content.Load<Texture2D>(@"GameState Sprites\MainMenu\exit button"), graphics.GraphicsDevice);
            btnExit.setExitPosition(new Vector2(460, 575));

            //Settings button
            btnSettings = new Settings(Content.Load<Texture2D>(@"GameState Sprites\MainMenu\settings button"), graphics.GraphicsDevice);
            btnSettings.setSettingsPosition(new Vector2(1150, 0));

            //Control Button
            btnControl = new Controls(Content.Load<Texture2D>(@"GameState Sprites\MainMenu\control button"), graphics.GraphicsDevice);
            btnControl.setControlPosition(new Vector2(475, 510));

            //Back Button
            btnReturn = new Return(Content.Load<Texture2D>(@"GameState Sprites\backBtn"), graphics.GraphicsDevice);
            btnReturn.setReturnPosition(new Vector2(0, 0));

            //About Button
            about = Content.Load<Texture2D>(@"GameState Sprites\About\about button");
            aboutRec = new Rectangle(1225, 740, about.Width / 3, about.Height / 3);

            //Game Title
            gameTitle = Content.Load<SpriteFont>(@"Fonts\gameTitle");
            titleLoc = new Vector2(425, 252);

            //Level Selection Screen
            levelSelectTitle = Content.Load<Texture2D>(@"GameState Sprites\Level Selection\level");
            lvlSelectRec = new Rectangle(440, 0, 300, 300);

            selctionBg = Content.Load<Texture2D>(@"GameState Sprites\Level Selection\level select background");
            selectionRec = new Rectangle(0, 0, screenW, screenH);
            #region Option Screen Load
            //Options Screen
            optionsBg = Content.Load<Texture2D>(@"GameState Sprites\Options\options screen background");
            optionsBgRec = new Rectangle(0, 0, screenW, screenH);

            optionsTitle = Content.Load<Texture2D>(@"GameState Sprites\Options\options screen");
            optionsTitleRec = new Rectangle(430, 0, 400, 400);

            volSlider = Content.Load<Texture2D>(@"GameState Sprites\Options\volSlider");
            volSliderRec = new Rectangle(200, 250, volSlider.Width, volSlider.Height);

            buttonSlider = Content.Load<Texture2D>(@"GameState Sprites\Options\button slider");
            buttonSliderRec = new Rectangle(640, 358, screenW / 11, screenH / 12);

            resetBtn = Content.Load<Texture2D>(@"GameState Sprites\Options\newReset");
            resetRec = new Rectangle(480, 200, screenW / 5, screenH / 8);
            #endregion

            //About Screen
            aboutTitle = Content.Load<Texture2D>(@"GameState Sprites\About\About");
            aboutTitleRec = new Rectangle(490, 0, 200, 100);
            infoFont = Content.Load<SpriteFont>(@"Fonts\gameText");
            infoLoc = new Vector2(0, 150);

            //Control Screen
            controlTitle = Content.Load<Texture2D>(@"GameState Sprites\Controls\Control");
            controlTitleRec = new Rectangle(450, 0, 200, 200);

            controlFont = Content.Load<SpriteFont>(@"Fonts\gameText");
            controlLoc = new Vector2(0, 125);

            #region Level Buttons Load
            //Level Numbers
            levelnumber[0] = Content.Load<Texture2D>(@"GameState Sprites\Level Selection\Level number 1");
            lvlNumRec[0] = new Rectangle(60, 228, screenW / 6, screenH / 6);

            levelnumber[1] = Content.Load<Texture2D>(@"GameState Sprites\Level Selection\Level number 2");
            lvlNumRec[1] = new Rectangle(360, 228, screenW / 6, screenH / 6);

            levelnumber[2] = Content.Load<Texture2D>(@"GameState Sprites\Level Selection\Level number 3");
            lvlNumRec[2] = new Rectangle(660, 228, screenW / 6, screenH / 6);

            levelnumber[3] = Content.Load<Texture2D>(@"GameState Sprites\Level Selection\Level number 4");
            lvlNumRec[3] = new Rectangle(960, 228, screenW / 6, screenH / 6);

            levelnumber[4] = Content.Load<Texture2D>(@"GameState Sprites\Level Selection\Level number 5");
            lvlNumRec[4] = new Rectangle(60, 428, screenW / 6, screenH / 6);
            #endregion

            #region Levels Load
            //Level Audio
            levelTheme = Content.Load<Song>(@"Audio\level audio");

            //Levels
            level1 = Content.Load<Texture2D>(@"GameState Sprites\Levels\15 by 15 orthogonal maze");
            //Gathers colors of the level 1
            level1Colors = new Color[level1.Width * level1.Height];
            level1.GetData(level1Colors);
            level1Rec = new Rectangle(0, 0, screenW, screenH);

            level2 = Content.Load<Texture2D>(@"GameState Sprites\Levels\16 by 16 orthogonal maze");
            //Gathers colors of the level 2
            level2Colors = new Color[level2.Width * level2.Height];
            level2.GetData(level2Colors);

            level2Rec = new Rectangle(0, 0, screenW, screenH);
            level3 = Content.Load<Texture2D>(@"GameState Sprites\Levels\18 by 18 orthogonal maze");
            level3Rec = new Rectangle(0, 0, screenW, screenH);
            #endregion

            //Lose Screen
            lose = Content.Load<Texture2D>(@"GameState Sprites\Lose\you lose");
            loseRec = new Rectangle(0, 0, screenW, screenH);

            //Win
            win = Content.Load<Texture2D>(@"GameState Sprites\Win\win screen");
            winRec = new Rectangle(0, 0, screenW, screenH);

            //Pause
            pauseSc = Content.Load<Texture2D>(@"GameState Sprites\pause screen");
            pauseScRec = new Rectangle(0, 0, screenW, screenH);
            pauseMusic = Content.Load<Song>(@"Audio\DAH! so nice");

            //Level Stats
            levelFont = Content.Load<SpriteFont>(@"Fonts\levelText");
            timerLoc = new Vector2(this.Window.ClientBounds.Width / 2 - levelFont.MeasureString(timer).X / 2, 0);

            #region Character Load
            //Character
            upWalk = Content.Load<Texture2D>(@"Character Sprites\upWalk");
            leftWalk = Content.Load<Texture2D>(@"Character Sprites\leftWalk");
            rightWalk = Content.Load<Texture2D>(@"Character Sprites\rightWalk");
            downWalk = Content.Load<Texture2D>(@"Character Sprites\downWalk");
            currentAnim = upWalk;
            walkRec = new Rectangle(622, 739, currentAnim.Width / 9, currentAnim.Height);
            walkRec2 = new Rectangle(walkRec.X, walkRec.Y + 10, 30, 50);
            walkSrcRec = new Rectangle(0, 0, currentAnim.Width / 9, currentAnim.Height);
            //Gathers the color of the sprite
            playerData = new Color[currentAnim.Width * currentAnim.Height];
            currentAnim.GetData(playerData);
            //Health Bar
            healthFont = Content.Load<SpriteFont>(@"Fonts\health");
            healthPos = new Vector2(walkRec.X, walkRec.Y);
            #endregion

            #region Maze Elements Load
            //Health Regen
            hpPower[0] = Content.Load<Texture2D>(@"Maze Elements\health regen");
            hpPowerRec[0] = new Rectangle((int)healthLoc.X, (int)healthLoc.Y, hpPower[0].Width / 7, hpPower[0].Height / 7);
            hpPower[1] = Content.Load<Texture2D>(@"Maze Elements\health regen");
            hpPowerRec[1] = new Rectangle((int)healthLoc2.X, (int)healthLoc2.Y, hpPower[1].Width / 7, hpPower[1].Height / 7);
            hpPower[2] = Content.Load<Texture2D>(@"Maze Elements\health regen");
            hpPowerRec[2] = new Rectangle((int)healthLoc3.X, (int)healthLoc3.Y, hpPower[2].Width / 7, hpPower[2].Height / 7);
            //Speed Boost
            speedPower[0] = Content.Load<Texture2D>(@"Maze Elements\speed boost");
            speedPowerRec[0] = new Rectangle((int)speedLoc.X, (int)speedLoc.Y, speedPower[0].Width / 3, speedPower[0].Height / 3);
            speedPower[1] = Content.Load<Texture2D>(@"Maze Elements\speed boost");
            speedPowerRec[1] = new Rectangle((int)speedLoc2.X, (int)speedLoc2.Y, speedPower[1].Width / 3, speedPower[1].Height / 3);
            speedPower[2] = Content.Load<Texture2D>(@"Maze Elements\speed boost");
            speedPowerRec[2] = new Rectangle((int)speedLoc3.X, (int)speedLoc3.Y, speedPower[2].Width / 3, speedPower[2].Height / 3);
            //AoE Trap
            aoeTrap = Content.Load<Texture2D>(@"Maze Elements\AoE Trap");
            trapRec[0] = new Rectangle((int)trapLoc.X, (int)trapLoc.Y, aoeTrap.Width, aoeTrap.Height);
            trapRec[1] = new Rectangle((int)trapLoc2.X, (int)trapLoc2.Y, aoeTrap.Width, aoeTrap.Height);
            trapRec[2] = new Rectangle((int)trapLoc3.X, (int)trapLoc3.Y, aoeTrap.Width, aoeTrap.Height);
            trapRec[3] = new Rectangle((int)trapLoc4.X, (int)trapLoc4.Y, aoeTrap.Width, aoeTrap.Height);
            #endregion

            #region Enemy Load
            //Enemy
            enemy = Content.Load<Texture2D>(@"Character Sprites\spaceship");
            enemyRec = new Rectangle(620, 726, enemy.Width / 4, enemy.Height / 4);
            //Gathers the color of the sprite
            enemyData = new Color[enemy.Width * enemy.Height];
            enemy.GetData(enemyData);
            #endregion
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //Allows game to exit
            if (kb.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            //Keyboard State
            prevKb = kb;
            kb = Keyboard.GetState();

            //Coordinates && MouseState
            prevMouse = mouse;
            mouse = Mouse.GetState();
            font = "X: " + mouse.X + "\nY: " + mouse.Y;

            //Player limits
            //Limitation();

            //Allows to switch between game states
            switch (currentGameState)
            {
                #region MainMenu
                case GameState.MainMenu:
                    //Play||Start Button
                    if (btnPlay.isClicked == true) currentGameState = GameState.LevelSelect;
                    btnPlay.Update(mouse);
                    //Options||Setiings Button
                    if (btnSettings.isClicked == true) currentGameState = GameState.Options;
                    btnSettings.Update(mouse);
                    //Controls Button
                    if (btnControl.isClicked == true) currentGameState = GameState.Controls;
                    btnControl.Update(mouse);
                    //Exit Button
                    if (btnExit.isClicked == true) this.Exit();
                    btnExit.Update(mouse);
                    //About Button
                    AboutBtn();
                    //On any button click play sound effect
                    if (btnPlay.isClicked || btnControl.isClicked || btnSettings.isClicked) onClick.Play();
                    break;
                #endregion
                #region Options
                case GameState.Options:
                    //Back Button
                    if (btnReturn.isClicked == true) currentGameState = GameState.MainMenu;
                    btnReturn.Update(mouse);
                    break;
                #endregion
                #region Controls
                case GameState.Controls:
                    //Back Button
                    if (btnReturn.isClicked == true) currentGameState = GameState.MainMenu;
                    btnReturn.Update(mouse);
                    break;
                #endregion
                #region About
                case GameState.About:
                    //Back Button
                    if (btnReturn.isClicked == true) currentGameState = GameState.MainMenu;
                    btnReturn.Update(mouse);
                    break;
                #endregion
                #region Level Select
                case GameState.LevelSelect:
                    //Back Button
                    if (btnReturn.isClicked == true) currentGameState = GameState.MainMenu;
                    btnReturn.Update(mouse);
                    //Level buttons
                    Level1Btn();
                    Level2Btn();
                    Level3Btn();
                    break;
                #endregion
                #region Level 1
                case GameState.Level1:
                    //Stops all sound effects when the level has started
                    onClickInst.Stop();
                    YouWinInst.Stop();
                    YouLoseInst.Stop();
                    if (isCounting)
                    {
                        //Timer CountDown
                        time -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    //Makes sure that time cannot pass 0
                    if (time < 0) time = 0;

                    //Pauses game
                    if (kb.IsKeyDown(Keys.P))
                    {
                        //MediaPlayer.Play(pauseMusic);
                        currentGameState = GameState.Paused;
                        isCounting = false;
                    }
                    //Player Movement
                    CharacterMovement(gameTime, walkRec2, level1Colors, level1Rec);
                    //Will only occur check when the enemy has spawned in
                    if (spawnTime > time)
                    {
                        //Enemy and Player Collision Detection
                        if (EnemyDetection(walkRec, playerData, enemyRec, enemyData))
                        {
                            currentGameState = GameState.Lose;
                            isCounting = false;
                            YouLoseInst.Play();
                        }
                    }

                    //PowerUp
                    PowerUpCollision(gameTime);

                    //Enemy chases the player
                    //EnemyChase();

                    //When player passes this point they beat the level
                    if (walkRec.Y <= 0 && walkRec.X >= 600 && walkRec.X <= 678)
                    {
                        currentGameState = GameState.Win;
                        hasBeatenLvl1 = true;
                        YouWinInst.Play();
                    }
                    break;
                #endregion
                #region Level 2
                case GameState.Level2:
                    //Stops all sound effects when the level has started
                    onClickInst.Stop();
                    YouWinInst.Stop();
                    YouLoseInst.Stop();
                    if (isCounting)
                    {
                        //Timer CountDown
                        time -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    //Makes sure that time cannot pass 0
                    if (time < 0) time = 0;

                    //Pauses game
                    if (kb.IsKeyDown(Keys.P))
                    {
                        //MediaPlayer.Play(pauseMusic);
                        currentGameState = GameState.Paused;
                        isCounting = false;
                    }
                    //Player Movement
                    CharacterMovement(gameTime, walkRec, level2Colors, level2Rec);
                    //Will only occur check when the enemy has spawned in
                    if (spawnTime > time)
                    {
                        //Enemy and Player Collision Detection
                        if (EnemyDetection(walkRec, playerData, enemyRec, enemyData))
                        {
                            currentGameState = GameState.Lose;
                            isCounting = false;
                            YouLoseInst.Play();
                        }
                    }

                    //Enemy chases the player
                    //EnemyChase();

                    //When player passes this point they beat the level
                    if (walkRec.Y <= 5 && walkRec.X >= 565 && walkRec.X <= 635)
                    {
                        currentGameState = GameState.Win;
                        hasBeatenLvl2 = true;
                        YouWinInst.Play();
                    }

                    break;
                #endregion
                #region Level 3
                case GameState.Level3:
                    //Stops all sound effects when the level has started
                    onClickInst.Stop();
                    YouWinInst.Stop();
                    YouLoseInst.Stop();
                    if (isCounting)
                    {
                        //Timer CountDown
                        time -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    //Makes sure that time cannot pass 0
                    if (time < 0) time = 0;

                    //Pauses game
                    if (kb.IsKeyDown(Keys.P))
                    {
                        //MediaPlayer.Play(pauseMusic);
                        currentGameState = GameState.Paused;
                        isCounting = false;
                    }
                    //Player Movement
                    CharacterMovement(gameTime, walkRec, level1Colors, level1Rec);
                    //Will only occur check when the enemy has spawned in
                    if (spawnTime > time)
                    {
                        //Enemy and Player Collision Detection
                        if (EnemyDetection(walkRec, playerData, enemyRec, enemyData))
                        {
                            currentGameState = GameState.Lose;
                            isCounting = false;
                            YouLoseInst.Play();
                        }
                    }

                    //Enemy chases the player
                    //EnemyChase();

                    //When player passes this point they beat the level
                    if (walkRec.Y <= 5 && walkRec.X >= 565 && walkRec.X <= 635)
                    {
                        currentGameState = GameState.Win;
                        hasBeatenLvl3 = true;
                        YouWinInst.Play();
                    }

                    break;
                #endregion
                #region Lose
                case GameState.Lose:
                    //Stops all music
                    MediaPlayer.Stop();
                    RetryBtnLose();
                    //Returns user to the main menu
                    MenuBtnLose();
                    break;
                #endregion
                #region Paused
                case GameState.Paused:
                    MediaPlayer.Stop();
                    //Allows player to resume after pausing
                    ResumeBtn();
                    //Allows player to quit the game
                    QuitBtn();
                    break;
                #endregion
                #region Win
                case GameState.Win:
                    //Stops all music
                    MediaPlayer.Stop();
                    //Returns user to the main menu
                    MenuBtnWin();
                    //Returns player back to level
                    RetryBtnWin();
                    //Next Level
                    NextLvlBtn();
                    break;
                #endregion
            }

            //When it does reach zero the player loses
            if (time == 0)
            {
                YouLoseInst.Play();
                currentGameState = GameState.Lose;
            }

            //Allows the health font to follow player
            healthPos.X = walkRec.X - 15;
            healthPos.Y = walkRec.Y - 15;
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            //Coordinates
            spriteBatch.DrawString(mouseFont, font, mouseFontLoc, Color.Red);

            //Draws in based on the current gamestate
            switch (currentGameState)
            {
                #region MainMenu
                case GameState.MainMenu:
                    //MainMenu Screen
                    //Background && Title
                    spriteBatch.Draw(background, backgroundRec, Color.White);
                    spriteBatch.DrawString(gameTitle, title, titleLoc, Color.White);
                    //Buttons
                    btnPlay.Draw(spriteBatch);
                    btnSettings.Draw(spriteBatch);
                    btnControl.Draw(spriteBatch);
                    btnExit.Draw(spriteBatch);
                    spriteBatch.Draw(about, aboutRec, Color.White);
                    break;
                #endregion
                #region Level Select
                case GameState.LevelSelect:
                    //Level Selection Screen
                    //Background && Title
                    spriteBatch.Draw(selctionBg, selectionRec, Color.White);
                    spriteBatch.Draw(levelSelectTitle, lvlSelectRec, Color.White);
                    //Counted loop to draw each of the level buttons
                    for (int l = 0; l < levelnumber.Length; l++)
                    {
                        spriteBatch.Draw(levelnumber[l], lvlNumRec[l], Color.White);
                    }
                    //Back Button
                    btnReturn.Draw(spriteBatch);

                    spriteBatch.DrawString(mouseFont, font, mouseFontLoc, Color.Red);
                    break;
                #endregion
                #region Options
                case GameState.Options:
                    //Options Screen
                    //Background && Title
                    spriteBatch.Draw(optionsBg, optionsBgRec, Color.White);
                    spriteBatch.Draw(optionsTitle, optionsTitleRec, Color.White);
                    //Buttons
                    spriteBatch.Draw(resetBtn, resetRec, Color.White);
                    btnReturn.Draw(spriteBatch);
                    //Volume Slider
                    spriteBatch.Draw(volSlider, volSliderRec, Color.White);
                    spriteBatch.Draw(buttonSlider, buttonSliderRec, Color.White);
                    //Strings
                    spriteBatch.DrawString(mouseFont, font, mouseFontLoc, Color.Red);
                    break;
                #endregion
                #region Controls
                case GameState.Controls:
                    //Controls Screen
                    //Title
                    spriteBatch.Draw(controlTitle, controlTitleRec, Color.White);
                    //Text
                    spriteBatch.DrawString(controlFont, controls, controlLoc, Color.White);
                    //Back Button
                    btnReturn.Draw(spriteBatch);
                    break;
                #endregion
                #region About
                case GameState.About:
                    //About Screen
                    //Title
                    spriteBatch.Draw(aboutTitle, aboutTitleRec, Color.White);
                    //Text
                    spriteBatch.DrawString(infoFont, info, infoLoc, Color.White);
                    //Back Button
                    btnReturn.Draw(spriteBatch);
                    break;
                #endregion
                #region Level 1
                case GameState.Level1:
                    //Maze
                    spriteBatch.Draw(level1, level1Rec, Color.White);
                    //Trap
                    for (int t = 0; t < 4; t++)
                    {
                        spriteBatch.Draw(aoeTrap, trapRec[t], Color.White);
                    }
                    //Timer
                    spriteBatch.DrawString(levelFont, timer + ((int)time).ToString() + "s", timerLoc, Color.Red);
                    //Power-Ups
                    for (int i = 0; i < 3; i++)
                    {
                        if (hpIsActive[i])
                        {
                            spriteBatch.Draw(hpPower[i], hpPowerRec[i], Color.White);
                        }
                        if (speedIsActive[i])
                        {
                            spriteBatch.Draw(speedPower[i], speedPowerRec[i], Color.White);
                        }
                    }
                    //Player
                    spriteBatch.Draw(currentAnim, walkRec, walkSrcRec, Color.White);
                    spriteBatch.DrawString(healthFont, health + hp, healthPos, Color.Red);
                    //Controls when the enemy is drawn into the game
                    if (spawnTime > time)
                    {
                        if (direction == UP)
                        {
                            spriteBatch.Draw(enemy, enemyRec, Color.White);
                        }
                        else if (direction == DOWN)
                        {
                            spriteBatch.Draw(enemy, enemyRec, null, Color.White);
                        }
                    }
                    break;
                #endregion
                #region Level 2
                case GameState.Level2:
                    spriteBatch.Draw(level2, level2Rec, Color.White);
                    //Player
                    spriteBatch.Draw(currentAnim, walkRec, walkSrcRec, Color.White);
                    spriteBatch.DrawString(healthFont, health + hp, healthPos, Color.Red);
                    //Controls when the enemy is drawn into the game
                    if (spawnTime > time)
                    {
                        spriteBatch.Draw(enemy, enemyRec, Color.White);
                    }
                    //Timer
                    spriteBatch.DrawString(levelFont, timer + ((int)time).ToString() + "s", timerLoc, Color.Red);
                    //Power-Ups
                    for (int i = 0; i < 3; i++)
                    {
                        if (hpIsActive[i])
                        {
                            spriteBatch.Draw(hpPower[i], hpPowerRec[i], Color.White);
                        }
                        if (speedIsActive[i])
                        {
                            spriteBatch.Draw(speedPower[i], speedPowerRec[i], Color.White);
                        }
                    }
                    //Trap
                    for (int t = 0; t < 4; t++)
                    {
                        spriteBatch.Draw(aoeTrap, trapRec[t], Color.White);
                    }
                    break;
                #endregion
                #region Level 3
                case GameState.Level3:
                    spriteBatch.Draw(level3, level3Rec, Color.White);
                    //Player
                    spriteBatch.Draw(currentAnim, walkRec, walkSrcRec, Color.White);
                    spriteBatch.DrawString(healthFont, health + hp, healthPos, Color.Red);
                    //Controls when the enemy is drawn into the game
                    if (spawnTime > time)
                    {
                        spriteBatch.Draw(enemy, enemyRec, Color.White);
                    }
                    //Timer
                    spriteBatch.DrawString(levelFont, timer + ((int)time).ToString() + "s", timerLoc, Color.Red);
                    //Power-Ups
                    for (int i = 0; i < 3; i++)
                    {
                        if (hpIsActive[i])
                        {
                            spriteBatch.Draw(hpPower[i], hpPowerRec[i], Color.White);
                        }
                        if (speedIsActive[i])
                        {
                            spriteBatch.Draw(speedPower[i], speedPowerRec[i], Color.White);
                        }
                    }
                    break;
                #endregion
                #region Paused
                case GameState.Paused:
                    spriteBatch.Draw(pauseSc, pauseScRec, Color.White);
                    spriteBatch.DrawString(mouseFont, font, mouseFontLoc, Color.Red);
                    break;
                #endregion
                #region Lose
                case GameState.Lose:
                    spriteBatch.Draw(lose, loseRec, Color.White);
                    spriteBatch.DrawString(mouseFont, font, mouseFontLoc, Color.Red);
                    break;
                #endregion
                #region Win
                case GameState.Win:
                    spriteBatch.Draw(win, winRec, Color.White);
                    spriteBatch.DrawString(mouseFont, font, mouseFontLoc, Color.Red);
                    break;
                #endregion
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Loads the about page, that displays the game info
        /// </summary>
        private void AboutBtn()
        {
            const int btnX = 1237;
            const int btnY = 741;
            const int btnW = 28;
            const int btnH = 51;

            if (mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton == ButtonState.Released &&
                mouse.X >= btnX && mouse.X <= (btnX + btnW) && mouse.Y >= btnY && mouse.Y <= (btnY + btnH))
            {
                onClick.Play();
                currentGameState = GameState.About;
            }
        }
        /// <summary>
        /// Brings the user to stage\level 1 of the game
        /// </summary>
        private void Level1Btn()
        {
            if (mouse.LeftButton == ButtonState.Pressed && mouse.X >= lvlNumRec[0].X && mouse.X <= lvlNumRec[0].X + lvlNumRec[0].Width 
                && mouse.Y >= lvlNumRec[0].Y && mouse.Y <= lvlNumRec[0].Y + lvlNumRec[0].Height)
            {
                onClick.Play();
                MediaPlayer.Play(levelTheme);
                time = 90.0f;
                spawnTime = 88.0f;
                leftSpeed = 3f;
                rightSpeed = 3f;
                upSpeed = 3f;
                downSpeed = 3f;
                isCounting = true;
                isLevel1 = true;
                for (int i = 0; i < 3; i++)
                {
                    hpIsActive[i] = true;
                    speedIsActive[i] = true;
                    invincIsActive[i] = true;
                }
                currentGameState = GameState.Level1;
            }
        }
        /// <summary>
        /// Brings the user to stage\level 2 of the game, if they have already beaten level 1
        /// </summary>
        private void Level2Btn()
        {
            if (mouse.LeftButton == ButtonState.Pressed && mouse.X >= lvlNumRec[1].X && mouse.X <= lvlNumRec[1].X + lvlNumRec[1].Width
                && mouse.Y >= lvlNumRec[1].Y && mouse.Y <= lvlNumRec[1].Y + lvlNumRec[1].Height)
            {
                //Can be clicked if the user already beaten level 1 
                if (hasBeatenLvl1)
                {
                    currentGameState = GameState.Level2;
                    MediaPlayer.Play(levelTheme);
                    time = 100.0f;
                    spawnTime = 97.0f;
                    leftSpeed = 3f;
                    rightSpeed = 3f;
                    upSpeed = 3f;
                    downSpeed = 3f;
                    isCounting = true;
                    isLevel2 = true;
                    walkRec = new Rectangle(673, 739, currentAnim.Width / 9, currentAnim.Height);
                    currentAnim = upWalk;
                    enemyRec = new Rectangle(673, 726, enemy.Width / 4, enemy.Height / 4);
                    ////Maze Elements
                    //healthLoc = new Vector2(502, 574);
                    //healthLoc2 = new Vector2(911, 157);
                    //healthLoc3 = new Vector2(100, 404);
                    //speedLoc = new Vector2(499, 574);
                    //speedLoc2 = new Vector2(350, 447);
                    //speedLoc3 = new Vector2(25, 515);
                }
            }
        }
        /// <summary>
        /// Brings the user to stage\level 3 of the game, if they have already beaten level 2
        /// </summary>
        private void Level3Btn()
        {
            if (mouse.LeftButton == ButtonState.Pressed && mouse.X >= lvlNumRec[2].X && mouse.X <= lvlNumRec[2].X + lvlNumRec[2].Width
                && mouse.Y >= lvlNumRec[2].Y && mouse.Y <= lvlNumRec[2].Y + lvlNumRec[2].Height)
            {
                //Can be clicked if the user already beaten level 1 
                if (hasBeatenLvl2)
                {
                    currentGameState = GameState.Level3;
                    MediaPlayer.Play(levelTheme);
                    time = 110.0f;
                    spawnTime = 107.0f;
                    leftSpeed = 3f;
                    rightSpeed = 3f;
                    upSpeed = 3f;
                    downSpeed = 3f;
                    isCounting = true;
                    isLevel3 = true;
                    walkRec = new Rectangle(675, 736, currentAnim.Width / 9, currentAnim.Height);
                    currentAnim = upWalk;
                    enemyRec = new Rectangle(675, 726, enemy.Width / 4, enemy.Height / 4);
                }
            }
        }
        /// <summary>
        /// Directional character movement
        /// </summary>
        /// <param name="gameTime">Allows the animate param to work inside CharacterMovement</param>
        private void CharacterMovement(GameTime gameTime, Rectangle playerRec, Color[] mazeColor, Rectangle maze)
        {
            //Directional character movement
            //Downward
            if (kb.IsKeyDown(Keys.D))
            {
                //isRight = true;
                walkRec.X += (int)rightSpeed;
                walkRec2.X += (int)rightSpeed;
                currentAnim = rightWalk;
                Animate(gameTime);
            }
            //Left
            else if (kb.IsKeyDown(Keys.A))
            {
                //isLeft = true;
                walkRec.X -= (int)leftSpeed;
                walkRec2.X -= (int)leftSpeed;
                currentAnim = leftWalk;
                Animate(gameTime);
            }
            //Forward
            else if (kb.IsKeyDown(Keys.W))
            {
                //isUp = true;
                walkRec.Y -= (int)upSpeed;
                walkRec2.Y -= (int)upSpeed;
                currentAnim = upWalk;
                Animate(gameTime);
            }
            //Downward
            else if (kb.IsKeyDown(Keys.S))
            {
                //isDown = true;
                walkRec.Y += (int)downSpeed;
                walkRec2.Y += (int)downSpeed;
                currentAnim = downWalk;
                Animate(gameTime);
            }
            //Draw the last facing direction the player was in
            else
            {
                walkSrcRec = new Rectangle(0, 0, currentAnim.Width / 9, currentAnim.Height);
            }
            //Wall Detection
            //Moving Forward
            if (WallDetectionTop(playerRec, mazeColor, maze))
            {
                if (currentAnim == upWalk)
                {
                    upSpeed = 0f;
                }
            }
            else if(!(WallDetectionTop(playerRec, mazeColor, maze)))
            {
               if (currentAnim != upWalk)
                {
                    upSpeed = 3f;
                }
            }
            //Moving Left
            if (WallDetectionLeft(playerRec, mazeColor, maze))
            {
                if (currentAnim == leftWalk)
                {
                    leftSpeed = 0f;
                }
            }
            else if (!(WallDetectionLeft(playerRec, mazeColor, maze)))
            {
                if (currentAnim != leftWalk)
                {
                    leftSpeed = 3f;
                }
            }
            //Moving Right
            if (WallDetectionRight(playerRec, mazeColor, maze))
            {
                if (currentAnim == rightWalk)
                {
                    rightSpeed = 0f;
                }
            }
            else if (!(WallDetectionRight(playerRec, mazeColor, maze)))
            {
                if (currentAnim != rightWalk)
                {
                    rightSpeed = 3f;
                }
            }
            //Moving Downward
            if (WallDetectionDown(playerRec, mazeColor, maze))
            {
                if (currentAnim == downWalk)
                {
                    downSpeed = 0f;
                }
            }
            else if(!(WallDetectionDown(playerRec, mazeColor, maze)))
            {
                if (currentAnim != downWalk)
                {
                    downSpeed = 3f;
                }
            }
        }
        /// <summary>
        /// Character animation
        /// </summary>
        /// <param name="gameTime">Controls when the animation plays</param>
       private void Animate(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsed >= delay)
            {
                frames = (frames + 1) % 9;
                elapsed = 0;
            }

            walkSrcRec = new Rectangle(30 * frames, 0, currentAnim.Width / 9, currentAnim.Height);
        }
        /// <summary>
        /// Allows the enemy ship to follow the player
        /// </summary>
        ///
        private void EnemyMovement()
        {
            
        }
        /// <summary>
        /// Displayed on lose screen, will allow user to retry the current level
        /// </summary>
        private void RetryBtnLose()
        {
            const int btnX = 1011;
            const int btnW = 269;
            const int btnY = 689;
            const int btnH = 111;

            if (mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton == ButtonState.Released &&
                mouse.X >= btnX && mouse.X <= (btnX + btnW) && mouse.Y >= btnY && mouse.Y <= (btnY + btnH))
            {
                //If user loses they are brought back to the level
                if (isLevel1)
                {
                    onClick.Play();
                    MediaPlayer.Play(levelTheme);
                    //Resets the game
                    time = 90.0f;
                    spawnTime = 88.0f;
                    leftSpeed = 3f;
                    rightSpeed = 3f;
                    upSpeed = 3f;
                    downSpeed = 3f;
                    isCounting = true;
                    for (int i = 0; i < 3; i++)
                    {
                        hpIsActive[i] = true;
                        speedIsActive[i] = true;
                        invincIsActive[i] = true;
                    }
                    walkRec = new Rectangle(622, 739, currentAnim.Width / 9, currentAnim.Height);
                    currentAnim = upWalk;
                    enemyRec = new Rectangle(622, 726, enemy.Width / 4, enemy.Height / 4);
                    currentGameState = GameState.Level1;
                }
                if (isLevel2)
                {
                    onClick.Play();
                    MediaPlayer.Play(levelTheme);
                    //Resets the game
                    time = 100.0f;
                    spawnTime = 97.0f;
                    isCounting = true;
                    for (int i = 0; i < 3; i++)
                    {
                        hpIsActive[i] = true;
                        speedIsActive[i] = true;
                        invincIsActive[i] = true;
                    }
                    walkRec = new Rectangle(673, 739, currentAnim.Width / 9, currentAnim.Height);
                    currentAnim = upWalk;
                    enemyRec = new Rectangle(673, 726, enemy.Width / 4, enemy.Height / 4);
                    currentGameState = GameState.Level2;
                }
                if (isLevel3)
                {
                    onClick.Play();
                    MediaPlayer.Play(levelTheme);
                    //Resets the game
                    time = 110.0f;
                    spawnTime = 107.0f;
                    isCounting = true;
                    for (int i = 0; i < 3; i++)
                    {
                        hpIsActive[i] = true;
                        speedIsActive[i] = true;
                        invincIsActive[i] = true;
                    }
                    walkRec = new Rectangle(675, 736, currentAnim.Width / 9, currentAnim.Height);
                    currentAnim = upWalk;
                    enemyRec = new Rectangle(675, 726, enemy.Width / 4, enemy.Height / 4);
                    currentGameState = GameState.Level3;
                }
            }
        }
        /// <summary>
        /// Displayed on lose screen, will allow user to return to the main menu
        /// </summary>
            private void MenuBtnLose()
        {
            const int btnX = 0;
            const int btnW = 267;
            const int btnY = 692;
            const int btnH = 108;

            if (mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton == ButtonState.Released &&
                mouse.X >= btnX && mouse.X <= (btnX + btnW) && mouse.Y >= btnY && mouse.Y <= (btnY + btnH))
            {
                onClick.Play();
                MediaPlayer.Play(backgroundTheme);
                //Resets the game values
                time = 90.0f;
                spawnTime = 88.0f;
                isLevel1 = false;
                walkRec = new Rectangle(622, 739, currentAnim.Width / 9, currentAnim.Height);
                currentAnim = upWalk;
                enemyRec = new Rectangle(622, 726, enemy.Width / 4, enemy.Height / 4);
                currentGameState = GameState.MainMenu;
            }
        }
        /// <summary>
        /// Displayed on win screen, will allow user to return to the main menu
        /// </summary>
        private void MenuBtnWin()
        {
            const int btnX = 0;
            const int btnW = 400;
            const int btnY = 680;
            const int btnH = 120;

            if (mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton == ButtonState.Released &&
                mouse.X >= btnX && mouse.X <= (btnX + btnW) && mouse.Y >= btnY && mouse.Y <= (btnY + btnH))
            {
                onClick.Play();
                MediaPlayer.Play(backgroundTheme);
                //Resets the game values
                time = 90.0f;
                spawnTime = 88.0f;
                isLevel1 = false;
                walkRec = new Rectangle(622, 739, currentAnim.Width / 9, currentAnim.Height);
                currentAnim = upWalk;
                enemyRec = new Rectangle(622, 726, enemy.Width / 4, enemy.Height / 4);
                currentGameState = GameState.MainMenu;
            }
        }
        /// <summary>
        /// Displayed on win screen, will allow user to retry the current level
        /// </summary>
        private void RetryBtnWin()
        {
            const int btnX = 489;
            const int btnW = 301;
            const int btnY = 702;
            const int btnH = 98;

            if (mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton == ButtonState.Released &&
               mouse.X >= btnX && mouse.X <= (btnX + btnW) && mouse.Y >= btnY && mouse.Y <= (btnY + btnH))
            {
                //If user wins but want to retry the level
                if (hasBeatenLvl1)
                {
                    onClick.Play();
                    MediaPlayer.Play(levelTheme);
                    //Resets the game
                    time = 90.0f;
                    spawnTime = 88.0f;
                    leftSpeed = 3f;
                    rightSpeed = 3f;
                    upSpeed = 3f;
                    downSpeed = 3f;
                    isCounting = true;
                    walkRec = new Rectangle(622, 739, currentAnim.Width / 9, currentAnim.Height);
                    currentAnim = upWalk;
                    enemyRec = new Rectangle(622, 726, enemy.Width / 4, enemy.Height / 4);
                    currentGameState = GameState.Level1;
                }
                if (hasBeatenLvl2)
                {
                    onClick.Play(); 
                    MediaPlayer.Play(levelTheme);
                    //Resets the game
                    time = 100.0f;
                    spawnTime = 97.0f;
                    leftSpeed = 3f;
                    rightSpeed = 3f;
                    upSpeed = 3f;
                    downSpeed = 3f;
                    isCounting = true;
                    walkRec = new Rectangle(673, 736, currentAnim.Width / 9, currentAnim.Height);
                    currentAnim = upWalk;
                    enemyRec = new Rectangle(673, 726, enemy.Width / 4, enemy.Height / 4);
                    currentGameState = GameState.Level2;
                }
                if (hasBeatenLvl3)
                {
                    onClick.Play();
                    MediaPlayer.Play(levelTheme);
                    //Resets the game
                    time = 110.0f;
                    spawnTime = 107.0f;
                    leftSpeed = 3f;
                    rightSpeed = 3f;
                    upSpeed = 3f;
                    downSpeed = 3f;
                    isCounting = true;
                    walkRec = new Rectangle(675, 736, currentAnim.Width / 9, currentAnim.Height);
                    currentAnim = upWalk;
                    enemyRec = new Rectangle(675, 726, enemy.Width / 4, enemy.Height / 4);
                    currentGameState = GameState.Level3;
                }
            }
        }
        /// <summary>
        /// Allows player to resume the game when paused
        /// </summary>
        private void ResumeBtn()
        {
            const int btnX = 430;
            const int btnW = 207;
            const int btnY = 283;
            const int btnH = 33;

            if (mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton == ButtonState.Released &&
                mouse.X >= btnX && mouse.X <= (btnX + btnW) && mouse.Y >= btnY && mouse.Y <= (btnY + btnH))
            {
                if (isLevel1)
                {
                    currentGameState = GameState.Level1;
                    isCounting = true;
                    MediaPlayer.Play(levelTheme);
                }
                if (isLevel2)
                {
                    currentGameState = GameState.Level2;
                    isCounting = true;
                    MediaPlayer.Play(levelTheme);
                }
                if (isLevel3)
                {
                    currentGameState = GameState.Level3;
                    isCounting = true;
                    MediaPlayer.Play(levelTheme);
                }
            }
        }
        /// <summary>
        /// Exits game from the pause screen
        /// </summary>
        private void QuitBtn()
        {
            const int btnX = 378;
            const int btnW = 311;
            const int btnY = 385;
            const int btnH = 39;

            if (mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton == ButtonState.Released &&
               mouse.X >= btnX && mouse.X <= (btnX + btnW) && mouse.Y >= btnY && mouse.Y <= (btnY + btnH))
            {
                this.Exit();
            }
        }
        /// <summary>
        /// Brings the user to stage\level of the game, if they've already beaten the previous level
        /// </summary>
        private void NextLvlBtn()
        {
            const int btnX = 903;
            const int btnW = 343;
            const int btnY = 735;
            const int btnH = 28;

            if (mouse.LeftButton == ButtonState.Pressed && prevMouse.LeftButton == ButtonState.Released &&
               mouse.X >= btnX && mouse.X <= (btnX + btnW) && mouse.Y >= btnY && mouse.Y <= (btnY + btnH))
            {
                //Brings the user to level 2
                if (hasBeatenLvl1)
                {
                    currentGameState = GameState.Level2;
                    MediaPlayer.Play(levelTheme);
                    time = 100.0f;
                    spawnTime = 97.0f;
                    leftSpeed = 3f;
                    rightSpeed = 3f;
                    upSpeed = 3f;
                    downSpeed = 3f;
                    isCounting = true;
                    isLevel1 = false;
                    isLevel2 = true;
                    walkRec = new Rectangle(673, 739, currentAnim.Width / 9, currentAnim.Height);
                    currentAnim = upWalk;
                    enemyRec = new Rectangle(673, 726, enemy.Width / 4, enemy.Height / 4);
                }
                //Brings user to level 3
                if (hasBeatenLvl2)
                {
                    currentGameState = GameState.Level3;
                    MediaPlayer.Play(levelTheme);
                    time = 110.0f;
                    spawnTime = 107.0f;
                    leftSpeed = 3f;
                    rightSpeed = 3f;
                    upSpeed = 3f;
                    downSpeed = 3f;
                    isCounting = true;
                    isLevel2 = false;
                    isLevel3 = true;
                    walkRec = new Rectangle(675, 739, currentAnim.Width / 9, currentAnim.Height);
                    currentAnim = upWalk;
                    enemyRec = new Rectangle(675, 726, enemy.Width / 4, enemy.Height / 4);
                }
            }
        }
        /// <summary>
        /// This will tell the game when the enemy has caught up to the player, based on the color differences in their sprites
        /// </summary>
        /// <param name="player">Uses the data from the player's dest recangle</param>
        /// <param name="playerColor">Uses the colors gathered from the player sprite</param>
        /// <param name="ufo">Uses the data from the enemy's dest recangle</param>
        /// <param name="enemyColor">Uses the colors gathered from the enemy sprite</param>
        /// <returns>Returns true when both images aren't completely transparent, else it returns false</returns>
        private bool EnemyDetection(Rectangle player, Color[] playerColor, Rectangle ufo, Color[] enemyColor)
        {
            int top = Math.Max(player.Top, ufo.Top);
            int bottom = Math.Min(player.Bottom, ufo.Bottom);
            int left = Math.Max(player.Left, ufo.Left);
            int right = Math.Min(player.Right, ufo.Right);
            //Checks to see if the to rectangles collided
            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    Color colour1 = playerColor[(x - player.Left) +
                                    (y - player.Top) * player.Width];
                    Color colour2 = enemyColor[(x - ufo.Left) +
                                    (y - ufo.Top) * ufo.Width];

                    if (colour1.A != 0 && colour2.A != 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private bool EnemyWallDetection(Rectangle ufoRec, Color[] mazeColor, Rectangle maze)
        {
            Color enemyColor = mazeColor[ufoRec.Y * maze.Width + (ufoRec.X + (ufoRec.Width / 2))];
            return enemyColor.A != 0;
        }
        /// <summary>
        /// This will tell the game when the player has collided with one of the walls when walking forward
        /// Using the midpoint of the top rectangle
        /// </summary>
        /// <param name="playerRec">Uses the data from the player's dest recangle</param>
        /// <param name="mazeColor">Uses the data collected from the maze sprite</param>
        /// <param name="maze">Uses the data from the mazes' dest recangle</param>
        /// <returns>Will return true if the player colors' alpha value is not transparent</returns>
        private bool WallDetectionTop(Rectangle playerRec, Color[] mazeColor, Rectangle maze)
        {
            Color playerColor = mazeColor[playerRec.Y * maze.Width + (playerRec.X + (playerRec.Width / 2))];
            return playerColor.A != 0;
        }
        /// <summary>
        /// This will tell the game when the player has collided with one of the walls when walking left
        /// Using the midpoint of the left side of the rectangle
        /// </summary>
        /// <param name="playerRec">Uses the data from the player's dest recangle</param>
        /// <param name="mazeColor">Uses the data collected from the maze sprite</param>
        /// <param name="maze">Uses the data from the mazes' dest recangle</param>
        /// <returns>Will return true if the player colors' alpha value is not transparent</returns>
        private bool WallDetectionLeft(Rectangle playerRec, Color[] mazeColor, Rectangle maze)
        {
            Color playerColor = mazeColor[(playerRec.Y + (playerRec.Height / 2)) * maze.Width + playerRec.X];
            return playerColor.A != 0;
        }
        /// <summary>
        /// This will tell the game when the player has collided with one of the walls when walking right
        /// Using the midpoint of the right side of the rectangle
        /// </summary>
        /// <param name="playerRec">Uses the data from the player's dest recangle</param>
        /// <param name="mazeColor">Uses the data collected from the maze sprite</param>
        /// <param name="maze">Uses the data from the mazes' dest recangle</param>
        /// <returns>Will return true if the player colors' alpha value is not transparent</returns>
        private bool WallDetectionRight(Rectangle playerRec, Color[] mazeColor, Rectangle maze)
        {
            Color playerColor = mazeColor[(playerRec.Y + (playerRec.Height / 2)) * maze.Width + (playerRec.X + playerRec.Width)];
            return playerColor.A != 0;
        }
        /// <summary>
        /// This will tell the game when the player has collided with one of the walls when walking down
        /// Using the midpoint of the bottom of the rectangle
        /// </summary>
        /// <param name="playerRec">Uses the data from the player's dest recangle</param>
        /// <param name="mazeColor">Uses the data collected from the maze sprite</param>
        /// <param name="maze">Uses the data from the mazes' dest recangle</param>
        /// <returns>Will return true if the player colors' alpha value is not transparent</returns>
        private bool WallDetectionDown(Rectangle playerRec, Color[] mazeColor, Rectangle maze)
        {
            Color playerColor = mazeColor[(playerRec.Y + playerRec.Height) * maze.Width + (playerRec.X + (playerRec.Width / 2))];
            return playerColor.A != 0;
        }
        /// <summary>
        /// Checks whether the player has got the power-up, and whether it is still active
        /// </summary>
        /// <param name="gameTime">Controls how long the player has the power-up active</param>
        private void PowerUpCollision(GameTime gameTime)
        {
            for (int i = 0; i < 3; i++)
            {
                if (isLevel1)
                {
                    //Health Power-Ups
                    if (hpIsActive[i])
                    {
                        if (walkRec.X <= (hpPowerRec[i].X + hpPowerRec[i].Width) && (walkRec.X + walkRec.Width) >= hpPowerRec[i].X
                            && walkRec.Y <= (hpPowerRec[i].Y + hpPowerRec[i].Height) && (walkRec.Y + walkRec.Height ) >= hpPowerRec[i].Y)
                        {
                            hp = 100;
                            hpIsActive[i] = false;
                        }
                    }
                    //Speed Power-Ups
                    if (speedIsActive[i])
                    {
                         if (walkRec.X <= (speedPowerRec[i].X + speedPowerRec[i].Width) && (walkRec.X + walkRec.Width) >= speedPowerRec[i].X
                            && walkRec.Y <= (speedPowerRec[i].Y + speedPowerRec[i].Height) && (walkRec.Y + walkRec.Height ) >= speedPowerRec[i].Y)
                        {
                            speedIsActive[i] = false;
                            isFaster = true;
                        }
                    }
                    for (int t = 0; t < 4; t++)
                    {
                        //Traps
                        if (walkRec.X <= (trapRec[t].X + trapRec[t].Width) && (walkRec.X + walkRec.Width) >= trapRec[t].X
                               && walkRec.Y <= (trapRec[t].Y + trapRec[t].Height) && (walkRec.Y + walkRec.Height) >= trapRec[t].Y)
                        {
                            //dmgTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                            //hp = hp - (int)(dmgTimer * dmg);
                        }
                    }
                }
            }
            //Controls the how long the player has the speed power-up
            if (isFaster)
            {
                speedTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                leftSpeed = 4f;
                rightSpeed = 4f;
                upSpeed = 4f;
                downSpeed = 4f;
            }
            if (speedTimer <= 0)
            {
                isFaster = false;
                leftSpeed = 3f;
                rightSpeed = 3f;
                upSpeed = 3f;
                downSpeed = 3f;
                speedTimer = 3f;
            }
        }
    }
}
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Pong
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Vector2 bal_pos, redPlayer_pos, bluePlayer_pos, scuttlecrab_pos, Porokoekje2_pos, Mustache1_pos, Achtergrond_pos, PoroAchtergrond_pos, Healthrelic_pos;
        Texture2D bal_png, bluePlayer_png, redPlayer_png, cursorIndicator_png, scuttlecrab_png, Porokoekje2_png, Mustache1_png, Achtergrond_png, PoroAchtergrond_png, Healthrelic_png;
        SpriteFont spriteFont1, spriteFont2, spriteFont3, spriteFont4, spriteFont5;
        public int Width = 1280, Height = 720;
        public int scuttlecrab_X, scuttlecrab_Y, Porokoekje2_X, Porokoekje2_Y, Mustache1_X, Mustache1_Y, Healthrelic_X, Healthrelic_Y, Direction, bluePlayer_Y, redPlayer_Y, bluePlayer_X, redPlayer_X, bluePlayerVelocity_Y, redPlayerVelocity_Y, bluePlayerVelocity_X, redPlayerVelocity_X, ball_X, ball_Y, redPLayerLives, bluePLayerLives, winCondition = 0;
        protected static Point screen;
        KeyboardState oldKeyboardState, currentKeyboardState; // Keyboard state variables
        double ballTravelAngle, ballTravelSpeed, ballVelocity_Y, ballVelocity_X, zz, angleObjectHitWall;
        Random rnd = new Random();
        public int gameState = 0, menuState = 0, MenuOptions;
        public int redPlayerScore_Y, redPlayerScore_X, bluePlayerScore_Y, bluePlayerScore_X, cursor_X, cursor_Y, start_Y, start_X, exit_X, exit_Y, options_X, options_Y, cursorIndicating = 0, cursorString, PvP_X, PvP_Y, PvE_X, PvE_Y;
        public string Btimeleft = "", bonustime = "", bonus4 = "", bonus = "", scored = "", winner = "null", start = "Start", exit = "Exit", options = "Options", PvP = "2 Players", PvE = "1 Player, 1 Computer", bluePLayerLives_string = "0", redPLayerLives_string = "0";
        Vector2 cursorIndicator_pos, start_pos, exit_pos, options_pos, PvP_pos, PvE_pos, bluePlayerScore_pos, redPlayerScore_pos;
        Boolean AI_1, bounced = false, lastBounced = false;
        public Boolean powerUps = true;
        public int Powerup; //Er is een Powerup in het spel
        public int Bonus; //Er is een Bonus
        public int ExtraTekst; //Extra tekst van W
        public int ETekstB4; //Extra tekst van V
        public int Bonus123; //Als deze 1 is wordt de tekst van bonus 1, 2 en 3 geschreven
        int V; //tijd waarop er tekst van bonus 4 geschreven moet worden
        public int W; //tijd waarop er tekst van welke speler er gescoord heeft geschreven moet worden
        int P; //Tijd waarop de tekst van bonus 1, 2 en 3 geschreven moet worden
        int Vx = 3; //tijd hoe lang de tekst van V en W in beeld moet blijven
        public int BonusTimeBlue = 0; //Geeft aan dat de blauwe bonustime geschreven moet worden
        public int BonusTimeRed = 0; //Geeft aan dat de rode bonustime geschreven moet worden
        public int T; //Dit is de bonustijd
        public int LastScored; //Wie er als laatst gescored heeft
        int Wx = 0; //Als deze 0 is mag de W tekst in beeld zijn
        int startingLives = 5;
        public int BallhitPowerup, blueScale = 1, redScale = 1; //De bal raakt de Powerup
        int G = 5; //Tijd waarop eerste Powerup spawntime wordt ingesteld in seconden
        int Sx = 10; //Tijd hoe lang een Powerup blijft in seconden
        int Bx = 10; //Tijd hoe lang een Bonus blijft in seconden
        int B = 0; // Tijd waarop Bonus start
        int S = 0; //Aantal seconden dat de game duurt
        int redPlayerSpeed_Y = 5, bluePlayerSpeed_Y = 5;
        int WelkePowerup = 0; //Geeft aan welke Powerup er spawned
        int Ballhitplayer = 0; //Geeft aan welke speler als laatst de bal heeft geraakt, 1 == Rood, 2 == Blauw
        int BlueplayerBonus = 0; //Geeft aan of de blauwe speler de bonus heeft
        int RedplayerBonus = 0; //Geeft aan of de rode speler de bonus heeft
        int Snorbonus = 0; //Geeft aan of de normale besturing werkt of niet
        int Reset = 0; //Speler heeft gescoord en bonussen worden gereset als deze 1 is
        public int startingBallSpeed = 5, Row = 2;
        public double ballIncreasePercentage = 1.05, ballIncreaseConstant = 0.5;
        //row 1: Starting ball speed(3,5,7) BallIncreasePercentage(0%, 5%, 10%, 15%)
        public string optionName11 = "Starting ball speed", optionName12 = "ball increase percentage", option12a = "0%", option12b = "5%", option12c = "10%", option12d = "15%";
        public int option11a = 3, option11b = 5, option11c = 7;
        public int optionName11_X, optionName12_X, option12a_X, option12b_X, option12c_X, option12d_X, option11a_X, option11b_X, option11c_X;
        public int optionName11_Y, optionName12_Y, option12a_Y, option12b_Y, option12c_Y, option12d_Y, option11a_Y, option11b_Y, option11c_Y;
        Vector2 optionName11_pos, optionName12_pos, option12a_pos, option12b_pos, option12c_pos, option12d_pos, option11a_pos, option11b_pos, option11c_pos;
        //row 2:Lives(1,3,5,7,9,11,13) Back to main
        public string optionName21 = "Lives", optionName22 = "Back to main menu";
        public int option21a = 1, option21b = 3, option21c = 5, option21d = 7, option21e = 9, option21f = 11, option21g = 13;
        public int optionName21_Y, optionName22_Y, option21a_Y, option21b_Y, option21c_Y, option21d_Y, option21e_Y, option21f_Y, option21g_Y;
        public int optionName21_X, optionName22_X, option21a_X, option21b_X, option21c_X, option21d_X, option21e_X, option21f_X, option21g_X;
        Vector2 optionName21_pos, optionName22_pos, option21a_pos, option21b_pos, option21c_pos, option21d_pos, option21e_pos, option21f_pos, option21g_pos;
        //row 3:ballIncreaseConstant(0,0.5,1,1.5,2) powerups(on, off)
        public string optionName31 = "Ball Increase Constant", optionName32 = "PowerUps?", option32a = "Yes", option32b = "No";
        public double option31a = 0, option31b = 0.5, option31c = 1, option31d = 1.5, option31e = 2.0;
        public int option31a_Y, option31b_Y, option31c_Y, option31d_Y, option31e_Y, optionName31_Y, optionName32_Y, option32a_Y, option32b_Y;
        public int option31a_X, option31b_X, option31c_X, option31d_X, option31e_X, optionName31_X, optionName32_X, option32a_X, option32b_X;
        Vector2 optionName31_pos, optionName32_pos, option32a_pos, option32b_pos, option31a_pos, option31b_pos, option31c_pos, option31d_pos, option31e_pos;
        protected Song song1, song2;
        int songPlaying = 0;
        public Game1()
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

            // TODO: Add your initialization logic here

            currentKeyboardState = new KeyboardState();
            base.Initialize();
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();
            ball_X = Width / 2 - bal_png.Width / 2;
            ball_Y = Height / 2 - bal_png.Height / 2;
            ballTravelSpeed = startingBallSpeed;


        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            bal_png = Content.Load<Texture2D>("bal");
            bluePlayer_png = Content.Load<Texture2D>("blauweSpeler");
            redPlayer_png = Content.Load<Texture2D>("rodeSpeler");
            cursorIndicator_png = Content.Load<Texture2D>("CursorIndicator");
            spriteFont1 = Content.Load<SpriteFont>("spriteFont1");
            spriteFont2 = Content.Load<SpriteFont>("spriteFont2");
            bluePlayer_Y = Height / 2 - bluePlayer_png.Height / 2;
            redPlayer_Y = Height / 2 - redPlayer_png.Height / 2;
            bluePlayer_X = Width - bluePlayer_png.Width * blueScale - bluePlayer_png.Width / 2;
            redPlayer_X = redPlayer_png.Width - redPlayer_png.Width / 2;
            scuttlecrab_png = Content.Load<Texture2D>("scuttlecrab");
            Porokoekje2_png = Content.Load<Texture2D>("Porokoekje2");
            Mustache1_png = Content.Load<Texture2D>("Mustache1");
            Healthrelic_png = Content.Load<Texture2D>("Healthrelic");
            Achtergrond_png = Content.Load<Texture2D>("Achtergrond");
            PoroAchtergrond_png = Content.Load<Texture2D>("PoroAchtergrond");
            spriteFont3 = Content.Load<SpriteFont>("spriteFont3");
            spriteFont4 = Content.Load<SpriteFont>("spriteFont4");
            spriteFont5 = Content.Load<SpriteFont>("spriteFont5");
            song1 = Content.Load<Song>("Braum");
            song2 = Content.Load<Song>("Challengers");
            MediaPlayer.Play(song1);
            MediaPlayer.Volume = (float)0.2;
            MediaPlayer.IsRepeating = true;
            do
            {
                ballTravelAngle = rnd.NextDouble() * 360;
            } while (ballTravelAngle > 315 || ballTravelAngle < 45 || (ballTravelAngle > 135 && ballTravelAngle < 225));

        }

        protected override void Update(GameTime gameTime)
        {
            oldKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
            int CenterHeight = Height / 2;
            if (gameState == 0)//Menu
            {
                if (songPlaying != 1)
                {
                    songPlaying = 1;
                    MediaPlayer.Stop();
                    MediaPlayer.Play(song1);
                }
                Wx = 1;
                if (currentKeyboardState.IsKeyDown(Keys.Up) && currentKeyboardState != oldKeyboardState && cursorIndicating != 0)
                {
                    cursorIndicating -= 1;
                }
                if (currentKeyboardState.IsKeyDown(Keys.Down) && currentKeyboardState != oldKeyboardState && cursorIndicating != MenuOptions - 1)
                {
                    cursorIndicating += 1;
                }
                if (menuState == 0)
                {
                    MenuOptions = 3;
                    start = "Start"; exit = "Exit"; options = "Options";

                    switch (cursorIndicating)
                    {
                        case 0:
                            cursor_Y = start_Y;
                            cursorString = start.Length;
                            break;
                        case 1:
                            cursor_Y = options_Y;
                            cursorString = options.Length;
                            break;
                        case 2:
                            cursor_Y = exit_Y;
                            cursorString = exit.Length;
                            break;
                    }
                    start_X = Width / 2 - start.Length * 14 / 2;
                    options_X = Width / 2 - options.Length * 14 / 2;
                    exit_X = Width / 2 - exit.Length * 14 / 2;

                    options_Y = CenterHeight;
                    start_Y = CenterHeight - Height / 20;
                    exit_Y = CenterHeight + Height / 20;

                    start_pos = new Vector2(start_X, start_Y);
                    options_pos = new Vector2(options_X, options_Y);
                    exit_pos = new Vector2(exit_X, exit_Y);
                }
                if (menuState == 1)
                {
                    MenuOptions = 2;

                    switch (cursorIndicating)
                    {
                        case 0:
                            cursor_Y = PvP_Y;
                            cursorString = PvP.Length;
                            break;
                        case 1:
                            cursor_Y = PvE_Y;
                            cursorString = PvE.Length;
                            break;

                    }
                    PvP_X = Width / 2 - PvP.Length * 14 / 2;
                    PvE_X = Width / 2 - PvE.Length * 14 / 2;
                    PvP_Y = CenterHeight - Height / 20;
                    PvE_Y = CenterHeight;
                    PvP_pos = new Vector2(PvP_X, PvP_Y);
                    PvE_pos = new Vector2(PvE_X, PvE_Y);
                }
                if (menuState == 2)
                /*2 rows 
                row 1: Starting ball speed(3,5,7) BallIncreasePercentage(0%, 5%, 10%, 15%)
                row 2:Lives(1,3,5,7,9,11,13) Back to main
                row 3:ballIncreaseConstant(0,0.5,1,1.5,2) powerups(on, off)*/
                {
                    optionName12_X = Width / 4 * 1 - 100;
                    optionName12_Y = Height / 2;
                    optionName11_X = optionName12_X; option12a_X = optionName12_X; option12b_X = optionName12_X; option12c_X = optionName12_X; option12d_X = optionName12_X; option11a_X = optionName12_X; option11b_X = optionName12_X; option11c_X = optionName12_X;
                    optionName11_Y = optionName12_Y - 14 * 4; option12a_Y = optionName12_Y + 14 * 1; option12b_Y = optionName12_Y + 14 * 2; option12c_Y = optionName12_Y + 14 * 3; option12d_Y = optionName12_Y + 14 * 4; option11a_Y = optionName12_Y - 14 * 3; option11b_Y = optionName12_Y - 14 * 2; option11c_Y = optionName12_Y - 14 * 1;
                    option21d_X = Width / 4 * 2 - 100;
                    option21d_Y = optionName12_Y;
                    optionName21_X = option21d_X; optionName22_X = option21d_X; option21a_X = option21d_X; option21b_X = option21d_X; option21c_X = option21d_X; option21e_X = option21d_X; option21f_X = option21d_X; option21g_X = option21d_X;
                    optionName21_Y = optionName12_Y - 14 * 4; optionName22_Y = optionName12_Y + 14 * 4; option21a_Y = optionName12_Y - 14 * 3; option21b_Y = optionName12_Y - 14 * 2; option21c_Y = optionName12_Y - 14 * 1; option21d_Y = optionName12_Y; option21e_Y = optionName12_Y + 14 * 1; option21f_Y = optionName12_Y + 14 * 2; option21g_Y = optionName12_Y + 14 * 3;
                    option31d_Y = optionName12_Y;
                    option31d_X = Width / 4 * 3 - 100;
                    option31a_X = option31d_X; option31b_X = option31d_X; option31c_X = option31d_X; option31e_X = option31d_X; optionName31_X = option31d_X; optionName32_X = option31d_X; option32a_X = option31d_X; option32b_X = option31d_X;
                    option31a_Y = option31d_Y - 14 * 3; option31b_Y = option31d_Y - 14 * 2; option31c_Y = option31d_Y - 14 * 1; option31e_Y = option31d_Y + 14 * 1; optionName31_Y = option31d_Y - 14 * 4; optionName32_Y = option31d_Y + 14 * 2; option32a_Y = option31d_Y + 14 * 3; option32b_Y = option31d_Y + 14 * 4;
                    optionName11_pos = new Vector2(optionName11_X, optionName11_Y); optionName12_pos = new Vector2(optionName12_X, optionName12_Y); option12a_pos = new Vector2(option12a_X, option12a_Y); option12b_pos = new Vector2(option12b_X, option12b_Y); option12c_pos = new Vector2(option12c_X, option12c_Y); option12d_pos = new Vector2(option12d_X, option12d_Y); option11a_pos = new Vector2(option11a_X, option11a_Y); option11b_pos = new Vector2(option11b_X, option11b_Y); option11c_pos = new Vector2(option11c_X, option11c_Y);
                    optionName21_pos = new Vector2(optionName21_X, optionName21_Y); optionName22_pos = new Vector2(optionName22_X, optionName22_Y); option21a_pos = new Vector2(option21a_X, option21a_Y); option21b_pos = new Vector2(option21b_X, option21b_Y); option21c_pos = new Vector2(option21c_X, option21c_Y); option21d_pos = new Vector2(option21d_X, option21d_Y); option21e_pos = new Vector2(option21e_X, option21e_Y); option21f_pos = new Vector2(option21f_X, option21f_Y); option21g_pos = new Vector2(option21g_X, option21g_Y);
                    optionName31_pos = new Vector2(optionName31_X, optionName31_Y); optionName32_pos = new Vector2(optionName32_X, optionName32_Y); option32a_pos = new Vector2(option32a_X, option32a_Y); option32b_pos = new Vector2(option32b_X, option32b_Y); option31a_pos = new Vector2(option31a_X, option31a_Y); option31b_pos = new Vector2(option31b_X, option31b_Y); option31c_pos = new Vector2(option31c_X, option31c_Y); option31d_pos = new Vector2(option31d_X, option31d_Y); option31e_pos = new Vector2(option31e_X, option31e_Y);

                    MenuOptions = 9;//Y value to an extent
                    int Rows = 3;// X value to an extent
                    cursor_X = Width / 4 * Row + 180;

                    if (currentKeyboardState.IsKeyDown(Keys.Left) && currentKeyboardState != oldKeyboardState)
                    {
                        Console.WriteLine("left button detected)" + Row);
                        if (Row != 1)
                        {
                            Row -= 1;
                        }
                    }
                    else if (currentKeyboardState.IsKeyDown(Keys.Right) && currentKeyboardState != oldKeyboardState)
                    {
                        Console.WriteLine("right button detected)" + Row);
                        if (Row != Rows)
                        {
                            Row += 1;
                        }
                    }
                    if (Row == 1)//row 1: Starting ball speed(3,5,7) BallIncreasePercentage(0%, 5%, 10%, 15%)
                    {
                        switch (cursorIndicating)
                        {
                            case 0:
                                cursor_Y = optionName11_Y;
                                break;
                            case 1:
                                cursor_Y = option11a_Y;
                                break;
                            case 2:
                                cursor_Y = option11b_Y;
                                break;
                            case 3:
                                cursor_Y = option11c_Y;
                                break;
                            case 4:
                                cursor_Y = optionName12_Y;
                                break;
                            case 5:
                                cursor_Y = option12a_Y;
                                break;
                            case 6:
                                cursor_Y = option12b_Y;
                                break;
                            case 7:
                                cursor_Y = option12c_Y;
                                break;
                            case 8:
                                cursor_Y = option12d_Y;
                                break;
                        }
                    }
                    else if (Row == 2)//row 2:Lives(1,3,5,7,9,11,13) Back to main
                    {
                        switch (cursorIndicating)
                        {
                            case 0: //livesname
                                cursor_Y = optionName21_Y;
                                break;
                            case 1:
                                cursor_Y = option21a_Y;
                                break;
                            case 2:
                                cursor_Y = option21b_Y;
                                break;
                            case 3:
                                cursor_Y = option21c_Y;
                                break;
                            case 4:
                                cursor_Y = option21d_Y;
                                break;
                            case 5:
                                cursor_Y = option21e_Y;
                                break;
                            case 6:
                                cursor_Y = option21f_Y;
                                break;
                            case 7:
                                cursor_Y = option21g_Y;
                                break;
                            case 8:
                                cursor_Y = optionName22_Y;
                                break;
                        }
                    }
                    else if (Row == 3)//row 3:ballIncreaseConstant(0,0.5,1,1.5,2) powerups(on, off)
                    {
                        switch (cursorIndicating)
                        {
                            case 0:
                                cursor_Y = optionName31_Y;
                                break;
                            case 1:
                                cursor_Y = option31a_Y;
                                break;
                            case 2:
                                cursor_Y = option31b_Y;
                                break;
                            case 3:
                                cursor_Y = option31c_Y;
                                break;
                            case 4:
                                cursor_Y = option31d_Y;
                                break;
                            case 5:
                                cursor_Y = option31e_Y;
                                break;
                            case 6:
                                cursor_Y = optionName32_Y;
                                break;
                            case 7:
                                cursor_Y = option32a_Y;
                                break;
                            case 8:
                                cursor_Y = option32b_Y;
                                break;
                        }
                    }


                }
                if (currentKeyboardState.IsKeyDown(Keys.Enter) && currentKeyboardState != oldKeyboardState)
                {
                    if (menuState == 0)
                    {
                        switch (cursorIndicating)
                        {
                            case 0://go to mode selection
                                menuState = 1;
                                cursorIndicating = 0;
                                bluePLayerLives = startingLives;
                                redPLayerLives = startingLives;
                                break;
                            case 1://go to options
                                menuState = 2;
                                Row = 2;
                                cursorIndicating = 5;
                                break;
                            case 2://exit game
                                this.Exit();
                                break;

                        }

                    }
                    else if (menuState == 1)//modeselection
                    {
                        scuttlecrab_X = rnd.Next(Width / 3, Width / 3 * 2 - scuttlecrab_png.Width);
                        scuttlecrab_Y = rnd.Next(0, Height - scuttlecrab_png.Height);
                        Porokoekje2_X = rnd.Next(Width / 3, Width / 3 * 2 - Porokoekje2_png.Width);
                        Porokoekje2_Y = rnd.Next(0, Height - Porokoekje2_png.Height);
                        Mustache1_X = rnd.Next(Width / 3, Width / 3 * 2 - Mustache1_png.Width);
                        Mustache1_Y = rnd.Next(0, Height - Mustache1_png.Height);
                        Healthrelic_X = rnd.Next(Width / 3, Width / 3 * 2 - Healthrelic_png.Width);
                        Healthrelic_Y = rnd.Next(0, Height - Healthrelic_png.Height);
                        WelkePowerup = rnd.Next(1, 5);
                        switch (cursorIndicating)
                        {
                            case 0://PvP, 1v1
                                gameState = 1;
                                AI_1 = false;

                                break;
                            case 1://PvE 1v1
                                gameState = 1;
                                AI_1 = true;
                                break;
                        }
                    }
                    else if (menuState == 2) //options screen
                    {
                        if (Row == 1)//row 1: Starting ball speed(3,5,7) BallIncreasePercentage(0%, 5%, 10%, 15%)
                        {
                            switch (cursorIndicating)
                            {
                                case 1:
                                    startingBallSpeed = 3;
                                    break;
                                case 2:
                                    startingBallSpeed = 5;
                                    break;
                                case 3:
                                    startingBallSpeed = 7;
                                    break;
                                case 5:
                                    ballIncreasePercentage = 1;
                                    break;
                                case 6:
                                    ballIncreasePercentage = 1.05;
                                    break;
                                case 7:
                                    ballIncreasePercentage = 1.10;
                                    break;
                                case 8:
                                    ballIncreasePercentage = 1.15;
                                    break;
                                default:
                                    break;
                            }
                        }
                        else if (Row == 2)//row 2:Lives(1,3,5,7,9,11,13) Back to main
                        {
                            switch (cursorIndicating)
                            {
                                case 1:
                                    startingLives = 1;
                                    break;
                                case 2:
                                    startingLives = 3;
                                    break;
                                case 3:
                                    startingLives = 5;
                                    break;
                                case 4:
                                    startingLives = 7;
                                    break;
                                case 5:
                                    startingLives = 9;
                                    break;
                                case 6:
                                    startingLives = 11;
                                    break;
                                case 7:
                                    startingLives = 13;
                                    break;
                                case 8:
                                    menuState = 0;
                                    cursorIndicating = 0;
                                    break;
                                default:
                                    break;
                            }
                        }
                        else if (Row == 3)//row 3:ballIncreaseConstant(0,0.5,1,1.5,2) powerups(on, off)
                        {
                            switch (cursorIndicating)
                            {
                                case 1:
                                    ballIncreaseConstant = 0;
                                    break;
                                case 2:
                                    ballIncreaseConstant = 0.5;
                                    break;
                                case 3:
                                    ballIncreaseConstant = 1;
                                    break;
                                case 4:
                                    ballIncreaseConstant = 1.5;
                                    break;
                                case 5:
                                    ballIncreaseConstant = 2;
                                    break;
                                case 7:
                                    powerUps = true;
                                    break;
                                case 8:
                                    powerUps = false;
                                    break;
                                default:
                                    break;
                            }
                        }

                    }

                }
                if (menuState != 2)//menu state 2 is special. needs other cursor indicator code...
                {
                    cursor_X = Width / 2 + cursorString * 14 / 2;
                }
                cursorIndicator_pos = new Vector2(cursor_X, cursor_Y);
            }
            else if (gameState == 1)//1v1
            {
                if (songPlaying != 2)
                {
                    songPlaying = 2;
                    MediaPlayer.Stop();
                    MediaPlayer.Play(song2);
                }
                if (currentKeyboardState.IsKeyDown(Keys.Escape))
                {
                    ExtraTekst = 0;
                    Reset = 1;
                    S = 0;
                    ETekstB4 = 0;
                    Bonus123 = 0;
                    gameState = 0;
                    menuState = 0;
                    Wx = 1;
                    cursorIndicating = 0;
                    bluePLayerLives = startingLives;
                    redPLayerLives = startingLives;
                    ball_X = Width / 2 - bal_png.Width / 2;
                    ball_Y = Height / 2 - bal_png.Height / 2;
                    ballTravelSpeed = startingBallSpeed;
                    bluePlayer_Y = Height / 2 - bluePlayer_png.Height / 2;
                    redPlayer_Y = Height / 2 - redPlayer_png.Height / 2;
                    do
                    {
                        ballTravelAngle = rnd.NextDouble() * 360;
                    } while (ballTravelAngle > 315 || ballTravelAngle < 45 || (ballTravelAngle > 135 && ballTravelAngle < 225));
                }
                lastBounced = bounced;
                bounced = false;
                bluePlayer_X = Width - bluePlayer_png.Width * blueScale - bluePlayer_png.Width / 2;

                if (ballTravelAngle > 0)
                {
                    ballTravelAngle += 360;
                }
                else if (ballTravelAngle < 0)
                {
                    ballTravelAngle -= 360;
                }
                int redPlayerSpeed_Y = 5, bluePlayerSpeed_Y = 5;
                if (powerUps)
                {
                    scuttlecrab_pos = new Vector2(scuttlecrab_X, scuttlecrab_Y);
                    Porokoekje2_pos = new Vector2(Porokoekje2_X, Porokoekje2_Y);
                    Mustache1_pos = new Vector2(Mustache1_X, Mustache1_Y);
                    Healthrelic_pos = new Vector2(Healthrelic_X, Healthrelic_Y);
                    int M = gameTime.TotalGameTime.Milliseconds;
                    if (M == 0)
                    {
                        S += 1;
                        Console.WriteLine("S is " + S);
                        if (BonusTimeBlue == 1 || BonusTimeRed == 1)
                        {
                            T -= 1;
                        }
                        else
                        {
                            T = Bx;
                        }
                    }
                    /*if (S == 2)
                    {
                        G = S + rnd.Next(5, 10); //Nieuwe G wordt gemaakt
                        Console.WriteLine("G new is " + G);
                    }*/
                    if (S == G) //Dit is de tijd waarop er een Powerup spawned
                    {
                        Console.WriteLine("S is " + S);
                        Console.WriteLine("G is " + G);

                    }

                    if (BallhitPowerup == 0 && Bonus == 0 && S >= G && S < G + Sx) //Dit is de tijd waarin er een Powerup in het spel is
                    {
                        Powerup = 1; //Er is een Powerup in het spel
                        Console.WriteLine("Powerup in spel");
                    }
                    if (Powerup == 1)
                    {
                        if (WelkePowerup == 1)
                        {
                            if (((ball_X >= scuttlecrab_X && ball_X <= scuttlecrab_X + scuttlecrab_png.Width) || (ball_X + bal_png.Width >= scuttlecrab_X && ball_X + bal_png.Width <= scuttlecrab_X + scuttlecrab_png.Width)) && ((ball_Y >= scuttlecrab_Y && ball_Y <= scuttlecrab_Y + scuttlecrab_png.Height) || (ball_Y + bal_png.Height >= scuttlecrab_Y && ball_Y + bal_png.Height <= scuttlecrab_Y + scuttlecrab_png.Height)))
                            { //Als de linker- of rechterzijde van de bal binnen de linker- en rechterzijde van de Powerup zitten en de boven- of onderzijde van de bal binnen de boven- en onderzijde van de Powerup zitten
                                BallhitPowerup = 1; //Dan is de Powerup wel hit met Powerup 1
                                Powerup = 0;//En dan verdwijnt de Powerup
                                Console.WriteLine("Powerup1 uit spel");
                                Console.WriteLine("BallhitPowerup1 aan op " + S + " seconden");
                                if (Ballhitplayer == 1)
                                {
                                    RedplayerBonus = 1;
                                    BlueplayerBonus = 0;
                                }
                                if (Ballhitplayer == 2)
                                {
                                    BlueplayerBonus = 1;
                                    RedplayerBonus = 0;
                                }
                            }
                            else
                            {
                                BallhitPowerup = 0; //Anders is de Powerup niet hit
                            }
                        }
                        if (WelkePowerup == 2)
                        {
                            if (((ball_X >= Porokoekje2_X && ball_X <= Porokoekje2_X + Porokoekje2_png.Width) || (ball_X + bal_png.Width >= Porokoekje2_X && ball_X + bal_png.Width <= Porokoekje2_X + Porokoekje2_png.Width)) && ((ball_Y >= Porokoekje2_Y && ball_Y <= Porokoekje2_Y + Porokoekje2_png.Height) || (ball_Y + bal_png.Height >= Porokoekje2_Y && ball_Y + bal_png.Height <= Porokoekje2_Y + Porokoekje2_png.Height)))
                            { //Als de linker- of rechterzijde van de bal binnen de linker- en rechterzijde van de Powerup zitten en de boven- of onderzijde van de bal binnen de boven- en onderzijde van de Powerup zitten
                                BallhitPowerup = 2; //Dan is de Powerup wel hit met Powerup 2
                                Powerup = 0;//En dan verdwijnt de Powerup
                                Console.WriteLine("Powerup2 uit spel");
                                Console.WriteLine("BallhitPowerup2 aan op " + S + " seconden");
                                if (Ballhitplayer == 1)
                                {
                                    RedplayerBonus = 1;
                                    BlueplayerBonus = 0;
                                }
                                if (Ballhitplayer == 2)
                                {
                                    BlueplayerBonus = 1;
                                    RedplayerBonus = 0;
                                }
                            }
                            else
                            {
                                BallhitPowerup = 0; //Anders is de Powerup niet hit
                            }
                        }
                        if (WelkePowerup == 3)
                        {
                            if (((ball_X >= Mustache1_X && ball_X <= Mustache1_X + Mustache1_png.Width) || (ball_X + bal_png.Width >= Mustache1_X && ball_X + bal_png.Width <= Mustache1_X + Mustache1_png.Width)) && ((ball_Y >= Mustache1_Y && ball_Y <= Mustache1_Y + Mustache1_png.Height) || (ball_Y + bal_png.Height >= Mustache1_Y && ball_Y + bal_png.Height <= Mustache1_Y + Mustache1_png.Height)))
                            { //Als de linker- of rechterzijde van de bal binnen de linker- en rechterzijde van de Powerup zitten en de boven- of onderzijde van de bal binnen de boven- en onderzijde van de Powerup zitten
                                BallhitPowerup = 3; //Dan is de Powerup wel hit met Powerup 3
                                Powerup = 0;//En dan verdwijnt de Powerup
                                Console.WriteLine("Powerup3 uit spel");
                                Console.WriteLine("BallhitPowerup3 aan op " + S + " seconden");
                                if (Ballhitplayer == 1)
                                {
                                    RedplayerBonus = 1;
                                    BlueplayerBonus = 0;
                                }
                                if (Ballhitplayer == 2)
                                {
                                    BlueplayerBonus = 1;
                                    RedplayerBonus = 0;
                                }
                            }
                            else
                            {
                                BallhitPowerup = 0; //Anders is de Powerup niet hit
                            }
                        }
                        if (WelkePowerup == 4)
                        {
                            if (((ball_X >= Healthrelic_X && ball_X <= Healthrelic_X + Healthrelic_png.Width) || (ball_X + bal_png.Width >= Healthrelic_X && ball_X + bal_png.Width <= Healthrelic_X + Healthrelic_png.Width)) && ((ball_Y >= Healthrelic_Y && ball_Y <= Healthrelic_Y + Healthrelic_png.Height) || (ball_Y + bal_png.Height >= Healthrelic_Y && ball_Y + bal_png.Height <= Healthrelic_Y + Healthrelic_png.Height)))
                            { //Als de linker- of rechterzijde van de bal binnen de linker- en rechterzijde van de Powerup zitten en de boven- of onderzijde van de bal binnen de boven- en onderzijde van de Powerup zitten
                                BallhitPowerup = 4; //Dan is de Powerup wel hit met Powerup 3
                                Powerup = 0;//En dan verdwijnt de Powerup
                                Console.WriteLine("Powerup4 uit spel");
                                Console.WriteLine("BallhitPowerup4 aan op " + S + " seconden");
                                if (Ballhitplayer == 1)
                                {
                                    RedplayerBonus = 1;
                                    BlueplayerBonus = 0;
                                }
                                if (Ballhitplayer == 2)
                                {
                                    BlueplayerBonus = 1;
                                    RedplayerBonus = 0;
                                }
                            }
                            else
                            {
                                BallhitPowerup = 0; //Anders is de Powerup niet hit                            
                            }
                        }
                    }
                    if (BallhitPowerup == 1)//Als de bal de Powerup1 raakt
                    {
                        P = S;
                        Bonus = 1;//Dan wordt een bonus1 gestart
                        B = S;//Een nieuwe B wordt gemaakt
                        Console.WriteLine("B is " + B);
                        BallhitPowerup = 0;
                        Console.WriteLine("BallhitPowerup uit op " + S + " seconden");
                    }
                    if (Bonus == 1)
                    {
                        if (S >= B && S < B + Bx)
                        {
                            Bonus = 1;
                            Console.WriteLine("Bonus1 active");
                        }
                        if (S == B + Bx || Reset == 1)//Bonus wordt uitgezet
                        {
                            Bonus = 0;
                            Console.WriteLine("Bonus1 inactive");
                            G = S + rnd.Next(5, 10); //Nieuwe G wordt gemaakt
                            Console.WriteLine("G new is " + G);
                            bluePlayerSpeed_Y = 5;//Hier wordt Bonus1 ongedaan gemaakt
                            Console.WriteLine("bluePlayerSpeed_Y is " + bluePlayerSpeed_Y);
                            redPlayerSpeed_Y = 5; //Hier wordt Bonus1 ongedaan gemaakt
                            Console.WriteLine("redPlayerSpeed_Y is " + redPlayerSpeed_Y);
                            scuttlecrab_X = rnd.Next(Width / 3, Width / 3 * 2 - scuttlecrab_png.Width);
                            scuttlecrab_Y = rnd.Next(0, Height - scuttlecrab_png.Height);
                            WelkePowerup = rnd.Next(1, 5);
                            Console.WriteLine("WelkePowerup is " + WelkePowerup);
                            bonus = "";
                            bonustime = "";
                            BonusTimeRed = 0;
                            BonusTimeBlue = 0;
                            Reset = 0;
                            Console.WriteLine("Reset is " + Reset);
                        }
                    }
                    if (RedplayerBonus == 1 && Bonus == 1) //De redplayer krijgt een speedup bonus voor tijd Bx
                    {
                        redPlayerSpeed_Y = 10;
                        Console.WriteLine("redPlayerSpeed_Y is " + redPlayerSpeed_Y);
                        Console.WriteLine("RedplayerBonus is " + Bonus);
                        bonus = "Redplayer ScuttleSpeedUp";
                        BonusTimeRed = 1;
                    }
                    if (BlueplayerBonus == 1 && Bonus == 1) //De blueplayer krijgt een speedup bonus voor tijd Bx
                    {
                        bluePlayerSpeed_Y = 10;
                        Console.WriteLine("bluePlayerSpeed_Y is " + bluePlayerSpeed_Y);
                        Console.WriteLine("BlueplayerBonus is " + Bonus);
                        bonus = "Blueplayer ScuttleSpeedUp";
                        BonusTimeBlue = 1;
                    }
                    if (BallhitPowerup == 2)//Als de bal de Powerup2 raakt
                    {
                        P = S;
                        Bonus = 2;//Dan wordt een bonus2 gestart
                        B = S;//Een nieuwe B wordt gemaakt
                        Console.WriteLine("B is " + B);
                        BallhitPowerup = 0;
                        Console.WriteLine("BallhitPowerup uit op " + S + " seconden");
                    }
                    if (Bonus == 2)
                    {
                        if (S >= B && S < B + Bx)
                        {
                            Bonus = 2;
                            Console.WriteLine("Bonus2 active");
                        }
                        if (S == B + Bx || Reset == 1)//Bonus wordt uitgezet
                        {
                            Bonus = 0;
                            redScale = 1;
                            blueScale = 1;
                            Console.WriteLine("Bonus2 inactive");
                            G = S + rnd.Next(5, 10); //Nieuwe G wordt gemaakt
                            Console.WriteLine("G new is " + G);
                            //Hier wordt Bonus2 ongedaan gemaakt
                            Porokoekje2_X = rnd.Next(Width / 3, Width / 3 * 2 - Porokoekje2_png.Width);
                            Porokoekje2_Y = rnd.Next(0, Height - Porokoekje2_png.Height);
                            WelkePowerup = rnd.Next(1, 5);
                            Console.WriteLine("WelkePowerup is " + WelkePowerup);
                            bonus = "";
                            bonustime = "";
                            Reset = 0;
                            BonusTimeRed = 0;
                            BonusTimeBlue = 0;
                            Console.WriteLine("Reset is " + Reset);
                        }
                    }
                    redScale = 1;
                    blueScale = 1;
                    if (RedplayerBonus == 1 && Bonus == 2) //De redplayer krijgt een ... bonus voor tijd Bx
                    {
                        redScale = 2;
                        Console.WriteLine("RedplayerBonus is " + Bonus);
                        bonus = "Redplayer Cookiebonus";
                        BonusTimeRed = 1;
                    }
                    if (BlueplayerBonus == 1 && Bonus == 2) //De blueplayer krijgt een ... bonus voor tijd Bx
                    {
                        blueScale = 2;
                        Console.WriteLine("BlueplayerBonus is " + Bonus);
                        bonus = "Blueplayer Cookiebonus";
                        BonusTimeBlue = 1;
                    }
                    if (BallhitPowerup == 3)//Als de bal de Powerup3 raakt
                    {
                        P = S;
                        Bonus = 3;//Dan wordt een bonus3 gestart
                        B = S;//Een nieuwe B wordt gemaakt
                        Console.WriteLine("B is " + B);
                        BallhitPowerup = 0;
                        Console.WriteLine("BallhitPowerup uit op " + S + " seconden");
                    }
                    if (Bonus == 3)
                    {
                        if (S >= B && S < B + Bx)
                        {
                            Bonus = 3;
                            Console.WriteLine("Bonus3 active");
                        }
                        if (S == B + Bx || Reset == 1)//Bonus wordt uitgezet
                        {
                            Bonus = 0;
                            Console.WriteLine("Bonus3 inactive");
                            G = S + rnd.Next(5, 10); //Nieuwe G wordt gemaakt
                            Console.WriteLine("G new is " + G);
                            Snorbonus = 0; //Zorgt ervoor dat de normale besturing weer werkt
                            Mustache1_X = rnd.Next(Width / 3, Width / 3 * 2 - Mustache1_png.Width);
                            Mustache1_Y = rnd.Next(0, Height - Mustache1_png.Height);
                            WelkePowerup = rnd.Next(1, 5);
                            Console.WriteLine("WelkePowerup is " + WelkePowerup);
                            bonus = "";
                            bonustime = "";
                            Reset = 0;
                            BonusTimeRed = 0;
                            BonusTimeBlue = 0;
                            Console.WriteLine("Reset is " + Reset);
                        }
                    }
                    if (RedplayerBonus == 1 && Bonus == 3) //De redplayer krijgt een ... bonus voor tijd Bx Mustahce madness
                    {
                        Snorbonus = 2; //Zorgt ervoor dat de normale besturing voor de blauwe speler niet meer werkt
                        if (currentKeyboardState.IsKeyDown(Keys.Up))
                        {
                            bluePlayerVelocity_Y = bluePlayerSpeed_Y;
                        }
                        if (currentKeyboardState.IsKeyDown(Keys.Down))
                        {
                            bluePlayerVelocity_Y = -bluePlayerSpeed_Y;
                        }
                        //de besturing voor de blauwe speler is omgedraaid als de rode speler een snorbonus heeft
                        Console.WriteLine("RedplayerBonus is " + Bonus);
                        bonus = "Blueplayer is suffering from MustacheMadness";
                        BonusTimeBlue = 1;
                    }
                    if (BlueplayerBonus == 1 && Bonus == 3) //De blueplayer krijgt een ... bonus voor tijd Bx
                    {
                        Snorbonus = 1; //Zorgt ervoor dat de normale besturing voor de rode speler niet meer werkt
                        if (currentKeyboardState.IsKeyDown(Keys.W))
                        {
                            redPlayerVelocity_Y = redPlayerSpeed_Y;
                        }
                        if (currentKeyboardState.IsKeyDown(Keys.S))
                        {
                            redPlayerVelocity_Y = -redPlayerSpeed_Y;
                        }
                        //de besturing voor de rode speler is omgedraaid als de blauwe speler een snorbonus heeft
                        //hier komt de Bonus3
                        Console.WriteLine("BlueplayerBonus is " + Bonus);
                        bonus = "Redplayer is suffering from MustacheMadness";
                        BonusTimeRed = 1;
                    }
                    if (S >= P && S <= P + Vx)
                    {
                        Bonus123 = 1;
                    }
                    else
                    {
                        Bonus123 = 0;
                    }
                    if (BallhitPowerup == 4)//Als de bal de Powerup4 raakt                  
                    {
                        Bonus = 4;//Dan wordt een bonus4 gestart
                        B = S;//Een nieuwe B wordt gemaakt
                        Console.WriteLine("B is " + B);
                        BallhitPowerup = 0;
                        Console.WriteLine("BallhitPowerup uit op " + S + " seconden");
                    }
                    if (Bonus == 4)
                    {
                        if (Ballhitplayer == 1)
                        {
                            if (redPLayerLives < startingLives)
                            {
                                ETekstB4 = 1;
                                V = S;
                                redPLayerLives += 1;
                                Console.WriteLine("redPlayerLives is +1");
                                bonus4 = "Redplayer got an extra life!";
                            }
                            else
                            {
                                ETekstB4 = 1;
                                V = S;
                                bonus4 = "Redplayer already got maxLives!";
                            }
                            Console.WriteLine("RedplayerBonus is " + Bonus);
                        }
                        if (Ballhitplayer == 2)
                        {
                            if (bluePLayerLives < startingLives)
                            {
                                ETekstB4 = 1;
                                V = S;
                                bluePLayerLives += 1;
                                Console.WriteLine("bluePlayerLives is +1");
                                bonus4 = "Blueplayer got an extra life!";
                            }
                            else
                            {
                                ETekstB4 = 1;
                                V = S;
                                bonus4 = "Blueplayer already got maxLives!";
                            }
                            Console.WriteLine("BlueplayerBonus is " + Bonus);
                        }
                        if (S == B || Reset == 1)//Bonus wordt uitgezet
                        {
                            G = S + rnd.Next(5, 10); //Nieuwe G wordt gemaakt
                            Console.WriteLine("G new is " + G);
                            //Hier wordt Bonus4 ongedaan gemaakt
                            Healthrelic_X = rnd.Next(Width / 3, Width / 3 * 2 - Healthrelic_png.Width);
                            Healthrelic_Y = rnd.Next(0, Height - Healthrelic_png.Height);
                            WelkePowerup = rnd.Next(1, 5);
                            Console.WriteLine("WelkePowerup is " + WelkePowerup);
                            bonus = "";
                            Reset = 0;
                            Console.WriteLine("Reset is " + Reset);
                            Bonus = 0;
                            Console.WriteLine("Bonus4");
                        }
                    }
                    if (S > V + Vx || Reset == 1)
                    {
                        ETekstB4 = 0;
                    }
                    if ((Bonus == 0 && S == G + Sx) || Reset == 1)//Dit is de tijd waarop er een nieuwe G wordt gemaakt en de Powerup verdwijnt uit het spel
                    {
                        Powerup = 0; //Er is geen Powerup in het spel
                        Console.WriteLine("Powerup uit spel");
                        G = S + rnd.Next(5, 10); //Nieuwe G wordt gemaakt
                        Console.WriteLine("G new is " + G);
                        BallhitPowerup = 0;
                        scuttlecrab_X = rnd.Next(Width / 3, Width / 3 * 2 - scuttlecrab_png.Width);
                        scuttlecrab_Y = rnd.Next(0, Height - scuttlecrab_png.Height);
                        Porokoekje2_X = rnd.Next(Width / 3, Width / 3 * 2 - Porokoekje2_png.Width);
                        Porokoekje2_Y = rnd.Next(0, Height - Porokoekje2_png.Height);
                        Mustache1_X = rnd.Next(Width / 3, Width / 3 * 2 - Mustache1_png.Width);
                        Mustache1_Y = rnd.Next(0, Height - Mustache1_png.Height);
                        Healthrelic_X = rnd.Next(Width / 3, Width / 3 * 2 - Healthrelic_png.Width);
                        Healthrelic_Y = rnd.Next(0, Height - Healthrelic_png.Height);
                        WelkePowerup = rnd.Next(1, 5);
                        Console.WriteLine("WelkePowerup is " + WelkePowerup);
                        bonus = "";
                        bonustime = "";
                        BonusTimeRed = 0;
                        BonusTimeBlue = 0;
                        Reset = 0;
                        Console.WriteLine("Reset is " + Reset);
                    }
                }
                if (currentKeyboardState.IsKeyDown(Keys.Up) && Snorbonus != 2)
                {
                    bluePlayerVelocity_Y = -bluePlayerSpeed_Y;
                }
                if (currentKeyboardState.IsKeyDown(Keys.Down) && Snorbonus != 2)
                {
                    bluePlayerVelocity_Y = bluePlayerSpeed_Y;
                }
                if ((currentKeyboardState.IsKeyDown(Keys.Down) && currentKeyboardState.IsKeyDown(Keys.Up)) || (currentKeyboardState.IsKeyUp(Keys.Up) && currentKeyboardState.IsKeyUp(Keys.Down)))
                {
                    bluePlayerVelocity_Y = 0;
                }
                if (bluePlayerVelocity_Y != 0)
                {
                    bluePlayer_Y += bluePlayerVelocity_Y;
                }
                if (AI_1)
                {
                    if (ball_Y + bal_png.Height / 2 < redPlayer_Y + redPlayerSpeed_Y && ball_Y + bal_png.Height / 2 > redPlayer_Y + redPlayer_png.Height * redScale - redPlayerSpeed_Y)
                    {
                        redPlayer_Y = ball_Y;
                        redPlayerVelocity_Y = 0;
                    }
                    else
                    {
                        if (ball_Y + bal_png.Height / 2 < redPlayer_Y + redPlayer_png.Height / 2 - redPlayerSpeed_Y)
                        {
                            redPlayerVelocity_Y = -redPlayerSpeed_Y;
                        }
                        else if (ball_Y + bal_png.Height / 2 > redPlayer_Y + redPlayer_png.Height / 2 + redPlayerSpeed_Y)
                        {
                            redPlayerVelocity_Y = redPlayerSpeed_Y;
                        }
                        else
                        {
                            redPlayerVelocity_Y = 0;
                        }
                    }
                }
                else
                {
                    if (currentKeyboardState.IsKeyDown(Keys.W) && Snorbonus != 1)
                    {
                        redPlayerVelocity_Y = -redPlayerSpeed_Y;
                    }
                    if (currentKeyboardState.IsKeyDown(Keys.S) && Snorbonus != 1)
                    {
                        redPlayerVelocity_Y = redPlayerSpeed_Y;
                    }
                    if ((currentKeyboardState.IsKeyDown(Keys.S) && currentKeyboardState.IsKeyDown(Keys.W)) || (currentKeyboardState.IsKeyUp(Keys.W) && currentKeyboardState.IsKeyUp(Keys.S)))
                    {
                        redPlayerVelocity_Y = 0;
                    }

                }
                if (redPlayerVelocity_Y != 0)
                {
                    redPlayer_Y += redPlayerVelocity_Y;
                }
                if (redPlayer_Y - redPlayerSpeed_Y < 0 || redPlayer_Y + redPlayer_png.Height * redScale + redPlayerSpeed_Y > Height)
                {
                    if (redPlayer_Y - redPlayerSpeed_Y < 0)
                    {
                        redPlayer_Y = 0;
                    }
                    else
                    {
                        redPlayer_Y = Height - redPlayer_png.Height * redScale;
                    }
                }
                if (bluePlayer_Y - bluePlayerSpeed_Y < 0 || bluePlayer_Y + bluePlayer_png.Height * blueScale + bluePlayerSpeed_Y > Height)
                {
                    if (bluePlayer_Y - bluePlayerSpeed_Y < 0)
                    {
                        bluePlayer_Y = 0;
                    }
                    else
                    {
                        bluePlayer_Y = Height - bluePlayer_png.Height * blueScale;
                    }
                }
                if ((ball_Y < 0 || ball_Y + bal_png.Height > Height) && bounced == false && bounced == lastBounced)
                {

                    bounced = true;
                    //calucalate the angel of collsion
                    angleObjectHitWall = 180 - ballTravelAngle;
                    while (angleObjectHitWall > 360)
                    {
                        angleObjectHitWall -= 360;
                    }
                    while (angleObjectHitWall < 0)
                    {
                        angleObjectHitWall += 360;
                    }
                    Console.WriteLine("Angle I hit with is: " + angleObjectHitWall);
                    ballTravelAngle = angleObjectHitWall;
                }
                if (ball_X < redPlayer_png.Width * redScale + redPlayer_X - (ballVelocity_X - 2) && ball_X > redPlayer_X + redPlayer_png.Width * redScale && bounced == lastBounced)
                {
                    if (ball_Y + bal_png.Height > redPlayer_Y && ball_Y < redPlayer_Y + redPlayer_png.Height * redScale)
                    {
                        Ballhitplayer = 1;
                        double areaMax = bal_png.Height * 2 + redPlayer_png.Height * redScale;
                        double areaHit = (ball_Y - bal_png.Height / 2) - (redPlayer_Y - bal_png.Height * 2);
                        zz = areaHit / areaMax;
                        Console.WriteLine(areaMax + " " + areaHit);
                        Console.WriteLine(ballTravelAngle);
                        Console.WriteLine("Hit at " + zz);
                        ballTravelAngle = (135 - 90 * zz) - (ballTravelAngle + 90);
                        Console.WriteLine(ballTravelAngle);
                        ballTravelSpeed = ballTravelSpeed * ballIncreasePercentage + ballIncreaseConstant;
                        Console.WriteLine(ballTravelSpeed);
                        bounced = true;
                    }
                }
                if (ball_X + bal_png.Width > bluePlayer_X - Convert.ToInt32(ballVelocity_X) && ball_X < bluePlayer_X && bounced == lastBounced)
                {
                    if (ball_Y + bal_png.Height > bluePlayer_Y && ball_Y < bluePlayer_Y + bluePlayer_png.Height * blueScale)
                    {
                        Ballhitplayer = 2;
                        double areaMax = bal_png.Height * 2 + bluePlayer_png.Height * blueScale;
                        double areaHit = (ball_Y - bal_png.Height / 2) - (bluePlayer_Y - bal_png.Height * 2);
                        zz = areaHit / areaMax;
                        Console.WriteLine(areaMax + " " + areaHit);
                        Console.WriteLine("Hit at " + zz);
                        ballTravelAngle = (225 + 90 * zz) - (ballTravelAngle - 90);
                        ballTravelSpeed = ballTravelSpeed * ballIncreasePercentage + ballIncreaseConstant;
                        Console.WriteLine(ballTravelSpeed);
                        bounced = true;
                    }
                }
                if (ball_X + bal_png.Width < 0 || ball_X > Width)
                {
                    Wx = 0;
                    Reset = 1;
                    bluePlayer_Y = Height / 2 - bluePlayer_png.Height / 2;
                    redPlayer_Y = Height / 2 - redPlayer_png.Height / 2;
                    Console.WriteLine("Reset is " + Reset);
                    if (ball_X > Width)//red player scored
                    {
                        bluePLayerLives -= 1;
                        LastScored = 1;
                        W = 0;
                    }
                    if (ball_X + bal_png.Width < 0)//blue player scored
                    {
                        redPLayerLives -= 1;
                        LastScored = 2;
                        W = 0;
                    }
                    if (bluePLayerLives == winCondition || redPLayerLives == winCondition)
                    {
                        gameState = 2;
                    }
                    ball_X = Width / 2 - bal_png.Width / 2;
                    ball_Y = Height / 2 - bal_png.Height / 2;
                    ballTravelSpeed = startingBallSpeed;
                    do
                    {
                        ballTravelAngle = rnd.NextDouble() * 360;
                    } while (ballTravelAngle > 315 || ballTravelAngle < 45 || (ballTravelAngle > 135 && ballTravelAngle < 225));
                    Console.WriteLine("I got reset! " + ballTravelAngle);
                    S = 0;
                }
                if (LastScored == 1)
                {
                    scored = "Redplayer scored!";
                }
                if (LastScored == 2)
                {
                    scored = "Blueplayer scored!";
                }
                if (S >= W && S <= W + Vx && Wx == 0)
                {
                    ExtraTekst = 1;
                }
                else
                {
                    ExtraTekst = 0;
                }
                // This is doing the math on the ball
                do
                {
                    ballVelocity_Y = Math.Cos(ballTravelAngle * Math.PI / 180) * ballTravelSpeed;
                    ballVelocity_X = Math.Sin(ballTravelAngle * Math.PI / 180) * ballTravelSpeed;
                    if (Convert.ToInt32(ballVelocity_X) == 0)
                    {
                        if (Ballhitplayer == 1)//red
                        {
                            ballTravelAngle += 1;
                        }
                        else//blue
                        {
                            ballTravelAngle -= 1;
                        }
                    }
                } while (ballVelocity_X == 0);
                ball_X += Convert.ToInt32(ballVelocity_X);
                ball_Y += Convert.ToInt32(ballVelocity_Y);
                bal_pos = new Vector2(ball_X, ball_Y);
                bluePlayer_pos = new Vector2(bluePlayer_X, bluePlayer_Y);
                redPlayer_pos = new Vector2(redPlayer_X, redPlayer_Y);
                redPlayerScore_Y = 50;
                bluePlayerScore_Y = 50;
                redPlayerScore_X = Width / 4 - 20;
                bluePlayerScore_X = Width / 4 + Width / 2 + 20;
                bluePLayerLives_string = bluePLayerLives.ToString();
                redPLayerLives_string = redPLayerLives.ToString();
                bonustime = T.ToString();
                Btimeleft = "Bonus ends in";
                redPlayerScore_pos = new Vector2(redPlayerScore_X, redPlayerScore_Y);
                bluePlayerScore_pos = new Vector2(bluePlayerScore_X, bluePlayerScore_Y);
            }
            else if (gameState == 2)
            {// end of gameState 1 start winState(display winner and reset varaibles for another game here!
                if (songPlaying != 1)
                {
                    songPlaying = 1;
                    MediaPlayer.Stop();
                    MediaPlayer.Play(song1);
                }
                Wx = 1;
                if (redPLayerLives == winCondition)
                {
                    winner = "Congrats to the Blue player!";
                }
                else if (bluePLayerLives == winCondition)
                {
                    winner = "Congrats to the Red player!";
                }
                //menu options I want: Main Menu, Instant rematch, exit game.
                if (currentKeyboardState.IsKeyDown(Keys.Up) && currentKeyboardState != oldKeyboardState && cursorIndicating != 0)
                {
                    cursorIndicating -= 1;
                }
                if (currentKeyboardState.IsKeyDown(Keys.Down) && currentKeyboardState != oldKeyboardState && cursorIndicating != MenuOptions - 1)
                {
                    cursorIndicating += 1;
                }
                MenuOptions = 3;
                start = "Rematch"; exit = "Exit"; options = "Main Menu";
                switch (cursorIndicating)
                {
                    case 0:
                        cursor_Y = start_Y;
                        cursorString = start.Length;
                        break;
                    case 1:
                        cursor_Y = options_Y;
                        cursorString = options.Length;
                        break;
                    case 2:
                        cursor_Y = exit_Y;
                        cursorString = exit.Length;
                        break;
                }
                start_X = Width / 2 - start.Length * 14 / 2;
                options_X = Width / 2 - options.Length * 14 / 2;
                exit_X = Width / 2 - exit.Length * 14 / 2;

                options_Y = CenterHeight;
                start_Y = CenterHeight - Height / 20;
                exit_Y = CenterHeight + Height / 20;

                start_pos = new Vector2(start_X, start_Y);
                options_pos = new Vector2(options_X, options_Y);
                exit_pos = new Vector2(exit_X, exit_Y);
                cursor_X = Width / 2 + cursorString * 14 / 2;
                cursorIndicator_pos = new Vector2(cursor_X, cursor_Y);
                if (currentKeyboardState.IsKeyDown(Keys.Enter) && currentKeyboardState != oldKeyboardState)
                {

                    switch (cursorIndicating)
                    {
                        case 0://Rematch
                            gameState = 1;
                            break;
                        case 1://Go to main menu
                            gameState = 0;
                            menuState = 0;
                            break;
                        case 2://exit game
                            this.Exit();
                            break;
                    }//reset everything here
                    cursorIndicating = 0;
                    bluePLayerLives = startingLives;
                    redPLayerLives = startingLives;
                    ball_X = Width / 2 - bal_png.Width / 2;
                    ball_Y = Height / 2 - bal_png.Height / 2;
                    ballTravelSpeed = startingBallSpeed;
                    bluePlayer_Y = Height / 2 - bluePlayer_png.Height / 2;
                    redPlayer_Y = Height / 2 - redPlayer_png.Height / 2;
                    ExtraTekst = 0;
                    Reset = 1;
                    S = 0;
                    ETekstB4 = 0;
                    Bonus123 = 0;
                    Wx = 1;
                    do
                    {
                        ballTravelAngle = rnd.NextDouble() * 360;
                    } while (ballTravelAngle > 315 || ballTravelAngle < 45 || (ballTravelAngle > 135 && ballTravelAngle < 225));
                }

            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            if (gameState == 0)
            {
                if (menuState != 2)
                {
                    spriteBatch.Draw(Achtergrond_png, Achtergrond_pos, Color.White);
                }
                else
                {
                    spriteBatch.Draw(Achtergrond_png, Achtergrond_pos, Color.Gray);
                }
                spriteBatch.Draw(cursorIndicator_png, cursorIndicator_pos, Color.White);
                if (menuState == 0)
                {
                    spriteBatch.DrawString(spriteFont1, start, start_pos, Color.White);
                    spriteBatch.DrawString(spriteFont1, options, options_pos, Color.White);
                    spriteBatch.DrawString(spriteFont1, exit, exit_pos, Color.White);
                }
                else if (menuState == 1)
                {
                    spriteBatch.DrawString(spriteFont1, PvP, PvP_pos, Color.White);
                    spriteBatch.DrawString(spriteFont1, PvE, PvE_pos, Color.White);
                }
                else if (menuState == 2)
                //row 1: Starting ball speed(3,5,7) BallIncreasePercentage(0%, 5%, 10%, 15%)

                {
                    spriteBatch.DrawString(spriteFont4, optionName11, optionName11_pos, Color.White);
                    spriteBatch.DrawString(spriteFont4, optionName12, optionName12_pos, Color.White);
                    if (startingBallSpeed == option11a)
                    {
                        spriteBatch.DrawString(spriteFont4, Convert.ToString(option11a), option11a_pos, Color.White);
                    }
                    else
                    {
                        spriteBatch.DrawString(spriteFont1, Convert.ToString(option11a), option11a_pos, Color.White);
                    }
                    if (startingBallSpeed == option11b)
                    {
                        spriteBatch.DrawString(spriteFont4, Convert.ToString(option11b), option11b_pos, Color.White);
                    }
                    else
                    {
                        spriteBatch.DrawString(spriteFont1, Convert.ToString(option11b), option11b_pos, Color.White);
                    }
                    if (startingBallSpeed == option11c)
                    {
                        spriteBatch.DrawString(spriteFont4, Convert.ToString(option11c), option11c_pos, Color.White);
                    }
                    else
                    {
                        spriteBatch.DrawString(spriteFont1, Convert.ToString(option11c), option11c_pos, Color.White);
                    }
                    if (ballIncreasePercentage == 1)
                    {
                        spriteBatch.DrawString(spriteFont4, option12a, option12a_pos, Color.White);
                    }
                    else
                    {
                        spriteBatch.DrawString(spriteFont1, option12a, option12a_pos, Color.White);
                    }
                    if (ballIncreasePercentage == 1.05)
                    {
                        spriteBatch.DrawString(spriteFont4, option12b, option12b_pos, Color.White);
                    }
                    else
                    {
                        spriteBatch.DrawString(spriteFont1, option12b, option12b_pos, Color.White);
                    }
                    if (ballIncreasePercentage == 1.10)
                    {
                        spriteBatch.DrawString(spriteFont4, option12c, option12c_pos, Color.White);
                    }
                    else
                    {
                        spriteBatch.DrawString(spriteFont1, option12c, option12c_pos, Color.White);
                    }
                    if (ballIncreasePercentage == 1.15)
                    {
                        spriteBatch.DrawString(spriteFont4, option12d, option12d_pos, Color.White);
                    }
                    else
                    {
                        spriteBatch.DrawString(spriteFont1, option12d, option12d_pos, Color.White);
                    }
                    //row 2: Lives(1,3,5,7,9,11,13) Back to main
                    spriteBatch.DrawString(spriteFont4, optionName21, optionName21_pos, Color.White);
                    spriteBatch.DrawString(spriteFont4, optionName22, optionName22_pos, Color.White);
                    if (startingLives == 1)
                    {
                        spriteBatch.DrawString(spriteFont4, Convert.ToString(option21a), option21a_pos, Color.White);
                    }
                    else
                    {
                        spriteBatch.DrawString(spriteFont1, Convert.ToString(option21a), option21a_pos, Color.White);
                    }
                    if (startingLives == 3)
                    {
                        spriteBatch.DrawString(spriteFont4, Convert.ToString(option21b), option21b_pos, Color.White);
                    }
                    else
                    {
                        spriteBatch.DrawString(spriteFont1, Convert.ToString(option21b), option21b_pos, Color.White);
                    }

                    if (startingLives == 5)
                    {
                        spriteBatch.DrawString(spriteFont4, Convert.ToString(option21c), option21c_pos, Color.White);
                    }
                    else
                    {
                        spriteBatch.DrawString(spriteFont1, Convert.ToString(option21c), option21c_pos, Color.White);
                    }

                    if (startingLives == 7)
                    {
                        spriteBatch.DrawString(spriteFont4, Convert.ToString(option21d), option21d_pos, Color.White);
                    }
                    else
                    {
                        spriteBatch.DrawString(spriteFont1, Convert.ToString(option21d), option21d_pos, Color.White);
                    }

                    if (startingLives == 9)
                    {
                        spriteBatch.DrawString(spriteFont4, Convert.ToString(option21e), option21e_pos, Color.White);
                    }
                    else
                    {
                        spriteBatch.DrawString(spriteFont1, Convert.ToString(option21e), option21e_pos, Color.White);
                    }

                    if (startingLives == 11)
                    {
                        spriteBatch.DrawString(spriteFont4, Convert.ToString(option21f), option21f_pos, Color.White);
                    }
                    else
                    {
                        spriteBatch.DrawString(spriteFont1, Convert.ToString(option21f), option21f_pos, Color.White);
                    }

                    if (startingLives == 13)
                    {
                        spriteBatch.DrawString(spriteFont4, Convert.ToString(option21g), option21g_pos, Color.White);
                    }
                    else
                    {
                        spriteBatch.DrawString(spriteFont1, Convert.ToString(option21g), option21g_pos, Color.White);
                    }
                    //row 3: BallIncreaseConstant(0,0.5,1,1.5,2) powerups(on, off)
                    spriteBatch.DrawString(spriteFont4, optionName31, optionName31_pos, Color.White);
                    spriteBatch.DrawString(spriteFont4, optionName32, optionName32_pos, Color.White);
                    if (ballIncreaseConstant == option31a)
                    {
                        spriteBatch.DrawString(spriteFont4, Convert.ToString(option31a), option31a_pos, Color.White);
                    }
                    else
                    {
                        spriteBatch.DrawString(spriteFont1, Convert.ToString(option31a), option31a_pos, Color.White);

                    }
                    if (ballIncreaseConstant == option31b)
                    {
                        spriteBatch.DrawString(spriteFont4, Convert.ToString(option31b), option31b_pos, Color.White);
                    }
                    else
                    {
                        spriteBatch.DrawString(spriteFont1, Convert.ToString(option31b), option31b_pos, Color.White);
                    }
                    if (ballIncreaseConstant == option31c)
                    {
                        spriteBatch.DrawString(spriteFont4, Convert.ToString(option31c), option31c_pos, Color.White);
                    }
                    else
                    {
                        spriteBatch.DrawString(spriteFont1, Convert.ToString(option31c), option31c_pos, Color.White);
                    }
                    if (ballIncreaseConstant == option31d)
                    {
                        spriteBatch.DrawString(spriteFont4, Convert.ToString(option31d), option31d_pos, Color.White);
                    }
                    else
                    {
                        spriteBatch.DrawString(spriteFont1, Convert.ToString(option31d), option31d_pos, Color.White);
                    }
                    if (ballIncreaseConstant == option31e)
                    {
                        spriteBatch.DrawString(spriteFont4, Convert.ToString(option31e), option31e_pos, Color.White);
                    }
                    else
                    {
                        spriteBatch.DrawString(spriteFont1, Convert.ToString(option31e), option31e_pos, Color.White);
                    }
                    if (powerUps)
                    {
                        spriteBatch.DrawString(spriteFont4, option32a, option32a_pos, Color.White);
                        spriteBatch.DrawString(spriteFont1, option32b, option32b_pos, Color.White);
                    }
                    else
                    {
                        spriteBatch.DrawString(spriteFont1, option32a, option32a_pos, Color.White);
                        spriteBatch.DrawString(spriteFont4, option32b, option32b_pos, Color.White);
                    }

                }

            }
            else if (gameState == 1)
            {
                spriteBatch.Draw(PoroAchtergrond_png, PoroAchtergrond_pos, Color.White);
                spriteBatch.Draw(bal_png, bal_pos, Color.White);
                //batch.Draw(SpriteTexture, position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                if (BonusTimeRed == 1)
                {
                    spriteBatch.DrawString(spriteFont5, bonustime, new Vector2(Width / 2 + Btimeleft.Length * 18 / 2 - bonustime.Length * 18 / 2 - 20, 50), Color.Red);
                    spriteBatch.DrawString(spriteFont5, Btimeleft, new Vector2(Width / 2 - Btimeleft.Length * 18 / 2 - bonustime.Length * 18 / 2 + 20, 50), Color.Red);
                }
                if (BonusTimeBlue == 1)
                {
                    spriteBatch.DrawString(spriteFont5, bonustime, new Vector2(Width / 2 + Btimeleft.Length * 18 / 2 - bonustime.Length * 18 / 2 - 20, 50), Color.Blue);
                    spriteBatch.DrawString(spriteFont5, Btimeleft, new Vector2(Width / 2 - Btimeleft.Length * 18 / 2 - bonustime.Length * 18 / 2 + 20, 50), Color.Blue);
                }
                if (ETekstB4 == 1 && ExtraTekst != 1)
                {
                    spriteBatch.DrawString(spriteFont3, bonus4, new Vector2(Width / 2 - bonus4.Length * 18 / 2, 50), Color.White);
                }
                if (ExtraTekst == 1 && LastScored == 1)
                {
                    spriteBatch.DrawString(spriteFont3, scored, new Vector2(Width / 2 - scored.Length * 18 / 2, 50), Color.Red);
                }
                if (ExtraTekst == 1 && LastScored == 2)
                {
                    spriteBatch.DrawString(spriteFont3, scored, new Vector2(Width / 2 - scored.Length * 18 / 2, 50), Color.Blue);
                }
                if (Bonus123 == 1 && BonusTimeRed == 1)
                {
                    spriteBatch.DrawString(spriteFont3, bonus, new Vector2(Width / 2 - bonus.Length * 18 / 2, Height / 5), Color.Red);
                }
                if (Bonus123 == 1 && BonusTimeBlue == 1)
                {
                    spriteBatch.DrawString(spriteFont3, bonus, new Vector2(Width / 2 - bonus.Length * 18 / 2, Height / 5), Color.Blue);
                }
                spriteBatch.Draw(redPlayer_png, redPlayer_pos, null, Color.White, 0f, Vector2.Zero, redScale, SpriteEffects.None, 0f);
                spriteBatch.Draw(bluePlayer_png, bluePlayer_pos, null, Color.White, 0f, Vector2.Zero, blueScale, SpriteEffects.None, 0f);
                spriteBatch.DrawString(spriteFont2, bluePLayerLives_string, bluePlayerScore_pos, Color.Blue);
                spriteBatch.DrawString(spriteFont2, redPLayerLives_string, redPlayerScore_pos, Color.Red);

                if (powerUps && Powerup == 1 && Bonus == 0)
                {
                    if (WelkePowerup == 1)
                    {
                        spriteBatch.Draw(scuttlecrab_png, scuttlecrab_pos, Color.White);
                    }
                    if (WelkePowerup == 2)
                    {
                        spriteBatch.Draw(Porokoekje2_png, Porokoekje2_pos, Color.White);
                    }
                    if (WelkePowerup == 3)
                    {
                        spriteBatch.Draw(Mustache1_png, Mustache1_pos, Color.White);
                    }
                    if (WelkePowerup == 4)
                    {
                        spriteBatch.Draw(Healthrelic_png, Healthrelic_pos, Color.White);
                    }
                }
            }
            else if (gameState == 2)
            {
                spriteBatch.Draw(Achtergrond_png, Achtergrond_pos, Color.White);
                spriteBatch.Draw(cursorIndicator_png, cursorIndicator_pos, Color.White);
                if (bluePLayerLives == winCondition)
                {
                    spriteBatch.DrawString(spriteFont2, winner, new Vector2(Width / 2 - winner.Length * 26 / 2, Height / 3 - 35), Color.Red);
                }
                else if (redPLayerLives == winCondition)
                {
                    spriteBatch.DrawString(spriteFont2, winner, new Vector2(Width / 2 - winner.Length * 26 / 2, Height / 3 - 35), Color.Blue);
                }
                spriteBatch.DrawString(spriteFont2, "Play again?", new Vector2(Width / 2 - 11 * 30 / 2, Height / 3 + 15), Color.White);
                spriteBatch.DrawString(spriteFont1, start, start_pos, Color.White);
                spriteBatch.DrawString(spriteFont1, options, options_pos, Color.White);
                spriteBatch.DrawString(spriteFont1, exit, exit_pos, Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
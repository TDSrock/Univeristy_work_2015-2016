using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using System;
using SoundEngineSpace;

/*
 * A class for representing the game world.
 */
class GameWorld
{

    /*
     * enum for different game states (playing or game over)
     */
    enum GameState
    {
        Playing, GameOver
    }

    /*
     * screen width and height
     */
    int screenWidth, screenHeight;

    /*
     * random number generator
     */
    Random random;

    /*
     * main game font
     */
    SpriteFont font;

    /*
     * sprite for representing a single tetris block element
     */
    Texture2D block;

    /*
     * the current game state
     */
    GameState gameState;

    /*
     * the main playing grid
     */
    TetrisGrid grid;

    SoundEngine soundEngine;


    public GameWorld(int width, int height, ContentManager Content)
    {
        
        screenWidth = width;
        screenHeight = height;
        random = new Random();
        gameState = GameState.Playing;
        block = Content.Load<Texture2D>("block");
        font = Content.Load<SpriteFont>("SpelFont");
        SoundEffect blockFallSE = Content.Load<SoundEffect>("BlockFallSE");
        Song BuildingWallsintheCold = Content.Load<Song>("91 Building Walls in the Cold");
        soundEngine = new SoundEngine();
        soundEngine.addSound(blockFallSE);
        soundEngine.addSong(BuildingWallsintheCold);
        soundEngine.SetSoundVolume(7);
        soundEngine.SetSongVolume(5);
        soundEngine.PlaySong(0);
        grid = new TetrisGrid(block, soundEngine);
        
    }

    public void Reset()
    {

    }

    public void HandleInput(GameTime gameTime, InputHelper inputHelper)
    {
        //GLOBAL CONTROLS HERE
        grid.HandleInput(gameTime, inputHelper);
    }

    public void Update(GameTime gameTime)
    {
        //ALL GLOBAL UPDATES HERE
        grid.Update(gameTime);
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        grid.Draw(gameTime, spriteBatch);
        DrawText(grid.Score.ToString(), grid.MyScorePossition, spriteBatch);
        spriteBatch.End();
    }

    /*
     * utility method for drawing text on the screen
     */
    public void DrawText(string text, Vector2 positie, SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(font, text, positie, Color.Blue);
    }

    public Random Random
    {
        get { return random; }
    }
}


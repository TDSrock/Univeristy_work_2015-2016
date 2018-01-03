using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SoundEngineSpace;

/*
 * a class for representing the Tetris playing grid
 */
class TetrisGrid
{
    public double Score = 0;
    public Vector2 MyScorePossition = Vector2.Zero;
    public double Cooldown = 2000;//IN MS
    private double TetNaarBenedenTijd = 2000;
    TetriminosGrid TetriminosGrid;
    TetriminosGrid InPlayGrid;
    ScoreManager scoreManager;
    private SoundEngine soundEngine;

    public TetrisGrid(Texture2D b, SoundEngine soundEngine)
    {
        gridblock = b;
        position = new Vector2(gridblock.Width, -gridblock.Height * 3);//draws the a uniform amount away from the edges, also forces row "0" ofscreen.
        InPlayGrid = new TetriminosGrid(b, 1);
        TetriminosGrid = new TetriminosGrid(b);
        MyScorePossition = new Vector2(gridblock.Width * 15, gridblock.Height * 7);
        scoreManager = new ScoreManager(MyScorePossition);
        this.soundEngine = soundEngine;
        this.Clear();
    }

    /*
     * sprite for representing a single grid block
     */
    Texture2D gridblock;

    /*
     * the position of the tetris grid
     */
    public Vector2 position;
    private Vector2 blockPosition;

    /*
     * width in terms of grid elements
     */
    public int Width
    {
        get { return 12; }
    }

    /*
     * height in terms of grid elements
     */
    public int Height
    {
        get { return 24; }
    }
    /*
     * clears the grid
     */
    public int[,] gridArray = new int[12, 24];//sets up the array with FOUR aditional data in height(for decting the lose state AND spawning in the tets) TODO: find a way to change the 12 and 2o into varaibles.

    public void Clear()
    {
        
        for (int i = 0; i < this.Width; i++)
        {
            for (int j = 0; j < this.Height; j++)
            {
                this.gridArray[i, j] = 0;
            }
        }
    }

    public bool WallCheck(int move)//move represents which direction called the function, 0 = left; 1 = right;
    {
        int LongestLine = InPlayGrid.LongestLine(InPlayGrid.grid2Array);
        if ((InPlayGrid.position.X == position.X && move == 0) || (InPlayGrid.position.X + LongestLine * gridblock.Width == position.X + Width * gridblock.Width && move == 1))//Checks for walls
        {
            return false;
        }
        int[] offset = OffsetGet();
        int Move = move;
        if (Move == 0)
            Move = -1;
        for (int i = 0; i < InPlayGrid.Width2; i++)//Checks for already exsisting blocks
        {
            for (int j = 0; j < InPlayGrid.Height2; j++)
            {
                if (InPlayGrid.grid2Array[i, j] != 0)
                {
                    if (gridArray[i + offset[0] / gridblock.Width + Move, j + offset[1] / gridblock.Height] != 0)
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    public void CheckForRow()
    {
        Console.WriteLine("CheckForRow called");
        int p = 0;
        int[] CheckForArray = new int[5] {0,0,0,0,0};//used to return the data this function obtains slot clarification: 0: Number of lines found 1+:number of the line that is empty.
        for (int j = this.Height -1; j > 0; j--)//seek for full lines from bottem
        {
            
            int i = 0;

            while (i < this.Width)
            {
                if (gridArray[i, j] == 0)
                {
                    i = this.Width + 1;//if we meet an empty slot move to the net line
                }
                if (i == this.Width - 1)//I got to be the width without being mutated unnaturly, we found a full line!
                {
                    CheckForArray[0] += 1;
                    p++;
                    CheckForArray[p] = j;
                }
                i++;
            }
        }
        if (CheckForArray[0] != 0)
        {
            Console.WriteLine("I have found " + CheckForArray[0] + " full rows" + CheckForArray[1] + CheckForArray[2] + CheckForArray[3] + CheckForArray[4]);
            removeRow(CheckForArray);
        }
    }

    public void removeRow(int[] RemoveMessageArray) //takes the array from CheckForRow() and and tells clearRow to remove them one by one.
    {
        for(int i = RemoveMessageArray[0]; i!=0; i--)//goal is to go through each row one by one, starting from the highest.
        {
            for(int j = this.Height; j > 0; j--)
            {
                if (RemoveMessageArray[i] == j)//means we found a row that needs to be removed
                {
                    ClearRow(j);
                }
            }
        }
        IncreasePoints(RemoveMessageArray[0]);
    }

    public void IncreasePoints(int p)//Handles the points additions with the Scoremanager
    {
        scoreManager.IncreasePoints(25 + 10 * p * p);
        if(Cooldown > 100)
            Cooldown -= 75;
        if (Cooldown <= 100)
            Cooldown = 25;
        Console.WriteLine(Cooldown);
        Score = scoreManager.GetPoints();
    }

    public void ClearRow(int i)//needs to clear the specific row AND copy ALL rows ABOVE it 1 down. geez...
    {
        Console.WriteLine("ClearRow called for row:" + i);
        for(int j = 0; j < this.Width; j++)
        {
            gridArray[j, i] = 0;
        }
        for (int k = i; k != 0; k--)
        {
            int m = k - 1;
            for (int l = 0; l < this.Width; l++)
            {
                gridArray[l, k] = gridArray[l, m];
            }
        }
    }

    public int[] OffsetGet() // Returns an array with the Offset variables of
    {
        int[] offset = new int[2];
        int offset_x = 0, offset_y = 0;
        Vector2 offsetVector = Vector2.Subtract(InPlayGrid.position, this.position);
        offset_x = (int)offsetVector.X;
        offset_y = (int)offsetVector.Y;
        Console.WriteLine("Offset vectors are(x,y) " + offset_x + ", " + offset_y);
        offset[0] = offset_x;
        offset[1] = offset_y;
        return offset;
    }

    public void CheckValidLocation()//blokje mag niet botsen met tetrisblokjes onder zich
    {
        int[] offset = new int[2];
        offset = OffsetGet();
        bool Mustplace = false;
        for (int i = 0; i < InPlayGrid.Width2; i++)
        {
            for (int j = 0; j < InPlayGrid.Height2; j++)
            {
                if (InPlayGrid.grid2Array[i, j] != 0)
                    if (j + offset[1] / gridblock.Height == this.Height -2)
                    {
                        Mustplace = true;
                    }
                    else if (this.gridArray[i + offset[0] / gridblock.Width, j + offset[1] / gridblock.Height + 1] != 0)//We have found a slot Beneath our active tet that is NOT empty
                    {
                        Mustplace = true;
                    }
                if (Mustplace)
                {
                    DedicatePlacement();
                    CheckForRow();
                    int[,] StoreArray = TetriminosGrid.GiveArray();
                    InPlayGrid.SetArray(StoreArray);
                    InPlayGrid.position = new Vector2(gridblock.Width * this.Width / 2, -gridblock.Height * 4);
                    CheckLose();
                    TetriminosGrid.Clear();
                    TetriminosGrid.welkeTet();
                    break;

                }
            }
            if (Mustplace)
                break;
        }
    }

    public bool CheckValidRotation(int[,] p)
    {
        int longestLine = InPlayGrid.LongestLine(p);
        if (longestLine + InPlayGrid.position.X / gridblock.Width > this.Width + 1) {//Checks if the rotation doesn't go out of bounds.
            return false;
        }
        int[] offset = OffsetGet();
        for (int i = 0; i < InPlayGrid.Width2; i++)//checks if the rotation doens't interefere with exsisting blocks
        {
            for (int j = 0; j < InPlayGrid.Height2; j++)
            {
                if (p[i, j] != 0)
                {
                    if(gridArray[i + offset[0] / gridblock.Width, j + offset[1] / gridblock.Height] != 0)
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    public void rotateLeft()
    {
        int[,] nieuw = new int[InPlayGrid.Width2, InPlayGrid.Height2];
        for (int i = 0; i < InPlayGrid.Width2; ++i)
            for (int j = 0; j < InPlayGrid.Height2; ++j)
                nieuw[i, j] = InPlayGrid.grid2Array[InPlayGrid.Width2 - j - 1, i];
        nieuw = InPlayGrid.checkTetOrientation(nieuw);

        //controleer of CheckValidRotation true returned
        if (CheckValidRotation(nieuw))
        {
            InPlayGrid.grid2Array = nieuw;
        }

    }

    public void rotateRight()
    {
        int[,] nieuw = new int[InPlayGrid.Width2, InPlayGrid.Height2];
        for (int i = 0; i < InPlayGrid.Width2; ++i)
            for (int j = 0; j < InPlayGrid.Height2; ++j)
                nieuw[j, InPlayGrid.Width2 - i - 1] = InPlayGrid.grid2Array[i, j];
        nieuw = InPlayGrid.checkTetOrientation(nieuw);
        //controleer of CheckValidRotation true returned
        if (CheckValidRotation(nieuw))
        {
            InPlayGrid.grid2Array = nieuw;
        }
    }//STILL BROKEN LOL

    public void DedicatePlacement()
    {
        int[] offset = new int[2];
        offset = OffsetGet();
        for (int i = 0; i < InPlayGrid.Width2; i++)
        {
            for (int j = 0; j < InPlayGrid.Height2; j++)
            {
                if (InPlayGrid.grid2Array[i, j] != 0)
                    gridArray[i + offset[0] / gridblock.Width, j + offset[1] / gridblock.Height] = InPlayGrid.grid2Array[i, j];
            }
        }
    }//Sets the blocks in their place

    public void TetNaarBeneden()
    {
        soundEngine.PlaySound(0);
        InPlayGrid.Velocity = new Vector2(0, gridblock.Height);
        CheckValidLocation();
    }//Moves tet down

    public void CheckLose()
    {
        for (int  i = 0; i < this.Width; i++)
        {
            if(gridArray[i,4] != 0)
            {
                //Go To lose state
                Console.WriteLine("LOST");
            }
        }
    }//Checks if we need to go too the lose state

    public void HandleInput(GameTime gameTime, InputHelper inputHelper)
    {
        //InPlayGrid.Velocity = new Vector2(0, 0);
        if (inputHelper.KeyPressed(Keys.M))//MAKE SURE THAT ROTATION IS DONE BEFORE MOVING TO PREVENT A BUG  WHERE BOTH ARE DONE AT THE SAME TIME RESULTING IN THE PLAY GETTING OUT OF BOUNDS
        {
            rotateRight();
        }

        if (inputHelper.KeyPressed(Keys.B))//MAKE SURE THAT ROTATION IS DONE BEFORE MOVING TO PREVENT A BUG  WHERE BOTH ARE DONE AT THE SAME TIME RESULTING IN THE PLAY GETTING OUT OF BOUNDS
        {
            rotateLeft();
        }

        if (inputHelper.KeyPressed(Keys.Left))
        {
            if(WallCheck(0))
                InPlayGrid.Velocity = new Vector2(-gridblock.Width, 0);
        }

        if (inputHelper.KeyPressed(Keys.Right))
        {
            if(WallCheck(1))
                InPlayGrid.Velocity = new Vector2(gridblock.Width, 0);
        }

        if (inputHelper.KeyPressed(Keys.Down))
        {
            TetNaarBeneden();
            TetNaarBenedenTijd = gameTime.TotalGameTime.TotalMilliseconds + Cooldown; ;
        }

        if (inputHelper.KeyPressed(Keys.Up))//REMOVE LATER
        {
            InPlayGrid.Velocity = new Vector2(0, -gridblock.Height);
        }


        InPlayGrid.position = Vector2.Add(InPlayGrid.position, InPlayGrid.Velocity);
    }

    public void Update(GameTime gameTime)
    {
        InPlayGrid.Velocity = new Vector2(0, 0);
        if (gameTime.TotalGameTime.TotalMilliseconds > TetNaarBenedenTijd)
        {
            TetNaarBeneden();
            TetNaarBenedenTijd = gameTime.TotalGameTime.TotalMilliseconds + Cooldown;

        }
    }

    /*
     * draws the grid on the screen
     */
    public void Draw(GameTime gameTime, SpriteBatch s)
    {
        for (int i = 0; i < this.Width; i++)
        {
            for (int j = 0; j < this.Height; j++)
            {
                blockPosition = new Vector2(i * gridblock.Height, j * gridblock.Width);
                switch (gridArray[i, j])
                {
                    case 0:
                        s.Draw(gridblock, Vector2.Add(blockPosition, position), Color.White);
                        break;
                    case 1:
                        s.Draw(gridblock, Vector2.Add(blockPosition, position), Color.Yellow);
                        break;
                    case 2:
                        s.Draw(gridblock, Vector2.Add(blockPosition, position), Color.CornflowerBlue);
                        break;
                    case 3:
                        s.Draw(gridblock, Vector2.Add(blockPosition, position), Color.Purple);
                        break;
                    case 4:
                        s.Draw(gridblock, Vector2.Add(blockPosition, position), Color.Orange);
                        break;
                    case 5:
                        s.Draw(gridblock, Vector2.Add(blockPosition, position), Color.Blue);
                        break;
                    case 6:
                        s.Draw(gridblock, Vector2.Add(blockPosition, position), Color.Green);
                        break;
                    case 7:
                        s.Draw(gridblock, Vector2.Add(blockPosition, position), Color.Red);
                        break;
                    default:
                        Console.WriteLine("Draw default");
                        break;
                } 
            }
            TetriminosGrid.Draw(gameTime, s);
            InPlayGrid.Draw(gameTime, s);
        }
        
    }
}
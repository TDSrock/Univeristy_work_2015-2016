using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;
/*
 * a class for representing the Tetris playing grid
 */
class TetrisGrid
{
    
    public TetrisGrid(Texture2D b)
    {
        gridblock = b;
        position = new Vector2(gridblock.Width, -gridblock.Height);//draws the a uniform amount away from the edges, also forces row "0" ofscreen.
        this.Clear();
        gridArray[1, 4] = 1;
        gridArray[2, 4] = 1;
        gridArray[3, 4] = 1;
        gridArray[4, 4] = 1;
        gridArray[5, 4] = 1;
        gridArray[6, 4] = 1;
        gridArray[7, 4] = 1;
        gridArray[8, 4] = 1;
        gridArray[9, 4] = 1;
        gridArray[10, 4] = 1;
        gridArray[11, 4] = 1;
        gridArray[0, 4] = 1;

        gridArray[1, 7] = 1;
        gridArray[2, 7] = 1;
        gridArray[3, 7] = 1;
        gridArray[4, 7] = 1;
        gridArray[5, 7] = 1;
        gridArray[6, 7] = 1;
        gridArray[7, 7] = 1;
        gridArray[8, 7] = 1;
        gridArray[9, 7] = 1;
        gridArray[10, 7] = 1;
        gridArray[11, 7] = 1;
        gridArray[0, 7] = 1;

        gridArray[1, 20] = 1;
        gridArray[2, 20] = 1;
        gridArray[3, 20] = 1;
        gridArray[4, 20] = 1;
        gridArray[5, 20] = 1;
        gridArray[6, 20] = 1;
        gridArray[7, 20] = 1;
        gridArray[8, 20] = 1;
        gridArray[9, 20] = 1;
        gridArray[10, 20] = 1;
        gridArray[11, 20] = 1;
        gridArray[0, 20] = 1;

        gridArray[4, 10] = 1;
        gridArray[8, 2] = 1;
        gridArray[11, 13] = 1;
        gridArray[8, 17] = 1;
        gridArray[1, 6] = 1;
        gridArray[1, 3] = 1;


    }

    /*
     * sprite for representing a single grid block
     */
    Texture2D gridblock;

    /*
     * the position of the tetris grid
     */
    Vector2 position, blockPosition;

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
        get { return 20; }
    }
    /*
     * clears the grid
     */
    public int[,] gridArray = new int[12, 20 + 1];//sets up the array with one aditional data in height(for decting the lose state) TODO: find a way to change the 12 and 2o into varaibles.

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

    public void CheckForRow()
    {
        int p = 0;
        int[] CheckForArray = new int[5] {0,0,0,0,0};//used to return the data this function obtains slot clarification: 0: Number of lines found 1+:number of the line that is empty.
        for (int j = this.Height; j > 0; j--)//seek for full lines from bottem
        {
            
            int i = 0;

            while (i < this.Width)
            {
                Console.WriteLine(j + " " + i + " " + CheckForArray[0]);
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
    public void removeRow(int[] RemoveMessageArray) //takes the array from CheckForRow() and removes all the rows + moves all the blocks down the amount needed.
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
    }
    public void ClearRow(int i)//needs to clear the specific row AND copy ALL rows ABOVE it 1 down. geez...
    {
        Console.WriteLine("ClearRow called for row:" + i);
        for(int j = 0; j < this.Width; j++)
        {
            gridArray[j, i] = 0;
            Console.WriteLine("cleared row: " + i + "block slot"+ j); 
        }
        for (int k = i; k != 0; k--)
        {
            Console.WriteLine(k);
            int m = k - 1;
            for (int l = 0; l < this.Width; l++)
            {
                gridArray[l, k] = gridArray[l, m];
            }
        }
    }

    /*
     * draws the grid on the screen
     */
    public void Draw(GameTime gameTime, SpriteBatch s)
    {
        for (int i = 0; i < this.Width; i++)
        {
            for (int j = 0; j < this.Height + 1; j++)
            {
                blockPosition = new Vector2(i * gridblock.Height, j * gridblock.Width);
                switch (gridArray[i, j])
                {
                    case 0:
                        s.Draw(gridblock, Vector2.Add(blockPosition, position), Color.White);
                        break;
                    case 1:
                        s.Draw(gridblock, Vector2.Add(blockPosition, position), Color.Red);
                        break;
                    default:
                        break;
                }              
                    
            }
        }
        
    }
}


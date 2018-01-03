using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


class TetriminosGrid //: GameWorld
{
    public TetriminosGrid(Texture2D b2, int q = 0) 
    {
        gridblock2 = b2;
        if (q == 0)
        {
            Console.WriteLine("Tetgrid made");
            position = new Vector2(gridblock2.Width * 15, gridblock2.Height * 2);
        }
        else//if (q == 1)
        {
            Console.WriteLine("INPLAY GRID");
            position = new Vector2(gridblock2.Width * 6, -gridblock2.Height * 4);
        }

        //position = new Vector2(gridblock2.Width * 15, gridblock2.Height * 8);//draws the TetriminosGrid
        //position = new Vector2(gridblock2.Width * 2, gridblock2.Height * 8);//draws the TetriminosGrid
        this.Clear();
        this.welkeTet();        
    }

    public int[,] grid2Array = new int[4, 4];//sets up the array

    public int[,] GiveArray()
    {
        return grid2Array;
    }

    public void SetArray(int[,] jam)
    {
        for (int p = 0; p < this.Width2; p++)
        {
            for (int q = 0; q < this.Height2; q++)
            {

                grid2Array[p, q] = jam[p, q];
            }
        }
     }
    
    public int lastTetrimino = 0, WelkeTetrimino = 0;

    public int LongestLine(int [,] p)
    {
        int L = 0;

        for (int i = 0; i < this.Width2; i++)
        {
            int K = 0;
            for (int j = 0; j < this.Height2; j++)
            {
                if(p[j,i] != 0)
                {
                    K = j + 1;
                }
            }
            if (L < K)
            {
                L = K;
            }
        }
        return L;
    }

    public void welkeTet()
    {

        Random random = new Random();
        lastTetrimino = WelkeTetrimino;
        WelkeTetrimino = random.Next(1, 8);
        do
        {
            WelkeTetrimino = random.Next(1, 8);
        } while (WelkeTetrimino == lastTetrimino);
        Console.WriteLine("New random tet now plz " + WelkeTetrimino );
        switch (WelkeTetrimino)
        {
            case 1:
                grid2Array[0, 2] = 1;
                grid2Array[0, 3] = 1;
                grid2Array[1, 2] = 1;
                grid2Array[1, 3] = 1;
                Console.WriteLine("WelkeTet is O");
                break; //O Tetrimino
            case 2:
                grid2Array[0, 3] = 2;
                grid2Array[1, 3] = 2;
                grid2Array[2, 3] = 2;
                grid2Array[3, 3] = 2;
                Console.WriteLine("WelkeTet is I");
                break; //I Tetrimino
            case 3:
                grid2Array[0, 3] = 3;
                grid2Array[1, 3] = 3;
                grid2Array[1, 2] = 3;
                grid2Array[2, 3] = 3;
                Console.WriteLine("WelkeTet is T");
                break; //T Tetrimino
            case 4:
                grid2Array[0, 3] = 4;
                grid2Array[1, 3] = 4;
                grid2Array[2, 3] = 4;
                grid2Array[2, 2] = 4;
                Console.WriteLine("WelkeTet is L");
                break; //L Tetrimino
            case 5:
                grid2Array[0, 2] = 5;
                grid2Array[0, 3] = 5;
                grid2Array[1, 3] = 5;
                grid2Array[2, 3] = 5;
                Console.WriteLine("WelkeTet is J");
                break; //J Tetrimino
            case 6:
                grid2Array[0, 3] = 6;
                grid2Array[1, 3] = 6;
                grid2Array[1, 2] = 6;
                grid2Array[2, 2] = 6;
                Console.WriteLine("WelkeTet is S");
                break; //S Tetrimino
            case 7:
                grid2Array[0, 2] = 7;
                grid2Array[0, 3] = 7;
                grid2Array[1, 2] = 7;
                grid2Array[1, 1] = 7;
                Console.WriteLine("WelkeTet is Z");
                break; //Z Tetrimino
        }
    }

    public int[,] checkTetOrientation(int [,] checkArray) //Time to check if he orientation is good. So we need to gind empty lines, if we do move everything over one slot. this needs to repeat untill it's good.
    {
        Boolean Checking = true, lockRow = false, lockCollums = false;
        while (Checking)
        {
            for (int i = 0; i < this.Width2; i++)
            {
                if (checkArray[0, i] != 0 && !lockRow)//we hit block so this row doesn't need to be moved
                {
                    Console.WriteLine("LOCKING ROWS");
                    lockRow = true;
                }
                if (checkArray[i, 3] != 0 && !lockCollums)//we hit a block so this row doens't need to be moved
                {
                    Console.WriteLine("LOCKING COLLUMS");
                    lockCollums = true;
                }
            }

            if (!lockRow)// this needs to copy ALL blocks to the left
            {
                for(int k = 0; k < this.Width2 - 1; k++)
                {
                    int m = k + 1;
                    for(int l = 0; l < this.Height2; l++)
                    {
                        checkArray[k, l] = checkArray[m, l];
                        checkArray[m, l] = 0;
                    }
                }
            }
            if (!lockCollums)// this needs to copy ALL  blocks down
            {
                for (int k = 3; k != 0; k--)
                {
                    int m = k - 1;
                    for (int l = 3; l != 0; l--)
                    {
                        checkArray[l, k] = checkArray[l, m];
                        checkArray[l, m] = 0;
                    }
                }
            }
            
            if(lockRow && lockCollums)//we are now done! yay
            {
                Checking = false;
            }
       }
        Console.WriteLine("DONE WITH THIS CRUD UGHHH");
        return checkArray;

    }

    /*
     * sprite for representing a single grid block
     */
    Texture2D gridblock2;

    /*
     * the position of the tetris grid
     */

    public Vector2 position, Velocity;
    private Vector2 blockPosition;

    /*
     * width in terms of grid elements
     */

    public int Width2
    {
        get { return 4; }
    }

    /*
     * height in terms of grid elements
     */

    public int Height2
    {
        get { return 4; }
    }

    /*
     * clears the grid
     */

    public void Clear()
    {
        for (int p = 0; p < this.Width2; p++)
        {
            for (int q = 0; q < this.Height2; q++)
            {
                this.grid2Array[p, q] = 0;
            }
        }
    }

    /*
     * draws the grid on the screen
     */
    public void Draw(GameTime gameTime, SpriteBatch s2)
    {
        for (int p = 0; p < this.Width2; p++)
        {
            for (int q = 0; q < this.Height2; q++)
            {
                blockPosition = new Vector2(p * gridblock2.Height, q * gridblock2.Width);             
                switch (grid2Array[p, q])
                {
                    case 0:
                        //s2.Draw(gridblock2, Vector2.Add(blockPosition, position), Color.White);//disable this later
                        break;
                    case 1:
                        s2.Draw(gridblock2, Vector2.Add(blockPosition, position), Color.Yellow);
                        break;
                    case 2:
                        s2.Draw(gridblock2, Vector2.Add(blockPosition, position), Color.CornflowerBlue);
                        break;
                    case 3:
                        s2.Draw(gridblock2, Vector2.Add(blockPosition, position), Color.Purple);
                        break;
                    case 4:
                        s2.Draw(gridblock2, Vector2.Add(blockPosition, position), Color.Orange);
                        break;
                    case 5:
                        s2.Draw(gridblock2, Vector2.Add(blockPosition, position), Color.Blue);
                        break;
                    case 6:
                        s2.Draw(gridblock2, Vector2.Add(blockPosition, position), Color.Green);
                        break;
                    case 7:
                        s2.Draw(gridblock2, Vector2.Add(blockPosition, position), Color.Red);
                        break;
                    default:
                        Console.WriteLine("Draw default");
                        break;
                }

            }
        }

    }
}

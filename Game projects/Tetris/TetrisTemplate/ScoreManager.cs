using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


class ScoreManager
{
    public double Score = 0;
    public Vector2 possition = new Vector2(0, 0);

    public ScoreManager(Vector2 Possition,double StartingScore = 0)
    {
        SetPoints(StartingScore);
        SetPossition(Possition);
    }

    public void SetPossition(Vector2 p)
    {
        possition = p;
    }

    public Vector2 GetPossition()
    {
        return possition;
    }

    public void IncreasePoints(double p)
    {
        Score += p;
    }

    public void DecreasePoints(double p)
    {
        Score -= p;
    }

    public void SetPoints(double p)
    {
        Score = p;
    }

    public void MultiplyCurrentPoints(double P)
    {
        Score *= P;
    }

    public double GetPoints()
    {
        return Score;
    }

    public int GetPoints_int()
    {
        return (int)Score;
    }

}


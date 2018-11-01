using System;

public class EullerAlgorithmSeeder
{
    Random Randomer;
    // int RandomBetweenColumnBorder = 4, RandomBetweenRowBorder = 5;

    double RandomBetweenColumnBorder = 0.25, RandomBetweenRowBorder = 0.9f;

    public bool BetweenColumnBorder()
    {
        return Randomer.NextDouble() <= RandomBetweenColumnBorder;
    }
    public bool BetweenRowBorder()
    {
        return Randomer.NextDouble() <= RandomBetweenRowBorder;
    }

    public EullerAlgorithmSeeder(int seed)
    {
        Randomer = new Random();
    }
    public EullerAlgorithmSeeder()
    : this(Environment.TickCount) { }
}
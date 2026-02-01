namespace MaskedRobbery;

public class ScoreService
{
    public static int Score { get; private set; }
    
    public static void Add(int score) => Score += score;
    
    public static void Reset() => Score = 0;
}
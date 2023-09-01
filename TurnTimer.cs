




public class TurnTimer
{
    public int CurrentTurn { get; set; }

    public TurnTimer()
    {
        CurrentTurn = 0;
    }

    public void PlayTurn()
    {

    }
    public void AdvanceTurn()
    {
        CurrentTurn++;
    }
}


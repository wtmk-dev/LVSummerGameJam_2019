public class PlayerModel
{
    public int Level {get;set;}
    public int MaxLevel = 3;
    public float CurrentConfidence {get;set;}

    public bool hasWon = false;
    public bool hasLost = false;

    public PlayerModel()
    {
        Level = 0;
        CurrentConfidence = 0.0f;
    }

    public void LevelUp()
    {
        if(CurrentConfidence > 1.0f)
        {
            Level++;
            if(Level == MaxLevel)
            {
                hasWon = true;
                CurrentConfidence = 1.0f;
            }
            else
            {
                CurrentConfidence = 0.0f;
            }
        }

        if(CurrentConfidence < 0.0f)
        {
            Level--;
            if(Level < -1)
            {
                hasLost = true;
                CurrentConfidence = 0.0f;
            }
            else
            {
                CurrentConfidence = 1.0f;
            }
        }
    }

}
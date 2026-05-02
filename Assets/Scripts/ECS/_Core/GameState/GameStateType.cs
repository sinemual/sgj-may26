namespace Client.Data
{
    public enum GameStateType
    {
        None = 0,
        Init = 1,
        GameOver = 4,
        Pause = 6,
        Win = 10,
        Lose = 11,
        CatchingStep = 12,
        GatheringStep = 13,
        HomeStep = 14,
        RaceStep = 15,
        GameEnd = 16
        
    }
}
namespace Signals
{
    public struct ScoreIncreasedSignal
    {
        public int AddedScore;

        public ScoreIncreasedSignal(int addedScore = 1)
        {
            AddedScore = addedScore;
        }
    }
}
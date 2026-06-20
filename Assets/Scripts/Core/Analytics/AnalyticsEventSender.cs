using Firebase.Analytics;

namespace Core.Analytics
{
    public class AnalyticsEventSender
    {
        public void PlayerDiedEvent(int score)
        {
            FirebaseAnalytics.LogEvent(FirebaseAnalytics.ParameterScore, new Parameter("Score", score));
        }
    }
}
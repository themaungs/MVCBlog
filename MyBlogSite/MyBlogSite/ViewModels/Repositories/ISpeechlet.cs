using AlexaSkillsKit.Speechlet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlogSite.Repositories
{
    public interface ISpeechletAsync
    {
        Task<SpeechletResponse> OnIntentAsync(IntentRequest intentRequest, Session session);
        Task<SpeechletResponse> OnLaunchAsync(LaunchRequest launchRequest, Session session);
        Task OnSessionStartedAsync(SessionStartedRequest sessionStartedRequest, Session session);
        Task OnSessionEndedAsync(SessionEndedRequest sessionEndedRequest, Session session);
    }
    public interface ISpeechlet
    {
        SpeechletResponse OnIntent(IntentRequest intentRequest, Session session);
        SpeechletResponse OnLaunch(LaunchRequest launchRequest, Session session);
        void OnSessionStarted(SessionStartedRequest sessionStartedRequest, Session session);
        void OnSessionEnded(SessionEndedRequest sessionEndedRequest, Session session);
    }
}

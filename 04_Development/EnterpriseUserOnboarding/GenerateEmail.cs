using UiPath.CodedWorkflows;

namespace EnterpriseUserOnboarding
{
    public class GenerateEmail : CodedWorkflow
    {
        [Workflow]
        public string Execute(string username, AppConfig config)
        {
            return username + "@" + config.EmailDomain;
        }
    }
}
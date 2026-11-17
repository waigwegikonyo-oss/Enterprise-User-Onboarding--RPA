using UiPath.CodedWorkflows;

namespace EnterpriseUserOnboarding
{
    public class RunOnboarding : CodedWorkflow
    {
        [Workflow]
        public void Execute()
        {
            AppConfig config = workflows.LoadConfiguration();
            workflows.ProcessEmployees(config.InputFile);
        }
    }
}
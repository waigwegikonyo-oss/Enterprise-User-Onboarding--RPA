using System;
using System.Collections.Generic;
using System.Linq;
using UiPath.CodedWorkflows;

namespace EnterpriseUserOnboarding
{
    public class CloseProcess : CodedWorkflow
    {
        [Workflow]
        public void Execute(string runId, DateTime start, List<RecordOutcome> outcomes)
        {
            int processed = outcomes.Count(o => o.Status == "Processed");
            int rejected = outcomes.Count(o => o.Status == "Rejected");
            int failed = outcomes.Count(o => o.Status == "Failed");
            double seconds = (DateTime.Now - start).TotalSeconds;

            Log($"Process finished. RunId: {runId} | Total: {outcomes.Count} | Processed: {processed} | Rejected: {rejected} | Failed: {failed} | Duration: {seconds:F1}s");
        }
    }
}
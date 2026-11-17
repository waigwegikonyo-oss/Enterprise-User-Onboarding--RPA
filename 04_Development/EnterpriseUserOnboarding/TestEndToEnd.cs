using System.Collections.Generic;
using System.Linq;
using UiPath.CodedWorkflows;

namespace EnterpriseUserOnboarding
{
    public class TestEndToEnd : CodedWorkflow
    {
        [Workflow]
        public void Execute()
        {
            List<RecordOutcome> valid = workflows.ProcessEmployees("employees_valid.csv");
            Check("E2E-01 valid file: 8 processed", valid.Count(o => o.Status == "Processed") == 8);
            Check("E2E-02 valid file: 0 rejected", valid.Count(o => o.Status == "Rejected") == 0);

            List<RecordOutcome> invalid = workflows.ProcessEmployees("employees_invalid.csv");
            Check("E2E-03 invalid file: 1 processed", invalid.Count(o => o.Status == "Processed") == 1);
            Check("E2E-04 invalid file: 9 rejected", invalid.Count(o => o.Status == "Rejected") == 9);
        }

        private void Check(string name, bool ok)
        {
            Log($"{(ok ? "PASS" : "FAIL")} {name}");
        }
    }
}
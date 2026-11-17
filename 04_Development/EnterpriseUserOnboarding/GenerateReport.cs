using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UiPath.CodedWorkflows;

namespace EnterpriseUserOnboarding
{
    public class GenerateReport : CodedWorkflow
    {
        [Workflow]
        public string Execute(string runId, DateTime start, List<RecordOutcome> outcomes)
        {
            DateTime end = DateTime.Now;
            int processed = outcomes.Count(o => o.Status == "Processed");
            int rejected = outcomes.Count(o => o.Status == "Rejected");
            int failed = outcomes.Count(o => o.Status == "Failed");

            var lines = new List<string>
            {
                "Enterprise User Onboarding - Run Report",
                "========================================",
                $"Run ID:     {runId}",
                $"Started:    {start:yyyy-MM-dd HH:mm:ss}",
                $"Finished:   {end:yyyy-MM-dd HH:mm:ss}",
                $"Duration:   {(end - start).TotalSeconds:F1} seconds",
                "",
                $"Total records: {outcomes.Count}",
                $"Processed:     {processed}",
                $"Rejected:      {rejected}",
                $"Failed:        {failed}",
                "",
                "Rejected / failed records",
                "-------------------------"
            };

            foreach (RecordOutcome o in outcomes.Where(o => o.Status != "Processed"))
            {
                lines.Add($"Row {o.RowNumber} | {o.EmployeeID} | {o.FullName} | {o.ErrorCode}");
            }

            lines.Add("");
            lines.Add("Processed accounts");
            lines.Add("------------------");

            foreach (RecordOutcome o in outcomes.Where(o => o.Status == "Processed"))
            {
                lines.Add($"{o.EmployeeID} | {o.Username} | {o.Email} | {o.AccessLevel}");
            }

            string path = Path.Combine(OutputPaths.OutputDir(), $"report_{runId}.txt");
            File.WriteAllLines(path, lines);
            Log($"Report written: {path}");
            return path;
        }
    }
}
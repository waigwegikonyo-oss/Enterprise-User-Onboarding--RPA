using System;
using System.IO;
using UiPath.CodedWorkflows;

namespace EnterpriseUserOnboarding
{
    public class WriteAuditLog : CodedWorkflow
    {
        [Workflow]
        public void Execute(string runId, RecordOutcome o)
        {
            string path = Path.Combine(OutputPaths.OutputDir(), $"audit_log_{runId}.csv");

            if (!File.Exists(path))
            {
                File.AppendAllText(path,
                    "Timestamp,RunId,Row,EmployeeID,FullName,Status,ErrorCode,Username,Email,AccessLevel,Systems"
                    + Environment.NewLine);
            }

            string[] fields = new string[]
            {
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), runId, o.RowNumber.ToString(),
                o.EmployeeID, o.FullName, o.Status, o.ErrorCode, o.Username, o.Email,
                o.AccessLevel, o.Systems
            };

            File.AppendAllText(path,
                string.Join(",", Array.ConvertAll(fields, Quote)) + Environment.NewLine);
        }

        private static string Quote(string v)
        {
            v = v ?? "";
            return "\"" + v.Replace("\"", "\"\"") + "\"";
        }
    }
}
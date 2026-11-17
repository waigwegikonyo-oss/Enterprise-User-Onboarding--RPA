using System;
using System.IO;
using UiPath.CodedWorkflows;

namespace EnterpriseUserOnboarding
{
    public class Initialize : CodedWorkflow
    {
        [Workflow]
        public string Execute(string inputFileName)
        {
            string runId = Guid.NewGuid().ToString().Substring(0, 8);
            Log($"Process started. RunId: {runId}, Start: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");

            string projectDir = Environment.CurrentDirectory;
            string[] requiredFiles = new string[]
            {
                Path.GetFullPath(Path.Combine(projectDir, "..", "Config", "config.json")),
                Path.GetFullPath(Path.Combine(projectDir, "..", "Config", "access_matrix.csv")),
                Path.GetFullPath(Path.Combine(projectDir, "..", "..", "06_Test_Data", inputFileName))
            };

            foreach (string path in requiredFiles)
            {
                if (!File.Exists(path))
                {
                    throw new Exception($"Startup check failed. File not found: {path}");
                }
                Log($"Startup check OK: {path}");
            }

            Log("All startup checks passed.");
            return runId;
        }
    }
}
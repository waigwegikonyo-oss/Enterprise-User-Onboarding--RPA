using EnterpriseUserOnboarding.ObjectRepository;
using System;
using System.Collections.Generic;
using System.Data;
using UiPath.Activities.System.Jobs.Coded;
using UiPath.CodedWorkflows;
using UiPath.Core;
using UiPath.Core.Activities.Storage;
using UiPath.Excel;
using UiPath.Excel.Activities;
using UiPath.Excel.Activities.API;
using UiPath.Excel.Activities.API.Models;
using UiPath.Mail.Activities.Api;
using UiPath.Orchestrator.Client.Models;
using UiPath.Testing;
using UiPath.Testing.Activities.Api.Models;
using UiPath.Testing.Activities.Models;
using UiPath.Testing.Activities.TestData;
using UiPath.Testing.Activities.TestDataQueues.Enums;
using UiPath.Testing.Enums;
using UiPath.UIAutomationNext.API.Contracts;
using UiPath.UIAutomationNext.API.Models;
using UiPath.UIAutomationNext.Enums;
using System.IO;
namespace EnterpriseUserOnboarding
{
    public class Initialize : CodedWorkflow
    {
        [Workflow]
        public void Execute()
        {
string runId = Guid.NewGuid().ToString().Substring(0, 8);
DateTime startTime = DateTime.Now;
Log($"Process started. RunId: {runId}, Start: {startTime:yyyy-MM-dd HH:mm:ss}");

string projectDir = Environment.CurrentDirectory;
string[] requiredFiles = new string[]
{
    Path.GetFullPath(Path.Combine(projectDir, "..", "Config", "config.json")),
    Path.GetFullPath(Path.Combine(projectDir, "..", "Config", "access_matrix.csv")),
    Path.GetFullPath(Path.Combine(projectDir, "..", "..", "06_Test_Data", "employees_valid.csv"))
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
        }
    }
}
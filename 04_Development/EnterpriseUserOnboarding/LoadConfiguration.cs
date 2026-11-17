using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UiPath.CodedWorkflows;

namespace EnterpriseUserOnboarding
{
    public class UsernameRules
    {
        public bool Lowercase { get; set; }
        public bool StripNonLetters { get; set; }
        public int CollisionSuffixStart { get; set; }
    }

    public class AppConfig
    {
        public string ProcessName { get; set; }
        public string EmailDomain { get; set; }
        public string UsernameFormat { get; set; }
        public UsernameRules UsernameRules { get; set; }
        public string InputFile { get; set; }
        public string AccessMatrixFile { get; set; }
        public string DateFormat { get; set; }
        public List<string> ValidDepartments { get; set; }
        public List<string> RequiredFields { get; set; }
        public int MaxRetryCount { get; set; }
        public string LogLevel { get; set; }
    }

    public class LoadConfiguration : CodedWorkflow
    {
        [Workflow]
        public AppConfig Execute()
        {
            string configPath = Path.GetFullPath(
                Path.Combine(Environment.CurrentDirectory, "..", "Config", "config.json"));

            string json = File.ReadAllText(configPath);
            AppConfig config = JsonConvert.DeserializeObject<AppConfig>(json);

            if (config == null)
            {
                throw new Exception("Config file is empty or not valid JSON.");
            }

            Log($"Config loaded: {config.ProcessName}");
            Log($"Email domain: {config.EmailDomain}");
            Log($"Departments: {string.Join(", ", config.ValidDepartments)}");
            Log($"Collision suffix starts at: {config.UsernameRules.CollisionSuffixStart}");

            return config;
        }
    }
}
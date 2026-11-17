using System;
using System.Collections.Generic;
using System.IO;
using UiPath.CodedWorkflows;

namespace EnterpriseUserOnboarding
{
    public class LoadAccessMatrix : CodedWorkflow
    {
        [Workflow]
        public Dictionary<string, AccessGrant> Execute()
        {
            string path = Path.GetFullPath(Path.Combine(
                Environment.CurrentDirectory, "..", "Config", "access_matrix.csv"));

            var matrix = new Dictionary<string, AccessGrant>();
            string[] lines = File.ReadAllLines(path);

            for (int i = 1; i < lines.Length; i++)
            {
                string[] c = lines[i].Split(',');
                if (c.Length < 4) continue;

                matrix[c[0].Trim() + "|" + c[1].Trim()] = new AccessGrant
                {
                    Systems = c[2].Trim(),
                    AccessLevel = c[3].Trim()
                };
            }

            Log($"Access matrix loaded: {matrix.Count} department/role entries");
            return matrix;
        }
    }
}
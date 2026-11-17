using System.Collections.Generic;
using System.Text.RegularExpressions;
using UiPath.CodedWorkflows;

namespace EnterpriseUserOnboarding
{
    public class GenerateUsername : CodedWorkflow
    {
        [Workflow]
        public string Execute(Employee emp, AppConfig config, HashSet<string> usedUsernames)
        {
            string first = emp.FirstName.Trim();
            string last = emp.LastName.Trim();

            string baseName = first.Substring(0, 1) + last;

            if (config.UsernameRules.StripNonLetters)
            {
                baseName = Regex.Replace(baseName, "[^a-zA-Z]", "");
            }
            if (config.UsernameRules.Lowercase)
            {
                baseName = baseName.ToLowerInvariant();
            }

            string candidate = baseName;
            int suffix = config.UsernameRules.CollisionSuffixStart;

            while (usedUsernames.Contains(candidate))
            {
                candidate = baseName + suffix;
                suffix++;
            }

            usedUsernames.Add(candidate);
            return candidate;
        }
    }
}
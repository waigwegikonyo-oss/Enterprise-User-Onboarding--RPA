using System;
using System.Collections.Generic;
using UiPath.CodedWorkflows;

namespace EnterpriseUserOnboarding
{
    public class AssignAccess : CodedWorkflow
    {
        [Workflow]
        public AccessGrant Execute(Employee emp, Dictionary<string, AccessGrant> accessMatrix)
        {
            string key = emp.Department + "|" + emp.Role;

            if (!accessMatrix.TryGetValue(key, out AccessGrant grant))
            {
                throw new Exception($"No access entry for {key}");
            }

            return grant;
        }
    }
}
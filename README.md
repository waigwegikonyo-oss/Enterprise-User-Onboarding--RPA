# Enterprise User Onboarding Automation

An automated new-employee onboarding process built with UiPath Studio (coded workflows in C#). It reads employee records, validates them against business rules, generates usernames and email addresses, assigns system access from an access matrix, and writes an audit log, a run report and welcome messages.

## What it does
- Validates each record (one error code per rejected row)
- Generates unique usernames with collision handling (jkamau, jkamau2)
- Assigns access by department and role
- Produces an audit log, a run report and a welcome message per employee
- Continues past bad records and logs each one

## Project structure
| Folder | Contents |
|---|---|
| 01 to 03 | Business analysis, process design, solution design |
| 04_Development | UiPath project (C# coded workflows) and Config |
| 06_Test_Data | Valid and invalid test files and the expected-results sheet |
| 07_Output | Sample audit logs, reports and welcome messages |
| 08_Documentation | Test log |
| 09_Screenshots | Evidence of test runs and outputs |

## Testing
Automated tests compare actual results to an expected-results sheet:
- Validation: 10 cases (TestValidation)
- Generation: 8 cases (TestGeneration)
- End to end: 4 checks (TestEndToEnd)

See `08_Documentation/TEST_LOG.md` for results, including a defect found and fixed during testing.

## How to run
1. Clone the repo and open `04_Development/EnterpriseUserOnboarding` in UiPath Studio.
2. Run `RunOnboarding.cs`. It reads the input file named in `Config/config.json`.
3. Outputs appear in `07_Output`.

## Notes
This project was first prototyped in UiPath Studio Web, where file access was verified through Google Drive, and then built in desktop Studio so it can read local files.

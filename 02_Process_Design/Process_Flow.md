# Process Flow

## Employee Onboarding Automation
          START

            ↓

    Receive Employee File

            ↓

    Read Employee Record

            ↓

       Validate Data

            ↓

      Is Data Valid?

            |
            |
     NO----------YES
      |            ↓
      |
      |    Assign Access Rights
      |
      |            ↓
      | 
      |    Generate Notification
      |
      |            ↓
      |
      |       Write Audit Log
      |
      |            ↓
      |
      |      Generate Reports
      |
      |            ↓
      |---------> End
 

---

# Process States

| State | Description |
|-|-|
| Received | Input received |
| Validating | Checking information |
| Processing | Creating onboarding details |
| Completed | Successfully processed |
| Failed | Exception occurred |



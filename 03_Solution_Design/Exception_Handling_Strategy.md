# Exception Handling Strategy

## Business Exceptions

Examples:

- Missing employee ID
- Duplicate employee
- Invalid department

Action:

- Log exception
- Skip employee
- Continue processing

---

## System Exceptions

Examples:

- File missing
- File locked
- Unexpected workflow failure

Action:

- Retry
- Log error
- Stop if unrecoverable

---

## Retry Policy

Maximum retries:

3

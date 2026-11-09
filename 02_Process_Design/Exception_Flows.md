# Exception Flow Design

## Purpose

This document defines how the automation handles failures.

---

# 1. Business Exceptions

Business exceptions occur when input data violates business rules.

---

# Exception 001: Missing Employee Information

Example:

Missing department.

Process:

1. Detect missing field.
2. Stop employee processing.
3. Log failure.
4. Continue with next employee.

---

# Exception 002: Duplicate Employee

Example:

Employee ID already exists.

Process:

1. Check existing records.
2. Mark transaction as duplicate.
3. Record reason.
4. Continue processing.

---

# Exception 003: Invalid Department

Process:

1. Compare department against approved list.
2. Reject invalid value.
3. Generate exception report.

---

# 2. System Exceptions

System exceptions occur due to technical failures.

---

# Exception 004: Input File Missing

Process:

1. Attempt file access.
2. Retry operation.
3. Log failure if unavailable.
4. Stop execution.

---

# Exception 005: Report Generation Failure

Process:

1. Retry report generation.
2. Capture error.
3. Notify support team.

---

# 3. Exception Logging

Every exception must contain:

- Timestamp
- Employee ID
- Workflow name
- Error category
- Error message
- Resolution status

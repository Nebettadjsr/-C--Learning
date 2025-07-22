# Simple Banking System - C# Console App

## Overview

This is a small C# console application simulating a basic banking system.  
It allows multiple users to:

- **Create accounts** with a balance and PIN code
- **Switch between accounts**
- **Check account balance**
- **Deposit money**
- **Withdraw money** (with insufficient funds check)

A pre-configured demo user is provided:
- **Name**: Herbert  
- **PIN**: 1234  
- **Balance**: $9876.50

---

## Features

- Object-oriented design using the `BankAccount` class.
- Dictionary-based account management (`Dictionary<string, BankAccount>`).
- PIN validation with limited attempts.
- Input validation for positive numbers and sufficient balance checks.
- Simple console-based menu system.

---

## Usage Notes

- The program is written in **English**, but runs on your system's default locale.  
- **Decimal separators** depend on your system's locale:
    - English/US: Use `.` (e.g., `15.5`)
    - German/EU: Use `,` (e.g., `15,5`)

If you run this app in an English locale environment and enter `15,5`,  
the input will **NOT be recognized as 15.5**, but instead as `155`.  
This behavior is **not fixed in this exercise**, but is explained in comments.

---

## Learning Purpose

This project is a **learning exercise** to practice:

- Classes & Objects
- Constructors
- Dictionaries
- Input validation
- Program flow control (loops, switch cases, etc.)

### 🔧 **Further explanations and developer notes are included directly in the code as XML comments** (`/// <summary> ... </summary>`).

---

## License

MIT License  
(C) 2025 Bianca Krause - For educational use only.

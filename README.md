# TransactionApp

# 💰 Fee Calculation Service

A backend service that processes financial transactions and calculates applicable fees based on dynamic, runtime-defined business rules.

## 🧾 Project Description

The application supports various types of financial transactions (e.g., POS, ECOMMERCE) and determines transaction fees using **dynamic rules** stored in the database. This eliminates the need to redeploy when business logic changes.

It is built using:

- ASP.NET Core (.NET 8)
- FluentValidation
- Entity Framework Core
- xUnit for testing
- Moq for mocking
- Dynamic Expresso for runtime rule evaluation

---
### 📦 Installation

1. **Clone the repository**

```bash
git clone https://github.com/your-username/TransactionApp.git
cd fee-calculation-service
```

2. **Update the database connection**

```bash
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=FeeCalcDb;Username=postgres;Password=yourpassword"
}
```
3. **Apply Migrations**
```bash
Add-Migration InitialMigration
```
```bash
Update-Database
```
4. **Run the application**

## 📌 Known Limitations & Future Improvements**

Limitations:

- Rule validation (expression syntax) is currently not enforced on insert
- No web-based UI to manage rules yet
- Logging and exception handling can be improved
- Currently assumes all rules return decimal fees (no percentage-based tagging)
## ⚠️ Core Challenge: Dynamic Rule Evaluation
One of the key challenges in this project was enabling dynamic fee rule evaluation without requiring code changes or redeployment.
The intended approach was to store rule conditions and fee logic as string expressions in a Rule database entity,
use Dynamic Expresso to interpret and evaluate those expressions at runtime,build a flexible rule engine capable of applying multiple matching rules per transaction.While the foundational structure was implemented (rule model, interpreter integration, partial rule engine), the complete logic for dynamically composing, evaluating, and aggregating multiple applicable rules per transaction wasn't fully solved within the time constraints of the challenge.
## 👨‍💻 Author
Built by: Matej Minoski

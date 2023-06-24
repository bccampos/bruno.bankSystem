# Problem
•	A user can have as many accounts as they want.
•	A user can create and delete accounts.
•	A user can deposit and withdraw from accounts.
•	An account cannot have less than $100 at any time in an account.
•	A user cannot withdraw more than 90% of their total balance from an account in a single transaction.
•	A user cannot deposit more than $10,000 in a single transaction.

# Solution
•	ASP.NET 7 / Mapster / XUnit 
•	Using CQRS & MediatR following Clean Architecture & DDD 

![image](https://github.com/bccampos/bruno.bankSystem/assets/36283909/250b2b10-691b-4ea5-9b9b-bb1acf6041b6)

# Swagger UI 
![image](https://github.com/bccampos/bruno.bankSystem/assets/36283909/d890d519-a546-419e-8de4-7aaf3cd6c316)

# Fake Account Data for tests
1) **Account01:**  Account Number: 123, Initial Balance: 500
2) **Account02:**  Account Number: 1234, Initial Balance: 100

# Tests cover all business rules requirements 
•	WebApiTests / ApplicationTests / DomainTests / ArchitectureTests
![image](https://github.com/bccampos/bruno.bankSystem/assets/36283909/7f87eed2-2a9c-4d86-8a87-5e6bdef4ba52)





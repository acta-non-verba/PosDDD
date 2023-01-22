# PosDDD
An attempt to apply DDD principles 

Given the below problem statement I implemented the POS system.

ToDo:
- Introduce Middlewares for RateLimit, ExceptionHandler, AntiXSSInjectionGuard, 
- Create remaining controllers apart from deal and order controllers
- Add custom Comparers for Value Objects (DealType, Quantity etc)
- Add more unit test for now it covers only the important aspects.
- Add more DatabaseContext if needed for handling additional BoundedContext.
- Find more SOLIDifying opportunities
- Clean-up Dependency Injection from Program.cs

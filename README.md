# PosDDD
An attempt to apply DDD principles 

Given the below problem statement I implemented the POS system.


Point-of -sale system for a convenience store. You must use DDD principle and clean architecture while follow SOLID design principles and TDD.
Assume that a scanner is already available to scan the barcode to add the SKU to the scanned inventory items into the current sale.
The following is the limited subset of the inventory items:
SKU	Item			          Price
453	Apples per kg.		  INR 75
799	Hair Tie per pair		INR 15
125	Amul Butter 100g	  INR 20

The following is the list of features that the POS system should support.
1.	Every morning, the store owner should be able to configure the deals for the day from one of the deals below:
a.	INR 1 off the normal price of an item.
b.	10% off the normal price of an item.
c.	Buy two items get one item free.

2.	For an item that doesn’t have a deal the system just costs the normal price.
3.	At the end of the sale, the point-of-sale system should calculate the total of the items being purchased. Consider 5% sales tax or VAT on the total of the items purchased.




ToDo:
- Introduce Middlewares for RateLimit, ExceptionHandler, AntiXSSInjectionGuard, 
- Add the missing integration with Barcode scanner service in Infrastructure layer 
  with adapter/repository pattern to call those services/api interface(s).
- Create remaining controllers apart from deal and order controllers
- Add custom Comparers for Value Objects (DealType, Quantity etc)
- Add more unit test for now it covers only the important aspects.
- Add more DatabaseContext if needed for handling additional BoundedContext.
- Find more SOLIDifying opportunities
- Clean-up Dependency Injection from Program.cs

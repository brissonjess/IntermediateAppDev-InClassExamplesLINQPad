<Query Kind="Statements">
  <Connection>
    <ID>a006419e-f549-45ef-9a94-058b66849f19</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//SAMPLE FOR ENTITY SUBSET AND ENTITY FROM CHILD TO PARENT ON WHERE

//Q: List all of the customers served by employee Jane Peacock
// 	Show the customer Lastname, FirstName, City, State, Phone, Email
//		Hint: Navigatr between entities x.schemaName.field 


/*
Notes:
	- When looking at the connection menu on the right-hand-side of LinqPad you can tell which 
		attribute is the parent and which is the child by looking at the Blue (parent) and Green (child) menu items
	
	- 
*/

//SQL Method Query solution (using a C# statement)
var customers = 
from x in Customers
where x.SupportRepIdEmployee.LastName.Equals("Peacock") && 
	  x.SupportRepIdEmployee.FirstName.Equals("Jane")
orderby x.LastName
select new{ 
		x.LastName, 
		x.FirstName,
		x.City,
		x.State,
		x.Phone,
		x.Email
		};
customers.Dump();

// SQL Expression  solution (using C# Expression)
	//Name is a new attribute that you create in order to ensure that name works in any scenario 
/*
from x in Customers
where x.SupportRepIdEmployee.FirstName.Equals("Jane") &&
	  x.SupportRepIdEmployee.LastName.Equals("Peacock") //in this case SupportRepIdEmployee is equal 
select new{
	Name = x.LastName + ", " + x.FirstName,
	City = x.City,
	State = x.State,
	Phone = x.Phone,
	Email = x.Email
}
*/
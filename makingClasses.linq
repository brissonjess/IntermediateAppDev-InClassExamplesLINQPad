<Query Kind="Program">
  <Connection>
    <ID>47b9d349-efb1-4f43-aa04-c9945113b9f2</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//Data Transfer Objects (DTO) AKA Plain Ordinary Common Objects (POCO) are class structures that allow us to divide our code descritions between different levels of our program
	//POCO are used for Flat Data sets, especially for when we publish a report. These are reserved for your native data-types, like string, double, int, etc.
	//DTO are used for Structure Data Sets, which can contain an inner class or List<> (not yuor native data-types). 
void Main()
{
	//list of bill counts for all waiters
	//This query will create a flat dataset 
	//The columns are native datatyes (ie. int, string, double, etc.)
	//One is not concerned with repeated data in a column
	//Instead of using anonymous datatype (new{....}) we wish to use a defined class definition
	var BestWaiter = from x in Waiters
				//POCO class:
				select new WaiterBillCounts{
					Name = x.FirstName + " " + x.LastName,
					TCount = x.Bills.Count()
				};
	BestWaiter.Dump();
	var paramMonth = 4;
	var paramYear = 2014;
	var waiterbills = from x in Waiters
					where x.LastName.Contains("k") //will just give us two records
					orderby x.LastName, x.FirstName
					//DTO class:
					select new WaiterBills{
							Name = x.LastName + ", " + x.FirstName,
							TotalBillCount = x.Bills.Count(),
							BillInfo = (from y in x.Bills
										where y.BillItems.Count() > 0
										&& y.BillDate.Month == DateTime.Today.Month - paramMonth
										&& y.BillDate.Year == paramYear
										//POCO class:
										select new BillItemSummary{
											BillId = y.BillID,
											BillDate = y.BillDate,
											TableID = y.TableID,
											Total = y.BillItems.Sum(b => b.SalePrice * b.Quantity)
													}
										).ToList() //how to convert a class to a list
								};
	waiterbills.Dump();
}

// Define other methods and classes here
// Two examples of POCO class
public class WaiterBillCounts{
	//whatever receiving field on your query in your Select appears as a property in this class
	public string Name{get; set;}
	public int TCount{get; set;}
}

public class BillItemSummary{
	public int BillId{get;set;}
	public DateTime BillDate{get;set;}
	public int? TableID{get;set;} // ? means the field can be nullable
	public decimal Total{get;set;}
}

//Example of a DTO (structured) class
//
public class WaiterBills{
	public string Name{get;set;}
	public int TotalBillCount {get;set;}
	public List<BillItemSummary> BillInfo{get;set;}
}
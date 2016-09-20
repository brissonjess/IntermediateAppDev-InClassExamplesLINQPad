var billsThisMonth = from item in Bills
					 where item.PaidStatus
					 && item.BillDate.Month == DateTime.Today.Month -1
					 && item.BillDate.Year == DateTime.Today.Year
					 orderby item.BillDate descending 
					 select new
					 {
						BillDate = item.BillDate, //get the bill date
						BillId = item.BillId, //get the id
						NumberInParty = item.NumberInParty, //people served
						TotalBillable = item.BillItems.Sum (bi => biQuantity * bi.UnitCost), //total potentially billable (BillItem.Quantity * BillItem.UnitCost)
						ActualBillTotal = item.BillItems.Sum (bi => bi.Quantity * bi.SalePrice) //actual amount billed 
					 };
var title = string.Format("Total income for {0} {1}",DateTime.Today.AddMonths(-1).ToString("MMM"),DateTime.Today.Year);
billsThisMonth.Sum(tm => tm.ActualBillTotal).ToString("C").Dump(title, true);
billsThisMonth.Sum(tm => tm.NumberInParty).Dump("Patrons served", true);

////get the following from the Bills table for the current month:
//Billdate, ID, people served, total potentially billable (BillItem.Quantity * BillItem.UnitCost), and actual billed 
//Then display the total income for the mo nth and the number of patrons served 

var billsThisMonth = from item in Bills
					 where item.PaidStatus
					 && item.BillDate.Month == DateTime.Today.Month -1
					 && item.BillDate.Year == DateTime.Today.Year 
					 orderby item.BillDate descending
					 select new
					 {
						BillDate = item.BillDate, //select the bill date 
						BillId = item.BillId, //bill id 
						NumberInParty = item.NumberInParty, //num of people served
						TotalBillable = item.BillItems.Sum (bi => bi.Quantity * bi.UnitCost), //total potential sales
						ActualBillTotal = item.BillItems.Sum (bi => bi.Quantity * bi.SalePrice) //actual sales 
					 };
var title = string.Format("Total income for {0} {1}", DateTime.Today.AddMonths(-1).ToString("MMMM"),DateTime.Today.Year);
billsThisMonth.Sum (tm => tm.ActualBillTotal).ToString("C").Dump(title, true);
billsThisMonth.Sum (tm => tm.NumberInParty).Dump("Patrons served", true);
var report = from item in billsThisMonth 
			 group item by item.BillDate.Day into dailySummary 
			 select new 
			 {
				Day = dailySummary.Key,
				DailyPatrons = daiySummary.Sum (s => s.NumberInParty),
				Income = dailySummary.Sum (s =>ActualBillTotal)
			 };
report.OrderBy (r => r.Day).Dump("Daily Income);

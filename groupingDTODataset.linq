<Query Kind="Expression">
  <Connection>
    <ID>c8723e5f-45f7-47df-9da0-4b2549b25059</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//multiple column group
	/* grouping data placed in a local temp data set for further processing 
	   .Key allows your to have access to the value(s) in your group key(s)
	   if you have multiple group columns they MUST be in an anonymous datatype 
	   to create a DTO type collection you can use .ToList() on the temp dataset 
	   you can have a custom anonymous data collection by using a nested query 
	*/

//why do we want to do this?
/*
	In order to be able to get your data from your query at a later time you'll have to save 
		the data into a temporary data set variable. In this example we called it tempDataSet.
*/


//Step A
from food in Items
    group food by new {food.MenuCategoryID, food.CurrentPrice};
	
//Step B (DTO style dataset)
from food in Items
    group food by new {food.MenuCategoryID, food.CurrentPrice} into tempDataSet
	select new{
		MenuCategoryID = tempDataSet.Key.MenuCategoryID,
		CurrentPrice = tempDataSet.Key.CurrentPrice,
		//you write a nested query here in order to get a certain amount of records back
		//if you want ALL of the records you would type: FoodItems = tempDataSet.ToList()
		FoodItems = tempDataSet.ToList()
	};
//Step C DTO custom style dataset
from food in Items
    group food by new {food.MenuCategoryID, food.CurrentPrice} into tempDataSet
	select new{
		MenuCategoryID = tempDataSet.Key.MenuCategoryID,
		CurrentPrice = tempDataSet.Key.CurrentPrice,
		//you write a nested query here in order to get a certain amount of records back
		//if you want ALL of the records you would type: FoodItems = tempDataSet.ToList()
		FoodItems = from x in tempDataSet
					select new {
						ItemID = x.ItemID,
						FoodDescription = x.Description,
						TimeServed = x.BillItems.Count()//ex of aggregate function 
					}
	}
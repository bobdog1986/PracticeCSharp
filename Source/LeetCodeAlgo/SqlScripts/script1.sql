
/*
///183. Customers Who Never Order

select name as Customers from Customers where NOT EXISTS (SELECT * FROM Orders WHERE Customers.id=Orders.customerId)


///584. Find Customer Referee

select name from Customer where isnull(referee_id) || referee_id !=2

///595. Big Countries

select name,population, area from World where area>=3000000 or population>=25000000











///1757. Recyclable and Low Fat Products

select product_id   from Products where low_fats='Y' and recyclable ='Y'
*/

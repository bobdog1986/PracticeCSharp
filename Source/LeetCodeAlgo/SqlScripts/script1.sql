﻿
/*

///175. Combine Two Tables

select FirstName, LastName, City, State from Person left join Address on Person.PersonId = Address.PersonId;


///176. Second Highest Salary
select (
	select distinct Salary from Employee order by Salary Desc limit 1 offset 1
) as SecondHighestSalary

///182. Duplicate Emails

select email as Email
from Person
group by email
having count(*) > 1

///183. Customers Who Never Order

select name as Customers from Customers where NOT EXISTS (SELECT * FROM Orders WHERE Customers.id=Orders.customerId)

///196. Delete Duplicate Emails

delete from Person where id not in(
	select t.id from (
		select min(id) as id from Person group by email
	) t
)

///197. Rising Temperature

SELECT wt1.Id
FROM Weather as wt1, Weather as wt2
WHERE wt1.temperature> wt2.temperature AND TO_DAYS(wt1.recordDate)-TO_DAYS(wt2.recordDate)=1;

///511. Game Play Analysis I

select player_id, min(event_date) as first_login
from activity
group by player_id


///584. Find Customer Referee

select name from Customer where isnull(referee_id) || referee_id !=2

///586. Customer Placing the Largest Number of Orders

select customer_number from orders
group by customer_number
order by count(*) desc limit 1;

///595. Big Countries

select name,population, area from World where area>=3000000 or population>=25000000

///607. Sales Person

SELECT name FROM salesperson
where sales_id not in
(SELECT sales_id FROM orders
LEFT JOIN company
ON orders.com_id=company.com_id
WHERE company.name='RED')

///608. Tree Node

select id,
(case when p_id is null then 'Root'
	when id in (select p_id from tree) then 'Inner'
	else 'Leaf' end) as Type
from tree

///627. Swap Salary

update salary set sex = CHAR(ASCII('f') ^ ASCII('m') ^ ASCII(sex));

///1050. Actors and Directors Who Cooperated At Least Three Times

SELECT actor_id, director_id
FROM ActorDirector
GROUP BY actor_id, director_id
HAVING COUNT(*)>=3

///1084. Sales Analysis III

SELECT s.product_id, product_name
FROM Sales s
LEFT JOIN Product p
ON s.product_id = p.product_id
GROUP BY s.product_id
HAVING MIN(sale_date) >= CAST('2019-01-01' AS DATE) AND
		MAX(sale_date) <= CAST('2019-03-31' AS DATE)

///1141. User Activity for the Past 30 Days I

select activity_date as day, count(distinct user_id) as active_users
from Activity
where datediff('2019-07-27', activity_date) <30
group by activity_date

///1148. Article Views I

SELECT DISTINCT author_id AS id FROM Views
where author_id = viewer_id
ORDER BY id

///1158. Market Analysis I

SELECT u.user_id AS buyer_id, join_date, COUNT(order_date) AS orders_in_2019
FROM Users as u
LEFT JOIN Orders as o
ON u.user_id = o.buyer_id
AND YEAR(order_date) = '2019'
GROUP BY u.user_id

///1393. Capital Gain/Loss

SELECT stock_name, SUM(
	CASE
		WHEN operation = 'Buy' THEN -price
		ELSE price
	END
) AS capital_gain_loss
FROM Stocks
GROUP BY stock_name

///1407. Top Travellers

select u.name, IfNull(sum(r.distance),0) as travelled_distance
from users u left OUTER join rides r
on u.id = r.user_id
group by u.id
order by travelled_distance desc, name asc

///1484. Group Sold Products By The Date

SELECT
	sell_date,
	COUNT(DISTINCT product) AS num_sold,
	GROUP_CONCAT(DISTINCT product ORDER BY product) AS products
FROM Activities
GROUP BY sell_date;


///1527. Patients With a Condition

SELECT * FROM PATIENTS WHERE
CONDITIONS LIKE '% DIAB1%' OR
CONDITIONS LIKE 'DIAB1%';

///1581. Customer Who Visited but Did Not Make Any Transactions

select customer_id, count(visit_id) as count_no_trans
from visits
where visit_id not in (select visit_id from transactions)
group by customer_id

///1587. Bank Account Summary II

select
	a.name,
	sum(b.amount) balance
from
	Users a
join
	Transactions b
on
	a.account = b.account
group by
	a.account
having
	balance > 10000;

///1667. Fix Names in a Table

select user_id,concat(upper(substr(name,1,1)),lower(substr(name,2))) as name
from users order by user_id

///1693. Daily Leads and Partners

select date_id, make_name, count(distinct lead_id) as unique_leads ,count(distinct partner_id) as unique_partners
from DailySales
group by date_id,make_name;

///1729. Find Followers Count

select user_id, count(distinct follower_id) as followers_count from Followers group by user_id;

///1741. Find Total Time Spent by Each Employee

select event_day as day, emp_id , sum(out_time -in_time ) as total_time
from Employees
group by day, emp_id

///1757. Recyclable and Low Fat Products

select product_id from Products where low_fats='Y' and recyclable ='Y'

///1795. Rearrange Products Table

SELECT product_id, 'store1' AS store, store1 AS price FROM Products WHERE store1 IS NOT NULL
UNION
SELECT product_id, 'store2' AS store, store2 AS price FROM Products WHERE store2 IS NOT NULL
UNION
SELECT product_id, 'store3' AS store, store3 AS price FROM Products WHERE store3 IS NOT NULL
ORDER BY 1,2 ASC

///1873. Calculate Special Bonus

SELECT employee_id ,
			CASE
				WHEN mod(employee_id ,2)=1 and name not like 'M%'
					THEN salary
				ELSE 0
			END
			as bonus
FROM Employees ;

///1890. The Latest Login in 2020

select user_id , max(time_stamp) as last_stamp
from Logins
where YEAR(time_stamp)=2020
group by user_id

///1965. Employees With Missing Information

SELECT sub.employee_id
FROM (
	SELECT e.employee_id, name, salary
	FROM employees AS e
	LEFT JOIN salaries AS s
	ON e.employee_id = s.employee_id

	UNION

	SELECT s.employee_id, name, salary
	FROM employees AS e
	RIGHT JOIN salaries AS s
	ON e.employee_id = s.employee_id) AS sub
WHERE sub.name IS NULL OR sub.salary IS NULL
ORDER BY sub.employee_id

*/

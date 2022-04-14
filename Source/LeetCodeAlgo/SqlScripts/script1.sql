
/*

///175. Combine Two Tables

select FirstName, LastName, City, State from Person left join Address on Person.PersonId = Address.PersonId;


///176. Second Highest Salary
select (
	select distinct Salary from Employee order by Salary Desc limit 1 offset 1
) as SecondHighestSalary

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

///584. Find Customer Referee

select name from Customer where isnull(referee_id) || referee_id !=2

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

///1141. User Activity for the Past 30 Days I

select activity_date as day, count(distinct user_id) as active_users
from Activity
where datediff('2019-07-27', activity_date) <30
group by activity_date

///1148. Article Views I

SELECT DISTINCT author_id AS id FROM Views
where author_id = viewer_id
ORDER BY id

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

///1667. Fix Names in a Table

select user_id,concat(upper(substr(name,1,1)),lower(substr(name,2))) as name
from users order by user_id

///1693. Daily Leads and Partners

select date_id, make_name, count(distinct lead_id) as unique_leads ,count(distinct partner_id) as unique_partners
from DailySales
group by date_id,make_name;

///1729. Find Followers Count

select user_id, count(distinct follower_id) as followers_count from Followers group by user_id;

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
			as bonus FROM Employees ;

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

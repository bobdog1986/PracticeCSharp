
/*
///183. Customers Who Never Order

select name as Customers from Customers where NOT EXISTS (SELECT * FROM Orders WHERE Customers.id=Orders.customerId)

///196. Delete Duplicate Emails

delete from Person where id not in(
    select t.id from (
        select min(id) as id from Person group by email
    ) t
)

///584. Find Customer Referee

select name from Customer where isnull(referee_id) || referee_id !=2

///595. Big Countries

select name,population, area from World where area>=3000000 or population>=25000000



///627. Swap Salary

update salary set sex = CHAR(ASCII('f') ^ ASCII('m') ^ ASCII(sex));



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

///1667. Fix Names in a Table

select user_id,concat(upper(substr(name,1,1)),lower(substr(name,2))) as name
from users order by user_id



///1757. Recyclable and Low Fat Products

select product_id   from Products where low_fats='Y' and recyclable ='Y'


///1873. Calculate Special Bonus

SELECT employee_id ,
            CASE
                WHEN mod(employee_id ,2)=1 and name not like 'M%'
                    THEN salary
                ELSE 0
            END
            as bonus FROM Employees ;




*/

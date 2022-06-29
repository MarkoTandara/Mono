CREATE TABLE Customer
( CustomerID uniqueidentifier,
FirstName varchar(50),
LastName varchar(50),
PRIMARY KEY (CustomerID)
)

CREATE TABLE Orders
(
OrderID uniqueidentifier,
CustomerID uniqueidentifier,
OrderName varchar(50),
PRIMARY KEY (OrderID),
CONSTRAINT FK_CustomerOrder FOREIGN KEY (CustomerID)
REFERENCES Customer(CustomerID)
)

INSERT INTO Customer VALUES
(NEWID(), 'Luka', 'Lukic'),
(NEWID(), 'Marija', 'Maric'),
(NEWID(), 'Ivo', 'Ivic')

INSERT INTO Orders VALUES
(NEWID(), (SELECT CustomerID FROM Customer WHERE FirstName='Luka' AND LastName='Lukic'), 'Cevapi'),
(NEWID(), (SELECT CustomerID FROM Customer WHERE FirstName='Marija' AND LastName='Maric'), 'Burger'),
(NEWID(), (SELECT CustomerID FROM Customer WHERE FirstName='Ivo' AND LastName='Ivic'), 'Pizza'),
(NEWID(), (SELECT CustomerID FROM Customer WHERE FirstName='Luka' AND LastName='Lukic'), 'Cevapi')

SELECT *
FROM Customer
INNER JOIN Orders
ON Customer.CustomerID = Orders.CustomerID;

SELECT Orders.OrderName, count(*) AS "Broj Narudzbi"
FROM Orders
JOIN Customer
ON Customer.CustomerID = Orders.CustomerID 
GROUP BY Orders.OrderName;


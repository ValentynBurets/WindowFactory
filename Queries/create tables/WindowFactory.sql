create table Person(
	Id int NOT NULL primary key,
	[Name] nvarchar(50) DEFAULT('Name'),
	Surname nvarchar(50) NOT NULL,
	PhoneNumber varchar(10) NOT NULL,
	Email nvarchar(50)
);

create table WorkerType(
	Id int identity(1, 1) NOT NULL primary key,
	[Name] nvarchar(50) NOT NULL
);


create table Worker(
	Id int identity(1, 1) NOT NULL primary key,
	PersonId int foreign key references Person(Id),
	TypeId int foreign key references WorkerType(Id),
	[PassWord] nvarchar(50) NOT NULL DEFAULT 'password' UNIQUE
);

--DROP TABLE SaleryList

create table SalaryList(
	Id int identity(1,1) NOT NULL  primary key,
	WorkerId int foreign key references Worker(Id),
	[Date] date,
	Salary decimal NOT NULL
);

create table [Order](
	Id int identity(1,1) NOT NULL primary key,
	OrderNumber int NOT NULL,
	CustomerId int NOT NULL foreign key references Person(Id),
	ManagerId int NOT NULL foreign key references Person(Id),
	TotalPrice decimal,
	Paid decimal,
	DateCreate datetime DEFAULT(CONVERT(varchar, getdate(), 0)),
	DateComplited datetime,
	ApproximateDateComplite date DEFAULT(CONVERT(varchar, GETDATE() + 7, 1))
);

--DROP TABLE Payment

create table Payment(
	Id int identity(1,1) NOT NULL primary key,
	OrderId int NOT NULL foreign key references [Order](Id),
	PayerId int foreign key references Person(Id),
	Paid decimal,
	[Date] datetime DEFAULT GETDATE()
);

create table Color(
	Id int identity(20, 1) NOT NULL primary key,
	[Name] nvarchar(50) NOT NULL
);


create table Product(
	Id int identity(1,1) NOT NULL primary key,
	ColorId int NOT NULL foreign key references Color(Id),
	OrderId int foreign key references [Order](Id),
	EngineerId int NOT NULL foreign key references Worker(Id),
	Height int,
	Width int,
	DateCreate datetime DEFAULT GETDATE(),
	DateComplite datetime,
	Price decimal
);

create table Country(
	Id int identity(50, 1) NOT NULL primary key,
	[Name] nvarchar(50) NOT NULL,
);


create table MaterialType(
	Id int NOT NULL primary key,
	[Name] nvarchar(50) NOT NULL,
	QuantityType nvarchar(50) NOT NULL
);


create table Material(
	Id int identity(1, 1) NOT NULL primary key,
	MaterialTypeId int foreign key references MaterialType(Id),
	CountryId int foreign key references Country(Id),
	[Name] nvarchar(50)
);

create table Storage(
	Id int identity(1, 1) NOT NULL primary key,
	MaterialId int foreign key references Material(Id),
	Price decimal,
	Quantity int
);

create table MaterialList(
	Id int identity(1, 1) NOT NULL primary key,
	MaterialId int foreign key references Storage(Id),
	ProductId int foreign key references Product(Id),
	[Date] datetime DEFAULT GETDATE(),
	Quantity int
);

--DROP TABLE SupplyOrder

create table SupplyOrder(
	Id int identity(1,1) NOT NULL primary key,
	OrderNumber int NOT NULL,
	DateCreate datetime DEFAULT GETDATE(),
	DateComplite datetime,
	ManagerId int foreign key references Worker(Id),
);

create table Offer(
	Id int NOT NULL primary key,
	SupplierId int foreign key references Person(Id),
	CompanyName nvarchar(50),
	MateriaId int foreign key references Material(Id),
	Quantity int,
	Price decimal,
	DeliveryDate datetime
);

--DROP TABLE SupplyItem

create table SupplyItem(
	Id int identity(1, 1) NOT NULL primary key,
	MaterialId int foreign key references Material(Id),
	Quantity int,
	CreationDate date DEFAULT GETDATE(),
	СompletionDate date DEFAULT GETDATE(),
	OfferId int foreign key references Offer(Id),
	SypplyOrderId int foreign key references SupplyOrder(Id)
);
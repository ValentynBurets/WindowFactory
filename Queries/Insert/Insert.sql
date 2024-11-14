
INSERT INTO MaterialType
VALUES (1, 'furniture', '1'),
	   (2, 'profile', 'm'),
	   (3, 'glass', 'm^2')

INSERT INTO WorkerType
VALUES ('accountant'),
	   ('engineer'),
	   ('manager')


insert into dbo.Color
( Name )
	values
( 'larch' ), 
( 'white willow' ),
( 'white maple' ),
( 'white pine' ),
( 'white oak' ),
( 'brown' ),
( 'ash' ), 
( 'white ash' ),
( ' red pine' ),
( 'white  beech' ),
( ' beech' )
go

insert into  dbo.Country
( Name )
	values
( 'Austria' ),
( 'Brazil' ),
( 'China' ),
( 'Egipt' ),
( 'India' ),
( 'Japan' ),
( 'Mexico' ),
( 'Russia' ),
( 'Saudi Arabia' ),
( 'Thailand' ),
( 'Turkey' ),
( 'United Kingdom' ),
( 'Vietnam' ),
( 'Afghanistan' ),
( 'Austria' )
go


--Id, materialTypeId, CountryId, Name
INSERT INTO Material
VALUES (1, 50, 'Mila ProLinea'),
	   (2, 51, 'some profile'),
       (3, 52, 'some glass')

--Id, MaterialId, price, Quantity
INSERT INTO Storage
VALUES (1, 120, 1),
		(2, 160, 6),
		(3, 120, 3)



INSERT INTO  Person
(Id, [Name], [SurName], [PhoneNumber], [Email])
VALUES
(1, 'Petro', 'Romanyk', '0599125367', 'Romanyk@gmail.com' ),
(2, 'Anatoly', 'Antonov', '0954735235', 'Anton1ov2@gmail.com'  ),
(3, 'Anton', 'Antonov', '0993526853', 'Antonov@gmail.com'  ),
(4, 'Valentyn', 'Ivanov', '0943573523', 'Ivano3v@gmail.com'  ),
(5, 'Igor', 'Ivangovg', '0995369374', 'Ivangovg@gmail.com'  ),
(6, 'Olexiy', 'Antonov', '0996853723', 'Antonov@gmail.com'  )

INSERT INTO Worker
(TypeId, PersonId, [PassWord])
VALUES
(1, 1, '123'),
(1, 1, '231'),
(2, 2, '563'),
(2, 2, '523'),
(3, 3, '734'),
(3, 3, '732')
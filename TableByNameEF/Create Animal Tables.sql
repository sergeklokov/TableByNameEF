use pubs

DROP TABLE IF EXISTS AnimalDog
create table AnimalDog(
	ID int identity primary key,
	Name varchar(50),
	Color varchar(20),
	Age int
);
insert into AnimalDog (Name, Color, Age) values ('Grey', 'Grey', 5);
insert into AnimalDog (Name, Color, Age) values ('Barky', 'Black', null);
insert into AnimalDog (Name, Color, Age) values ('Quiet', 'Brown', 8);

DROP TABLE IF EXISTS AnimalCat
create table AnimalCat(
	ID int identity primary key,
	Name varchar(50),
	Color varchar(20),
	Age int
);
insert into AnimalCat (Name, Color, Age) values ('Purr', 'White', 3);
insert into AnimalCat (Name, Color, Age) values ('Puss', 'Fluffy', 2);
insert into AnimalCat (Name, Color, Age) values ('Cutie', 'Yellow', 7);
insert into AnimalCat (Name, Color, Age) values ('Smelly', 'Blue', 9);

DROP TABLE IF EXISTS AnimalHorse
create table AnimalHorse(
	ID int identity primary key,
	Name varchar(50),
	Color varchar(20),
	Age int
);

DROP TABLE IF EXISTS AnimalRat
create table AnimalRat(
	ID int identity primary key,
	Name varchar(50),
	Color varchar(20),
	Age int
);
insert into AnimalRat (Name, Color, Age) values ('Cute', 'White', 1);
insert into AnimalRat (Name, Color, Age) values ('Teddy', 'White w dots', null);


create database TextReader
go

create table Member(
uid integer primary key identity (1,1),
memNumber integer,
memAss varchar (50),
memStartDate varchar (100),
memPerc varchar (100))
go

create procedure Add_Employee
@memNumber integer,
@memAss varchar (100),
@memStartDate varchar (100),
@memPerc varchar (100)
as
insert into Member (memNumber, memAss ,memStartDate ,memPerc)
values (@memNumber ,@memAss ,@memStartDate ,@memPerc)
go


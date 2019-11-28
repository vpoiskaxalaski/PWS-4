create table Student
(
	Id int identity(1,1) primary key,
	[Name] nvarchar(max)
)

create table Note
(
	Id int identity(1,1) primary key,
	Subj nvarchar(max),
	Note int null,
	StudentId int not null foreign key 
	references Student(Id)
)

insert into Student values 
('Andrei'), ('Yaroslav'), ('Bogdan')

insert into Note values
('���', 4, 3),
('���', 8, 2),
('���', 5, 1),
('����', 10, 1),
('����', 8, 2),
('����', 4, 3),
('��', 9, 1),
('��', 8, 2),
('��', 8, 3)
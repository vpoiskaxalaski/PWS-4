CREATE DATABASE pws_labs;

USE pws_labs;

CREATE TABLE Student (
Id int primary key not null,
Name nvarchar(15)
)

CREATE TABLE Note (
Id int primary key not null,
Subj nvarchar(15),
Note1 int,
StudentId int foreign key references Student(Id)
)

DROP TABLE Note

INSERT INTO Student VALUES(1,'Aliona'), (2,'Katya'),(3,'Nastya')

INSERT INTO Note VALUES(1, 'математика', 8, 1), (2, 'математика', 8, 2), (3, 'математика', 7, 3), (4, 'русский', 7, 1), (5, 'русский', 9, 2), (6, 'русский', 7, 3)

SELECT * FROM Student
SELECT * FROM Note

UPDATE Note SET Subj='math' WHERE Id>0 and Id<4 
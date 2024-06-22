Student Management System - TechNeurons


The web application was developed using ASP.NET MVC(C#),Entity Framework for ORM and MS SQL Server.
Entity Framework Code First approach was followed.


Entity Framework Model Classes for Student, Teacher, Principal, ClassDivision, EduStatus were manually defined and assigned Data Annotations for Validations.

Defining Class & Division(as ClassDivision) and Education Status(as EduStatus) as separate entities and referencing foreign key, 

instead of defining them as attributes of student and teacher, seemed like the logical approach to make the operations involving them easier and more efficient.

Migrations were created and Run using models to create the Database and Tables in SQL Server.

View and Controller for Principal were manually created and defined. 

The Portal action in PrincipalController performs the operations to get the Most Excellent,Average and poor classes and Displays in the Portal view.

Basic Views for Read,Write,Update,Delete operations for Teacher and Student as well as Controllers(with actions) were auto generated by scaffolding using Models and linked to Principal Portal.

Views and Controller Actions for Portal of Student and Teacher were created separately.

EduStatus as a separate entity has the message attribute which is simply displayed on the Student Portal as Student.EduStatus.Message using Entity Framework Model, as Student has foreign key EduStatus.

ClassDivision as a separate entity and being foreign key referenced in Teacher and Student is used to display the list of students of a particular teacher's class using the EF Model as : 

Teacher.ClassDivision.Students(which is a list of students linked to that classdivision).

Basic Login functionality was implemented for Principal,Teacher and Student using Session[], Login Authentication done by controller and ViewModel.


Challenges Faced:


Listing Students of a Teacher's class, message for student according to edu status and Getting Most Excellent,Average and Poor class seemed too dificult intially when class,division and edu status were considered as attributes.

Defining ClassDivision and EduStatus(with message as an attribute) as separate entities and referncing using foreign key in Teacher and Student made the task much easier and more efficient.

Entity Framework model for ClassDivision contains the list of Teachers and Students (as it is a foreign key reference) making the operations much easier to perform.


PROJECT SETUP

Softwares Required : Microsoft Visual Studio(2022 Community) and Microsoft SQL Server(Express Edition 2022) & SQL Server Management Studio

Steps:

1.Download Project from Git and open SLN file in Visual Studio.

2.In project open Web.config file(from root, not Views folder) and change the server in data source of the Connection String:
  <connectionStrings><add name="StudentManagementConnection" connectionString="data source=SUDPOOL-ASUSROG\SQLEXPRESS;Initial Catalog=StudentManagementDB;Persist Security Info=True;Integrated Security=True;" providerName="System.Data.SqlClient" /></connectionStrings>
  Note: This connection string is for Windows Authentication in SQL Server

3.In Visual Studio go to Tools -> NuGet Package Manager -> Package Manager Console

4.In the Package Manager Console execute update-database command to run migrations which will create the database and all tables.

5.In SQL Server Management Studio, connect to your server, open New Query in StudentManagementDB database(created by migrations in step 4) and run the following queries to input data for Principal,ClassDivision and EduStatus tables :

INSERT INTO Principals VALUES ('principal','abc123')

INSERT INTO ClassDivisions
VALUES
('1','A','1A'),
('1','B','1B'),
('1','C','1C'),
('2','A','2A'),
('2','B','2B'),
('2','C','2C'),
('3','A','3A'),
('3','B','3B'),
('3','C','3C'),
('4','A','4A'),
('4','B','4B'),
('4','C','4C'),
('5','A','5A'),
('5','B','5B'),
('5','C','5C'),
('6','A','6A'),
('6','B','6B'),
('6','C','6C'),
('7','A','7A'),
('7','B','7B'),
('7','C','7C'),
('8','A','8A'),
('8','B','8B'),
('8','C','8C'),
('9','A','9A'),
('9','B','9B'),
('9','C','9C'),
('10','A','10A'),
('10','B','10B'),
('10','C','10C'),
('11','A','11A'),
('11','B','11B'),
('11','C','11C'),
('12','A','12A'),
('12','B','12B'),
('12','C','12C');

INSERT INTO EduStatus
VALUES
(1,'Excellent','You''re at the Top of the Mountain! and only half way up, think about it! '),
(2,'Good','You have good grades! Now aim for the Top!'),
(3,'Average','If you try more, you can be great at academics.'),
(4,'Poor','Dont worry too much, give it your best and you can definitely improve.');


6.Now the Project can be run in Visual Studio. Teacher and Student data can be added through the Principal portal(login credentials inserted in step 5) in the application.



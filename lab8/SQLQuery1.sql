use lab8db;

CREATE TABLE Airplane (
    ID INT PRIMARY KEY,
    Type NVARCHAR(50),
    Model NVARCHAR(50),
    Passenger_Seats INT,
    Year_of_Manufacture INT,
    Cargo_Capacity DECIMAL(10, 2),
    Last_Maintenance_Date DATE
);

CREATE TABLE CrewMember (
    ID INT PRIMARY KEY,
    Full_Name NVARCHAR(100),
    Position NVARCHAR(50),
    Age INT,
    Experience INT,
	Photo NVARCHAR(200),
	Airplane_ID INT,
    FOREIGN KEY (Airplane_ID) REFERENCES Airplane(ID)
);

drop table CrewMember;


INSERT INTO Airplane (ID, Type, Model, Passenger_Seats, Year_of_Manufacture, Cargo_Capacity, Last_Maintenance_Date)
VALUES (1, '������������', 'Airbus A320', 180, 2015, 5000.00, '2022-08-15');
INSERT INTO Airplane (ID, Type, Model, Passenger_Seats, Year_of_Manufacture, Cargo_Capacity, Last_Maintenance_Date)
VALUES (2, '��������', 'Boeing 747', 0, 2008, 200000.00, '2023-01-10');
INSERT INTO Airplane (ID, Type, Model, Passenger_Seats, Year_of_Manufacture, Cargo_Capacity, Last_Maintenance_Date)
VALUES (3, '�������', 'IL 45', 100, 1993, 500000.00, '2022-03-11');


INSERT INTO CrewMember (ID, Full_Name, Position, Age, Experience, Photo)
VALUES (1, '����������� ������ ����������', '�����', 35, 10, 'C:/');
INSERT INTO CrewMember (ID, Full_Name, Position, Age, Experience,Photo)
VALUES (2, '������� ������ �������������', '������', 28, 5, 'C:/');
INSERT INTO CrewMember (ID, Full_Name, Position, Age, Experience,Photo)
VALUES (3, '�������� ������ ���������', '�����', 22, 2,'C:/');



INSERT INTO Airplane_CrewMember (Airplane_ID, CrewMember_ID)
VALUES (1, 3);
INSERT INTO Airplane_CrewMember (Airplane_ID, CrewMember_ID)
VALUES (1, 2);
INSERT INTO Airplane_CrewMember (Airplane_ID, CrewMember_ID)
VALUES (2, 1);
INSERT INTO Airplane_CrewMember (Airplane_ID, CrewMember_ID)
VALUES (3, 1);


DELETE FROM Airplane;

DELETE FROM CrewMember;

DELETE FROM Airplane_CrewMember;


CREATE PROCEDURE DeleteAirplane
    @AirplaneID INT
AS
BEGIN
    BEGIN TRANSACTION;

    DELETE FROM CrewMember WHERE Airplane_ID = @AirplaneID;

    DELETE FROM Airplane WHERE ID = @AirplaneID;

    COMMIT;
END


drop PROCEDURE DeleteAirplane;
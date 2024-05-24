use lab8db;

CREATE TABLE Products (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(255) NOT NULL,
    InventoryNumber VARCHAR(255) NOT NULL,
    Size VARCHAR(255),
    Weight DECIMAL(10, 2),
    Type VARCHAR(255),
    ArrivalDate DATE,
    Quantity INT,
    Price DECIMAL(10, 2)
);
drop table Products;


CREATE TABLE Manufacturers (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Organization VARCHAR(255) NOT NULL,
    Country VARCHAR(255),
    Address VARCHAR(255),
    Phone VARCHAR(255),
	Photo VARCHAR(255),
	ProductId INT,
    FOREIGN KEY (ProductId) REFERENCES Products(Id)
);

drop table Manufacturers;




INSERT INTO Products (Name, InventoryNumber, Size, Weight, Type, ArrivalDate, Quantity, Price)
VALUES ('�������', 'INV123', '15 ������', 2.5, '�����������', '2024-04-01', 10, 1500.00);

INSERT INTO Products (Name, InventoryNumber, Size, Weight, Type, ArrivalDate, Quantity, Price)
VALUES ('��������� �������', 'INV456', '5.5 ������', 0.3, '�����������', '2024-04-05', 20, 800.00);


INSERT INTO Manufacturers (Organization, Country, Address, Phone, Photo, ProductId)
VALUES ('�������� �', '������', '������', '+7-123-456-7890', 'D:/', 1);

INSERT INTO Manufacturers (Organization, Country, Address, Phone, Photo, ProductId)
VALUES ('�������� �', '���', '���-����', '+1-987-654-3210', 'D:/', 2);


CREATE PROCEDURE DeleteProductAndManufacturer
    @InventoryNumber VARCHAR(255)
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY
        DECLARE @ProductId INT;

        -- ��������� �������������� �������� �� ������������ ������
        SELECT @ProductId = Id
        FROM Products
        WHERE InventoryNumber = @InventoryNumber;

        IF (@ProductId IS NOT NULL)
        BEGIN
            -- �������� ������ �� ������� Manufacturers
            DELETE FROM Manufacturers
            WHERE ProductId = @ProductId;

            -- �������� ������ �� ������� Products �� ��������������
            DELETE FROM Products
            WHERE Id = @ProductId;

            COMMIT;
        END
        ELSE
        BEGIN
            -- ������� � ��������� ����������� ������� �� ������
            RAISERROR ('������� � ��������� ����������� ������� �� ������.', 16, 1);
        END;
    END TRY
    BEGIN CATCH
        ROLLBACK;
        -- ��������� ������
        -- ��������, ��� ����������� �������� ��� ��������� ������ ����
    END CATCH;
END;
drop procedure DeleteProductAndManufacturer;


CREATE PROCEDURE DeleteManufacturerByOrganization
    @Organization VARCHAR(255)
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY
        DECLARE @ManufacturerId INT;

        -- ��������� �������������� ������������� �� �������� �����������
        SELECT @ManufacturerId = Id
        FROM Manufacturers
        WHERE Organization = @Organization;

        IF (@ManufacturerId IS NOT NULL)
        BEGIN
            -- �������� ������ �� ������� Manufacturers �� ��������������
            DELETE FROM Manufacturers
            WHERE Id = @ManufacturerId;

            COMMIT;
        END
        ELSE
        BEGIN
            -- ������������� � ��������� ������������ �� ������
            RAISERROR ('������������� � ��������� ������������ �� ������.', 16, 1);
        END;
    END TRY
    BEGIN CATCH
        ROLLBACK;
        -- ��������� ������
        -- ��������, ��� ����������� �������� ��� ��������� ������ ����
    END CATCH;
END;
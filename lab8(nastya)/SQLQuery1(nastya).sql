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
VALUES ('Ноутбук', 'INV123', '15 дюймов', 2.5, 'Электроника', '2024-04-01', 10, 1500.00);

INSERT INTO Products (Name, InventoryNumber, Size, Weight, Type, ArrivalDate, Quantity, Price)
VALUES ('Мобильный телефон', 'INV456', '5.5 дюймов', 0.3, 'Электроника', '2024-04-05', 20, 800.00);


INSERT INTO Manufacturers (Organization, Country, Address, Phone, Photo, ProductId)
VALUES ('Компания А', 'Россия', 'Москва', '+7-123-456-7890', 'D:/', 1);

INSERT INTO Manufacturers (Organization, Country, Address, Phone, Photo, ProductId)
VALUES ('Компания Б', 'США', 'Нью-Йорк', '+1-987-654-3210', 'D:/', 2);


CREATE PROCEDURE DeleteProductAndManufacturer
    @InventoryNumber VARCHAR(255)
AS
BEGIN
    BEGIN TRANSACTION;

    BEGIN TRY
        DECLARE @ProductId INT;

        -- Получение идентификатора продукта по инвентарному номеру
        SELECT @ProductId = Id
        FROM Products
        WHERE InventoryNumber = @InventoryNumber;

        IF (@ProductId IS NOT NULL)
        BEGIN
            -- Удаление записи из таблицы Manufacturers
            DELETE FROM Manufacturers
            WHERE ProductId = @ProductId;

            -- Удаление записи из таблицы Products по идентификатору
            DELETE FROM Products
            WHERE Id = @ProductId;

            COMMIT;
        END
        ELSE
        BEGIN
            -- Продукт с указанным инвентарным номером не найден
            RAISERROR ('Продукт с указанным инвентарным номером не найден.', 16, 1);
        END;
    END TRY
    BEGIN CATCH
        ROLLBACK;
        -- Обработка ошибки
        -- Возможно, вам потребуется добавить код обработки ошибки сюда
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

        -- Получение идентификатора производителя по названию организации
        SELECT @ManufacturerId = Id
        FROM Manufacturers
        WHERE Organization = @Organization;

        IF (@ManufacturerId IS NOT NULL)
        BEGIN
            -- Удаление записи из таблицы Manufacturers по идентификатору
            DELETE FROM Manufacturers
            WHERE Id = @ManufacturerId;

            COMMIT;
        END
        ELSE
        BEGIN
            -- Производитель с указанной организацией не найден
            RAISERROR ('Производитель с указанной организацией не найден.', 16, 1);
        END;
    END TRY
    BEGIN CATCH
        ROLLBACK;
        -- Обработка ошибки
        -- Возможно, вам потребуется добавить код обработки ошибки сюда
    END CATCH;
END;
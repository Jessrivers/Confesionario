CREATE DATABASE ConcesionariaDB;
GO

USE ConcesionariaDB;
GO

CREATE TABLE Marcas
(
    Id TINYINT IDENTITY NOT NULL PRIMARY KEY,
    Clave VARCHAR(15) NOT NULL UNIQUE,
    NombreMarca VARCHAR(100) NOT NULL
);
GO

CREATE TABLE Carros
(
    NoSerie VARCHAR(10) NOT NULL PRIMARY KEY,
    Modelo VARCHAR(100) NOT NULL,
    IdMarca TINYINT NOT NULL,
    CONSTRAINT FK_Carros_Marcas FOREIGN KEY (IdMarca) REFERENCES Marcas(Id)
);
GO

CREATE TABLE Clientes
(
    NoCliente VARCHAR(10) NOT NULL PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Apellidos VARCHAR(200) NOT NULL,
    Correo VARCHAR(150)
);
GO

CREATE TABLE Vendedores
(
    NoVendedor VARCHAR(10) NOT NULL PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Apellidos VARCHAR(200) NOT NULL,
    Correo VARCHAR(150)
);
GO

CREATE TABLE Ventas
(
    NoVenta VARCHAR(10) NOT NULL PRIMARY KEY,
    NoSerie VARCHAR(10) NOT NULL UNIQUE,
    NoCliente VARCHAR(10) NOT NULL,
    NoVendedor VARCHAR(10) NOT NULL,
    CONSTRAINT FK_Ventas_Carros FOREIGN KEY (NoSerie) REFERENCES Carros(NoSerie),
    CONSTRAINT FK_Ventas_Clientes FOREIGN KEY (NoCliente) REFERENCES Clientes(NoCliente),
    CONSTRAINT FK_Ventas_Vendedores FOREIGN KEY (NoVendedor) REFERENCES Vendedores(NoVendedor)
);
GO
INSERT INTO Marcas (Clave, NombreMarca) VALUES
('TOY', 'Toyota'),
('NIS', 'Nissan'),
('CHE', 'Chevrolet');
GO

INSERT INTO Carros (NoSerie, Modelo, IdMarca) VALUES
('CAR0000001', 'Corolla', 1),
('CAR0000002', 'Versa', 2),
('CAR0000003', 'Aveo', 3);
GO

INSERT INTO Clientes (NoCliente, Nombre, Apellidos, Correo) VALUES
('CLI0000001', 'Ana', 'Lopez', 'ana@example.com'),
('CLI0000002', 'Luis', 'Perez', 'luis@example.com');
GO

INSERT INTO Vendedores (NoVendedor, Nombre, Apellidos, Correo) VALUES
('VEN0000001', 'Maria', 'Garcia', 'maria@example.com'),
('VEN0000002', 'Jose', 'Ramirez', 'jose@example.com');
GO

INSERT INTO Ventas (NoVenta, NoSerie, NoCliente, NoVendedor) VALUES
('VTA0000001', 'CAR0000001', 'CLI0000001', 'VEN0000001');
GO
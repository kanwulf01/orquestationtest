IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'PermisosDB')
  CREATE DATABASE PermisosDB;
GO

USE PermisosDB;
GO

CREATE TABLE TipoPermiso (
  Id INT IDENTITY(1,1) PRIMARY KEY,
  Descripcion VARCHAR(500) NOT NULL
);
GO

INSERT INTO TipoPermiso (Descripcion) VALUES
  ('Vacaciones'),
  ('Licencia médica'),
  ('Permiso sin goce de salario');
GO

CREATE TABLE Permiso (
  Id INT IDENTITY(1,1) PRIMARY KEY,
  NombreEmpleado VARCHAR(100) NOT NULL,
  ApellidoEmpleado VARCHAR(100) NOT NULL,
  TipoPermiso INT NOT NULL FOREIGN KEY REFERENCES TipoPermiso(Id),
  FechaPermiso DATETIME NOT NULL
);
GO

-- Querys para la creación de la base de datos y la inserción de datos de prueba

CREATE DATABASE DI_MFSD_J_Garcia;

USE DI_MFSD_J_Garcia;

SELECT * FROM "User";
SELECT * FROM "Rol";
SELECT * FROM "Transaction";

DELETE FROM [User] Where [UserID] > 0;
DELETE FROM [Rol] Where [RolID] > 0;

SET IDENTITY_INSERT Rol ON;
INSERT INTO [Rol] ([RolID], [Name])
VALUES (1, 'Admin');
INSERT INTO [Rol] ([RolID], [Name])
VALUES (2, 'General');
SET IDENTITY_INSERT Rol OFF;

INSERT INTO [User] ([Email], [Password], [Name], [RolRefID])
VALUES ('exampleAdmin@dto.com', 'clave1234', 'ADMIN', 1);

INSERT INTO [User] ([Email], [Password], [Name], [RolRefID])
VALUES ('general@dto.com', 'clave1234', 'General', 2);


UPDATE [User]
SET [Password]= 'clave1234'
WHERE [Email] = 'exampleAdmin@dto.com';
--création de la base de données si elle n'existe pas
IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'BookingAPI')
BEGIN
    CREATE DATABASE [BookingAPI]

END
GO
    USE [BookingAPI]
GO
  USE [BookingAPI]
 --création de la table si elle n'existe pas
 --les lettres BT sont utilisées comme norme de dénomination (Booking Table)
 --les lettres btc sont utilisées comme norme de dénomination des colonnes (Booking Table Column)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='BTClients' and xtype='U')
BEGIN
    CREATE TABLE BTClients (
        btcIdClient INT PRIMARY KEY IDENTITY (1, 1),
        btcUserName VARCHAR(150),
        btcPassword varchar(500)
    )
END
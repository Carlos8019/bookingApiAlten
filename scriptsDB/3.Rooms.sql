  USE [BookingAPI]
 --création de la table si elle n'existe pas
 --les lettres BT sont utilisées comme norme de dénomination (Booking Table)
 --les lettres btc sont utilisées comme norme de dénomination des colonnes (Booking Table Column)
IF EXISTS (SELECT * FROM sysobjects WHERE name='BTRooms' and xtype='U')
BEGIN
    DROP TABLE BTRooms
END

    CREATE TABLE BTRooms (
        btrIdRoom INT PRIMARY KEY IDENTITY (1, 1),
        btrCodeRoom VARCHAR(20),
        btrDescriptionRoom VARCHAR(250),
        btrPriceRoom FLOAT
    )
    --enregistrer le donnee du room 1 qui est détaillée dans la demande
    INSERT INTO BTRooms(btrCodeRoom,btrDescriptionRoom,btrPriceRoom)
    VALUES('001','Room 001 Cancun',20)

  USE [BookingAPI]
 --création de la table si elle n'existe pas
 --les lettres BT sont utilisées comme norme de dénomination (Booking Table)
 --les lettres btc sont utilisées comme norme de dénomination des colonnes (Booking Table Column)
IF EXISTS (SELECT * FROM sysobjects WHERE name='BTReservations' and xtype='U')
BEGIN
    DROP TABLE BTReservations
END
    CREATE TABLE BTReservations (
        btrIdReservation INT PRIMARY KEY IDENTITY(1,1),
        btrIdClient INT,
        btrStartDate INT,
        btrEndDate INT,
        btrIdRoom INT,
        CONSTRAINT FK_ReservationClient FOREIGN KEY (btrIdClient)
        REFERENCES BTClients(btcIdClient),
        CONSTRAINT FK_ReservationRoom FOREIGN KEY (btrIdRoom)
        REFERENCES BTRooms(btrIdRoom)
    )
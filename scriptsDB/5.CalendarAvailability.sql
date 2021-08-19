  USE [BookingAPI]
 --création de la table si elle n'existe pas
 --les lettres BT sont utilisées comme norme de dénomination (Booking Table)
 --les lettres btc sont utilisées comme norme de dénomination des colonnes (Calendar table column)
IF EXISTS (SELECT * FROM sysobjects WHERE name='BTCalendarAvailability' and xtype='U')
BEGIN
    DROP TABLE BTCalendarAvailability
END
    CREATE TABLE BTCalendarAvailability (
        btcIdCalendar INT PRIMARY KEY IDENTITY(1,1),
        btcDateCalendar INT,
        btcStatusCalendar INT
    )
--enregistrer les dates de cette annee
DECLARE @StartDate DATE = '20210101', @EndDate DATE = '20211230'
INSERT into BTCalendarAvailability(btcDateCalendar,btcStatusCalendar)
SELECT  CONVERT(char(8),DATEADD(DAY, nbr - 1, @StartDate),112)date,1 status
FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY c.object_id ) AS Nbr
          FROM      sys.columns c
        ) nbrs
WHERE   nbr - 1 <= DATEDIFF(DAY, @StartDate, @EndDate)

USE [BookingAPI]
select * from BTCalendarAvailability
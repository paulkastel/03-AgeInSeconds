--Select all historical events from table and order them by date
--SELECT * FROM TodayInHistory ORDER BY HistoricalDate ASC

--Select all historical events with specific date
--SELECT *  FROM TodayInHistory WHERE HistoricalDate = '1925-06-06'

-----------------------------------------------------------------------------

--Delete all historical events older than 1580
--DELETE FROM TodayInHistory WHERE HistoricalDate < '1580-01-01' 

--Delete all historical events newer than 2018-01-01
--DELETE FROM TodayInHistory WHERE HistoricalDate > '2018-01-01' 

-----------------------------------------------------------------------------

--Select all birthdays events from table and order them from newest to oldest
--SELECT * FROM Birthday ORDER BY BirthdayDate DSC

-----------------------------------------------------------------------------

--Delete all birthdays older than 1580
--DELETE FROM Birthday WHERE BirthdayDate < '1580-01-01' 

--Delete all birthdays newer than 2018-01-01
--DELETE FROM Birthday WHERE BirthdayDate > '2018-01-01' 

-----------------------------------------------------------------------------

/* --Select all events with the same date
SELECT 
   TodayInHistory.HistoricalDate,
   TodayInHistory.WhatHappened,
   Birthday.BirthdayDate,
   Birthday.WhoBorn

FROM TodayInHistory 
CROSS JOIN Birthday
WHERE TodayInHistory.HistoricalDate = Birthday.BirthdayDate
*/

-----------------------------------------------------------------------------

/* --Find date in db when happenend the most both famous birthdays and events
   SELECT TodayInHistory.HistoricalDate, WhoBorn, WhatHappened FROM Birthday, TodayInHistory,
   (
   SELECT HistoricalDate, COUNT(HistoricalDate) AS NumberOfEvents FROM TodayInHistory
   GROUP BY HistoricalDate HAVING (COUNT(HistoricalDate) >1)
   ) AS H, 
   (
   SELECT BirthdayDate, COUNT(BirthdayDate) AS NumberOfBirthdays FROM Birthday
   GROUP BY BirthdayDate HAVING (COUNT(BirthdayDate) >1)
   ) AS B
   WHERE H.HistoricalDate = B.BirthdayDate AND B.BirthdayDate = Birthday.BirthdayDate AND H.HistoricalDate = TodayInHistory.HistoricalDate
*/
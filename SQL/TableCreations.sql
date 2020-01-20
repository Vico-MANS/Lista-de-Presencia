USE MALM;

CREATE TABLE PERSON (
	PERSON_ID INT IDENTITY PRIMARY KEY,
	FIRSTNAME VARCHAR(40) NOT NULL,
	LASTNAME VARCHAR(100) NOT NULL,
	BIRTHDAY DATE,
	WORKER BIT DEFAULT 0 NOT NULL
	);

CREATE TABLE PROGRAM (
	PROGRAM_ID INT IDENTITY PRIMARY KEY,
	NAME VARCHAR(50) NOT NULL--,
	--ID_EDUCATOR INT NOT NULL,
	--CONSTRAINT FK_ProgramEducator FOREIGN KEY (ID_EDUCATOR) REFERENCES PERSON(PERSON_ID)
	);

CREATE TABLE PERSON_PROGRAM ( -- not useful anymore since we assign person's to groups now (PERSON_GROUP)
	ID_PERSON INT NOT NULL,
	ID_PROGRAM INT NOT NULL,
	CONSTRAINT FK_PersonID FOREIGN KEY (ID_PERSON) REFERENCES PERSON(PERSON_ID),
	CONSTRAINT FK_ProgramID FOREIGN KEY (ID_PROGRAM) REFERENCES PROGRAM(PROGRAM_ID)
	);

CREATE TABLE PRESENCE (
	ID_PERSON INT NOT NULL,
	DIA DATE NOT NULL,
	CONSTRAINT PK_Presence PRIMARY KEY CLUSTERED (ID_PERSON, DIA),
	CONSTRAINT FK_PersonPresence FOREIGN KEY (ID_PERSON) REFERENCES PERSON(PERSON_ID)
	);

CREATE TABLE WEEKLY_PRESENCE (
	ID_PERSON INT NOT NULL,
	WEEK_DAY INT NOT NULL,
	CONSTRAINT PK_WeeklyPresence PRIMARY KEY CLUSTERED (ID_PERSON, WEEK_DAY),
	CONSTRAINT FK_PersonWeeklyPresence FOREIGN KEY (ID_PERSON) REFERENCES PERSON(PERSON_ID)
	);

CREATE TABLE SERVICIO (
	SERVICIO_ID INT IDENTITY PRIMARY KEY,
	ID_PROGRAM INT NOT NULL,
	NAME VARCHAR(30) NOT NULL,
	CONSTRAINT FK_ProgramServicio FOREIGN KEY (ID_PROGRAM) REFERENCES PROGRAM(PROGRAM_ID)
	);

CREATE TABLE GRUPO (
	GRUPO_ID INT PRIMARY KEY,
	ID_SERVICIO INT NOT NULL,
	ID_PERSON INT NOT NULL,
	NAME VARCHAR(30) NOT NULL, -- does the group have a name?
	START_DATE DATE NOT NULL,
	END_DATE DATE NOT NULL,
	CONSTRAINT FK_ServicioGrupo FOREIGN KEY (ID_SERVICIO) REFERENCES SERVICIO(SERVICIO_ID),
	CONSTRAINT FK_PersonGrupo FOREIGN KEY (ID_PERSON) REFERENCES PERSON(PERSON_ID)
	);

CREATE TABLE PERSON_GRUPO (
	ID_PERSON INT NOT NULL,
	ID_GRUPO INT NOT NULL,
	CONSTRAINT PK_PersonGrupo PRIMARY KEY CLUSTERED (ID_PERSON, ID_GRUPO),
	CONSTRAINT FK_PersonGrupo_PersonID FOREIGN KEY (ID_PERSON) REFERENCES PERSON(PERSON_ID),
	CONSTRAINT FK_PersonGrupo_GrupoID FOREIGN KEY (ID_GRUPO) REFERENCES GRUPO(GRUPO_ID)
	);

CREATE TABLE PUBLIC_HOLIDAY (
	DATE_DAY DATE PRIMARY KEY,
	HOLIDAY_NAME VARCHAR(30) NOT NULL
	);

DECLARE @start_dt as DATE = '1/1/1990';		-- Date from which the calendar table will be created.
DECLARE @end_dt as DATE = '1/1/2050';		-- Calendar table will be created up to this date (not including).

CREATE TABLE CALENDAR (
 DATE_ID DATE PRIMARY KEY,
 DATE_YEAR SMALLINT,
 DATE_MONTH TINYINT,
 DATE_DAY TINYINT,
 WEEKDAY_ID TINYINT,
 WEEKDAY_NAME VARCHAR(10),
 MONTH_NAME VARCHAR(10),
 DAY_OF_YEAR SMALLINT,
 QUARTER_ID TINYINT,
 FIRST_DAY_OF_MONTH DATE,
 LAST_DAY_OF_MONTH DATE,
 START_DTS DATETIME,
 END_DTS DATETIME
);

WHILE @start_dt < @end_dt
BEGIN
	INSERT INTO CALENDAR(
		DATE_ID, DATE_YEAR, DATE_MONTH, DATE_DAY, 
		WEEKDAY_ID, WEEKDAY_NAME, MONTH_NAME, DAY_OF_YEAR, QUARTER_ID, 
		FIRST_DAY_OF_MONTH, LAST_DAY_OF_MONTH, 
		START_DTS, END_DTS
	)	
	VALUES(
		@start_dt, YEAR(@start_dt), MONTH(@start_dt), DAY(@start_dt), 
		DATEPART(WEEKDAY, @start_dt), DATENAME(WEEKDAY, @start_dt), DATENAME(MONTH, @start_dt), DATEPART(DAYOFYEAR, @start_dt), DATEPART(QUARTER, @start_dt),
		DATEADD(DAY,-(DAY(@start_dt)-1),@start_dt), DATEADD(DAY,-(DAY(DATEADD(MONTH,1,@start_dt))),DATEADD(MONTH,1,@start_dt)), 
		CAST(@start_dt as DATETIME), DATEADD(SECOND,-1,CAST(DATEADD(DAY, 1, @start_dt) as DATETIME))
	)
	SET @start_dt = DATEADD(DAY, 1, @start_dt)
END

SELECT top 50 * FROM CALENDAR order by DATE_ID;

INSERT INTO PUBLIC_HOLIDAY VALUES('2020-01-01', 'New Year');

SELECT * FROM PUBLIC_HOLIDAY WHERE CONVERT(VARCHAR(30), DATE_DAY, 103) = '06/01/2020';

SELECT p.LASTNAME+', '+p.FIRSTNAME as 'PERSON NAME', g.GRUPO_ID as 'GROUP ID', s.NAME as 'SERVICE NAME', (SELECT FIRSTNAME+' '+LASTNAME FROM PERSON WHERE PERSON_ID = g.ID_PERSON) as EDUCATOR
FROM PERSON p, PERSON_GRUPO pg, GRUPO g, SERVICIO s 
WHERE p.PERSON_ID = 5 AND pg.ID_PERSON = p.PERSON_ID AND g.GRUPO_ID = pg.ID_GRUPO AND S.SERVICIO_ID = g.ID_SERVICIO;

SELECT CONVERT(VARCHAR, DIA, 103) AS DIA, DATEDIFF(DAY, '12/01/2019', DIA) AS WEEKDAY FROM (
                                                    SELECT DIA FROM PRESENCE
                                                    WHERE ID_PERSON = 3
                                                    AND DIA BETWEEN CONVERT(VARCHAR(30), CAST('12/01/2019' AS DATETIME), 102)
                                                    AND CONVERT(VARCHAR(30), CAST('12/31/2019' AS DATETIME), 102))
                                             AS SUB_QUERY

SELECT DATE_ID AS DATE
    FROM CALENDAR WHERE DATE_ID BETWEEN
    CONVERT(VARCHAR(30), CAST('09/01/2019' AS DATETIME), 102) AND CONVERT(VARCHAR(30), CAST('09/30/2019' AS DATETIME), 102)
    AND (WEEKDAY_ID = 7 OR WEEKDAY_ID = 1)

INSERT INTO PERSON (FIRSTNAME, LASTNAME) VALUES ('Jimmy', 'Feliber');

INSERT INTO PRESENCE (DIA, ID_PERSON) VALUES ('2015-12-17', 1);

SELECT * FROM PRESENCE WHERE DIA = convert(varchar(30), cast('09/23/2019' as datetime), 102) AND ID_PERSON = 6
IF @@ROWCOUNT = 0
    INSERT INTO PRESENCE(DIA, ID_PERSON) VALUES(convert(varchar(30), cast('09/23/2019' as datetime), 102), 6)

SELECT DIA, DATEDIFF(DAY, '09/23/2019', DIA)+1 AS WEEKDAY FROM (
SELECT DIA
FROM PRESENCE WHERE ID_PERSON = 1 AND DIA BETWEEN CONVERT(VARCHAR(30), CAST('09/23/2019' AS DATETIME), 102)
AND CONVERT(VARCHAR(30), CAST('09/29/2019' AS DATETIME), 102)) AS SUB_QUERY

DELETE FROM PRESENCE WHERE DIA = CONVERT(VARCHAR(30), CAST('09/26/2019' AS DATETIME), 102) AND ID_PERSON = 1;

SELECT * FROM PERSON;

SELECT * FROM Presence;

SELECT * FROM PERSON_PROGRAM;

SELECT * FROM WEEKLY_PRESENCE;

SELECT * FROM PROGRAM;

SELECT * FROM SERVICIO;

SELECT * FROM GRUPO;

SELECT * FROM PERSON_GRUPO;

DELETE FROM PERSON WHERE FIRSTNAME = 'JULIE'

DROP TABLE PERSON;

DROP TABLE PRESENCE;

DROP TABLE WEEKLY_PRESENCE;

DROP TABLE PROGRAM;

DROP TABLE PERSON_PROGRAM;

DROP TABLE SERVICIO;

DROP TABLE GRUPO;

DROP TABLE PUBLIC_HOLIDAY;

SELECT PERSON_ID AS ID, (FIRSTNAME + ' ' + LASTNAME) AS NAME FROM PERSON WHERE PERSON_ID IN (SELECT ID_PERSON FROM PERSON_PROGRAM WHERE ID_PROGRAM = 4);

SELECT * FROM PERSON_PROGRAM WHERE ID_PERSON = 24;

DELETE FROM PERSON;

DELETE FROM PERSON_PROGRAM;

DELETE FROM PROGRAM;

SELECT GRUPO_ID AS ID, GRUPO.NAME+' ('+CONVERT(NVARCHAR,GRUPO_ID)+')' AS NAME, START_DATE, END_DATE FROM GRUPO WHERE ID_SERVICIO IN (SELECT SERVICIO_ID FROM SERVICIO WHERE ID_PROGRAM = 1);

SELECT GRUPO_ID, NAME AS GROUP_NAME, CONVERT(VARCHAR, START_DATE, 103)+' - '+CONVERT(VARCHAR, END_DATE, 103) AS DATES,
	(SELECT NAME FROM SERVICIO WHERE SERVICIO_ID = ID_SERVICIO) AS SERVICE_NAME,
	(SELECT FIRSTNAME+' '+LASTNAME FROM PERSON WHERE PERSON_ID = ID_PERSON) AS PERSON_NAME
FROM GRUPO 
WHERE GRUPO_ID = 150001;

SELECT ID_GRUPO FROM PERSON_GRUPO WHERE ID_PERSON = 5;

SELECT NAME AS SERVICE, (SELECT NAME FROM PROGRAM WHERE PROGRAM_ID = ID_PROGRAM) AS PROGRAM FROM SERVICIO ORDER BY PROGRAM, SERVICE;

SELECT CONVERT(VARCHAR, DIA, 103) AS DIA, DATEDIFF(DAY, '12/01/2019', DIA) AS WEEKDAY FROM (
                                                    SELECT DIA FROM PRESENCE
                                                    WHERE ID_PERSON = 3
                                                    AND DIA BETWEEN CONVERT(VARCHAR(30), CAST('12/01/2019' AS DATETIME), 102)
                                                    AND CONVERT(VARCHAR(30), CAST('12/31/2019' AS DATETIME), 102))
                                             AS SUB_QUERY

SELECT GRUPO_ID AS ID, GRUPO.NAME+' ('+CONVERT(NVARCHAR,GRUPO_ID)+')' AS NAME FROM GRUPO WHERE ID_SERVICIO IN (SELECT SERVICIO_ID FROM SERVICIO WHERE ID_PROGRAM = 1)

SELECT PERSON_ID AS ID, (FIRSTNAME + ' ' + LASTNAME) AS NAME FROM PERSON WHERE WORKER != 1 AND PERSON_ID IN (SELECT ID_PERSON FROM PERSON_PROGRAM WHERE ID_PROGRAM = 2)

SELECT PERSON_ID AS ID, (FIRSTNAME + ' ' + LASTNAME) AS NAME FROM PERSON WHERE WORKER != 1 AND PERSON_ID IN 
	(SELECT ID_PERSON FROM PERSON_GRUPO WHERE ID_GRUPO IN 
		(SELECT GRUPO_ID FROM GRUPO WHERE ID_SERVICIO IN 
			(SELECT SERVICIO_ID FROM SERVICIO WHERE ID_PROGRAM = 2)))
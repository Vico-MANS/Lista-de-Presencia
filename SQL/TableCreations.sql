USE MALM;

CREATE TABLE HUMAN (
	HUMAN_ID INT IDENTITY PRIMARY KEY,
	FIRSTNAME VARCHAR(20) NOT NULL,
	LASTNAME VARCHAR(40) NOT NULL,
	BIRTHDAY DATE
	);

CREATE TABLE PRESENCE (
	ID_HUMAN INT NOT NULL,
	DIA DATE NOT NULL,
	CONSTRAINT PK_Presence PRIMARY KEY CLUSTERED (ID_HUMAN, DIA),
	CONSTRAINT FK_HumanPresence FOREIGN KEY (ID_HUMAN) REFERENCES HUMAN(HUMAN_ID)
	);

INSERT INTO HUMAN (FIRSTNAME, LASTNAME) VALUES ('Jimmy', 'Feliber');

INSERT INTO PRESENCE (DIA, ID_HUMAN) VALUES ('2015-12-17', 1);

SELECT * FROM PRESENCE WHERE DIA = convert(varchar(30), cast('09/23/2019' as datetime), 102) AND ID_HUMAN = 6
IF @@ROWCOUNT = 0
    INSERT INTO PRESENCE(DIA, ID_HUMAN) VALUES(convert(varchar(30), cast('09/23/2019' as datetime), 102), 6)

SELECT * FROM Human;

SELECT * FROM Presence;

DELETE FROM Human WHERE FIRSTNAME = 'JULIE'

DROP TABLE HUMAN;

DROP TABLE Presence;
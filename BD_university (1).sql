CREATE DATABASE UNIVERSITY

select*from teacher
select*from predmet
select*from cafedra
select*from study_process
select*from groupp
select*from audit_nagruz
select*from control_form


select DISTINCT groupp_name from groupp

select cf.homework,cf.control_work,cf.zachet,cf.exam from control_form as cf,audit_nagruz as an where cf.ID_control_form = an.control_form_id



CREATE TABLE teacher
(
   ID_teacher integer IDENTITY,
   last_name varchar (30) NOT NULL,
   first_name varchar (30) NOT NULL,
   patronymic varchar (30) NOT NULL,
   god_prepod integer NOT NULL,
   email varchar (50) NOT NULL,
   cafedra_id integer NOT NULL
)
go

ALTER TABLE teacher
	ADD CONSTRAINT XPKteacher PRIMARY KEY CLUSTERED (ID_teacher ASC)
go


INSERT INTO teacher VALUES('Макаров', 'Илья', 'Иванович', 10,'makarov@yandex.ru', 1)
INSERT INTO teacher VALUES('Иванов', 'Иван', 'Иванович', 5,'ivanov@ymail.ru', 1)
INSERT INTO teacher VALUES('Сергеев', 'Сергей', 'Сергеевич', 16,'sergeev@mail.ru', 2)
INSERT INTO teacher VALUES('Максимов', 'Максим', 'Максимович', 3,'maksimov@yandex.ru', 4)
INSERT INTO teacher VALUES('Давыдов', 'Давид', 'Давидович', 4,'davidov@yandex.ru', 3)
INSERT INTO teacher VALUES('Петров', 'Петр', 'Петрович', 20,'petrov@mail.ru', 5)
INSERT INTO teacher VALUES('Семенов', 'Семен', 'Семенович', 11,'semenov@mail.ru', 4)
INSERT INTO teacher VALUES('Данилов', 'Виктор', 'Викторович', 8,'danilov@mail.ru', 6)
INSERT INTO teacher VALUES('Владленов', 'Владлен', 'Владленович', 17,'vladlenov@yandex.ru', 1)


CREATE TABLE cafedra
(
   ID_cafedra integer IDENTITY,
   cafedra_name varchar (100) NOT NULL,
   predmet_name_id integer NOT NULL
)
go

INSERT INTO cafedra VALUES ('Кафедра вычислительной техники', 2), ('Кафедра химии и физики', 4),('Кафедра искусственного интеллекта', 6),('Кафедра философии', 1),('Кафедра безопасности программных решений', 5),('Кафедра инженерной экологии', 3)

ALTER TABLE cafedra
	ADD CONSTRAINT XPKcafedra PRIMARY KEY CLUSTERED (ID_cafedra ASC)
go

CREATE TABLE predmet
(
	ID_predmet integer IDENTITY,
	predmet_name varchar (50) NOT NULL
)
go

ALTER TABLE predmet
	ADD CONSTRAINT XPKpredmet PRIMARY KEY CLUSTERED (ID_predmet ASC)
go

INSERT INTO predmet VALUES('Философия'),('Информатика'),('Экология'),('Химия'),('Компьютерная безопасность'),('Технологии искусственного интеллекта')

CREATE TABLE study_process
(
   ID_study_process integer IDENTITY,
   teacher_id integer NOT NULL,
   groupp_id integer NOT NULL,
   audit_nagruz_id integer NOT NULL
)
go

ALTER TABLE study_process
	ADD CONSTRAINT XPKstudy_process PRIMARY KEY CLUSTERED (ID_study_process ASC)
go

INSERT INTO study_process VALUES(3,2,4),(2,1,3),(5,3,2),(6,5,6),(4,6,1),(1,4,5)

CREATE TABLE groupp
(
   ID_groupp integer IDENTITY,
   groupp_name varchar (50) NOT NULL
)
go

ALTER TABLE groupp
	ADD CONSTRAINT XPKgroupp PRIMARY KEY CLUSTERED (ID_groupp ASC)
go

INSERT INTO groupp VALUES('БСБО-01-21'),('БИСО-03-19'), ('БСБО-13-20'), ('БИСО-05-21'), ('БИСО-06-21'),('БСБО-19-20') 


CREATE TABLE audit_nagruz
(
   ID_audit_nagruz integer IDENTITY,
   time_kolvo integer NOT NULL,
   control_form_id integer NOT NULL
)
go

ALTER TABLE audit_nagruz
	ADD CONSTRAINT XPKaudit_nagruz PRIMARY KEY CLUSTERED (ID_audit_nagruz ASC)
go

INSERT INTO audit_nagruz VALUES(108,3), (104,2), (144,1), (110,4), (72,6), (107,5)

CREATE TABLE control_form
(
   ID_control_form integer IDENTITY,
   homework bit,
   control_work bit ,
   zachet bit,
   exam bit 
)
go

INSERT INTO control_form VALUES(1,1,1,1), (0,0,1,0), (0,0,1,1), (1,0,1,1), (0,1,1,1), (1,1,1,0)

ALTER TABLE control_form
	ADD CONSTRAINT XPKcontrol_form PRIMARY KEY CLUSTERED (ID_control_form ASC)
go


ALTER TABLE cafedra
	ADD CONSTRAINT  R_pred_name FOREIGN KEY (predmet_name_id) REFERENCES predmet (ID_predmet)
		ON DELETE CASCADE
		ON UPDATE CASCADE
go


ALTER TABLE teacher
	ADD CONSTRAINT  R_cafedra FOREIGN KEY (cafedra_id) REFERENCES cafedra(ID_cafedra)
		ON DELETE CASCADE
		ON UPDATE CASCADE
go
ALTER TABLE study_process
	ADD CONSTRAINT  R_teacher FOREIGN KEY (teacher_id) REFERENCES teacher(ID_teacher)
		ON DELETE CASCADE
		ON UPDATE CASCADE
go

ALTER TABLE study_process
	ADD CONSTRAINT  R_groupp FOREIGN KEY (groupp_id) REFERENCES groupp(ID_groupp)
		ON DELETE CASCADE
		ON UPDATE CASCADE
go


ALTER TABLE study_process
	ADD CONSTRAINT  R_audit_nagruz FOREIGN KEY (audit_nagruz_id) REFERENCES audit_nagruz(ID_audit_nagruz)
		ON DELETE CASCADE
		ON UPDATE CASCADE
go


ALTER TABLE audit_nagruz
	ADD CONSTRAINT  R_control_form FOREIGN KEY (control_form_id) REFERENCES control_form(ID_control_form)
		ON DELETE CASCADE
		ON UPDATE CASCADE
go



---ИНДЕКСЫ----

----Будет использоваться при выборке из teacher преподавателей, с опытом преподавания больше 10 -----

CREATE NONCLUSTERED INDEX Idx_teacher_god_prepod
ON teacher (last_name)
WHERE god_prepod > 10
GO

---Таблица person составной индекс по полям (age, adrress) – составной, не уникальный, некластеризованный -----

CREATE NONCLUSTERED INDEX Idx_teacher_cafedra_last_name
ON teacher (cafedra_id ASC, last_name ASC)
GO



---ЗАПРОСЫ----

---подзапрос from (вывести ФИО преподавателя с опытом преподавания больше 5 лет)----

SELECT t1.last_name AS 'Фамилия',t1.first_name AS 'Имя',t1.patronymic AS 'Отчество'
FROM (SELECT t.last_name, t.first_name, t.patronymic FROM teacher t WHERE t.god_prepod > 5)t1

----подзапрос select (вывести ФИО преподавателя, у которого опыт 10 лет и который преподает  на кафедре вычислительной техники)----

SELECT t.last_name AS 'Фамилия',t.first_name AS 'Имя',t.patronymic AS 'Отчество',
(SELECT cafedra_name  FROM cafedra WHERE cafedra_name = 'Кафедра вычислительной техники') AS 'Кафедра'
FROM teacher t INNER JOIN cafedra c ON t.cafedra_id = c.ID_cafedra 
WHERE god_prepod = 10

---подзапрос where (вывести название кафедры, на которой преподается предмет-Компьютерная безопасность ) ----

SELECT c.cafedra_name AS ' Название кафедры' FROM cafedra c INNER JOIN predmet p ON p.ID_predmet = c.predmet_name_id
WHERE EXISTS (SELECT p.predmet_name  FROM predmet WHERE p.predmet_name = 'Компьютерная безопасность')

---select коррелированный (вывести один из видов аудиторной нагрузки, кол-во преподаваемых часов и форму контроля, связанной с д\з)----

SELECT a.ID_audit_nagruz  AS 'Тип аудиторной нагрузки', a.time_kolvo  AS 'Кол-во часов',
(
	SELECT c.homework FROM control_form c WHERE a.control_form_id = c.ID_control_form
)  AS 'Домашняя работа'
FROM audit_nagruz a

---where коррелированный (вывести кафедру, на которой преподает преподаватель с почтой sergeev@mail.ru)----

SELECT c.cafedra_name AS 'Название кафедры' 
FROM cafedra c
WHERE EXISTS (SELECT email FROM teacher t WHERE c.ID_cafedra = t.cafedra_id  AND t.email = 'sergeev@mail.ru')

---where коррелированный ----

SELECT c.cafedra_name AS 'Название кафедры'
FROM cafedra c 
WHERE EXISTS (SELECT p.predmet_name FROM predmet p WHERE p.ID_predmet = c.predmet_name_id AND (p.predmet_name = 'Информатика'  OR  p.predmet_name = 'Компьютерная безопасность'))

---многотабличный having (вывести фамилию и название кафедры преподавателя с максимальным опытом, при условии, что он должен быть меньше 10)----

SELECT t.last_name AS 'Фамилия преподавателя', c.cafedra_name AS 'Кафедра',max(t.god_prepod) AS 'Опыт преподавания (макс)'
FROM teacher t  INNER JOIN cafedra c ON t.cafedra_id = c.ID_cafedra
GROUP BY t.last_name, c.cafedra_name
HAVING max(t.god_prepod)<10

---запрос с case выражением (вывести название кафедры с 3 номером, на которой преподается предмет Технологии искусственного интеллекта ) ---


SELECT c.cafedra_name AS 'Название кафедры',
CASE
    WHEN c.ID_cafedra = 3 THEN
    (
        SELECT predmet_name
        FROM predmet p INNER JOIN cafedra c  ON p.ID_predmet = c.predmet_name_id
        WHERE (predmet_name = 'Технологии искусственного интеллекта')
    )
     WHEN c.ID_cafedra <> 3 THEN 'Кафедра не указана' WHEN c.ID_cafedra IS NULL THEN '0'
     END 'Дисциплина'
  FROM cafedra c INNER JOIN predmet p  ON c.predmet_name_id = p.ID_predmet



----any, some или all (вывести информацию о преподавателях, работающих на 4 кафедре, опыт преподавания которых больше, чем опыт любого преподавателя с 6 кафедры)  ----

SELECT last_name AS 'Фамилия', first_name AS 'Имя', patronymic AS 'Отчество', 
email AS 'Почта' FROM teacher WHERE cafedra_id = 4 AND god_prepod > ANY (SELECT god_prepod FROM teacher WHERE cafedra_id = 6)


----РОЛИ----

CREATE LOGIN student WITH PASSWORD='student'
go
CREATE USER user1 for login student
go

CREATE ROLE Users
go
ALTER ROLE Users ADD MEMBER user1
go

GRANT SELECT ON teacher TO Users
go
GRANT SELECT ON predmet TO Users
go
GRANT SELECT ON cafedra TO Users 
go
GRANT SELECT ON study_process TO Users 
go
GRANT SELECT ON groupp TO Users
go
GRANT SELECT ON audit_nagruz TO Users
go
GRANT SELECT ON control_form TO Users 
go
GRANT EXECUTE TO Users
go
 --------------------------------
CREATE LOGIN teacher WITH PASSWORD = 'teacher'
go
CREATE USER admin1 for login prepod
go

CREATE ROLE Administrators
go
ALTER ROLE Administrators ADD MEMBER admin1
go

GRANT SELECT ON teacher TO Administrators
go
GRANT SELECT ON predmet TO Administrators
go
GRANT SELECT ON cafedra TO Administrators 
go
GRANT SELECT ON study_process TO Administrators 
go
GRANT SELECT ON groupp TO Administrators
go
GRANT SELECT ON audit_nagruz TO Administrators
go
GRANT SELECT ON control_form TO Administrators 
go
GRANT INSERT ON teacher TO Administrators
go
GRANT INSERT ON predmet TO Administrators
go
GRANT INSERT ON cafedra TO Administrators
go
GRANT INSERT ON study_process TO Administrators
go
GRANT INSERT ON groupp TO Administrators
go
GRANT INSERT ON audit_nagruz TO Administrators
go
GRANT INSERT ON control_form TO Administrators
go
GRANT UPDATE ON teacher TO Administrators
go
GRANT UPDATE ON predmet TO Administrators
go
GRANT UPDATE ON cafedra TO Administrators
go
GRANT UPDATE ON study_process TO Administrators
go
GRANT UPDATE ON groupp TO Administrators
go
GRANT UPDATE ON audit_nagruz TO Administrators
go
GRANT UPDATE ON control_form TO Administrators
go
GRANT DELETE ON teacher TO Administrators
go
GRANT DELETE ON predmet TO Administrators
go
GRANT DELETE ON cafedra TO Administrators
go
GRANT DELETE ON study_process TO Administrators
go
GRANT DELETE ON groupp TO Administrators
go
GRANT DELETE ON audit_nagruz TO Administrators
go
GRANT DELETE ON control_form TO Administrators
go
GRANT SELECT ON VekFunk TO Administrators
go
GRANT EXECUTE TO Administrators
go


------ХРАНИМЫЕ ПРОЦЕДУРЫ-----


----Хранимые процедуры для teacher ----

CREATE PROCEDURE Insertteacher(@last_name varchar (30), @first_name varchar (30), @patronymic varchar (30), @god_prepod int, @email varchar (50), @cafedra_id int)
AS
BEGIN
INSERT INTO teacher (last_name, first_name, patronymic, god_prepod, email, cafedra_id)
VALUES (@last_name, @first_name, @patronymic, @god_prepod, @email, @cafedra_id)
END

CREATE PROCEDURE Updateteacher (@ID int, @last_name varchar (30), @first_name varchar (30), @patronymic varchar (30), @god_prepod int, @email varchar (50), @cafedra_id int )
AS
BEGIN
UPDATE  teacher
SET last_name = @last_name,
first_name = @first_name,
patronymic = @patronymic,
god_prepod = @god_prepod,
email = @email,
cafedra_id = @cafedra_id
WHERE ID_teacher = @ID
END

CREATE PROCEDURE Deleteteacher (@ID int)
AS
BEGIN
DELETE FROM teacher WHERE ID_teacher = @ID
END



----Хранимые процедуры для predmet ---

CREATE PROCEDURE Insertpredmet(@predmet_name varchar (50))
AS
BEGIN
INSERT INTO predmet(predmet_name)
VALUES (@predmet_name)
END

CREATE PROCEDURE Updatepredmet(@ID int,@predmet_name varchar (50))
AS
BEGIN
UPDATE  predmet
SET predmet_name = @predmet_name
WHERE ID_predmet = @ID
END

CREATE PROCEDURE Deletepredmet (@ID int)
AS
BEGIN
DELETE FROM predmet WHERE ID_predmet = @ID
END


----Хранимые процедуры для cafedra ---

CREATE PROCEDURE Insertcafedra(@cafedra_name varchar (100), @predmet_name_id int)
AS
BEGIN
INSERT INTO cafedra (cafedra_name, predmet_name_id)
VALUES (@cafedra_name, @predmet_name_id)
END

CREATE PROCEDURE Updatecafedra(@ID int,@cafedra_name varchar (100), @predmet_name_id int)
AS
BEGIN
UPDATE  cafedra
SET cafedra_name = @cafedra_name,
predmet_name_id = @predmet_name_id
WHERE ID_cafedra = @ID
END

CREATE PROCEDURE Deletecafedra (@ID int)
AS
BEGIN
DELETE FROM cafedra WHERE ID_cafedra = @ID
END


----Хранимые процедуры для study process ---

CREATE PROCEDURE Insertstudy_process(@teacher_id int, @groupp_id int, @audit_nagruz_id int)
AS
BEGIN
INSERT INTO study_process(teacher_id, groupp_id,audit_nagruz_id)
VALUES (@teacher_id, @groupp_id, @audit_nagruz_id)
END

CREATE PROCEDURE Updatestudy_process(@ID int,@teacher_id int, @groupp_id int, @audit_nagruz_id int)
AS
BEGIN
UPDATE  study_process
SET teacher_id = @teacher_id,
groupp_id = @groupp_id,
audit_nagruz_id = @audit_nagruz_id
WHERE ID_study_process = @ID
END

CREATE PROCEDURE Deletestudy_process (@ID int)
AS
BEGIN
DELETE FROM study_process WHERE ID_study_process = @ID
END

----Хранимые процедуры для groupp ---

CREATE PROCEDURE Insertgroupp(@groupp_name varchar (50))
AS
BEGIN
INSERT INTO groupp (groupp_name)
VALUES (@groupp_name)
END

CREATE PROCEDURE Updategroupp(@ID int,@groupp_name varchar (50))
AS
BEGIN
UPDATE  groupp
SET groupp_name = @groupp_name
WHERE ID_groupp = @ID
END

CREATE PROCEDURE Deletegroupp (@ID int)
AS
BEGIN
DELETE FROM groupp WHERE ID_groupp = @ID
END

----Хранимые процедуры для audit_nagruz ---

CREATE PROCEDURE Insertaudit_nagruz(@time_kolvo int, @control_form_id int)
AS
BEGIN
INSERT INTO audit_nagruz (time_kolvo, control_form_id)
VALUES (@time_kolvo, @control_form_id)
END

CREATE PROCEDURE Updateaudit_nagruz(@ID int, @time_kolvo int, @control_form_id int)
AS
BEGIN
UPDATE audit_nagruz
SET time_kolvo = @time_kolvo,
control_form_id = @control_form_id
WHERE ID_audit_nagruz= @ID
END

CREATE PROCEDURE Deleteaudit_nagruz (@ID int)
AS
BEGIN
DELETE FROM audit_nagruz WHERE ID_audit_nagruz = @ID
END


----Хранимые процедуры для смены ---

CREATE PROCEDURE Insertcontrol_form(@homework bit, @control_work bit, @zachet bit, @exam bit)
AS
BEGIN
INSERT INTO control_form (homework, control_work, zachet, exam)
VALUES (@homework, @control_work, @zachet, @exam)
END

CREATE PROCEDURE Updatecontrol_form(@ID int,@homework bit, @control_work bit, @zachet bit, @exam bit)
AS
BEGIN
UPDATE control_form
SET homework = @homework,
control_work = @control_work,
zachet = @zachet,
exam = @exam
WHERE ID_control_form = @ID
END

CREATE PROCEDURE Deletecontrol_form (@ID int)
AS
BEGIN
DELETE FROM control_form WHERE ID_control_form = @ID
END


---Отдельная хранимая процедура---

BEGIN TRANSACTION
DELETE FROM audit_nagruz WHERE time_kolvo <50
ROLLBACK
COMMIT TRANSACTION



----КУРСОР-----

CREATE PROCEDURE Kursor(@ID_teacher int, @last_name varchar (30), @first_name varchar (30), @patronymic varchar (30), @god_prepod int, @email varchar (50), @cafedra_id int)
AS
DECLARE crs cursor FOR SELECT ID_teacher, last_name, first_name, patronymic, god_prepod, email, cafedra_id FROM teacher
DECLARE @ID int DECLARE @last_name_t varchar (30)
DECLARE @first_name_t varchar (30)DECLARE @patronymic_t varchar (30)
DECLARE @god_prepod_t int DECLARE @email_t varchar (50)
DECLARE @cafedra_id_t int

BEGIN OPEN crs 
  FETCH crs INTO @ID, @last_name_t, @first_name_t , @patronymic_t, @god_prepod_t, @email_t, @cafedra_id_t  IF(@ID = @ID_teacher) 
  BEGIN   UPDATE teacher SET last_name = @last_name,
   first_name = @first_name, patronymic = @patronymic, god_prepod = @god_prepod, email = @email, cafedra_id = @cafedra_id WHERE current of crs  END
END

-------СКАЛЯРНАЯ И ВЕКТОРНАЯ ФУНКЦИИ-----

---Векторная функция----
DROP FUNCTION VekFunk

CREATE FUNCTION VekFunk (@god_prepod int) RETURNS TABLE 
AS RETURN 
(  SELECT DISTINCT * FROM teacher where god_prepod < @god_prepod
 ) 

 select DISTINCT * from VekFunk(10)

---Скалярная функция -----

CREATE FUNCTION Scalar_Funk (@scalfunk varchar(100))RETURNS TABLE
AS
RETURN(
 SELECT t.last_name AS 'Фамилия',t.first_name AS 'Имя', t.patronymic AS 'Отчество', t.god_prepod AS 'Опыт преподавания', t.email AS 'Эл. почта', c.cafedra_name AS 'Кафедра',p.predmet_name AS 'Предмет' FROM teacher t INNER JOIN cafedra c ON t.cafedra_id = c.ID_cafedra
INNER JOIN predmet p ON p.ID_predmet = c.predmet_name_id WHERE last_name = @scalfunk
)

select DISTINCT * from Scalar_Funk('Петров')

------ТРИГГЕРЫ ------

--Триггер на удаление кафедр, за которыми не закреплены преподаватели

CREATE TRIGGER OnGrouppDeleted
ON teacher
AFTER DELETE AS
BEGIN
DELETE FROM
        cafedra 
    WHERE
        NOT EXISTS
        (
            SELECT * FROM
               teacher t
            WHERE
                t.cafedra_id = cafedra.ID_cafedra
        )
END



----VIEW-----
drop view TeachView

select * from TeachView 
create view TeachView
 (predmet_name,cafedra_name)  as select p.predmet_name, c.cafedra_name from cafedra as c , predmet as p where p.ID_predmet = c.predmet_name_id

create trigger TeachTrig
 on TeachView
instead of insert
as
begin
DECLARE @predmet_name varchar(40) 
DECLARE @cafedra_name varchar(40)

DECLARE Curso cursor for (select * from inserted) 
OPEN Curso
Fetch next from Curso into @predmet_name,@cafedra_name
While @@FETCH_STATUS = 0
begin
If @predmet_name not in(select predmet_name from predmet)
BEGIN
insert into predmet(predmet_name) values (@predmet_name)
END
DECLARE @pred_ID int = (select ID_predmet from predmet where predmet.predmet_name = @predmet_name)
IF (SELECT COUNT(*) FROM cafedra WHERE cafedra_name = @cafedra_name) = 0
        INSERT INTO cafedra(cafedra_name, predmet_name_id) VALUES(@cafedra_name, @pred_ID)
Fetch next from Curso into @predmet_name,@cafedra_name
end
Close Curso DEALLOCATE Curso
end

insert into TeachView values ('Биохимия','Кафедра биологии')

---------------------------------------------------------
select pr.ID_predmet,pr.predmet_name,c.ID_cafedra,c.cafedra_name,c.predmet_name_id,t.ID_teacher,t.last_name,t.first_name,t.patronymic,t.god_prepod,t.email,t.cafedra_id, g.ID_groupp,g.groupp_name,cf.ID_control_form,cf.homework,cf.control_work,cf.zachet,cf.exam,an.ID_audit_nagruz,an.time_kolvo,an.control_form_id,sp.ID_study_process,sp.teacher_id,sp.groupp_id,sp.audit_nagruz_id from predmet as pr,cafedra as c,teacher as t,groupp as g,control_form as cf,audit_nagruz as an,study_process as sp where pr.ID_predmet = c.predmet_name_id and c.ID_cafedra = t.cafedra_id and t.ID_teacher = sp.teacher_id and g.ID_groupp = sp.groupp_id and cf.ID_control_form = an.control_form_id and an.ID_audit_nagruz = sp.audit_nagruz_id
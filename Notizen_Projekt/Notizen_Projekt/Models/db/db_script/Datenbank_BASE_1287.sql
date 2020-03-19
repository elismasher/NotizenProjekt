create database if not exists db_NotizenProjekt collate utf8mb4_general_ci;
use db_NotizenProjekt;

create table usersNotizenProjekt(
	id int not null auto_increment,
    firstname varchar(100) null,
    lastname varchar(100) not null,
    email varchar(100) not null,
    gender int not null,
    username varchar(100) not null unique,
    password varchar(128) not null,
    
    constraint id_PK primary key(id)
)engine=InnoDB;


-- Die Notizen werden hier alle in einer Tabelle abgespeichert. N:M beziehung mit User
CREATE TABLE notesNotizenProjekt(
	id int NOT NULL auto_increment,
	noteTitle varchar(100),
    noteText varchar(200) NOT NULL,
    dateWritten DATETIME,
    dateLastEdit DATETIME,
    statusNote int,
    colourNote int,
    
    constraint id_PK primary key(id)
)engine=InnoDB;

CREATE TABLE userNote(
	idUser int NOT NULL,
    idNote int NOT NULL,
    
    constraint id_PK primary key(idUser, idNote),
    constraint idUser_fk foreign key(idUser) references usersNotizenProjekt(id),
    constraint idNote_fk foreign key(idNote) references notesNotizenProjekt(id)
)engine=InnoDB;


-- Die Tags werden hier alle in einer Tabelle abgespeichert. N:M beziehung mit Notes
CREATE TABLE tagsNotizenProjekt(
	id int NOT NULL auto_increment,
    tagName varchar(200),
    
    constraint id_PK primary key(id)
)engine=InnoDB;

CREATE TABLE userTag(
	idNote int NOT NULL,
    idTag int NOT NULL,
    
    constraint id_PK primary key(idNote, idTag),
    constraint idNote2_fk foreign key(idNote) references notesNotizenProjekt(id),
    constraint idTag_fk foreign key(idTag) references tagsNotizenProjekt(id)
)engine=InnoDB;





INSERT INTO usersNotizenProjekt VALUES(null, "Elias", "Rist", "elias.rist@nms2.at", 0, "Elismasher", sha2("Passwort1234", 256));
INSERT INTO usersNotizenProjekt VALUES(null, "Lukas", "Heber", "heber.lukas@gmail.at", 0, "Lukas", sha2("Passwort1234", 256));
INSERT INTO usersNotizenProjekt VALUES(null, "Test", "nachname", "test@test.at", 0, "Test", sha2("Test1234", 256));

select * from usersNotizenProjekt;
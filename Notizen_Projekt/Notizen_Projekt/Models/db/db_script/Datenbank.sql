create database if not exists db_NotizenProjekt collate utf8mb4_general_ci;
use db_NotizenProjekt;

create table users(
	id int not null auto_increment,
    firstname varchar(100) not null,
    lastname varchar(100) not null,
    email varchar(100) not null,
    gender int not null,
    username varchar(100) not null unique,
    password varchar(128) not null,
    
    constraint id_PK primary key(id)
)engine=InnoDB;


-- Die Notizen werden hier alle in einer Tabelle abgespeichert. N:M beziehung mit User
CREATE TABLE notes(
	id int NOT NULL auto_increment,
	noteTitle varchar(100),
    noteText varchar(1500) NOT NULL,
    dateWritten DATETIME default NOW(),
    dateLastEdit DATETIME,
    statusNote int,
    colourNote varchar(20),
    idUser int NOT NULL,
    
    constraint id_PK primary key(id),
    constraint idUser_fk foreign key(idUser) references users(id) on delete cascade on update cascade
)engine=InnoDB;

-- Die Tags werden hier alle in einer Tabelle abgespeichert. N:M beziehung mit Notes
CREATE TABLE tags(
	id int NOT NULL auto_increment,
    tagName varchar(200),
    userId int NOT NULL,
    
    constraint id_PK primary key(id),
    constraint userId_fk foreign key(userId) references users(id) on delete cascade on update cascade
)engine=InnoDB;

CREATE TABLE NM_noteTag(
	idNote int NOT NULL,
    idTag int NOT NULL,
    
    constraint id_PK primary key(idNote, idTag),
    constraint idNote2_fk foreign key(idNote) references notes(id) on delete cascade on update cascade,
    constraint idTag_fk foreign key(idTag) references tags(id)
)engine=InnoDB;


INSERT INTO users VALUES(null, "Elias", "Rist", "elias.rist@nms2.at", 0, "Elismasher", sha2("Klexi2408", 256));
INSERT INTO users VALUES(null, "Lukas", "Heber", "heber.lukas@gmail.at", 0, "Lukas", sha2("Passwort1234", 256));
INSERT INTO users VALUES(null, "Test", "nachname", "test@test.at", 0, "Test", sha2("Test1234", 256));


-- UPDATE users SET password = sha2("neues Passwort", 256) WHERE id = 1;

INSERT INTO notes(noteTitle, noteText, dateLastEdit, statusNote, colourNote, idUser) VALUES("Hi du Ei!", "!Ei du Hi", now(), 0, 2, 3);
INSERT INTO tags VALUES(null, "Hondululu", 1);
INSERT INTO tags VALUES(null, "Blah", 1);

INSERT INTO tags VALUES(null, "Blah", 2);

INSERT INTO NM_noteTag VALUES(1, 1);

select * from NM_noteTag;

select * from users;
select * from notes;
select * from tags;

SELECT * FROM notes n JOIN NM_noteTag nT ON n.id = nT.idNote JOIN tags t ON nT.idTag = t.id WHERE n.idUser = 3;

SELECT * FROM tags t LEFT JOIN NM_noteTag nT ON t.id = nT.idTag WHERE nT.idNote = 1;


-
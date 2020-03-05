create database if not exists db_daten collate utf8mb4_general_ci;
use db_daten;

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

INSERT INTO usersNotizenProjekt VALUES(null, "David", "Holzhammer", "holzi@gmail.com", 0, "holzi2", sha2("Passwort1234", 256));

select * from usersNotizenProjekt;
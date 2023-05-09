use aero
go

create table Register(
id_user int identity(1,1) not null,
login_user varchar(50) not null,
password_user varchar(50) not null);

insert into register (login_user, password_user) values ('admin', 'admin')

select* from register
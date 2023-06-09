create database aero
go
USE aero
GO
PRINT N'Recreating the objects for the database'
--Drop all FKs in the database
declare @table_name sysname, @constraint_name sysname
declare i cursor static for 
select c.table_name, a.constraint_name
from INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS a join INFORMATION_SCHEMA.KEY_COLUMN_USAGE b
on a.unique_constraint_name=b.constraint_name join INFORMATION_SCHEMA.KEY_COLUMN_USAGE c
on a.constraint_name=c.constraint_name
WHERE upper(c.table_name) in (upper('Company'),upper('Pass_in_trip'),upper('Passenger'),upper('Trip'))
open i
fetch next from i into @table_name,@constraint_name
while @@fetch_status=0
begin
	exec('ALTER TABLE '+@table_name+' DROP CONSTRAINT '+@constraint_name)
	fetch next from i into @table_name,@constraint_name
end
close i
deallocate i
GO
--Drop all tables
declare @object_name sysname, @sql varchar(8000)
declare i cursor static for 
SELECT table_name from INFORMATION_SCHEMA.TABLES
where upper(table_name) in (upper('Company'),upper('Pass_in_trip'),upper('Passenger'),upper('Trip'))

open i
fetch next from i into @object_name
while @@fetch_status=0
begin
	set @sql='DROP TABLE [dbo].['+@object_name+']'
	exec(@sql)
	fetch next from i into @object_name
end
close i
deallocate i
GO

CREATE TABLE [dbo].[Company] (
	[ID_comp] [int] identity(1,1) NOT NULL ,
	[name] [char] (10) NOT NULL 
)
GO

CREATE TABLE [dbo].[Pass_in_trip] (
	[trip_no] [int] NOT NULL ,
	[date] [datetime] NOT NULL ,
	[ID_psg] [int] NOT NULL ,
	[place] [char] (10) NOT NULL 
)
GO

CREATE TABLE [dbo].[Passenger] (
	[ID_psg] [int] identity(1,1) NOT NULL ,
	[name] [char] (20) NOT NULL 
)
GO

CREATE TABLE [dbo].[Trip] (
	[trip_no] [int] NOT NULL ,
	[ID_comp] [int] NOT NULL ,
	[plane] [char] (10) NOT NULL ,
	[town_from] [char] (25) NOT NULL ,
	[town_to] [char] (25) NOT NULL ,
	[time_out] [datetime] NOT NULL ,
	[time_in] [datetime] NOT NULL 
)
GO

ALTER TABLE [dbo].[Company] WITH NOCHECK ADD 
	CONSTRAINT [PK2] PRIMARY KEY  CLUSTERED 
	(
		[ID_comp] 
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Pass_in_trip] WITH NOCHECK ADD 
	CONSTRAINT [PK_pt] PRIMARY KEY  CLUSTERED 
	(
		[trip_no],
		[date],
		[ID_psg]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Passenger] WITH NOCHECK ADD 
	CONSTRAINT [PK_psg] PRIMARY KEY  CLUSTERED 
	(
		[ID_psg]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Trip] WITH NOCHECK ADD 
	CONSTRAINT [PK_t] PRIMARY KEY  CLUSTERED 
	(
		[trip_no]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Pass_in_trip] ADD 
	CONSTRAINT [FK_Pass_in_trip_Passenger] FOREIGN KEY 
	(
		[ID_psg]
	) REFERENCES [dbo].[Passenger] (
		[ID_psg]
	),
	CONSTRAINT [FK_Pass_in_trip_Trip] FOREIGN KEY 
	(
		[trip_no]
	) REFERENCES [dbo].[Trip] (
		[trip_no]
	)
GO

ALTER TABLE [dbo].[Trip] ADD 
	CONSTRAINT [FK_Trip_Company] FOREIGN KEY 
	(
		[ID_comp]
	) REFERENCES [dbo].[Company] (
		[ID_comp]
	)
GO
                                                                                                                                                                                                                                                                 
----Company------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ 
insert into Company values('Don_avia  ')
insert into Company values('Aeroflot  ')
insert into Company values('Dale_avia ')
insert into Company values('air_France')
insert into Company values('British_AW')
GO

                                                                                                                                                                                                                                                                 
----Passenger------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ 
insert into Passenger values('Bruce Willis        ')
insert into Passenger values('George Clooney      ')
insert into Passenger values('Kevin Costner       ')
insert into Passenger values('Donald Sutherland   ')
insert into Passenger values('Jennifer Lopez      ')
insert into Passenger values('Ray Liotta          ')
insert into Passenger values('Samuel L. Jackson   ')
insert into Passenger values('Nikole Kidman       ')
insert into Passenger values('Alan Rickman        ')
insert into Passenger values('Kurt Russell        ')
insert into Passenger values('Harrison Ford       ')
insert into Passenger values('Russell Crowe       ')
insert into Passenger values('Steve Martin        ')
insert into Passenger values('Michael Caine       ')
insert into Passenger values('Angelina Jolie      ')
insert into Passenger values('Mel Gibson          ')
insert into Passenger values('Michael Douglas     ')
insert into Passenger values('John Travolta       ')
insert into Passenger values('Sylvester Stallone  ')
insert into Passenger values('Tommy Lee Jones     ')
insert into Passenger values('Catherine Zeta-Jones')
insert into Passenger values('Antonio Banderas    ')
insert into Passenger values('Kim Basinger        ')
insert into Passenger values('Sam Neill           ')
insert into Passenger values('Gary Oldman         ')
insert into Passenger values('Clint Eastwood      ')
insert into Passenger values('Brad Pitt           ')
insert into Passenger values('Johnny Depp         ')
insert into Passenger values('Pierce Brosnan      ')
insert into Passenger values('Sean Connery        ')
insert into Passenger values('Bruce Willis        ')
insert into Passenger values('Mullah Omar         ')

GO

                                                                                                                                                                                                                                                                 
----Trip------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ 
insert into Trip values(1100,4,'Boeing    ','Rostov                   ','Paris                    ','19000101 14:30:00.000','19000101 17:50:00.000')
insert into Trip values(1101,4,'Boeing    ','Paris                    ','Rostov                   ','19000101 08:12:00.000','19000101 11:45:00.000')
insert into Trip values(1123,3,'TU-154    ','Rostov                   ','Vladivostok              ','19000101 16:20:00.000','19000101 03:40:00.000')
insert into Trip values(1124,3,'TU-154    ','Vladivostok              ','Rostov                   ','19000101 09:00:00.000','19000101 19:50:00.000')
insert into Trip values(1145,2,'IL-86     ','Moscow                   ','Rostov                   ','19000101 09:35:00.000','19000101 11:23:00.000')
insert into Trip values(1146,2,'IL-86     ','Rostov                   ','Moscow                   ','19000101 17:55:00.000','19000101 20:01:00.000')
insert into Trip values(1181,1,'TU-134    ','Rostov                   ','Moscow                   ','19000101 06:12:00.000','19000101 08:01:00.000')
insert into Trip values(1182,1,'TU-134    ','Moscow                   ','Rostov                   ','19000101 12:35:00.000','19000101 14:30:00.000')
insert into Trip values(1187,1,'TU-134    ','Rostov                   ','Moscow                   ','19000101 15:42:00.000','19000101 17:39:00.000')
insert into Trip values(1188,1,'TU-134    ','Moscow                   ','Rostov                   ','19000101 22:50:00.000','19000101 00:48:00.000')
insert into Trip values(1195,1,'TU-154    ','Rostov                   ','Moscow                   ','19000101 23:30:00.000','19000101 01:11:00.000')
insert into Trip values(1196,1,'TU-154    ','Moscow                   ','Rostov                   ','19000101 04:00:00.000','19000101 05:45:00.000')
insert into Trip values(7771,5,'Boeing    ','London                   ','Singapore                ','19000101 01:00:00.000','19000101 11:00:00.000')
insert into Trip values(7772,5,'Boeing    ','Singapore                ','London                   ','19000101 12:00:00.000','19000101 02:00:00.000')
insert into Trip values(7773,5,'Boeing    ','London                   ','Singapore                ','19000101 03:00:00.000','19000101 13:00:00.000')
insert into Trip values(7774,5,'Boeing    ','Singapore                ','London                   ','19000101 14:00:00.000','19000101 06:00:00.000')
insert into Trip values(7775,5,'Boeing    ','London                   ','Singapore                ','19000101 09:00:00.000','19000101 20:00:00.000')
insert into Trip values(7776,5,'Boeing    ','Singapore                ','London                   ','19000101 18:00:00.000','19000101 08:00:00.000')
insert into Trip values(7777,5,'Boeing    ','London                   ','Singapore                ','19000101 18:00:00.000','19000101 06:00:00.000')
insert into Trip values(7778,5,'Boeing    ','Singapore                ','London                   ','19000101 22:00:00.000','19000101 12:00:00.000')
insert into Trip values(8881,5,'Boeing    ','London                   ','Paris                    ','19000101 03:00:00.000','19000101 04:00:00.000')
insert into Trip values(8882,5,'Boeing    ','Paris                    ','London                   ','19000101 22:00:00.000','19000101 23:00:00.000')

GO

                                                                                                                                                                                                                                                                 
----Pass_in_trip------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ 
insert into Pass_in_trip values(1100,'20030429 00:00:00.000',1,'1a        ')
insert into Pass_in_trip values(1123,'20030405 00:00:00.000',3,'2a        ')
insert into Pass_in_trip values(1123,'20030408 00:00:00.000',1,'4c        ')
insert into Pass_in_trip values(1123,'20030408 00:00:00.000',6,'4b        ')
insert into Pass_in_trip values(1124,'20030402 00:00:00.000',2,'2d        ')
insert into Pass_in_trip values(1145,'20030405 00:00:00.000',3,'2c        ')
insert into Pass_in_trip values(1181,'20030401 00:00:00.000',1,'1a        ')
insert into Pass_in_trip values(1181,'20030401 00:00:00.000',6,'1b        ')
insert into Pass_in_trip values(1181,'20030401 00:00:00.000',8,'3c        ')
insert into Pass_in_trip values(1181,'20030413 00:00:00.000',5,'1b        ')
insert into Pass_in_trip values(1182,'20030413 00:00:00.000',5,'4b        ')
insert into Pass_in_trip values(1187,'20030414 00:00:00.000',8,'3a        ')
insert into Pass_in_trip values(1188,'20030401 00:00:00.000',8,'3a        ')
insert into Pass_in_trip values(1182,'20030413 00:00:00.000',9,'6d        ')
insert into Pass_in_trip values(1145,'20030425 00:00:00.000',5,'1d        ')
insert into Pass_in_trip values(1187,'20030414 00:00:00.000',10,'3d        ')
insert into Pass_in_trip values(8882,'20051106 00:00:00.000',37,'1a        ') 
insert into Pass_in_trip values(7771,'20051107 00:00:00.000',37,'1c        ') 
insert into Pass_in_trip values(7772,'20051107 00:00:00.000',37,'1a        ') 
insert into Pass_in_trip values(8881,'20051108 00:00:00.000',37,'1d        ') 
insert into Pass_in_trip values(7778,'20051105 00:00:00.000',10,'2a        ') 
insert into Pass_in_trip values(7772,'20051129 00:00:00.000',10,'3a        ')
insert into Pass_in_trip values(7771,'20051104 00:00:00.000',11,'4a        ')
insert into Pass_in_trip values(7771,'20051107 00:00:00.000',11,'1b        ')
insert into Pass_in_trip values(7771,'20051109 00:00:00.000',11,'5a        ')
insert into Pass_in_trip values(7772,'20051107 00:00:00.000',12,'1d        ')
insert into Pass_in_trip values(7773,'20051107 00:00:00.000',13,'2d        ')
insert into Pass_in_trip values(7772,'20051129 00:00:00.000',13,'1b        ')
insert into Pass_in_trip values(8882,'20051113 00:00:00.000',14,'3d        ')
insert into Pass_in_trip values(7771,'20051114 00:00:00.000',14,'4d        ')
insert into Pass_in_trip values(7771,'20051116 00:00:00.000',14,'5d        ')
insert into Pass_in_trip values(7772,'20051129 00:00:00.000',14,'1c        ')

GO



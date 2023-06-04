USE master;
CREATE DATABASE aero_Snapshot ON
( 
    NAME = aero,
    FILENAME = 'C:\Snapshots\aero_Snapshot.ss' 
)
AS SNAPSHOT OF aero;
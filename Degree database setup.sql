drop database if exists "studentrecordkeeper";
create database "studentrecordkeeper";
set datestyle to 'SQL, DMY';

create table student
(
	name text null constraint valid_name check (length(name) >0),
	studentid integer primary key,
	transferstudent boolean set default FALSE,
	transferschool text,
	department text,
	yearlevel integer
);
CREATE UNIQUE INDEX studentid ON table (student);

create table courses
(
	ID serial primary key,
	
	coursecode text null constraint valid_cousrsecode check (length(coursecode) > 0),
	coursename text null constraint valid_name check (length(name) >0),
	degree boolean set default FALSE,
	courseyear integer null constraint valid_year check(courseyear>2000)
);
CREATE UNIQUE INDEX coursename ON table (courses);

create table transfermakeupcourses
(
	ID serial primary key,
	studentid integer,
	coursecode text null constraint valid_cousrsecode check (length(coursecode) >= 0),
	coursename text null constraint valid_name check (length(name) >0),
	courseyear integer null constraint valid_year check(courseyear>2000),
	degree boolean
);

create table coursegrades
(
	ID serial primary key,
	coursename text null constraint valid_name check (length(name) >0),
	courseyear integer null constraint valid_year check(courseyear>2000),
	grade integer null constraint valid_grade check(0<=grade<=100),
	passcheck boolean
);




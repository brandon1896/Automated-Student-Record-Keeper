drop database if exists "studentrecordkeeper";
create database "studentrecordkeeper";
set datestyle to 'SQL, DMY';

create table student
(
	name text null constraint valid_name check (length(name) >0),
	studentid integer primary key,
	transferstudent boolean set default FALSE,
	previouschool text,
	program	text,
	yearlevel integer
);
CREATE UNIQUE INDEX studentid ON table (student);

create table courses
(
	ID serial primary key,
	coursesubject text null constraint valid_cousrsecode check (length(coursesubject) > 0),
	coursenumber integer,
	coursesection text,
	coursename text null constraint valid_name check (length(name) >0),
	courseyear integer null constraint valid_year check(courseyear>2000),
	yearlevel integer,
	credits integer
);
CREATE UNIQUE INDEX coursename ON table (courses);

create table complementarycourses
(
	ID serial primary key,
	coursesubject text null constraint valid_cousrsecode check (length(coursesubject) > 0),
	coursenumber integer,
	coursesection text,
	coursename text null constraint valid_name check (length(name) >0),
	yearlevel integer,
	courseyear integer null constraint valid_year check(courseyear>2000),
	credits integer
);
CREATE UNIQUE INDEX coursename ON table (courses);

create table makeupcourses
(
	ID serial primary key,
	studentid integer,
	coursesubject text null constraint valid_cousrsecode check (length(coursesubject) > 0),
	coursenumber integer,
	coursesection text,
	courseyear integer null constraint valid_year check(courseyear>2000),
	yearlevel integer,
	credits integer
);

create table coursegrades
(
	ID serial primary key,
	coursesubject text null constraint valid_name check (length(name) >0),
	coursenumber integer,
	takenyear integer null constraint valid_year check(courseyear>2000),
	grade integer null constraint valid_grade check(0<=grade<=100)
);




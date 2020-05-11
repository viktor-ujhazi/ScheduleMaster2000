DROP TABLE IF EXISTS "user" CASCADE;
DROP TABLE IF EXISTS task CASCADE;
DROP TABLE IF EXISTS schedule CASCADE;
DROP TABLE IF EXISTS "column" CASCADE;
DROP TABLE IF EXISTS slot CASCADE;


CREATE TABLE "user" (
	user_id SERIAL PRIMARY KEY,
	username TEXT ,
	email TEXT UNIQUE NOT NULL,
	"password" TEXT NOT NULL,
	"admin" BOOLEAN

);

CREATE TABLE task(
	task_id SERIAL PRIMARY KEY,
	title TEXT,
	"content" TEXT,
	user_id INTEGER NOT NULL REFERENCES "user"(user_id)
);

CREATE TABLE schedule(
	schedule_id SERIAL PRIMARY KEY,
	title TEXT,
	num_of_columns INTEGER CHECK (num_of_columns >0 AND num_of_columns <8),
	user_id INTEGER NOT NULL REFERENCES "user"(user_id),
	"public" BOOLEAN
);

CREATE TABLE "column"(
	column_id SERIAL PRIMARY KEY,
	schedule_id INTEGER NOT NULL REFERENCES "schedule"(schedule_id),
	title TEXT

);

CREATE TABLE slot(
	slot_id SERIAL PRIMARY KEY,
	schedule_id INTEGER NOT NULL REFERENCES "schedule"(schedule_id),
	column_id INTEGER NOT NULL REFERENCES "column"(column_id),
	task_id INTEGER NOT NULL REFERENCES "task"(task_id),
	start_slot INTEGER,
	"length" INTEGER
	
);




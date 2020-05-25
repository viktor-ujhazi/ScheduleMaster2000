DROP TABLE IF EXISTS users CASCADE;
DROP TABLE IF EXISTS tasks CASCADE;
DROP TABLE IF EXISTS schedules CASCADE;
DROP TABLE IF EXISTS days CASCADE;
DROP TABLE IF EXISTS slots CASCADE;


CREATE TABLE users(
	"user_id" SERIAL PRIMARY KEY,
	username TEXT ,
	email TEXT UNIQUE NOT NULL,
	"password" TEXT NOT NULL,
	"admin" BOOLEAN NOT NULL DEFAULT FALSE

);

CREATE TABLE tasks(
	task_id SERIAL PRIMARY KEY,
	title TEXT,
	"content" TEXT,
	"user_id" INTEGER NOT NULL REFERENCES users(user_id)
);

CREATE TABLE schedules(
	schedule_id SERIAL PRIMARY KEY,
	title TEXT,
	num_of_days INTEGER CHECK (num_of_days >0 AND num_of_days <8),
	"user_id" INTEGER NOT NULL REFERENCES users(user_id),
	is_public BOOLEAN NOT NULL DEFAULT FALSE
);

CREATE TABLE days(
	day_id SERIAL PRIMARY KEY,
	schedule_id INTEGER NOT NULL REFERENCES schedules(schedule_id),
	title TEXT

);

CREATE TABLE slots(
	slot_id SERIAL PRIMARY KEY,
	schedule_id INTEGER NOT NULL REFERENCES schedules(schedule_id),
	day_id INTEGER NOT NULL REFERENCES days(day_id),
	task_id INTEGER NOT NULL REFERENCES tasks(task_id),
	start_slot INTEGER,
	slot_length INTEGER
	
);


CREATE OR REPLACE FUNCTION insert_into_schedule(p_title TEXT, p_num_of_days INTEGER, p_user_id INTEGER) RETURNS void AS $$
	declare p_schedule_id integer;
BEGIN
    
	
	INSERT INTO schedules (title, num_of_days, "user_id") VALUES (p_title, p_num_of_days, p_user_id) RETURNING schedule_id into p_schedule_id;
		
	FOR counter IN 1..p_num_of_days LOOP
    
	INSERT INTO days(schedule_id, title) VALUES (p_schedule_id, ' ');
	
  	END LOOP;
	
	
END;
$$ LANGUAGE plpgsql;

/* Create users */

INSERT INTO users (username, email, password, admin) VALUES ('admin', 'admin@admin.com', 'JeB0p77PTp6agih+iDW35G/PeWz7kvxsC2lc7yWc4q5wN05F', TRUE);
INSERT INTO users (username, email, password) VALUES ('user1', 'user1@users.com', 'Zy6TEAkAoJfLirW0ngjuisCsAKyBmYEY7xs0cHVnFKeEfRqF');

/* Create tasks for users */

INSERT INTO tasks (title, "content", "user_id") VALUES ('Task 1 ', 'content from admin', '1');
INSERT INTO tasks (title, "content", "user_id") VALUES ('Task 2 ', 'another content from admin', '1');

/* Create schedules and days for users */


SELECT insert_into_schedule('Schedule Title 1', 4, 1);
SELECT insert_into_schedule('Title 2', 2, 1);
SELECT insert_into_schedule('Title 3', 5, 1);
SELECT insert_into_schedule('Title 4', 1, 1);

/* Create day titles */

UPDATE days SET title = 'Day title 1' WHERE day_id = 1;
UPDATE days SET title = 'Day title 2' WHERE day_id = 2;
UPDATE days SET title = 'Day title 3' WHERE day_id = 3;
UPDATE days SET title = 'Day title 4' WHERE day_id = 4;
UPDATE days SET title = 'Day title 5' WHERE day_id = 5;
UPDATE days SET title = 'Day title 6' WHERE day_id = 6;
UPDATE days SET title = 'Day title 7' WHERE day_id = 7;
UPDATE days SET title = 'Day title 8' WHERE day_id = 8;
UPDATE days SET title = 'Day title 9' WHERE day_id = 9;
UPDATE days SET title = 'Day title 10' WHERE day_id = 10;
UPDATE days SET title = 'Day title 11' WHERE day_id = 11;
UPDATE days SET title = 'Day title 12' WHERE day_id = 12;

/* Create slots */

INSERT INTO slots (schedule_id, day_id, task_id, start_slot, slot_length) VALUES (1,1,1,1,6);

INSERT INTO slots (schedule_id, day_id, task_id, start_slot, slot_length) VALUES (1,2,1,12,5);



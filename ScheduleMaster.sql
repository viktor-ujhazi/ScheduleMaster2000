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
	"length" INTEGER
	
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

INSERT INTO tasks (title, "content", "user_id") VALUES ('Task 1 user1', 'content from user1', '2');

/* Create schedules and days for users */


SELECT insert_into_schedule('Schedule Title 1', 4, 1);





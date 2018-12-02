CREATE TABLE users
(
id SERIAL PRIMARY KEY,
name text,
lastname text,
date_current date,
sender_id int
)

CREATE TABLE photo
(
id SERIAL PRIMARY KEY,
path text,
date_create date,
user_id int REFERENCES users(id)
)

CREATE TABLE emotion
(
id SERIAL PRIMARY KEY,
contempt int,
disgust int,
anger int,
fear int,
happiness int,
neutral int,
sadness int,
surprise int,
photo_id int REFERENCES photo(id),
create_date date
)
create table public.journals (
	id SERIAL NOT NULL PRIMARY KEY,
	text TEXT NULL,
	event_id BIGINT NULL,
	created_date_utc TIMESTAMP NOT NULL DEFAULT (now() at time zone 'utc')
);
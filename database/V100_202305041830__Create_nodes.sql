create table public.nodes (
    id SERIAL NOT NULL PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    tree_name VARCHAR(255) NOT NULL,
    parent_id INTEGER NULL
);
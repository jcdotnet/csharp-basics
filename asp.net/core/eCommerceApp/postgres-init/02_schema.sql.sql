CREATE TABLE public."Users"
(
    "UserId" uuid NOT NULL,
    "UserName" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "Email" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "Password" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "Gender" character varying(15) COLLATE pg_catalog."default",
    CONSTRAINT "Users_pkey" PRIMARY KEY ("UserId")
);

INSERT INTO public."Users" ("UserId", "Email", "UserName", "Gender", "Password")
VALUES 
('c32f8b42-60e6-4c02-90a7-9143ab37189f', 'john@email.com', 'John Doe', 'Male', 'John123!'),
('8ff22c7d-18c7-4ef0-a0ac-988ecb2ac7f5', 'jane@email.com', 'Jane Doe', 'Female', 'Jane123!');
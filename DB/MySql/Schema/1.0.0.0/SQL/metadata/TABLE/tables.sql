
  CREATE TABLE NODES
   (	ID VARCHAR(24) NOT NULL,
	LABEL VARCHAR(64) NOT NULL,
	 CONSTRAINT PK_NODES PRIMARY KEY (ID)
   )  ENGINE=InnoDb;

  CREATE TABLE ADJACENT_NODES
   (	ID VARCHAR(24) NOT NULL,
	ADJ_ID VARCHAR(24) NOT NULL
   )  ENGINE=InnoDb;

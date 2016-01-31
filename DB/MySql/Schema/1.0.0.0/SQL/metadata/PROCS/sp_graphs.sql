/* --------------------------------------------------------------------------

                    GRAPHS stored procedures and functions

   -------------------------------------------------------------------------- */

DELIMITER $$

/* -------------------------------------------------------------------------- */ 

DROP PROCEDURE IF EXISTS clear$$

CREATE PROCEDURE clear()
BEGIN
  DELETE FROM adjacent_nodes;
  DELETE FROM nodes;
END$$

/* -------------------------------------------------------------------------- */ 

DROP PROCEDURE IF EXISTS insert_node$$

CREATE PROCEDURE insert_node(p_id VARCHAR(24), p_label VARCHAR(64), p_nodes TEXT)
BEGIN
  DECLARE v_pos INT DEFAULT 1;
  DECLARE v_prev_pos INT DEFAULT 1;
  DECLARE v_len INT DEFAULT 0;
  DECLARE v_delim TEXT DEFAULT ',';

  IF p_id = '' THEN
    SIGNAL SQLSTATE '10001' SET MESSAGE_TEXT = 'Node Id can not be empty string', MYSQL_ERRNO = 10001;
  END IF;

  INSERT INTO nodes (id, label) VALUES (p_id, p_label);
  
  SET v_len = LENGTH(p_nodes);
  SET v_pos = LOCATE(v_delim, p_nodes, v_prev_pos);

  REPEAT
    IF v_pos > 0 THEN
      SET @s = SUBSTRING(p_nodes, v_prev_pos, v_pos - v_prev_pos);
      SET v_prev_pos = v_pos + 1;
      SET v_pos = LOCATE(v_delim, p_nodes, v_prev_pos);
    ELSE
      SET @s = SUBSTRING(p_nodes, v_prev_pos, v_len - v_prev_pos + 1);
      SET v_pos = -1;
    END IF;

    IF @s <> '' THEN
      INSERT INTO adjacent_nodes (id, adj_id) VALUES (p_id, @s);
    END IF;
  UNTIL v_pos = -1 END REPEAT; 
END$$

/* -------------------------------------------------------------------------- */ 

DROP PROCEDURE IF EXISTS get_graph$$

CREATE PROCEDURE get_graph()
BEGIN
  SELECT n.id, n.label, an.adj_id FROM nodes n LEFT OUTER JOIN adjacent_nodes an ON n.id = an.id ORDER BY 1;
END$$

/* -------------------------------------------------------------------------- */ 

DELIMITER ;
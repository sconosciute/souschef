START TRANSACTION;

ALTER TABLE users ADD pw_hash text NOT NULL DEFAULT '';

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240813215107_Passwords', '8.0.7');

COMMIT;

DELETE FROM "PrestationType";
INSERT INTO "PrestationType" ("PrestationTypeId", "Name", "Status", "CreatedDateTime", "ModifiedDateTime", "CreatedBy", "ModifiedBy") VALUES
	(1, 'Changement(s) de(s) pneu(s)', 1, NULL, NULL, NULL, NULL),
	(2, 'Réparation hydraulique', 1, NULL, NULL, NULL, NULL),
	(3, 'Changement des freins (réglage)', 1, NULL, NULL, NULL, NULL),
	(4, 'Réparation électrique', 1, NULL, NULL, NULL, NULL),
	(5, 'Réparation pneumatique (air)', 1, NULL, NULL, NULL, NULL),
	(6, 'Changement (lamier, cercle à bille, brides)', 1, NULL, NULL, NULL, NULL),
	(7, 'Soudures', 1, NULL, NULL, NULL, NULL),
	(8, 'Réparation boîte de vitesse (moteur)', 1, NULL, NULL, NULL, NULL),
	(9, 'Problème mazout', 1, NULL, NULL, NULL, NULL),
	(10, 'Réglages divers', 1, NULL, NULL, NULL, NULL),
	(11, 'Graissage (niveau, huile, eau)', 1, NULL, NULL, NULL, NULL),
	(12, 'Changement carreau', 1, NULL, NULL, NULL, NULL),
	(13, 'Changement roulement (moyen)', 1, NULL, NULL, NULL, NULL),
	(14, 'Carrosserie', 1, NULL, NULL, NULL, NULL),
	(15, 'Changement roulement mât', 1, NULL, NULL, NULL, NULL),
	(16, 'Changement goujons de roue', 1, NULL, NULL, NULL, NULL),
	(17, 'changer tresse (cable)', 1, NULL, NULL, NULL, NULL),
	(18, 'Casses', 1, NULL, NULL, NULL, NULL);

DELETE FROM "InterventionType";
INSERT INTO "InterventionType" ("InterventionTypeId", "Name", "Status", "CreatedDateTime", "ModifiedDateTime", "CreatedBy", "ModifiedBy") VALUES
	(1, 'PE Petit Entretien', 1, NULL, NULL, NULL, NULL),
	(2, 'GE Grand Entretien', 1, NULL, NULL, NULL, NULL),
	(3, 'CT Contrôle Technique', 1, NULL, NULL, NULL, NULL),
	(4, 'Panne', 1, NULL, NULL, NULL, NULL),
	(5, 'Changement de pneu', 1, NULL, NULL, NULL, NULL),
	(6, 'Rangement', 1, NULL, NULL, NULL, NULL),
	(7, 'Nettoyage', 1, NULL, NULL, NULL, NULL);

DELETE FROM "Status";
INSERT INTO "Status" ("StatusId", "Name", "Status", "CreatedDateTime", "ModifiedDateTime", "CreatedBy", "ModifiedBy") VALUES
	(1, 'Open', NULL, NULL, NULL, NULL, NULL),
	(2, 'InProgress', NULL, NULL, NULL, NULL, NULL),
	(3, 'Close', NULL, NULL, NULL, NULL, NULL);

DELETE FROM "UserProfile";
INSERT INTO "UserProfile" ("UserProfileId", "Name", "ProfileLevel", "SiteId", "CreatedDateTime", "ModifiedDateTime", "CreatedBy", "ModifiedBy") VALUES
	(1, 'Responsable', 2, 'MAR', NULL, NULL, NULL, NULL),
	(2, 'Garage', 1, 'MAR', NULL, NULL, NULL, NULL),
	(3, 'Administrateur', 4, 'MAR', NULL, NULL, NULL, NULL),
	(4, 'Compta', 3, 'MAR', NULL, NULL, NULL, NULL),
	(5, 'Responsable', 2, 'LRBS', NULL, NULL, NULL, NULL),
	(6, 'Garage', 1, 'LRBS', NULL, NULL, NULL, NULL),
	(7, 'Administrateur', 4, 'LRBS', NULL, NULL, NULL, NULL);

DELETE FROM "ProfileLevelType";
INSERT INTO "ProfileLevelType" ("ProfileLevelTypeId", "Name", "Status", "CreatedDateTime", "ModifiedDateTime", "CreatedBy", "ModifiedBy") VALUES
	(1, 'Encodeur', 1, NULL, NULL, NULL, NULL),
	(2, 'Responsable', 1, NULL, NULL, NULL, NULL),
	(3, 'Invoice', 1, NULL, NULL, NULL, NULL),
	(4, 'Admin', 1, NULL, NULL, NULL, NULL);
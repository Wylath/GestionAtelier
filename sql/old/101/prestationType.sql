/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES  */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- Export de données de la table PrestationType : -1 rows
DELETE FROM "PrestationType";
/*!40000 ALTER TABLE "PrestationType" DISABLE KEYS */;
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
/*!40000 ALTER TABLE "PrestationType" ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;

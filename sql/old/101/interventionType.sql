/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES  */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- Export de données de la table InterventionType : -1 rows
DELETE FROM "InterventionType";
/*!40000 ALTER TABLE "InterventionType" DISABLE KEYS */;
INSERT INTO "InterventionType" ("InterventionTypeId", "Name", "Status", "CreatedDateTime", "ModifiedDateTime", "CreatedBy", "ModifiedBy") VALUES
	(1, 'PE Petit Entretien', 1, NULL, NULL, NULL, NULL),
	(2, 'GE Grand Entretien', 1, NULL, NULL, NULL, NULL),
	(3, 'CT Contrôle Technique', 1, NULL, NULL, NULL, NULL),
	(4, 'Panne', 1, NULL, NULL, NULL, NULL),
	(5, 'Changement de pneu', 1, NULL, NULL, NULL, NULL),
	(6, 'Rangement', 1, NULL, NULL, NULL, NULL),
	(7, 'Nettoyage', 1, NULL, NULL, NULL, NULL);
/*!40000 ALTER TABLE "InterventionType" ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;

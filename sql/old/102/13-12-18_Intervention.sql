ALTER TABLE Intervention
ALTER COLUMN DateEstimate DateTime;

ALTER TABLE Intervention
ALTER COLUMN StatusDate DateTime;

ALTER TABLE Intervention
DROP COLUMN TimeEstimate;

ALTER TABLE Intervention
ADD TimeEstimate float;
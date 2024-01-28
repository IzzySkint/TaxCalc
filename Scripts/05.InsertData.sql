USE TaxCalc;
GO

INSERT INTO TaxCalculationType
VALUES (1, 'Flat Value'), 
(2, 'Flat Rate'), 
(3, 'Progressive');
GO

INSERT INTO PostalCode
VALUES 
('7441', 3),
('A100', 1),
('7000', 2),
('1000', 3);
GO

INSERT INTO Tax
([From], [To], Rate, [Value], TaxCalculationTypeId)
VALUES 
(0, 199999, 0.05, 0, 1),
(200000, 3000000, 10000, 0, 1),
(0, 3000000, 0.175, 0, 2),
(0, 8350, 0.10, 0, 3),
(8351, 33950, 0.15, 0, 3),
(33951, 82250, 0.25, 0, 3),
(82251, 171550, 0.28, 0, 3),
(171551, 372950, 0.33, 0, 3),
(372951, 3000000, 0.35, 0, 3);
GO
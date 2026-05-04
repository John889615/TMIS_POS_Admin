USE [TMIS_Development]
GO

IF OBJECT_ID('POS_PaymentTypeIcons', 'U') IS NOT NULL
	DROP TABLE POS_PaymentTypeIcons
GO

CREATE TABLE POS_PaymentTypeIcons
(
	PaymentTypeIconID INT NOT NULL PRIMARY KEY IDENTITY (1, 1),
	IconPath VARCHAR(50) NOT NULL,
	Category VARCHAR(255) NOT NULL,
	DateCreated DATETIME NOT NULL DEFAULT GETDATE()
)

INSERT INTO POS_PaymentTypeIcons (IconPath, Category, DateCreated)
VALUES ('bi-credit-card', 'card', GETDATE()),
       ('bi-credit-card-2-back', 'card', GETDATE()),
       ('bi-credit-card-2-back-fill', 'card', GETDATE()),
       ('bi-credit-card-2-front', 'card', GETDATE()),
       ('bi-credit-card-2-front-fill', 'card', GETDATE()),
       ('bi-credit-card-fill', 'card', GETDATE()),
       ('bi-paypal', 'provider', GETDATE()),
       ('bi-alipay', 'provider', GETDATE()),
       ('bi-wallet', 'cash', GETDATE()),
       ('bi-wallet-fill', 'cash', GETDATE()),
       ('bi-wallet2', 'cash', GETDATE()),
       ('bi-cash', 'cash', GETDATE()),
       ('bi-cash-coin', 'cash', GETDATE()),
       ('bi-cash-stack', 'cash', GETDATE()),
       ('bi-person-badge', 'account', GETDATE()),
       ('bi-person-vcard', 'account', GETDATE()),
       ('bi-person-vcard-fill', 'account', GETDATE())
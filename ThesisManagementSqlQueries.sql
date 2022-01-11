--SELECT * FROM [dbo].[AspNetRoles]
--SELECT * FROM AspNetUsers


/*
admin user: admin@admin.com
admin pwd: Admin1234
*/


UPDATE AspNetUsers
SET UserType = 0
WHERE email = N'admin@admin.com'

/*
UPDATE AspNetRoles
SET RoleValue = 1
WHERE name = N'Promoter'
*/

--SET IDENTITY_INSERT [dbo].[Theses] ON


USE DiplomaManagementDb;
go


--SELECT * from __EFMigrationsHistory
--SELECT * FROM [dbo].[Roles]
SELECT * FROM [dbo].[AspNetRoleClaims]

SELECT * FROM [dbo].[AspNetUserClaims]
SELECT * FROM [dbo].[AspNetUserLogins]
SELECT * FROM [dbo].[AspNetRoles]
SELECT * FROM [dbo].[AspNetUsers]
SELECT * FROM [dbo].[AspNetUserTokens]

SELECT * FROM [dbo].[AspNetUserTokens]

update aspnetusers
set DepartmentId = 5
where email = N'promoter@promoter.com'

SELECT * FROM AspNetUsers
WHERE UserType = 2 AND DepartmentId = 5

SELECT * FROM AspNetUsers
WHERE StudentId = 12

UPDATE Theses
SET DepartmentId=5
WHERE id=3


SELECT * FROM [dbo].[Theses]
SELECT * FROM [dbo].[Departments]
SELECT * FROM [dbo].[ProposedTheseComments]
SELECT * FROM [dbo].[ProposedTheses]

SELECT AspNetUsers.*, AspNetRoles.Name FROM AspNetUsers
JOIN AspNetRoles ON AspNetUsers.UserType = AspNetRoles.RoleValue

/*
DELETE FROM [dbo].[AspNetUserClaims]
DELETE FROM [dbo].[AspNetUserLogins]
DELETE FROM [dbo].[AspNetUsers]
DELETE FROM [dbo].[AspNetRoles]
DELETE FROM [dbo].[AspNetUserTokens]
DELETE FROM [dbo].[Theses]
DELETE FROM [dbo].[Departments]
DELETE FROM [dbo].[ProposedTheseComments]
DELETE FROM [dbo].[ProposedTheses]
*/





--DBCC CHECKIDENT ('[aspnetusers]', RESEED, 0);
--GO


--DELETE FROM [dbo].[aspnetUsers]
--WHERE email = N'admin@admin.com';
--GO







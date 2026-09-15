CREATE PROCEDURE [dbo].[spUsers_Authenticate]
    @UserName nvarchar(16),
    @Password nvarchar(16)
AS
BEGIN
    SELECT [Id],
           [UserName],
           [FirstName],
           [LastName],
           [MiddleName]
    FROM [dbo].[Users]
    WHERE [UserName] = @UserName
      AND [Password] = @Password;
END
USE DotNetCourseDatabase
GO

SELECT [UserId],
[FirstName],
[LastName],
[Email],
[Gender],
[Active] FROM TutorialAppSchema.Users
order by 
    userId desc

SELECT [UserId],
[JobTitle],
[Department] FROM TutorialAppSchema.UserJobInfo

SELECT [UserId],
[Salary],
[AvgSalary] FROM TutorialAppSchema.UserSalary

INSERT INTO TutorialAppSchema.Users(
    [FirstName],
    [LastName],
    [Email],
    [Gender],
    [Active]
) VALUES (
    [FirstName],
    [LastName],
    [Email],
    [Gender],
    [Active]
)

UPDATE TutorialAppSchema.Users
SET     
    [FirstName] = 'Albertina',
    [LastName] = 'O''Bannon',
    [Email] = 'aofinan0@blogspot.com',
    [Gender] = 'Female',
    [Active] = 1
WHERE UserId = 1

 UPDATE TutorialAppSchema.Users
    SET
        [FirstName] = 'string',
        [LastName] = 'string',
        [Email] = 'string',
        [Gender] = 'string',
        [Active] = 0
        WHERE UserId = 0

INSERT INTO TutorialAppSchema.Users(
                            [FirstName],
                            [LastName],
                            [Email],
                            [Gender],
                            [Active]
                        ) VALUES ('TestUser','TestUser','TestUser@gmail.com','female','True')

SELECT TOP (1000) [UserId]
      ,[FirstName]
      ,[LastName]
      ,[Email]
      ,[Gender]
      ,[Active]
  FROM [DotNetCourseDatabase].[TutorialAppSchema].[Users]
  WHERE
    UserId BETWEEN 495 and 505

  Select count(*) FROM [DotNetCourseDatabase].[TutorialAppSchema].[Users]

  DELETE FROM [DotNetCourseDatabase].[TutorialAppSchema].[Users] WHERE UserId = 1001

  SELECT * FROM TutorialAppSchema.Users
  WHERE UserId IN (37, 2002)
  ORDER BY userId DESC

  SELECT UserId FROM TutorialAppSchema.Users WHERE Email = 'test2@test.com'
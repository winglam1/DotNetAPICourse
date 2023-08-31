SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [TutorialAppSchema].[spUsers_Get]
-- EXEC TutorialAppSchema.spUsers_Get @UserId=3
  @UserId INT = NULL,
  @Active BIT = NULL
AS
BEGIN

-- Old versions of SQL will work with this one...
--   IF OBJECT_ID('tempdb..#AverageDeptSalary', 'U') IS NOT NULL
--     BEGIN
--         DROP TABLE #AverageDeptSalary
--     END

-- Modern versions will work with this line
  DROP TABLE IF EXISTS #AverageDeptSalary 

  SELECT UserJobInfo.Department,
    AVG(UserSalary.Salary) AvgSalary
    INTO #AverageDeptSalary
  FROM 
    TutorialAppSchema.Users AS Users
  LEFT JOIN TutorialAppSchema.UserSalary AS UserSalary
    ON UserSalary.UserId = Users.UserId
  LEFT JOIN TutorialAppSchema.UserJobInfo AS UserJobInfo
    ON UserJobInfo.UserId = Users.UserId
  GROUP BY UserJobInfo.Department

  CREATE CLUSTERED INDEX cix_AverageDeptSalary_Department ON #AverageDeptSalary(Department)

  SELECT [Users].[UserId],
    [Users].[FirstName],
    [Users].[LastName],
    [Users].[Email],
    [Users].[Gender],
    [Users].[Active],
    UserSalary.Salary,
    UserJobInfo.Department,
    UserJobInfo.JobTitle,
    AvgSalary.AvgSalary
  FROM 
    TutorialAppSchema.Users AS Users
  LEFT JOIN TutorialAppSchema.UserSalary AS UserSalary
    ON UserSalary.UserId = Users.UserId
  LEFT JOIN TutorialAppSchema.UserJobInfo AS UserJobInfo
    ON UserJobInfo.UserId = Users.UserId
  LEFT JOIN #AverageDeptSalary AS AvgSalary
    ON AvgSalary.Department = UserJobInfo.Department
--   OUTER APPLY (
--     SELECT AVG(UserSalary2.Salary) AvgSalary
--     FROM 
--         TutorialAppSchema.Users AS Users
--     LEFT JOIN TutorialAppSchema.UserSalary AS UserSalary2
--         ON UserSalary2.UserId = Users.UserId
--     LEFT JOIN TutorialAppSchema.UserJobInfo AS UserJobInfo2
--         ON UserJobInfo2.UserId = Users.UserId
--     WHERE 
--         UserJobInfo2.Department = UserJobInfo.Department
--     GROUP BY 
--         UserJobInfo2.Department    
--   ) AS AvgSalary
  WHERE
    Users.UserId = ISNULL(@UserId, Users.UserId)
  AND
    ISNULL(Users.Active, 0) = COALESCE(@Active, Users.Active, 0)
END
GO

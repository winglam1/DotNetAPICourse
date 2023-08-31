USE DotNetCourseDatabase
GO

CREATE OR ALTER PROCEDURE TutorialAppSchema.spUser_Upsert
	@FirstName NVARCHAR(50),
	@LastName NVARCHAR(50),
	@Email NVARCHAR(50),
	@Gender NVARCHAR(50),
	@JobTitle NVARCHAR(50),
	@Department NVARCHAR(50),
    @Salary DECIMAL(18, 4),
	@Active BIT,
	@UserId INT = NULL
AS
BEGIN
    SELECT * FROM TutorialAppSchema.UserSalary
    SELECT * FROM TutorialAppSchema.UserJobInfo

    IF NOT EXISTS (SELECT * FROM TutorialAppSchema.Users WHERE UserId = @UserId)
        BEGIN

        IF NOT EXISTS (SELECT * FROM TutorialAppSchema.Users WHERE Email = @Email)
            BEGIN
                DECLARE @OutputUserId INT

                INSERT INTO TutorialAppSchema.Users(
                    [FirstName],
                    [LastName],
                    [Email],
                    [Gender],
                    [Active]
                ) VALUES (
                    @FirstName,
                    @LastName,
                    @Email,
                    @Gender,
                    @Active
                )
            END

            SET @OutputUserId = @@IDENTITY

            INSERT INTO TutorialAppSchema.UserSalary (
                UserId,
                Salary
            ) VALUES (
                @OutputUserId,
                @Salary
            )

            INSERT INTO TutorialAppSchema.UserJobInfo (
                UserId,
                Department,
                JobTitle
            ) VALUES (
                @OutputUserId,
                @Department,
                @JobTitle
            )
        END
    ELSE
        BEGIN
            UPDATE TutorialAppSchema.Users
                SET
                    FirstName = @FirstName,
                    LastName = @LastName,
                    Email = @Email,
                    Gender = @Gender,
                    Active = @Active
                WHERE
                    UserId = @UserId

            UPDATE 
                TutorialAppSchema.UserSalary
            SET 
                Salary = @Salary
            WHERE
                UserId = @UserId

            UPDATE 
                TutorialAppSchema.UserJobInfo
            SET 
                Department = @Department,
                JobTitle = @JobTitle
            WHERE
                UserId = @UserId
        END
END
USE DotNetCourseDatabase 

CREATE TABLE TutorialAppSchema.Posts(
  PostId INT IDENTITY(1,1),
  UserId INT,
  PostTitle NVARCHAR(255),
  PostContent VARCHAR(MAX),
  PostCreated DATETIME,
  PostUpdated DATETIME
)

CREATE CLUSTERED INDEX cix_Posts_UserId_PostId ON TutorialAppSchema.Posts(UserId, PostId)

SELECT [PostId],
[UserId],
[PostTitle],
[PostContent],
[PostCreated],
[PostUpdated] from TutorialAppSchema.Posts

SELECT [PostId]
      ,[UserId]
      ,[PostTitle]
      ,[PostContent]
      ,[PostCreated]
      ,[PostUpdated]
  FROM [DotNetCourseDatabase].[TutorialAppSchema].[Posts]

  INSERT INTO [TutorialAppSchema].[Posts] (
      [PostId]
      ,[UserId]
      ,[PostTitle]
      ,[PostContent]
      ,[PostCreated]
      ,[PostUpdated]
    ) VALUES ()

  UPDATE TutorialAppSchema.Posts
    SET PostContent = '', PostTitle = '', PostUpdated = GETDATE()
    WHERE PostId = 4

SELECT [Email],
[PasswrdHash],
[PasswordSalt] FROM TutorialAppSchema.Auth WHERE Email = ''

INSERT INTO TutorialAppSchema.Auth ([Email],
[PasswrdHash],
[PasswordSalt) VALUES ()

select * from [TutorialAppSchema].[Users] where FirstName = 'test'
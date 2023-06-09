USE DotNetCourseDatabase 

CREATE TABLE TutorialAppSchema.Auth(
  Email VARCHAR(50),
  PasswrdHash VARBINARY(MAX),
  PasswordSalt VARBINARY(MAX) 
)


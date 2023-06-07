SELECT TOP (1000) [ComputerId]
      ,[Motherboard]
      ,[CPUCores]
      ,[HasWifi]
      ,[HasLTE]
      ,[ReleaseDate]
      ,[Price]
      ,[VideoCard]
  FROM [DotNetCourseDatabase].[TutorialAppSchema].[Computer]

select [ComputerId],
[Motherboard],
[CPUCores],
[HasWifi],
[HasLTE],
[ReleaseDate],
[Price],
[VideoCard] from TutorialAppSchema.Computer

--SET IDENTITY_INSERT TutorialAppSchema.Computer ON
--SET IDENTITY_INSERT TutorialAppSchema.Computer OFF

INSERT INTO TutorialAppSchema.Computer ( 
    [Motherboard],
    [CPUCores],
    [HasWifi],
    [HasLTE],
    [ReleaseDate],
    [Price],
    [VideoCard]
    ) VALUES (
        'Sample-Motherboard',
        4,
        1,
        0,
        '2022-01-01',
        1000,
        'Sample-Videocard'
)

DELETE FROM TutorialAppSchema.Computer WHERE ComputerId = 1002

UPDATE TutorialAppSchema.Computer SET CPUCores = 4 WHERE ComputerId = 1
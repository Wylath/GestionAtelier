DROP TABLE [dbo].[ProfileLevelType];
CREATE TABLE [dbo].[ProfileLevelType](
	[ProfileLevelTypeId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Status] [int] NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifiedDateTime] [datetime] NULL,
	[CreatedBy] [nvarchar](20) NULL,
	[ModifiedBy] [nvarchar](20) NULL,
 CONSTRAINT [PK_ProfileLevelType] PRIMARY KEY CLUSTERED 
(
	[ProfileLevelTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

DELETE FROM [dbo].[ProfileLevelType];
INSERT [dbo].[ProfileLevelType] ([ProfileLevelTypeId], [Name], [Status], [CreatedDateTime], [ModifiedDateTime], [CreatedBy], [ModifiedBy]) VALUES 
(1, N'Encodeur', 1, NULL, NULL, NULL, NULL),
(2, N'Responsable', 1, NULL, NULL, NULL, NULL),
(3, N'Invoice', 1, NULL, NULL, NULL, NULL),
(4, N'Admin', 1, NULL, NULL, NULL, NULL);


ALTER TABLE [dbo].[UserProfile]  WITH CHECK ADD  CONSTRAINT [FK_UserProfile_ProfileLevelType] FOREIGN KEY([ProfileLevel])
REFERENCES [dbo].[ProfileLevelType] ([ProfileLevelTypeId]);

ALTER TABLE [dbo].[UserProfile] CHECK CONSTRAINT [FK_UserProfile_ProfileLevelType];






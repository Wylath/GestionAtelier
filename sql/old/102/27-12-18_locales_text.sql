DROP TABLE [dbo].[Broadcast_text];
CREATE TABLE [dbo].[Broadcast_text](
	[BroadcastTextID] [int] NOT NULL,
	[Text_enGB] [text] NOT NULL,
	[Text_frFR] [text] NULL,
	[Text_nlNL] [text] NULL,
	[Text_plPL] [text] NULL,
 CONSTRAINT [PK_Broadcast_text] PRIMARY KEY CLUSTERED 
(
	[BroadcastTextID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];

ALTER TABLE [Status]
ADD BroadcastTextID int NOT NULL DEFAULT 0;

DELETE FROM [dbo].[Broadcast_text] WHERE [BroadcastTextID] IN (1, 2, 3);
INSERT INTO [dbo].[Broadcast_text] ([BroadcastTextID], [Text_enGB], [Text_frFR]) VALUES
(1, 'Open', 'Ouvert'),
(2, 'InProgress', 'En cours'),
(3, 'Close', 'Fermer');

UPDATE [Status] SET [BroadcastTextID] = 1 WHERE [StatusId] = 1;
UPDATE [Status] SET [BroadcastTextID] = 2 WHERE [StatusId] = 2;
UPDATE [Status] SET [BroadcastTextID] = 3 WHERE [StatusId] = 3;
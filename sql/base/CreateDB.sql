DROP TABLE [dbo].[Employee];
CREATE TABLE [dbo].[Employee](
	[EmployeeId] [nvarchar](20) NOT NULL,
	[Name] [nvarchar](60) NOT NULL,
	[BadgeNumber] [nvarchar](15) NOT NULL,
	[Company] [nvarchar](10) NOT NULL,
	[Comment] [nvarchar](30) NOT NULL,
	[Responsible] [nvarchar](20) NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

DROP TABLE [dbo].[Intervention];
CREATE TABLE [dbo].[Intervention](
	[InterventionId] [int] IDENTITY(1,1) NOT NULL,
	[DateIn] [datetime] NOT NULL,
	[DateOut] [datetime] NULL,
	[DateEstimate] [datetime] NULL,
	[Applicant] [nvarchar](20) NOT NULL,
	[Submitter] [nvarchar](20) NOT NULL,
	[VehicleId] [nvarchar](13) NOT NULL,
	[InterventionTypeId] [int] NOT NULL,
	[StatusId] [int] NOT NULL,
	[StatusDate] [datetime] NOT NULL,
	[PieceOrder] [int] NOT NULL,
	[PieceCom] [text] NULL,
	[TimeEstimate] [float] NULL,
	[Priority] [int] NOT NULL,
	[SiteId] [nvarchar](13) NOT NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifiedDateTime] [datetime] NULL,
	[CreatedBy] [nvarchar](20) NULL,
	[ModifiedBy] [nvarchar](20) NULL,
	[Barcode] [int] NULL,
 CONSTRAINT [PK_Intervention] PRIMARY KEY CLUSTERED 
(
	[InterventionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];

DROP TABLE [dbo].[InterventionPrestationGroup];
CREATE TABLE [dbo].[InterventionPrestationGroup](
	[InterventionTypeId] [int] NOT NULL,
	[PrestationTypeId] [int] NOT NULL
) ON [PRIMARY];

DROP TABLE [dbo].[InterventionTire];
CREATE TABLE [dbo].[InterventionTire](
	[InterventionTireId] [int] IDENTITY(1,1) NOT NULL,
	[TireNumber] [int] NULL,
	[ChangeCause] [int] NULL,
	[InterventionId] [int] NULL,
	[NewTireBarcodeId] [int] NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifiedDateTime] [datetime] NULL,
	[CreatedBy] [nvarchar](20) NULL,
	[ModifiedBy] [nvarchar](20) NULL,
 CONSTRAINT [PK_PneuChange] PRIMARY KEY CLUSTERED 
(
	[InterventionTireId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

DROP TABLE [dbo].[Invoice];
CREATE TABLE [dbo].[Invoice](
	[InvoiceId] [int] IDENTITY(1,1) NOT NULL,
	[Ponderation] [int] NOT NULL,
	[TVA] [int] NOT NULL,
	[Price] [int] NOT NULL,
	[NumberInvoice] [int] NOT NULL,
	[DateInvoice] [date] NOT NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifiedDateTime] [datetime] NULL,
	[CreatedBy] [nvarchar](20) NULL,
	[ModifiedBy] [nvarchar](20) NULL,
 CONSTRAINT [PK_Bill] PRIMARY KEY CLUSTERED 
(
	[InvoiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

DROP TABLE [dbo].[ParmSite];
CREATE TABLE [dbo].[ParmSite](
	[SiteId] [nvarchar](13) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Status] [int] NULL,
	[CompanyID] [nvarchar](4) NULL,
 CONSTRAINT [PK_ParmSite] PRIMARY KEY CLUSTERED 
(
	[SiteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

DROP TABLE [dbo].[Prestation];
CREATE TABLE [dbo].[Prestation](
	[PrestationId] [int] IDENTITY(1,1) NOT NULL,
	[InterventionId] [int] NOT NULL,
	[HoursCount] [float] NOT NULL,
	[EmployeeId] [nvarchar](20) NOT NULL,
	[PrestationTypeId] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Hour] [int] NULL,
	[Kilometer] [int] NULL,
	[TruckCrane] [int] NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifiedDateTime] [datetime] NULL,
	[CreatedBy] [nvarchar](20) NULL,
	[ModifiedBy] [nvarchar](20) NULL,
	[InvoiceId] [int] NULL,
	[Note] [nvarchar](255) NULL,
	[HourFuel] [int] NULL,
	[KilometerFuel] [int] NULL,
	[DateFuel] [datetime] NULL,
 CONSTRAINT [PK_Prestation] PRIMARY KEY CLUSTERED 
(
	[PrestationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

DROP TABLE [dbo].[RightUserProfile];
CREATE TABLE [dbo].[RightUserProfile](
	[UserId] [int] NOT NULL,
	[UserProfileId] [int] NOT NULL,
 CONSTRAINT [PK_RightUserProfile] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[UserProfileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

DROP TABLE [dbo].[StatusHistory];
CREATE TABLE [dbo].[StatusHistory](
	[StatusHistoryId] [int] IDENTITY(1,1) NOT NULL,
	[StatusHistoryDate] [date] NOT NULL,
	[InterventionId] [int] NOT NULL,
	[StatusIdPrev] [int] NOT NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifiedDateTime] [datetime] NULL,
	[CreatedBy] [nvarchar](20) NULL,
	[ModifiedBy] [nvarchar](20) NULL,
 CONSTRAINT [PK_StatusHistory] PRIMARY KEY CLUSTERED 
(
	[StatusHistoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

DROP TABLE [dbo].[User];
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Active] [int] NOT NULL,
	[EmployeeId] [nvarchar](20) NOT NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifiedDateTime] [datetime] NULL,
	[CreatedBy] [nvarchar](20) NULL,
	[ModifiedBy] [nvarchar](20) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

DROP TABLE [dbo].[UserProfile];
CREATE TABLE [dbo].[UserProfile](
	[UserProfileId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ProfileLevel] [int] NOT NULL,
	[SiteId] [nvarchar](13) NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifiedDateTime] [datetime] NULL,
	[CreatedBy] [nvarchar](20) NULL,
	[ModifiedBy] [nvarchar](20) NULL,
 CONSTRAINT [PK_Profile] PRIMARY KEY CLUSTERED 
(
	[UserProfileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

DROP TABLE [dbo].[VehicleGroup];
CREATE TABLE [dbo].[VehicleGroup](
	[VehicleGroupId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifiedDateTime] [datetime] NULL,
	[CreatedBy] [nvarchar](20) NULL,
	[ModifiedBy] [nvarchar](20) NULL,
 CONSTRAINT [PK_GroupVehicle] PRIMARY KEY CLUSTERED 
(
	[VehicleGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

DROP TABLE [dbo].[VehicleGroupUser];
CREATE TABLE [dbo].[VehicleGroupUser](
	[UserId] [int] NOT NULL,
	[VehicleGroupId] [int] NOT NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifiedDateTime] [datetime] NULL,
	[CreatedBy] [nvarchar](20) NULL,
	[ModifiedBy] [nvarchar](20) NULL,
 CONSTRAINT [PK_VAttribution] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[VehicleGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

DROP TABLE [dbo].[VehicleGroupVehicle];
CREATE TABLE [dbo].[VehicleGroupVehicle](
	[VehicleId] [nvarchar](13) NOT NULL,
	[VehiculeGroupId] [int] NOT NULL,
 CONSTRAINT [PK_VehicleGroupVehicle] PRIMARY KEY CLUSTERED 
(
	[VehicleId] ASC,
	[VehiculeGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

DROP TABLE [dbo].[Fuel];
CREATE TABLE [dbo].[Fuel](
	[RECID] [int] IDENTITY(1,1) NOT NULL,
	[FichePrise] [int] NOT NULL,
	[DatePrise] [datetime] NULL,
	[UserName] [nvarchar](255) NULL,
	[VehicleId] [nvarchar](13) NULL,
	[BadgeNumber] [nvarchar](50) NULL,
	[Kilometer] [float] NULL,
	[Hour] [time](7) NULL,
	[Volume] [float] NULL,
	[CuveName] [nvarchar](250) NULL,
	[BornName] [nvarchar](250) NULL,
	[ProductName] [nvarchar](250) NULL,
	[Source] [nvarchar](20) NULL,
	[RUNDATE] [datetime] NULL,
	[DateEntretien] [datetime] NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifiedDateTime] [datetime] NULL,
	[CreatedBy] [nvarchar](20) NULL,
	[ModifiedBy] [nvarchar](20) NULL,
 CONSTRAINT [PK_Fuel] PRIMARY KEY CLUSTERED 
(
	[RECID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

DROP TABLE [dbo].[InterventionType];
CREATE TABLE [dbo].[InterventionType](
	[InterventionTypeId] [int] NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
	[Status] [int] NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifiedDateTime] [datetime] NULL,
	[CreatedBy] [nvarchar](20) NULL,
	[ModifiedBy] [nvarchar](20) NULL,
 CONSTRAINT [PK_IntType] PRIMARY KEY CLUSTERED 
(
	[InterventionTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

DROP TABLE [dbo].[PrestationType];
CREATE TABLE [dbo].[PrestationType](
	[PrestationTypeId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Status] [int] NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifiedDateTime] [datetime] NULL,
	[CreatedBy] [nvarchar](20) NULL,
	[ModifiedBy] [nvarchar](20) NULL,
 CONSTRAINT [PK_PresType] PRIMARY KEY CLUSTERED 
(
	[PrestationTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

DROP TABLE [dbo].[RecentStatement];
CREATE TABLE [dbo].[RecentStatement](
	[VehicleId] [nvarchar](13) NOT NULL,
	[Hour] [int] NULL,
	[Kilometer] [int] NULL,
 CONSTRAINT [PK_RecentStatement] PRIMARY KEY CLUSTERED 
(
	[VehicleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

DROP TABLE [dbo].[Status];
CREATE TABLE [dbo].[Status](
	[StatusId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Status] [int] NULL,
	[BroadcastTextID] [int] NOT NULL CONSTRAINT [DF_Status_BroadcastTextID]  DEFAULT ((0)),
	[CreatedDateTime] [datetime] NULL,
	[ModifiedDateTime] [datetime] NULL,
	[CreatedBy] [nvarchar](20) NULL,
	[ModifiedBy] [nvarchar](20) NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[StatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

DROP TABLE [dbo].[StockOut];
CREATE TABLE [dbo].[StockOut](
	[RECID] [bigint] NOT NULL,
	[StockOutNum] [int] NOT NULL,
	[StockOutDate] [date] NOT NULL,
	[ItemID] [nvarchar](40) NOT NULL,
	[DIm1] [nvarchar](13) NOT NULL,
	[Dim2] [nvarchar](13) NOT NULL,
	[Dim3] [nvarchar](13) NOT NULL,
	[ItemDesc] [nvarchar](70) NOT NULL,
	[Unit] [nvarchar](13) NOT NULL,
	[ShopActivityID] [nvarchar](30) NOT NULL,
	[CompanyID] [nvarchar](4) NOT NULL,
	[VehicleId] [nvarchar](13) NOT NULL,
	[Qty] [numeric](28, 12) NOT NULL,
	[StoremanID] [nvarchar](20) NOT NULL,
	[TakeID] [nvarchar](20) NOT NULL,
	[UnitPrice] [numeric](28, 12) NOT NULL,
	[BarCode] [nvarchar](80) NOT NULL,
	[SiteID] [nvarchar](13) NOT NULL,
	[Source] [nvarchar](20) NOT NULL,
	[RUNDATE] [date] NOT NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifiedDateTime] [datetime] NULL,
	[CreatedBy] [nvarchar](20) NULL,
	[ModifiedBy] [nvarchar](20) NULL,
 CONSTRAINT [PK_StockOut] PRIMARY KEY CLUSTERED 
(
	[RECID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

DROP TABLE [dbo].[Tires];
CREATE TABLE [dbo].[Tires](
	[RECID] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [nvarchar](4) NOT NULL,
	[ItemId] [nvarchar](40) NOT NULL,
	[Dim1] [nvarchar](13) NOT NULL,
	[Dim2] [nvarchar](13) NOT NULL,
	[Dim3] [nvarchar](13) NOT NULL,
	[BarCode] [nvarchar](80) NOT NULL,
	[Comment] [nvarchar](70) NULL,
	[RUNDATE] [date] NULL CONSTRAINT [DF_Tires_RUNDATE]  DEFAULT (getdate()),
	[ItemType] [nvarchar](50) NULL,
 CONSTRAINT [PK_Tires] PRIMARY KEY CLUSTERED 
(
	[RECID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

DROP TABLE [dbo].[Vehicles];
CREATE TABLE [dbo].[Vehicles](
	[VehicleId] [nvarchar](13) NOT NULL,
	[CompanyId] [nvarchar](4) NOT NULL,
	[AssetID] [nvarchar](20) NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
	[SerialNum] [nvarchar](40) NOT NULL,
	[VehicleType] [nvarchar](20) NULL,
	[Make] [nvarchar](60) NOT NULL,
	[Model] [nvarchar](60) NOT NULL,
	[Responsible] [nvarchar](20) NULL,
	[MajorType] [nvarchar](20) NOT NULL,
	[PaintNumber] [nvarchar](10) NOT NULL,
	[ModelYear] [nvarchar](10) NOT NULL,
	[TechInfo1] [nvarchar](254) NOT NULL,
	[TechInfo2] [nvarchar](254) NOT NULL,
	[TechInfo3] [nvarchar](254) NOT NULL,
	[FuelType] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[SalesDate] [datetime] NOT NULL,
	[NextControlTechn] [datetime] NOT NULL,
	[TotalCardNum] [nvarchar](20) NOT NULL,
	[BadgeEpack] [nvarchar](20) NOT NULL,
	[Department] [nvarchar](13) NOT NULL,
	[CostCenter] [nvarchar](13) NOT NULL,
	[Purpose] [nvarchar](13) NOT NULL,
	[PlateNumber] [nvarchar](13) NOT NULL,
	[PreviousPlateNumber] [nvarchar](10) NOT NULL,
	[IntervalHour] [int] NOT NULL,
	[IntervalKilometer] [int] NOT NULL,
	[DateHour] [datetime] NULL,
	[DateKilometer] [datetime] NULL,
	[RunDate] [date] NOT NULL,
	[CreatedDateTime] [datetime] NULL,
	[ModifiedDateTime] [datetime] NULL,
	[CreatedBy] [nvarchar](20) NULL,
	[ModifiedBy] [nvarchar](20) NULL,
 CONSTRAINT [PK_Vehicles] PRIMARY KEY CLUSTERED 
(
	[VehicleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY];

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

ALTER TABLE [dbo].[Intervention]  WITH CHECK ADD  CONSTRAINT [FK_Intervention_Employee] FOREIGN KEY([Submitter])
REFERENCES [dbo].[Employee] ([EmployeeId]);
ALTER TABLE [dbo].[Intervention] CHECK CONSTRAINT [FK_Intervention_Employee];
ALTER TABLE [dbo].[Intervention]  WITH CHECK ADD  CONSTRAINT [FK_Intervention_Employee1] FOREIGN KEY([Applicant])
REFERENCES [dbo].[Employee] ([EmployeeId]);
ALTER TABLE [dbo].[Intervention] CHECK CONSTRAINT [FK_Intervention_Employee1];
ALTER TABLE [dbo].[Intervention]  WITH CHECK ADD  CONSTRAINT [FK_Intervention_InterventionType] FOREIGN KEY([InterventionTypeId])
REFERENCES [dbo].[InterventionType] ([InterventionTypeId]);
ALTER TABLE [dbo].[Intervention] CHECK CONSTRAINT [FK_Intervention_InterventionType];
ALTER TABLE [dbo].[Intervention]  WITH CHECK ADD  CONSTRAINT [FK_Intervention_Site] FOREIGN KEY([SiteId])
REFERENCES [dbo].[ParmSite] ([SiteId]);
ALTER TABLE [dbo].[Intervention] CHECK CONSTRAINT [FK_Intervention_Site];
ALTER TABLE [dbo].[Intervention]  WITH CHECK ADD  CONSTRAINT [FK_Intervention_Status] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Status] ([StatusId]);
ALTER TABLE [dbo].[Intervention] CHECK CONSTRAINT [FK_Intervention_Status];
ALTER TABLE [dbo].[Intervention]  WITH CHECK ADD  CONSTRAINT [FK_Intervention_Vehicles] FOREIGN KEY([VehicleId])
REFERENCES [dbo].[Vehicles] ([VehicleId]);
ALTER TABLE [dbo].[Intervention] CHECK CONSTRAINT [FK_Intervention_Vehicles];
ALTER TABLE [dbo].[InterventionPrestationGroup]  WITH CHECK ADD  CONSTRAINT [FK_InterventionPrestationGroup_InterventionType] FOREIGN KEY([InterventionTypeId])
REFERENCES [dbo].[InterventionType] ([InterventionTypeId]);
ALTER TABLE [dbo].[InterventionPrestationGroup] CHECK CONSTRAINT [FK_InterventionPrestationGroup_InterventionType];
ALTER TABLE [dbo].[InterventionPrestationGroup]  WITH CHECK ADD  CONSTRAINT [FK_InterventionPrestationGroup_PrestationType] FOREIGN KEY([PrestationTypeId])
REFERENCES [dbo].[PrestationType] ([PrestationTypeId]);
ALTER TABLE [dbo].[InterventionPrestationGroup] CHECK CONSTRAINT [FK_InterventionPrestationGroup_PrestationType];
ALTER TABLE [dbo].[InterventionTire]  WITH CHECK ADD  CONSTRAINT [FK_InterventionTire_Intervention] FOREIGN KEY([InterventionId])
REFERENCES [dbo].[Intervention] ([InterventionId]);
ALTER TABLE [dbo].[InterventionTire] CHECK CONSTRAINT [FK_InterventionTire_Intervention];
ALTER TABLE [dbo].[InterventionTire]  WITH CHECK ADD  CONSTRAINT [FK_InterventionTire_Tires] FOREIGN KEY([NewTireBarcodeId])
REFERENCES [dbo].[Tires] ([RECID]);
ALTER TABLE [dbo].[InterventionTire] CHECK CONSTRAINT [FK_InterventionTire_Tires];
ALTER TABLE [dbo].[Prestation]  WITH CHECK ADD  CONSTRAINT [FK_Prestation_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([EmployeeId]);
ALTER TABLE [dbo].[Prestation] CHECK CONSTRAINT [FK_Prestation_Employee];
ALTER TABLE [dbo].[Prestation]  WITH CHECK ADD  CONSTRAINT [FK_Prestation_Intervention] FOREIGN KEY([InterventionId])
REFERENCES [dbo].[Intervention] ([InterventionId]);
ALTER TABLE [dbo].[Prestation] CHECK CONSTRAINT [FK_Prestation_Intervention];
ALTER TABLE [dbo].[Prestation]  WITH CHECK ADD  CONSTRAINT [FK_Prestation_Invoice] FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[Invoice] ([InvoiceId]);
ALTER TABLE [dbo].[Prestation] CHECK CONSTRAINT [FK_Prestation_Invoice];
ALTER TABLE [dbo].[Prestation]  WITH CHECK ADD  CONSTRAINT [FK_Prestation_PrestationType] FOREIGN KEY([PrestationTypeId])
REFERENCES [dbo].[PrestationType] ([PrestationTypeId]);
ALTER TABLE [dbo].[Prestation] CHECK CONSTRAINT [FK_Prestation_PrestationType];
ALTER TABLE [dbo].[RightUserProfile]  WITH CHECK ADD  CONSTRAINT [FK_RightUserProfile_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId]);
ALTER TABLE [dbo].[RightUserProfile] CHECK CONSTRAINT [FK_RightUserProfile_User];
ALTER TABLE [dbo].[RightUserProfile]  WITH CHECK ADD  CONSTRAINT [FK_RightUserProfile_UserProfile] FOREIGN KEY([UserProfileId])
REFERENCES [dbo].[UserProfile] ([UserProfileId]);
ALTER TABLE [dbo].[RightUserProfile] CHECK CONSTRAINT [FK_RightUserProfile_UserProfile];
ALTER TABLE [dbo].[StatusHistory]  WITH CHECK ADD  CONSTRAINT [FK_StatusHistory_Intervention] FOREIGN KEY([InterventionId])
REFERENCES [dbo].[Intervention] ([InterventionId]);
ALTER TABLE [dbo].[StatusHistory] CHECK CONSTRAINT [FK_StatusHistory_Intervention];
ALTER TABLE [dbo].[StatusHistory]  WITH CHECK ADD  CONSTRAINT [FK_StatusHistory_Status] FOREIGN KEY([StatusIdPrev])
REFERENCES [dbo].[Status] ([StatusId]);
ALTER TABLE [dbo].[StatusHistory] CHECK CONSTRAINT [FK_StatusHistory_Status];
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_User] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([EmployeeId]);
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_User];
ALTER TABLE [dbo].[UserProfile]  WITH CHECK ADD  CONSTRAINT [FK_UserProfile_ParmSite] FOREIGN KEY([SiteId])
REFERENCES [dbo].[ParmSite] ([SiteId]);
ALTER TABLE [dbo].[UserProfile] CHECK CONSTRAINT [FK_UserProfile_ParmSite];
ALTER TABLE [dbo].[VehicleGroupUser]  WITH CHECK ADD  CONSTRAINT [FK_VehicleGroupUser_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId]);
ALTER TABLE [dbo].[VehicleGroupUser] CHECK CONSTRAINT [FK_VehicleGroupUser_User];
ALTER TABLE [dbo].[VehicleGroupUser]  WITH CHECK ADD  CONSTRAINT [FK_VehicleGroupUser_VehicleGroup] FOREIGN KEY([VehicleGroupId])
REFERENCES [dbo].[VehicleGroup] ([VehicleGroupId]);
ALTER TABLE [dbo].[VehicleGroupUser] CHECK CONSTRAINT [FK_VehicleGroupUser_VehicleGroup];
ALTER TABLE [dbo].[VehicleGroupVehicle]  WITH CHECK ADD  CONSTRAINT [FK_VehicleGroupVehicle_VehicleGroup] FOREIGN KEY([VehiculeGroupId])
REFERENCES [dbo].[VehicleGroup] ([VehicleGroupId]);
ALTER TABLE [dbo].[VehicleGroupVehicle] CHECK CONSTRAINT [FK_VehicleGroupVehicle_VehicleGroup];
ALTER TABLE [dbo].[VehicleGroupVehicle]  WITH CHECK ADD  CONSTRAINT [FK_VehicleGroupVehicle_Vehicles] FOREIGN KEY([VehicleId])
REFERENCES [dbo].[Vehicles] ([VehicleId]);
ALTER TABLE [dbo].[VehicleGroupVehicle] CHECK CONSTRAINT [FK_VehicleGroupVehicle_Vehicles];
ALTER TABLE [dbo].[Fuel] ADD  CONSTRAINT [DF_Fuel_RUNDATE]  DEFAULT (getdate()) FOR [RUNDATE];
ALTER TABLE [dbo].[Tires] ADD  CONSTRAINT [DF_Tires_RUNDATE]  DEFAULT (getdate()) FOR [RUNDATE];
ALTER TABLE [dbo].[Fuel]  WITH CHECK ADD  CONSTRAINT [FK_Fuel_Vehicles] FOREIGN KEY([VehicleId])
REFERENCES [dbo].[Vehicles] ([VehicleId]);
ALTER TABLE [dbo].[Fuel] CHECK CONSTRAINT [FK_Fuel_Vehicles];
ALTER TABLE [dbo].[RecentStatement]  WITH CHECK ADD  CONSTRAINT [FK_RecentStatement_Vehicles] FOREIGN KEY([VehicleId])
REFERENCES [dbo].[Vehicles] ([VehicleId]);
ALTER TABLE [dbo].[RecentStatement] CHECK CONSTRAINT [FK_RecentStatement_Vehicles];
ALTER TABLE [dbo].[StockOut]  WITH CHECK ADD  CONSTRAINT [FK_StockOut_Employee] FOREIGN KEY([StoremanID])
REFERENCES [dbo].[Employee] ([EmployeeId]);
ALTER TABLE [dbo].[StockOut] CHECK CONSTRAINT [FK_StockOut_Employee];
ALTER TABLE [dbo].[StockOut]  WITH CHECK ADD  CONSTRAINT [FK_StockOut_Employee1] FOREIGN KEY([TakeID])
REFERENCES [dbo].[Employee] ([EmployeeId]);
ALTER TABLE [dbo].[StockOut] CHECK CONSTRAINT [FK_StockOut_Employee1];
ALTER TABLE [dbo].[StockOut]  WITH CHECK ADD  CONSTRAINT [FK_StockOut_Site] FOREIGN KEY([SiteID])
REFERENCES [dbo].[ParmSite] ([SiteId]);
ALTER TABLE [dbo].[StockOut] CHECK CONSTRAINT [FK_StockOut_Site];
ALTER TABLE [dbo].[StockOut]  WITH CHECK ADD  CONSTRAINT [FK_StockOut_Vehicles] FOREIGN KEY([VehicleId])
REFERENCES [dbo].[Vehicles] ([VehicleId]);
ALTER TABLE [dbo].[StockOut] CHECK CONSTRAINT [FK_StockOut_Vehicles];
ALTER TABLE [dbo].[Vehicles]  WITH CHECK ADD  CONSTRAINT [FK_Vehicles_Employee] FOREIGN KEY([Responsible])
REFERENCES [dbo].[Employee] ([EmployeeId]);
ALTER TABLE [dbo].[Vehicles] CHECK CONSTRAINT [FK_Vehicles_Employee];
ALTER TABLE [dbo].[UserProfile]  WITH CHECK ADD  CONSTRAINT [FK_UserProfile_ProfileLevelType] FOREIGN KEY([ProfileLevel])
REFERENCES [dbo].[ProfileLevelType] ([ProfileLevelTypeId]);
ALTER TABLE [dbo].[UserProfile] CHECK CONSTRAINT [FK_UserProfile_ProfileLevelType];
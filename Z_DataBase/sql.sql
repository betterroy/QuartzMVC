USE [QuartzMVC]
GO
/****** Object:  Table [dbo].[User]    Script Date: 06/14/2017 13:07:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserNmae] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[Age] [int] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON
INSERT [dbo].[User] ([Id], [UserNmae], [Password], [Age]) VALUES (202, N'demo', N'123', 25)
INSERT [dbo].[User] ([Id], [UserNmae], [Password], [Age]) VALUES (203, N'demo', N'123', 25)
SET IDENTITY_INSERT [dbo].[User] OFF
/****** Object:  Table [dbo].[QRTZ_TRIGGERS]    Script Date: 06/14/2017 13:07:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QRTZ_TRIGGERS](
	[SCHED_NAME] [nvarchar](100) NOT NULL,
	[TRIGGER_NAME] [nvarchar](150) NOT NULL,
	[TRIGGER_GROUP] [nvarchar](150) NOT NULL,
	[JOB_NAME] [nvarchar](150) NOT NULL,
	[JOB_GROUP] [nvarchar](150) NOT NULL,
	[DESCRIPTION] [nvarchar](250) NULL,
	[NEXT_FIRE_TIME] [bigint] NULL,
	[PREV_FIRE_TIME] [bigint] NULL,
	[PRIORITY] [int] NULL,
	[TRIGGER_STATE] [nvarchar](16) NOT NULL,
	[TRIGGER_TYPE] [nvarchar](8) NOT NULL,
	[START_TIME] [bigint] NOT NULL,
	[END_TIME] [bigint] NULL,
	[CALENDAR_NAME] [nvarchar](200) NULL,
	[MISFIRE_INSTR] [int] NULL,
	[JOB_DATA] [image] NULL,
 CONSTRAINT [PK_QRTZ_TRIGGERS] PRIMARY KEY CLUSTERED 
(
	[SCHED_NAME] ASC,
	[TRIGGER_NAME] ASC,
	[TRIGGER_GROUP] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[QRTZ_TRIGGERS] ([SCHED_NAME], [TRIGGER_NAME], [TRIGGER_GROUP], [JOB_NAME], [JOB_GROUP], [DESCRIPTION], [NEXT_FIRE_TIME], [PREV_FIRE_TIME], [PRIORITY], [TRIGGER_STATE], [TRIGGER_TYPE], [START_TIME], [END_TIME], [CALENDAR_NAME], [MISFIRE_INSTR], [JOB_DATA]) VALUES (N'QuartzScheduler', N'user触发器', N'userAser', N'测试添加user表', N'添加', NULL, 636328560010000000, NULL, 1, N'WAITING', N'CRON', 636328560010000000, NULL, NULL, 0, NULL)
INSERT [dbo].[QRTZ_TRIGGERS] ([SCHED_NAME], [TRIGGER_NAME], [TRIGGER_GROUP], [JOB_NAME], [JOB_GROUP], [DESCRIPTION], [NEXT_FIRE_TIME], [PREV_FIRE_TIME], [PRIORITY], [TRIGGER_STATE], [TRIGGER_TYPE], [START_TIME], [END_TIME], [CALENDAR_NAME], [MISFIRE_INSTR], [JOB_DATA]) VALUES (N'QuartzScheduler', N'触发器名', N'触发器组', N'定时更新user表', N'数据库操作', NULL, 636328555350000000, NULL, 1, N'WAITING', N'CRON', 636328555350000000, NULL, NULL, 0, NULL)
INSERT [dbo].[QRTZ_TRIGGERS] ([SCHED_NAME], [TRIGGER_NAME], [TRIGGER_GROUP], [JOB_NAME], [JOB_GROUP], [DESCRIPTION], [NEXT_FIRE_TIME], [PREV_FIRE_TIME], [PRIORITY], [TRIGGER_STATE], [TRIGGER_TYPE], [START_TIME], [END_TIME], [CALENDAR_NAME], [MISFIRE_INSTR], [JOB_DATA]) VALUES (N'QuartzScheduler', N'触发器名称', N'触发器分组', N'作业名称', N'作业分组', NULL, 636325819910000000, 636325819900000000, 1, N'WAITING', N'CRON', 636325074590000000, NULL, NULL, 0, NULL)
/****** Object:  Table [dbo].[QRTZ_SIMPROP_TRIGGERS]    Script Date: 06/14/2017 13:07:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QRTZ_SIMPROP_TRIGGERS](
	[SCHED_NAME] [nvarchar](100) NOT NULL,
	[TRIGGER_NAME] [nvarchar](150) NOT NULL,
	[TRIGGER_GROUP] [nvarchar](150) NOT NULL,
	[STR_PROP_1] [nvarchar](512) NULL,
	[STR_PROP_2] [nvarchar](512) NULL,
	[STR_PROP_3] [nvarchar](512) NULL,
	[INT_PROP_1] [int] NULL,
	[INT_PROP_2] [int] NULL,
	[LONG_PROP_1] [bigint] NULL,
	[LONG_PROP_2] [bigint] NULL,
	[DEC_PROP_1] [numeric](13, 4) NULL,
	[DEC_PROP_2] [numeric](13, 4) NULL,
	[BOOL_PROP_1] [bit] NULL,
	[BOOL_PROP_2] [bit] NULL,
 CONSTRAINT [PK_QRTZ_SIMPROP_TRIGGERS] PRIMARY KEY CLUSTERED 
(
	[SCHED_NAME] ASC,
	[TRIGGER_NAME] ASC,
	[TRIGGER_GROUP] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QRTZ_SIMPLE_TRIGGERS]    Script Date: 06/14/2017 13:07:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QRTZ_SIMPLE_TRIGGERS](
	[SCHED_NAME] [nvarchar](100) NOT NULL,
	[TRIGGER_NAME] [nvarchar](150) NOT NULL,
	[TRIGGER_GROUP] [nvarchar](150) NOT NULL,
	[REPEAT_COUNT] [int] NOT NULL,
	[REPEAT_INTERVAL] [bigint] NOT NULL,
	[TIMES_TRIGGERED] [int] NOT NULL,
 CONSTRAINT [PK_QRTZ_SIMPLE_TRIGGERS] PRIMARY KEY CLUSTERED 
(
	[SCHED_NAME] ASC,
	[TRIGGER_NAME] ASC,
	[TRIGGER_GROUP] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QRTZ_SCHEDULER_STATE]    Script Date: 06/14/2017 13:07:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QRTZ_SCHEDULER_STATE](
	[SCHED_NAME] [nvarchar](100) NOT NULL,
	[INSTANCE_NAME] [nvarchar](200) NOT NULL,
	[LAST_CHECKIN_TIME] [bigint] NOT NULL,
	[CHECKIN_INTERVAL] [bigint] NOT NULL,
 CONSTRAINT [PK_QRTZ_SCHEDULER_STATE] PRIMARY KEY CLUSTERED 
(
	[SCHED_NAME] ASC,
	[INSTANCE_NAME] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[QRTZ_SCHEDULER_STATE] ([SCHED_NAME], [INSTANCE_NAME], [LAST_CHECKIN_TIME], [CHECKIN_INTERVAL]) VALUES (N'QuartzScheduler', N'SKY-20170324TRO636325819354075335', 636325819881035475, 7500)
/****** Object:  Table [dbo].[QRTZ_PAUSED_TRIGGER_GRPS]    Script Date: 06/14/2017 13:07:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QRTZ_PAUSED_TRIGGER_GRPS](
	[SCHED_NAME] [nvarchar](100) NOT NULL,
	[TRIGGER_GROUP] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK_QRTZ_PAUSED_TRIGGER_GRPS] PRIMARY KEY CLUSTERED 
(
	[SCHED_NAME] ASC,
	[TRIGGER_GROUP] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QRTZ_LOCKS]    Script Date: 06/14/2017 13:07:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QRTZ_LOCKS](
	[SCHED_NAME] [nvarchar](100) NOT NULL,
	[LOCK_NAME] [nvarchar](40) NOT NULL,
 CONSTRAINT [PK_QRTZ_LOCKS] PRIMARY KEY CLUSTERED 
(
	[SCHED_NAME] ASC,
	[LOCK_NAME] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[QRTZ_LOCKS] ([SCHED_NAME], [LOCK_NAME]) VALUES (N'QuartzScheduler', N'STATE_ACCESS')
INSERT [dbo].[QRTZ_LOCKS] ([SCHED_NAME], [LOCK_NAME]) VALUES (N'QuartzScheduler', N'TRIGGER_ACCESS')
/****** Object:  Table [dbo].[QRTZ_JOB_DETAILS]    Script Date: 06/14/2017 13:07:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QRTZ_JOB_DETAILS](
	[SCHED_NAME] [nvarchar](100) NOT NULL,
	[JOB_NAME] [nvarchar](150) NOT NULL,
	[JOB_GROUP] [nvarchar](150) NOT NULL,
	[DESCRIPTION] [nvarchar](250) NULL,
	[JOB_CLASS_NAME] [nvarchar](250) NOT NULL,
	[IS_DURABLE] [bit] NOT NULL,
	[IS_NONCONCURRENT] [bit] NOT NULL,
	[IS_UPDATE_DATA] [bit] NOT NULL,
	[REQUESTS_RECOVERY] [bit] NOT NULL,
	[JOB_DATA] [image] NULL,
 CONSTRAINT [PK_QRTZ_JOB_DETAILS] PRIMARY KEY CLUSTERED 
(
	[SCHED_NAME] ASC,
	[JOB_NAME] ASC,
	[JOB_GROUP] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[QRTZ_JOB_DETAILS] ([SCHED_NAME], [JOB_NAME], [JOB_GROUP], [DESCRIPTION], [JOB_CLASS_NAME], [IS_DURABLE], [IS_NONCONCURRENT], [IS_UPDATE_DATA], [REQUESTS_RECOVERY], [JOB_DATA]) VALUES (N'QuartzScheduler', N'测试添加user表', N'添加', NULL, N'QuartzMVC.Job.CronJob, QuartzMVC', 1, 0, 0, 0, NULL)
INSERT [dbo].[QRTZ_JOB_DETAILS] ([SCHED_NAME], [JOB_NAME], [JOB_GROUP], [DESCRIPTION], [JOB_CLASS_NAME], [IS_DURABLE], [IS_NONCONCURRENT], [IS_UPDATE_DATA], [REQUESTS_RECOVERY], [JOB_DATA]) VALUES (N'QuartzScheduler', N'定时更新user表', N'数据库操作', NULL, N'QuartzMVC.Job.AddUserJob, QuartzMVC', 1, 0, 0, 0, NULL)
INSERT [dbo].[QRTZ_JOB_DETAILS] ([SCHED_NAME], [JOB_NAME], [JOB_GROUP], [DESCRIPTION], [JOB_CLASS_NAME], [IS_DURABLE], [IS_NONCONCURRENT], [IS_UPDATE_DATA], [REQUESTS_RECOVERY], [JOB_DATA]) VALUES (N'QuartzScheduler', N'作业名称', N'作业分组', NULL, N'QuartzMVC.Job.FirstJob, QuartzMVC', 1, 0, 0, 0, NULL)
/****** Object:  Table [dbo].[QRTZ_FIRED_TRIGGERS]    Script Date: 06/14/2017 13:07:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QRTZ_FIRED_TRIGGERS](
	[SCHED_NAME] [nvarchar](100) NOT NULL,
	[ENTRY_ID] [nvarchar](95) NOT NULL,
	[TRIGGER_NAME] [nvarchar](150) NOT NULL,
	[TRIGGER_GROUP] [nvarchar](150) NOT NULL,
	[INSTANCE_NAME] [nvarchar](200) NOT NULL,
	[FIRED_TIME] [bigint] NOT NULL,
	[SCHED_TIME] [bigint] NOT NULL,
	[PRIORITY] [int] NOT NULL,
	[STATE] [nvarchar](16) NOT NULL,
	[JOB_NAME] [nvarchar](150) NULL,
	[JOB_GROUP] [nvarchar](150) NULL,
	[IS_NONCONCURRENT] [bit] NULL,
	[REQUESTS_RECOVERY] [bit] NULL,
 CONSTRAINT [PK_QRTZ_FIRED_TRIGGERS] PRIMARY KEY CLUSTERED 
(
	[SCHED_NAME] ASC,
	[ENTRY_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[QRTZ_FIRED_TRIGGERS] ([SCHED_NAME], [ENTRY_ID], [TRIGGER_NAME], [TRIGGER_GROUP], [INSTANCE_NAME], [FIRED_TIME], [SCHED_TIME], [PRIORITY], [STATE], [JOB_NAME], [JOB_GROUP], [IS_NONCONCURRENT], [REQUESTS_RECOVERY]) VALUES (N'QuartzScheduler', N'SKY-20170324TRO636325819354075335636325819354035583', N'user触发器', N'userAser', N'SKY-20170324TRO636325819354075335', 636325819902406698, 636325819910000000, 1, N'ACQUIRED', NULL, NULL, 0, 0)
/****** Object:  Table [dbo].[QRTZ_CRON_TRIGGERS]    Script Date: 06/14/2017 13:07:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QRTZ_CRON_TRIGGERS](
	[SCHED_NAME] [nvarchar](100) NOT NULL,
	[TRIGGER_NAME] [nvarchar](150) NOT NULL,
	[TRIGGER_GROUP] [nvarchar](150) NOT NULL,
	[CRON_EXPRESSION] [nvarchar](120) NOT NULL,
	[TIME_ZONE_ID] [nvarchar](80) NULL,
 CONSTRAINT [PK_QRTZ_CRON_TRIGGERS] PRIMARY KEY CLUSTERED 
(
	[SCHED_NAME] ASC,
	[TRIGGER_NAME] ASC,
	[TRIGGER_GROUP] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[QRTZ_CRON_TRIGGERS] ([SCHED_NAME], [TRIGGER_NAME], [TRIGGER_GROUP], [CRON_EXPRESSION], [TIME_ZONE_ID]) VALUES (N'QuartzScheduler', N'user触发器', N'userAser', N'* * * * * ?', N'China Standard Time')
INSERT [dbo].[QRTZ_CRON_TRIGGERS] ([SCHED_NAME], [TRIGGER_NAME], [TRIGGER_GROUP], [CRON_EXPRESSION], [TIME_ZONE_ID]) VALUES (N'QuartzScheduler', N'触发器名', N'触发器组', N'0/5 * * * * ? ', N'China Standard Time')
INSERT [dbo].[QRTZ_CRON_TRIGGERS] ([SCHED_NAME], [TRIGGER_NAME], [TRIGGER_GROUP], [CRON_EXPRESSION], [TIME_ZONE_ID]) VALUES (N'QuartzScheduler', N'触发器名称', N'触发器分组', N'* * * * * ?', N'China Standard Time')
/****** Object:  Table [dbo].[QRTZ_CALENDARS]    Script Date: 06/14/2017 13:07:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QRTZ_CALENDARS](
	[SCHED_NAME] [nvarchar](100) NOT NULL,
	[CALENDAR_NAME] [nvarchar](200) NOT NULL,
	[CALENDAR] [image] NOT NULL,
 CONSTRAINT [PK_QRTZ_CALENDARS] PRIMARY KEY CLUSTERED 
(
	[SCHED_NAME] ASC,
	[CALENDAR_NAME] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QRTZ_BLOB_TRIGGERS]    Script Date: 06/14/2017 13:07:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QRTZ_BLOB_TRIGGERS](
	[SCHED_NAME] [nvarchar](100) NOT NULL,
	[TRIGGER_NAME] [nvarchar](150) NOT NULL,
	[TRIGGER_GROUP] [nvarchar](150) NOT NULL,
	[BLOB_DATA] [image] NULL,
 CONSTRAINT [PK_QRTZ_BLOB_TRIGGERS] PRIMARY KEY CLUSTERED 
(
	[SCHED_NAME] ASC,
	[TRIGGER_NAME] ASC,
	[TRIGGER_GROUP] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobQuartz]    Script Date: 06/14/2017 13:07:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[JobQuartz](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[JobGroup] [varchar](50) NOT NULL,
	[JobName] [varchar](50) NOT NULL,
	[JobDescription] [varchar](50) NOT NULL,
	[TriggerName] [varchar](50) NOT NULL,
	[TriggerGroupName] [varchar](50) NOT NULL,
	[TriggerType] [varchar](50) NOT NULL,
	[TriggerState] [varchar](50) NOT NULL,
	[NextFireTime] [datetime] NULL,
	[PreviousFireTime] [datetime] NULL,
	[UserName] [varchar](50) NOT NULL,
	[Addtime] [datetime] NOT NULL,
	[QuartzTime] [varchar](50) NULL,
 CONSTRAINT [PK_JobQuartz] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'作业分组' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'JobQuartz', @level2type=N'COLUMN',@level2name=N'JobGroup'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'作业名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'JobQuartz', @level2type=N'COLUMN',@level2name=N'JobName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'作业描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'JobQuartz', @level2type=N'COLUMN',@level2name=N'JobDescription'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'触发器名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'JobQuartz', @level2type=N'COLUMN',@level2name=N'TriggerName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'触发器分组' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'JobQuartz', @level2type=N'COLUMN',@level2name=N'TriggerGroupName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'触发器类别' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'JobQuartz', @level2type=N'COLUMN',@level2name=N'TriggerType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'触发器状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'JobQuartz', @level2type=N'COLUMN',@level2name=N'TriggerState'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'下一次运行时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'JobQuartz', @level2type=N'COLUMN',@level2name=N'NextFireTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上一次运行时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'JobQuartz', @level2type=N'COLUMN',@level2name=N'PreviousFireTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'添加人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'JobQuartz', @level2type=N'COLUMN',@level2name=N'UserName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'添加时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'JobQuartz', @level2type=N'COLUMN',@level2name=N'Addtime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'运行时间周期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'JobQuartz', @level2type=N'COLUMN',@level2name=N'QuartzTime'
GO
SET IDENTITY_INSERT [dbo].[JobQuartz] ON
INSERT [dbo].[JobQuartz] ([Id], [JobGroup], [JobName], [JobDescription], [TriggerName], [TriggerGroupName], [TriggerType], [TriggerState], [NextFireTime], [PreviousFireTime], [UserName], [Addtime], [QuartzTime]) VALUES (30, N'添加', N'测试添加user表', N'添加user表记录', N'user触发器', N'userAser', N'CronJob', N'未开', NULL, NULL, N'管理员', CAST(0x0000A790011D7BFC AS DateTime), N'* * * * * ?')
INSERT [dbo].[JobQuartz] ([Id], [JobGroup], [JobName], [JobDescription], [TriggerName], [TriggerGroupName], [TriggerType], [TriggerState], [NextFireTime], [PreviousFireTime], [UserName], [Addtime], [QuartzTime]) VALUES (36, N'作业分组', N'作业名称', N'作业描述', N'触发器名称', N'触发器分组', N'FirstJob', N'触发器状态', NULL, NULL, N'管理员', CAST(0x0000A78C01102D58 AS DateTime), N'* * * * * ?')
INSERT [dbo].[JobQuartz] ([Id], [JobGroup], [JobName], [JobDescription], [TriggerName], [TriggerGroupName], [TriggerType], [TriggerState], [NextFireTime], [PreviousFireTime], [UserName], [Addtime], [QuartzTime]) VALUES (37, N'数据库操作', N'定时更新user表', N'更新user表数据', N'触发器名', N'触发器组', N'AddUserJob', N'开', NULL, NULL, N'管理员', CAST(0x0000A790011B7F64 AS DateTime), N'0/5 * * * * ? ')
SET IDENTITY_INSERT [dbo].[JobQuartz] OFF
/****** Object:  Table [dbo].[File]    Script Date: 06/14/2017 13:07:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[File](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Url] [varchar](100) NOT NULL,
	[Size] [varchar](10) NOT NULL,
	[SuffixName] [varchar](10) NOT NULL,
	[AddTime] [datetime] NOT NULL,
	[UserName] [varchar](20) NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[File] ON
INSERT [dbo].[File] ([Id], [Name], [Url], [Size], [SuffixName], [AddTime], [UserName]) VALUES (15, N'111', N'222', N'222', N'333', CAST(0x0000A72D00000000 AS DateTime), N'11111')
SET IDENTITY_INSERT [dbo].[File] OFF

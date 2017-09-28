CREATE DATABASE Elections;

USE [Elections]
GO
/****** Object:  Table [dbo].[Candidates]    Script Date: 2017-09-28 01:22:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Candidates](
	[idcandidates] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](90) NOT NULL,
	[party] [nvarchar](45) NOT NULL,
 CONSTRAINT [PK_candidates] PRIMARY KEY CLUSTERED 
(
	[idcandidates] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Electorate]    Script Date: 2017-09-28 01:22:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Electorate](
	[idelectorate] [int] IDENTITY(1,1) NOT NULL,
	[name] [nchar](128) NOT NULL,
	[surname] [nchar](128) NOT NULL,
	[pesel] [nchar](128) NOT NULL,
	[voted] [tinyint] NOT NULL,
	[logged] [tinyint] NOT NULL,
 CONSTRAINT [PK_electorate] PRIMARY KEY CLUSTERED 
(
	[idelectorate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Loginattemps]    Script Date: 2017-09-28 01:22:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Loginattemps](
	[idloginattemps] [int] IDENTITY(1,1) NOT NULL,
	[date] [datetime] NOT NULL,
	[pesel] [nchar](128) NULL,
	[succesful] [tinyint] NOT NULL,
	[valid] [tinyint] NOT NULL,
 CONSTRAINT [PK_loginattemps] PRIMARY KEY CLUSTERED 
(
	[idloginattemps] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Votes]    Script Date: 2017-09-28 01:22:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Votes](
	[idvotes] [int] IDENTITY(1,1) NOT NULL,
	[date] [datetime] NOT NULL,
	[idcandidate] [int] NULL,
	[valid] [tinyint] NOT NULL,
	[withRights] [tinyint] NOT NULL,
 CONSTRAINT [PK_votes] PRIMARY KEY CLUSTERED 
(
	[idvotes] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Candidates] ON 

GO
INSERT [dbo].[Candidates] ([idcandidates], [name], [party]) VALUES (1, N'Mieszko I', N'Piastowie')
GO
INSERT [dbo].[Candidates] ([idcandidates], [name], [party]) VALUES (2, N'Bolesław Chrobry', N'Piastowie')
GO
INSERT [dbo].[Candidates] ([idcandidates], [name], [party]) VALUES (3, N'Władysław Łokietek', N'Piastowie')
GO
INSERT [dbo].[Candidates] ([idcandidates], [name], [party]) VALUES (4, N'Kazimierz Wielki', N'Piastowie')
GO
INSERT [dbo].[Candidates] ([idcandidates], [name], [party]) VALUES (5, N'Władysław Jagiełło', N'Dynastia Jagiellonów')
GO
INSERT [dbo].[Candidates] ([idcandidates], [name], [party]) VALUES (6, N'Władysław Warneńczyk', N'Dynastia Jagiellonów')
GO
INSERT [dbo].[Candidates] ([idcandidates], [name], [party]) VALUES (7, N'Zygmunt Stary', N'Dynastia Jagiellonów')
GO
INSERT [dbo].[Candidates] ([idcandidates], [name], [party]) VALUES (8, N'Henryk Walezy', N'Elekcyjni dla Polski')
GO
INSERT [dbo].[Candidates] ([idcandidates], [name], [party]) VALUES (9, N'Anna Jagiellonka', N'Elekcyjni dla Polski')
GO
INSERT [dbo].[Candidates] ([idcandidates], [name], [party]) VALUES (10, N'Stefan Batory', N'Elekcyjni dla Polski')
GO
INSERT [dbo].[Candidates] ([idcandidates], [name], [party]) VALUES (11, N'Zygmunt Waza', N'Wazowie')
GO
INSERT [dbo].[Candidates] ([idcandidates], [name], [party]) VALUES (12, N'Władysław Waza', N'Wazowie')
GO
INSERT [dbo].[Candidates] ([idcandidates], [name], [party]) VALUES (13, N'Jan Kazimierz', N'Wazowie')
GO
SET IDENTITY_INSERT [dbo].[Candidates] OFF
GO
SET IDENTITY_INSERT [dbo].[Electorate] ON 

GO
INSERT [dbo].[Electorate] ([idelectorate], [name], [surname], [pesel], [voted], [logged]) VALUES (1, N'A90E4CC3DD145E37944EF427195098702A2C26A6CDD2A891360E5AB3144685E9C2864CBC90EA0E1D2DC59D59212BD4FBBF3F44A4941EBEEB52DFF3A958944D07', N'DC8976934BDE4A3BA0F39E75762163B1C2447FE831F13CE49F7375B8C86A77E8289EB7D9F349F5FCBAA95FF45DB23003BC07B33EC95D62699E5F2A0BFA514E80', N'551236E8DB02F506FEDE83C52C29DF274FDD101A2A4BD261FD3D8666F9F0FC2EA96848123621EC7A28C03575DD602D19753A3E5BE11B945791BF8FE2A8B8F24E', 0, 0)
GO
SET IDENTITY_INSERT [dbo].[Electorate] OFF
GO
SET IDENTITY_INSERT [dbo].[Loginattemps] ON 


GO
INSERT [dbo].[Loginattemps] ([idloginattemps], [date], [pesel], [succesful], [valid]) VALUES (78, CAST(N'2017-09-28 01:09:20.667' AS DateTime), N'551236E8DB02F506FEDE83C52C29DF274FDD101A2A4BD261FD3D8666F9F0FC2EA96848123621EC7A28C03575DD602D19753A3E5BE11B945791BF8FE2A8B8F24E', 0, 0)
GO
INSERT [dbo].[Loginattemps] ([idloginattemps], [date], [pesel], [succesful], [valid]) VALUES (79, CAST(N'2017-09-28 01:09:25.733' AS DateTime), N'551236E8DB02F506FEDE83C52C29DF274FDD101A2A4BD261FD3D8666F9F0FC2EA96848123621EC7A28C03575DD602D19753A3E5BE11B945791BF8FE2A8B8F24E', 0, 1)
GO
INSERT [dbo].[Loginattemps] ([idloginattemps], [date], [pesel], [succesful], [valid]) VALUES (80, CAST(N'2017-09-28 01:09:35.170' AS DateTime), N'551236E8DB02F506FEDE83C52C29DF274FDD101A2A4BD261FD3D8666F9F0FC2EA96848123621EC7A28C03575DD602D19753A3E5BE11B945791BF8FE2A8B8F24E', 1, 1)
GO
INSERT [dbo].[Loginattemps] ([idloginattemps], [date], [pesel], [succesful], [valid]) VALUES (81, CAST(N'2017-09-28 01:20:03.597' AS DateTime), N'551236E8DB02F506FEDE83C52C29DF274FDD101A2A4BD261FD3D8666F9F0FC2EA96848123621EC7A28C03575DD602D19753A3E5BE11B945791BF8FE2A8B8F24E', 1, 1)
GO
INSERT [dbo].[Loginattemps] ([idloginattemps], [date], [pesel], [succesful], [valid]) VALUES (82, CAST(N'2017-09-28 01:20:11.957' AS DateTime), N'551236E8DB02F506FEDE83C52C29DF274FDD101A2A4BD261FD3D8666F9F0FC2EA96848123621EC7A28C03575DD602D19753A3E5BE11B945791BF8FE2A8B8F24E', 0, 0)
GO
SET IDENTITY_INSERT [dbo].[Loginattemps] OFF
GO
SET IDENTITY_INSERT [dbo].[Votes] ON 

GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (20, CAST(N'2017-09-27 14:14:16.407' AS DateTime), 1, 1, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (21, CAST(N'2017-09-27 14:16:28.320' AS DateTime), 6, 1, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (22, CAST(N'2017-09-27 14:17:38.150' AS DateTime), 10, 1, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (23, CAST(N'2017-09-27 14:28:11.943' AS DateTime), 13, 1, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (24, CAST(N'2017-09-27 14:29:59.477' AS DateTime), 8, 1, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (25, CAST(N'2017-09-27 14:31:32.250' AS DateTime), 8, 1, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (26, CAST(N'2017-09-27 14:38:39.943' AS DateTime), 9, 1, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (27, CAST(N'2017-09-27 14:39:28.817' AS DateTime), 4, 1, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (28, CAST(N'2017-09-27 14:40:03.320' AS DateTime), 11, 1, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (29, CAST(N'2017-09-27 14:44:54.197' AS DateTime), 2, 1, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (30, CAST(N'2017-09-27 14:46:23.097' AS DateTime), 4, 1, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (31, CAST(N'2017-09-27 14:47:07.527' AS DateTime), 10, 1, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (32, CAST(N'2017-09-27 14:52:07.107' AS DateTime), 12, 1, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (33, CAST(N'2017-09-27 15:10:25.550' AS DateTime), 5, 1, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (34, CAST(N'2017-09-27 15:16:09.743' AS DateTime), 5, 1, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (35, CAST(N'2017-09-27 15:17:01.870' AS DateTime), 8, 1, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (36, CAST(N'2017-09-27 15:21:05.123' AS DateTime), 8, 1, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (37, CAST(N'2017-09-27 15:22:08.360' AS DateTime), 8, 1, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (38, CAST(N'2017-09-27 15:23:08.180' AS DateTime), 11, 1, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (39, CAST(N'2017-09-27 15:27:44.933' AS DateTime), 8, 1, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (40, CAST(N'2017-09-27 15:30:08.067' AS DateTime), 12, 1, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (41, CAST(N'2017-09-27 15:42:54.827' AS DateTime), 9, 1, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (42, CAST(N'2017-09-27 15:43:50.407' AS DateTime), 8, 1, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (43, CAST(N'2017-09-27 15:45:02.473' AS DateTime), 9, 1, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (44, CAST(N'2017-09-27 15:45:55.370' AS DateTime), 8, 1, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (45, CAST(N'2017-09-27 15:47:24.733' AS DateTime), 12, 1, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (46, CAST(N'2017-09-27 15:47:50.403' AS DateTime), NULL, 0, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (47, CAST(N'2017-09-27 15:53:26.617' AS DateTime), 9, 1, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (48, CAST(N'2017-09-27 15:58:02.363' AS DateTime), 4, 1, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (49, CAST(N'2017-09-27 15:58:35.077' AS DateTime), 10, 1, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (50, CAST(N'2017-09-27 15:59:29.570' AS DateTime), NULL, 0, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (51, CAST(N'2017-09-27 16:01:50.327' AS DateTime), 2, 1, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (52, CAST(N'2017-09-27 16:02:27.040' AS DateTime), 5, 1, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (53, CAST(N'2017-09-27 16:04:59.787' AS DateTime), 4, 1, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (54, CAST(N'2017-09-27 16:09:54.987' AS DateTime), 6, 1, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (55, CAST(N'2017-09-27 16:12:02.770' AS DateTime), 1, 1, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (56, CAST(N'2017-09-27 16:16:49.803' AS DateTime), NULL, 0, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (57, CAST(N'2017-09-27 16:19:52.330' AS DateTime), 2, 1, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (58, CAST(N'2017-09-27 16:21:28.803' AS DateTime), 1, 1, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (59, CAST(N'2017-09-27 16:22:07.973' AS DateTime), NULL, 0, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (60, CAST(N'2017-09-27 16:22:56.167' AS DateTime), NULL, 0, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (61, CAST(N'2017-09-27 16:24:25.483' AS DateTime), 1, 1, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (66, CAST(N'2017-09-27 19:18:21.430' AS DateTime), 11, 1, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (67, CAST(N'2017-09-27 19:19:12.237' AS DateTime), 8, 1, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (82, CAST(N'2017-09-27 20:35:44.833' AS DateTime), 3, 1, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (86, CAST(N'2017-09-27 20:46:51.857' AS DateTime), 7, 1, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (93, CAST(N'2017-09-27 21:10:15.883' AS DateTime), 10, 1, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (94, CAST(N'2017-09-27 21:32:40.410' AS DateTime), 9, 1, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (95, CAST(N'2017-09-27 21:35:21.437' AS DateTime), 6, 1, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (107, CAST(N'2017-09-27 22:33:44.627' AS DateTime), 1, 1, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (140, CAST(N'2017-09-28 00:11:44.080' AS DateTime), 4, 1, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (141, CAST(N'2017-09-28 00:12:20.013' AS DateTime), 9, 1, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (142, CAST(N'2017-09-28 00:25:36.963' AS DateTime), 6, 1, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (144, CAST(N'2017-09-28 00:28:29.597' AS DateTime), 10, 1, 1)
GO
INSERT [dbo].[Votes] ([idvotes], [date], [idcandidate], [valid], [withRights]) VALUES (145, CAST(N'2017-09-28 00:30:40.500' AS DateTime), 4, 1, 1)
GO
SET IDENTITY_INSERT [dbo].[Votes] OFF
GO
ALTER TABLE [dbo].[Votes]  WITH CHECK ADD  CONSTRAINT [FK_votes_candidates] FOREIGN KEY([idcandidate])
REFERENCES [dbo].[Candidates] ([idcandidates])
GO
ALTER TABLE [dbo].[Votes] CHECK CONSTRAINT [FK_votes_candidates]
GO

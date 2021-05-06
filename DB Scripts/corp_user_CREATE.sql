USE [master]
GO

/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [corp_user]    Script Date: 28.04.2021 14:10:39 ******/
CREATE LOGIN [corp_user] WITH PASSWORD=N'2Kev7E/Zduedqphse6a9NfRhpF92pRSnHNIXIhw5/xU=', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[русский], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

ALTER LOGIN [corp_user] DISABLE
GO


CREATE TABLE [dbo].[RequestLog] (
    [RequestLogSN]    BIGINT        IDENTITY (1, 1) NOT NULL,
    [logTime]         DATETIME      DEFAULT (getdate()) NOT NULL,
    [ip]              VARCHAR (20)  NOT NULL,
    [host]            VARCHAR (30)  NOT NULL,
    [browser]         VARCHAR (MAX) NOT NULL,
    [requestType]     VARCHAR (20)  NOT NULL,
    [userHostAddress] VARCHAR (20)  NOT NULL,
    [userHostName]    VARCHAR (30)  NOT NULL,
    [httpMethod]      VARCHAR (30)  NOT NULL,
    PRIMARY KEY CLUSTERED ([RequestLogSN] ASC)
);
go
CREATE TABLE [dbo].[ActionLog] (
    [ActionLogSN]    BIGINT       IDENTITY (1, 1) NOT NULL,
    [logTime]        DATETIME     DEFAULT (getdate()) NOT NULL,
    [controllerName] VARCHAR (30) NOT NULL,
    [actionName]     VARCHAR (30) NOT NULL,
    [parame]         VARCHAR (10) NOT NULL,
    PRIMARY KEY CLUSTERED ([ActionLogSN] ASC)
);
go


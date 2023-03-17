CREATE TABLE Customer (
    CustomerId UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    EmailAdress VARCHAR(50) NOT NULL,
    PhoneNumber CHAR(13) NOT NULL
);

CREATE TABLE Worker (
    WorkerId INT PRIMARY KEY IDENTITY (1, 1) NOT NULL,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    EmailAdress VARCHAR(50) NOT NULL,
    NameInitials VARCHAR(50) NOT NULL
);

CREATE TABLE ErrorReport (
    ErrorReportId INT PRIMARY KEY IDENTITY (1, 1) NOT NULL,
    Description VARCHAR(300) NOT NULL,
    Time DATETIME NOT NULL,
    Status VARCHAR(20) NOT NULL,
    CustomerId UNIQUEIDENTIFIER,
    WorkerId INT,
    FOREIGN KEY (CustomerId) REFERENCES Customer (CustomerId),
    FOREIGN KEY (WorkerId) REFERENCES Worker (WorkerId)
);

CREATE TABLE Comment (
    CommentId INT PRIMARY KEY,
    Text VARCHAR(300) NOT NULL,
    Time DATETIME NOT NULL,
    ErrorReportId INT,
    WorkerId INT,
    FOREIGN KEY (ErrorReportId) REFERENCES ErrorReport(ErrorReportId),
    FOREIGN KEY (WorkerId) REFERENCES Worker(WorkerId)
);
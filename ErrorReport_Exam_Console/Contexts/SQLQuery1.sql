CREATE TABLE Customers (
    CustomerId uniqueidentifier PRIMARY KEY DEFAULT NEWSEQUENTIALID(),
    FirstName nvarchar(50) NOT NULL,
    LastName nvarchar(50) NOT NULL,
    EmailAddress nvarchar(50) NOT NULL,
    PhoneNumber char(13) NOT NULL
);
CREATE TABLE ErrorReports (
    ErrorReportId INT IDENTITY(1,1) PRIMARY KEY,
    Title VARCHAR(50) NOT NULL,
    Description VARCHAR(200) NOT NULL,
    Time DATETIME NOT NULL,
    CustomerId uniqueidentifier NOT NULL,
    EmailAddress nvarchar(50) NOT NULL,
    ErrorReportStatus NVARCHAR (30)  NOT NULL,
    FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId)
);
CREATE TABLE Comments (
    CommentId INT IDENTITY(1,1) PRIMARY KEY,
    Content VARCHAR(200) NOT NULL,
    Time2 DATETIME NOT NULL,
    ErrorReportId INT NOT NULL,
    FOREIGN KEY (ErrorReportId) REFERENCES ErrorReports(ErrorReportId)
);
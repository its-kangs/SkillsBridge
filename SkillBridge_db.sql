
-- ==========================================
-- SKILLBRIDGE DATABASE
-- ==========================================

-- 1. USERS TABLE
CREATE TABLE Users
(
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    PasswordHash VARCHAR(255) NOT NULL,
    Role VARCHAR(20) NOT NULL,
    Bio VARCHAR(500),
    date_joined DATE DEFAULT GETDATE(),

    CONSTRAINT CK_Users_Role
    CHECK (Role IN ('Student', 'Tutor'))
);


-- 2. SKILLS TABLE
CREATE TABLE Skills
(
    SkillID INT IDENTITY(1,1) PRIMARY KEY,
    SkillName VARCHAR(100) NOT NULL,
    Category VARCHAR(100)
);


-- 3. USER SKILLS TABLE
CREATE TABLE UserSkills
(
    UserSkillID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT NOT NULL,
    SkillID INT NOT NULL,
    Type VARCHAR(10) NOT NULL,
    ProficiencyLevel VARCHAR(50),

    CONSTRAINT FK_UserSkills_Users
    FOREIGN KEY (UserID) REFERENCES Users(UserID),

    CONSTRAINT FK_UserSkills_Skills
    FOREIGN KEY (SkillID) REFERENCES Skills(SkillID),

    CONSTRAINT CK_UserSkills_Type
    CHECK (Type IN ('Teach', 'Learn'))
);


-- 4. SESSION REQUESTS TABLE
CREATE TABLE SessionRequests
(
    RequestID INT IDENTITY(1,1) PRIMARY KEY,
    RequesterID INT NOT NULL,
    TeacherID INT NOT NULL,
    SkillID INT NOT NULL,
    Request_Date DATE NOT NULL,
    Request_Time TIME NOT NULL,
    Location VARCHAR(200),
    status VARCHAR(20) DEFAULT 'Pending',
    Message VARCHAR(500),

    CONSTRAINT FK_SessionRequests_Requester
    FOREIGN KEY (RequesterID) REFERENCES Users(UserID),

    CONSTRAINT FK_SessionRequests_Teacher
    FOREIGN KEY (TeacherID) REFERENCES Users(UserID),

    CONSTRAINT FK_SessionRequests_Skill
    FOREIGN KEY (SkillID) REFERENCES Skills(SkillID),

    CONSTRAINT CK_SessionRequests_Status
    CHECK (status IN ('Pending', 'Accepted', 'Rejected', 'Completed'))
);


-- 5. REVIEWS TABLE
CREATE TABLE Reviews
(
    ReviewID INT IDENTITY(1,1) PRIMARY KEY,
    RequestID INT NOT NULL,
    ReviewerID INT NOT NULL,
    Rating INT NOT NULL,
    Comment VARCHAR(500),
    ReviewDate DATE DEFAULT GETDATE(),

    CONSTRAINT FK_Reviews_SessionRequests
    FOREIGN KEY (RequestID) REFERENCES SessionRequests(RequestID),

    CONSTRAINT FK_Reviews_Users
    FOREIGN KEY (ReviewerID) REFERENCES Users(UserID),

    CONSTRAINT CK_Reviews_Rating
    CHECK (Rating BETWEEN 1 AND 5)
);


-- ==========================================
-- SAMPLE DATA
-- ==========================================

-- USERS
INSERT INTO Users
(Name, Email, PasswordHash, Role, Bio)
VALUES
('John Student', 'john@student.com', 'test123', 'Student',
 'Interested in learning new skills'),

('Faith Njeri', 'faith@tutor.com', 'test123', 'Tutor',
 'Graphic designer and creative tutor'),

('David Mwangi', 'david@tutor.com', 'test123', 'Tutor',
 'Web developer and programming tutor');


-- SKILLS
INSERT INTO Skills
(SkillName, Category)
VALUES
('Graphic Design', 'Design'),
('Web Development', 'Programming');


-- USER SKILLS
INSERT INTO UserSkills
(UserID, SkillID, Type, ProficiencyLevel)
VALUES
(2, 1, 'Teach', 'Advanced'),
(3, 2, 'Teach', 'Advanced'),
(1, 1, 'Learn', 'Beginner');


-- ==========================================
-- END OF SCRIPT
-- ==========================================
Select * from UserSkills;
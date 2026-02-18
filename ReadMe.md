# ONLINE APTITUDE TEST SYSTEM (CORE://WEBSTER)

## SYSTEM OVERVIEW
[cite_start]The Online Aptitude Test System is a web-based application developed to automate the process of conducting aptitude tests[cite: 19]. [cite_start]The system allows candidates to attempt online tests in multiple subjects such as General Knowledge, Mathematics, and Information Technology[cite: 20]. [cite_start]This project replaces traditional manual examination systems with automated result generation[cite: 21].

## PROJECT OBJECTIVES
* [cite_start]Automate the aptitude testing process[cite: 24].
* [cite_start]Reduce manual checking and evaluation efforts[cite: 25].
* [cite_start]Provide instant test results upon submission[cite: 26].
* [cite_start]Maintain secure candidate performance records[cite: 27].
* [cite_start]Ensure a secure and user-friendly system for both candidates and managers[cite: 28].

## TECHNOLOGY STACK
* [cite_start]Frontend: HTML, CSS, Bootstrap[cite: 102].
* [cite_start]Backend: ASP.NET (C#)[cite: 103].
* [cite_start]Database: SQL Server[cite: 104].
* [cite_start]ORM: Entity Framework[cite: 105].

## SYSTEM ARCHITECTURE
The system is divided into three primary modules:

### 1. Authentication & Authorization Module
Controls user access based on roles:
* [cite_start]Candidate: Authorized to attempt tests and view personal results[cite: 83].
* [cite_start]Manager: Authorized to manage the question bank and view candidate records[cite: 84].

### 2. Question Management Module (CRUD)
[cite_start]Enables managers to add, edit, delete, and view questions within the question bank [cite: 85-90].

### 3. Test Engine & Scoring Module
[cite_start]Handles the dynamic fetching of MCQs from the database[cite: 95]. [cite_start]It compares submitted answers with stored correct answers to calculate the total score[cite: 97, 98]. [cite_start]The system utilizes [ValidateInput(false)] to support HTML-based content within questions[cite: 100].

## DATABASE DESIGN
The relational database consists of the following primary tables:
* [cite_start]Candidates: Stores UserID, Username, Password, Email, and Role [cite: 59-64].
* [cite_start]Questions: Stores QuestionID, Subject, QuestionText, Options, and CorrectAnswer [cite: 65-73].
* [cite_start]Results: Logs ResultID, CandidateID, Subject, Score, and TestDate [cite: 74-79].

## INSTALLATION AND SETUP
1. Clone the repository to your local machine.
2. Restore the database using the .bak file provided in the /Database folder.
3. Update the connection string in the Web.config file to point to your local SQL Server instance.
4. Open the solution file (.sln) in Visual Studio.
5. Build and Run the project.

## SYSTEM LIMITATIONS
* [cite_start]Requires a persistent internet connection[cite: 161].
* [cite_start]Limited to specific question categories[cite: 162].
* [cite_start]Assessment is based on timer constraints[cite: 163].
* [cite_start]No negative marking system is implemented[cite: 164].

## PROJECT OPERATORS
* [cite_start]Ahmed Haneen (Student ID: 1591815) [cite: 2, 3]
* [cite_start]Waleed Khalid Kaim Khani (Student ID: 1571925) [cite: 4, 5]
* [cite_start]Raheel Ali (Student ID: 1592349) [cite: 6, 7]

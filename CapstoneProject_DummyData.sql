USE CapstoneProject;

SELECT * FROM Athletes;

INSERT INTO Athletes (Name, Email, Password, Age, Gender, Height, Weight, Contact, CreatedBy, CreatedDateTime)
VALUES 
('Biswanth Ch', 'biswanth@gmail.com', '2144', 25, 'Male', 180.5, 75.2, '1234567890', 'SystemAdmin', GETDATE()),
('Dhaatri', 'dhaatri@gmail.com', 'DB2144', 28, 'Female', 165.0, 60.0, '9876543210', 'SystemAdmin', GETDATE()),
('Harshith Pavan Kumar', 'harshith@gmail.com', '2228', 30, 'Male', 172.4, 80.3, '5554443322', 'SystemAdmin', GETDATE()),
('Harshini Durga Priya', 'harshini@gmail.com', '112228', 22, 'Female', 158.2, 55.8, '6667778888', 'SystemAdmin', GETDATE()),
('Nageswara Rao', 'nageswararao@gmail.com', 'TNR11', 27, 'Male', 170.0, 68.0, '9998887777', 'SystemAdmin', GETDATE());


SELECT * FROM PerformanceMetrics;
INSERT INTO PerformanceMetrics 
(AthleteId, AverageSpeed, BestSpeed, DistanceCovered, SkillLevel, Improvement, Accuracy, CaloriesBurned, Duration, Timestamp, CreatedBy, CreatedDateTime)
VALUES
(1, 12.5, 15.2, 3.0, 6, 5.2, 92.5, 320, 30, GETDATE(), 'Admin', GETDATE()),
(2, 11.3, 14.1, 2.8, 5, 4.1, 88.4, 310, 28, GETDATE(), 'CoachB', GETDATE()),
(3, 13.7, 16.4, 3.5, 8, 6.8, 95.1, 400, 35, GETDATE(), 'Admin', GETDATE()),
(4, 10.9, 13.5, 2.5, 4, 3.9, 85.7, 290, 25, GETDATE(), 'CoachC', GETDATE()),
(5, 12.1, 15.0, 3.2, 6, 4.5, 90.2, 330, 32, GETDATE(), 'Admin', GETDATE());

SELECT * FROM TrainingPrograms;
INSERT INTO TrainingPrograms (CoachId, Duration, Description, Intensity, CreatedBy, CreatedDateTime)
VALUES
(1, '4 weeks', 'Strength and conditioning for intermediate athletes.', 'High', 'SystemAdmin', GETDATE()),
(2, '6 weeks', 'Endurance training with cardio and flexibility focus.', 'Medium', 'SystemAdmin', GETDATE()),
(3, '3 weeks', 'Speed and agility drills for sprinters.', 'High', 'SystemAdmin', GETDATE()),
(1, '5 weeks', 'General fitness and core strengthening.', 'Low', 'SystemAdmin', GETDATE()),
(4, '8 weeks', 'Advanced resistance training and hypertrophy.', 'Very High', 'SystemAdmin', GETDATE());



SELECT * FROM Subscriptions;
INSERT INTO Subscriptions (AthleteId, ProgramId, SubscribedAt, CreatedBy, CreatedDateTime)
VALUES 
(1, 6, '2024-05-01 09:00:00', 'admin', GETDATE()),
(2, 2, '2024-05-03 11:30:00', 'coach_john', GETDATE()),
(3, 3, '2024-05-05 14:45:00', 'admin', GETDATE()),
(4, 4, '2024-05-07 08:20:00', 'coach_amy', GETDATE()),
(5, 5, '2024-05-10 10:15:00', 'admin', GETDATE());



SELECT * FROM Feedbacks;
INSERT INTO Feedbacks (CoachId, Date, Message, CreatedBy, CreatedDateTime)
VALUES 
(1, '2025-05-20', 'Great progress with the new training plan.', 'system', GETDATE()),
(2, '2025-05-21', 'Needs improvement in consistency.', 'system', GETDATE()),
(1, '2025-05-22', 'Excellent attitude during training.', 'admin', GETDATE()),
(3, '2025-05-23', 'Late for multiple sessions.', 'coach_manager', GETDATE()),
(2, '2025-05-24', 'Good work on speed drills this week.', 'admin', GETDATE());


SELECT * FROM AIPredictions;
INSERT INTO AIPredictions 
(AthleteId, PredictedSkillLevel, Recommendation, CreatedBy, CreatedDateTime)
VALUES
(1, 8, 'Focus on endurance and flexibility training.', 'SystemAI', GETDATE()),
(2, 6, 'Incorporate more strength-based routines.', 'SystemAI', GETDATE()),
(3, 3, 'Improve consistency with speed drills.', 'SystemAI', GETDATE()),
(4, 9, 'Maintain current regimen and add agility drills.', 'SystemAI', GETDATE()),
(5, 5, 'Increase cardiovascular activities and recovery.', 'SystemAI', GETDATE());

SELECT * FROM Coaches;
INSERT INTO Coaches (Name, Email, Password, Specialty, ExperienceLevel, CreatedBy, CreatedDateTime)
VALUES 
('Coach John', 'john@example.com', 'Password123', 'Strength', 'Intermediate', 'SystemAdmin', GETDATE()),
('Coach Lisa', 'lisa@example.com', 'Password456', 'Cardio', 'Advanced', 'SystemAdmin', GETDATE()),
('Coach Mark', 'mark@example.com', 'Password789', 'Speed', 'Expert', 'SystemAdmin', GETDATE()),
('Coach Emma', 'emma@example.com', 'Password222', 'Flexibility', 'Beginner', 'SystemAdmin', GETDATE());


SELECT * FROM PersonalizedTrainingPrograms;
INSERT INTO PersonalizedTrainingPrograms 
(CoachId, AthleteId, Description, Duration, Intensity, AIPredictionId, AssignedDate, CreatedBy, CreatedDateTime)
VALUES
(1, 1, 'Strength training focused on upper body.', '6 weeks', 'High', 1, '2025-05-01', 'Admin', GETDATE()),
(2, 2, 'Endurance running and stamina building.', '8 weeks', 'Medium', 2, '2025-05-02', 'Admin', GETDATE()),
(3, 3, 'Agility drills and core conditioning.', '4 weeks', 'High', 3, '2025-05-03', 'Admin', GETDATE()),
(4, 4, 'Flexibility and balance training.', '3 weeks', 'Low', 4, '2025-05-04', 'Admin', GETDATE()),
(3, 5, 'Powerlifting basics with progression.', '5 weeks', 'High', 5, '2025-05-05', 'Admin', GETDATE());


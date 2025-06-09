alter proc coachallname
as
begin
  select a.Name as Name,b.* from Coaches as a join TrainingPrograms as b on a.Id=b.CoachId;
end

exec coachallname
--
create proc feedbackinfo
as
begin
  select a.Name as Name,b.* from Coaches as a join Feedbacks as b on a.Id=b.CoachId;
end

exec feedbackinfo


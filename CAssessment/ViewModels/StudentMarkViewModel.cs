using CAssessment.Views;

namespace CAssessment.ViewModels;

public class StudentMarkViewModel : MarksViewModel, ITableItem
{
    public int StudentId { get; set; }
    public string StudentName { get; set; } = string.Empty;

    public List<string> ToTableRow()
    {
        return [
            StudentId.ToString(),
            StudentName,
            Attendance.ToString(),
            Assignment.ToString(),
            EndOfTerm.ToString(),
            TotalScore.ToString(),
            Grade.Grade,
            Grade.Point.ToString("0.00"),
        ];
    }
}

using CAssessment.Views;

namespace CAssessment.ViewModels;

public class CourseMarkViewModel : MarksViewModel, ITableItem
{
    public string CourseCode { get; set; } = string.Empty;
    public string CourseTitle { get; set; } = string.Empty;

    public List<string> ToTableRow()
    {
        return [
            CourseCode,
            CourseTitle,
            CreditHours.ToString(),
            Attendance.ToString(),
            Assignment.ToString(),
            EndOfTerm.ToString(),
            TotalScore.ToString(),
            Grade.Grade,
            Grade.Point.ToString("0.00"),
        ];
    }
}

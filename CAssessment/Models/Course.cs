using CAssessment.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAssessment.Models;

public class Course : ITableItem
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Code { get; set; } = null!;
    public int CreditHours { get; set; }

    public override string ToString()
    {
        return $"[{Code}] {Title}";
    }

    public List<string> ToTableRow()
    {
        return [Id.ToString(), Code, Title, CreditHours.ToString()];
    }
}

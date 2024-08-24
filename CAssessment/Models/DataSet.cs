using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAssessment.Models;

public class DataSet<T>
{
    private readonly List<T> _data = [];
    public void Add(T item) { }
}

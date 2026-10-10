using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESCOLAT2.Models.ViewModel
{
    public class PivotRowViewModel
    {
        public string AlunoNome { get; set; }
        public string CursoNome { get; set; }
        public Dictionary<int, string> NotasPorDisciplina { get; set; } = new();
    }
}
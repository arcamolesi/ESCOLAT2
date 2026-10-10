using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESCOLAT2.Models.ViewModel
{
    public class PivotNotasViewModel
    {
        public List<Disciplina> Disciplinas { get; set; } = new();
        public List<PivotRowViewModel> Linhas { get; set; } = new();
    }
}
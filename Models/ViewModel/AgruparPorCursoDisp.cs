using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESCOLAT2.Models.ViewModel
{
    public class AgruparCursoDisp
    {
        public string curso { get; set; }
        public string disciplina { get; set; }
        public float somaNota { get; set; }  
        public int contarNota { get; set; }
        public float mediaNota { get; set; }
        public float maiorNota { get; set; }
        public float menorNota { get; set; }
    }
}
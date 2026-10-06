using ESCOLAT2.Data;
using ESCOLAT2.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ESCOLAT2.Controllers
{
    public class AgruparController : Controller
    {

        private readonly Contexto contexto;

        public AgruparController(Contexto context)
        {
            contexto = context;
        }

        public IActionResult AgruparNotaPorCurso()
        {
            var lista = from item in contexto.Notas
                                                .Include(d =>d.Disciplina)
                                                .ThenInclude(c => c.Curso)
                                                //.OrderBy(d => d.Disciplina.Curso.Descricao)
                                                .ToList()
                        let curso = item.Disciplina.Curso.Descricao
                        group item by new { curso }
                                    into grupo
                        orderby grupo.Key.curso
                        select new AgruparCurso 
                        {
                            curso = grupo.Key.curso,
                            somaNota = grupo.Sum(n => n.Valor)  
                        };

            return View(lista);
        }


        public IActionResult AgruparNotaPorCursoDisp()
        {
            var lista = from item in contexto.Notas
                                                .Include(d =>d.Disciplina)
                                                .ThenInclude(c => c.Curso)
                                                //.OrderBy(d => d.Disciplina.Curso.Descricao)
                                                .ToList()

                        let curso = item.Disciplina.Curso.Descricao
                        let disciplina = item.Disciplina.Descricao
                        

                        group item by new { curso, disciplina }
                                    into grupo
                        orderby grupo.Key.curso, grupo.Key.disciplina

                        select new  AgruparCursoDisp 
                        {
                            curso = grupo.Key.curso,
                            disciplina = grupo.Key.disciplina,
                            somaNota = grupo.Sum(n => n.Valor),  
                            contarNota = grupo.Count(),
                            mediaNota = grupo.Average(n => n.Valor),
                            maiorNota = grupo.Max(n => n.Valor),
                            menorNota = grupo.Min(n => n.Valor)
                        };
                        
            return View(lista);
        }

    }
}

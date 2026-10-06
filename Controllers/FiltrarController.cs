using ESCOLAT2.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ESCOLAT2.Controllers
{
    public class FiltrarController: Controller
    {
        private readonly Contexto contexto;

        public FiltrarController(Contexto context)
        {
            contexto = context;
        }

        public IActionResult ListarNotas()
        {
            var lista = contexto.Notas
            .Include(a => a.Aluno)
            .Include(d => d.Disciplina)
            .ThenInclude(c =>c.Curso)
            .OrderBy(d => d.Disciplina.Curso.Descricao)
            .ThenBy (d => d.Disciplina.Descricao)
            .ThenBy(a => a.Aluno.Nome)
            .ToList(); 

            return View(lista); 
        }


        [HttpGet]
        public IActionResult FiltrarAlunoNota()
        {
            ViewData["AlunoId"] = new SelectList(contexto.Alunos, "Id", "Nome");
            ViewData["DisciplinaId"] = new SelectList(contexto.Disciplinas, "Id", "Descricao");
            return View();
        }

    [HttpPost]
    public IActionResult FiltrarAlunoNota(int? alunoId, int? disciplinaId)
    {
          
      var notas = contexto.Notas
                .Include(a => a.Aluno)
                .Include(d => d.Disciplina)
                .ThenInclude(d => d.Curso)
                .Where(n => !alunoId.HasValue || n.AlunoId == alunoId.Value)
                .ToList();
        
            return View("listarNotas", notas);
        }
        

    }


        
}
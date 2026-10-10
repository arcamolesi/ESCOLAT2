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

        public async Task<IActionResult> Pivot()
        {
            var alunos = await contexto.Alunos
                .Include(a => a.Curso)
                .Include(a => a.Notas)
                    .ThenInclude(n => n.Disciplina)
                .AsNoTracking()
                .ToListAsync();

            var todasDisciplinas = await contexto.Disciplinas
                .OrderBy(d => d.Id)
                .AsNoTracking()
                .ToListAsync();

            // Montar o Pivot
            var linhas = alunos.Select(aluno =>
            {
                // Agrupa as notas por Disciplina
                var notasPorDisciplina = aluno.Notas
                    .GroupBy(n => n.DisciplinaId)
                    .ToDictionary(
                        g => g.Key,
                        g =>
                        {
                            // Pega a nota do semestre 1
                            var notaSem1 = g.FirstOrDefault(n => n.Semestre == 1)?.Valor;

                            // Pega a nota do semestre 2
                            var notaSem2 = g.FirstOrDefault(n => n.Semestre == 2)?.Valor;

                            // Se não tiver nenhuma nota, retorna "NA"
                            if (notaSem1 == null && notaSem2 == null)
                                return "NA";

                            // Calcula a média (considerando 0 quando não tiver o semestre)
                            double n1 = notaSem1 ?? 0;
                            double n2 = notaSem2 ?? 0;

                            double media = (n1 + n2) / 2;

                            return media.ToString("0.00");
                        });

                // Monta o dicionário completo com todas as disciplinas
                var notasCompletas = todasDisciplinas.ToDictionary(
                    d => d.Id,
                    d => notasPorDisciplina.ContainsKey(d.Id)
                        ? notasPorDisciplina[d.Id]
                        : "NA"
                ); 
                    
                

                return new PivotRowViewModel
                {
                    AlunoNome = aluno.Nome,
                    CursoNome = aluno.Curso.Descricao,
                    NotasPorDisciplina = notasCompletas
                };
            })
            .OrderBy(l => l.CursoNome)
            .ThenBy(l => l.AlunoNome)
            .ToList();

            var viewModel = new PivotNotasViewModel
            {
                Disciplinas = todasDisciplinas,
                Linhas = linhas
            };

            return View(viewModel);
        }






    }
}

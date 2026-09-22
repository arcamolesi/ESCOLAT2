using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESCOLAT2.Models
{
   public class Curso
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória")]
        [StringLength(25,  ErrorMessage = "A descrição deve ter no máximo 100 caracteres")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; } = string.Empty;

        [Display(Name = "Sigla")]
        [Required(ErrorMessage = "A sigla é obrigatória")]
        [StringLength(3, ErrorMessage = "A sigla deve ter no máximo 3 caracteres")]
        public string Sigla { get; set; } = string.Empty;

        [Display(Name = "Área")]
        [Required(ErrorMessage = "A área é obrigatória")]
        [StringLength(10, ErrorMessage = "A área deve ter no máximo 10 caracteres")]
        public string Area { get; set; } = string.Empty;

        [Display(Name = "Mensalidade")]
        [Range(0, 15000, ErrorMessage = "O valor deve ser positivo")]
        public float Mensalidade { get; set; }

        // Relacionamentos
        public ICollection<Disciplina> Disciplinas { get; set; } = new List<Disciplina>();
        public ICollection<Aluno> Alunos { get; set; } = new List<Aluno>();
    }
}
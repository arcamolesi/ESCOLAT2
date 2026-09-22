
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESCOLAT2.Models
{
    public class Nota
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Aluno")]
        public int AlunoId { get; set; }
        [Display(Name = "Aluno")]
        public Aluno Aluno { get; set; }

        [Required]
        [ForeignKey("Disciplina")]
        public int DisciplinaId { get; set; }
        [Display(Name = "Disciplina")]
        public Disciplina Disciplina { get; set; }

        [Required]
        [Range(1, 2)]
        [Display(Name = "Semestre")]
        public int Semestre { get; set; }

        [Required]
        [Range(0, 10)]
        [Display(Name = "Valor")]
        public float Valor { get; set; }
    }
}
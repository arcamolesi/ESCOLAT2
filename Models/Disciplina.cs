using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESCOLAT2.Models
{

    public class Disciplina
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória")]
        [StringLength(150)]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; } = string.Empty;

        [Required]
        [ForeignKey("Curso")]
        public int CursoId { get; set; }

        public Curso Curso { get; set; }
    }

}
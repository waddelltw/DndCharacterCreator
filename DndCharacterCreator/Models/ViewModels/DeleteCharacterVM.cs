using DndCharacterCreator.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace DndCharacterCreator.Models.ViewModels
{
    public class DeleteCharacterVM
    {
        [Required]
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}

using DndCharacterCreator.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace DndCharacterCreator.Models.ViewModels
{
    public class DetailsCharacterVM
    {
        [Required]
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public Race Race { get; set; }
        [Required]
        public Class Class { get; set; }
        [Required]
        [Range(3, 18)]
        public int Strength { get; set; }
        [Required]
        [Range(3, 18)]
        public int Dexterity { get; set; }
        [Required]
        [Range(3, 18)]
        public int Constitution { get; set; }
        [Required]
        [Range(3, 18)]
        public int Intelligence { get; set; }
        [Required]
        [Range(3, 18)]
        public int Wisdom { get; set; }
        [Required]
        [Range(3, 18)]
        public int Charisma { get; set; }
        [Required]
        public Alignment Alignment { get; set; }
        public string? Description { get; set; }
        public string? Inventory { get; set; }
    }
}

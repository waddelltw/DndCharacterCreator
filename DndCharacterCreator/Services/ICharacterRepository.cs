using DndCharacterCreator.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace DndCharacterCreator.Services
{
    public interface ICharacterRepository
    {
        Task<List<Character>?> ReadAllAsync(string username);
        Task<Character?> ReadAsync(int id);
        Task<Character> CreateAsync(Character character);
        Task<Character?> UpdateAsync(int oldId, Character newCharacter);
        void DeleteAsync(int id);
        Task<IdentityUser?> ReadUserAsync(string username);
    }
}

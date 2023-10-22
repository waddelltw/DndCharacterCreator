using DndCharacterCreator.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DndCharacterCreator.Services
{
    public class DbCharacterRepository : ICharacterRepository
    {
        private readonly ApplicationDbContext _db;

        public DbCharacterRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<Character>?> ReadAllAsync(string username)
        {
            var allCharacters = await _db.Characters
                .Include(u => u.Player)
                .ToListAsync();
            var userCharacters = allCharacters.Where(u => u.Player.UserName == username).ToList();
            return userCharacters;
        }

        public async Task<Character?> ReadAsync(int id)
        {
            var character = await _db.Characters
                .Include(u => u.Player)
                .FirstAsync(c => c.Id == id);
            return character;
        }

        public async Task<Character?> UpdateAsync(int oldId, Character newCharacter)
        {
            var oldCharacter = await _db.Characters.FindAsync(oldId);
            if (oldCharacter != null)
            {
                oldCharacter.Name = newCharacter.Name;
                oldCharacter.Race = newCharacter.Race;
                oldCharacter.Class = newCharacter.Class;
                oldCharacter.Strength = newCharacter.Strength;
                oldCharacter.Dexterity = newCharacter.Dexterity;
                oldCharacter.Constitution = newCharacter.Constitution;
                oldCharacter.Intelligence = newCharacter.Intelligence;
                oldCharacter.Wisdom = newCharacter.Wisdom;
                oldCharacter.Charisma = newCharacter.Charisma;
                oldCharacter.Alignment = newCharacter.Alignment;
                oldCharacter.Description = newCharacter.Description;
                oldCharacter.Inventory = newCharacter.Inventory;
                await _db.SaveChangesAsync();
            }
            return oldCharacter;
        }
        public async Task<Character> CreateAsync(Character character)
        {
            await _db.Characters.AddAsync(character);
            await _db.SaveChangesAsync();
            return character;
        }

        public async void DeleteAsync(int id)
        {
            var character = await _db.Characters.FindAsync(id);
            if (character != null)
            {
                _db.Characters.Remove(character);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<IdentityUser?> ReadUserAsync(string username)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.UserName == username);
            return user;
        }
    }
}

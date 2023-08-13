using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DndCharacterCreator.Services;
using Microsoft.AspNetCore.Authorization;
using DndCharacterCreator.Models.Entities;
using DndCharacterCreator.Models.ViewModels;
using System.Xml.Linq;

namespace DndCharacterCreator.Controllers
{
    [Authorize]
    public class CharactersController : Controller
    {
        private readonly ICharacterRepository _repo;

        public CharactersController(ICharacterRepository repo)
        {
            _repo = repo;
        }

        //Reads all character for the logged in user and returns a view of it
        public async Task<IActionResult> Index()
        {
            if(User.Identity!.IsAuthenticated)
            {
                string username = User.Identity.Name ?? "";
                var characters = await _repo.ReadAllAsync(username);
                var model = characters!.Select(c =>
                    new DetailsCharacterVM
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Race = c.Race,
                        Class = c.Class,
                        Alignment = c.Alignment
                    }
                );
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //Reads the character from the given id and returns a view of it
        public async Task<IActionResult> Details(int id)
        {
            if(User.Identity!.IsAuthenticated)
            {
                var character = await _repo.ReadAsync(id);
                if(character != null)
                {
                    DetailsCharacterVM characterVM = new DetailsCharacterVM()
                    {
                        Id = character.Id,
                        Name = character.Name,
                        Race = character.Race,
                        Class = character.Class,
                        Strength = character.Strength,
                        Dexterity = character.Dexterity,
                        Constitution = character.Constitution,
                        Intelligence = character.Intelligence,
                        Wisdom = character.Wisdom,
                        Charisma = character.Charisma,
                        Alignment = character.Alignment,
                        Description = character.Description,
                        Inventory = character.Inventory
                    };
                    return View(characterVM);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //Returns the view with the form to create a character
        public IActionResult Create()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //Post method to create a character from the form view
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCharacterVM characterVM)
        {
            var username = User.Identity!.Name ?? "";
            var user = await _repo.ReadUserAsync(username);
            if (ModelState.IsValid && User.Identity!.IsAuthenticated && user != null && characterVM != null)
            {
                var character = characterVM.GetCharacter();
                character.Player = user;
                var newCharacter = await _repo.CreateAsync(character);
                return RedirectToAction("Details", new { id = newCharacter.Id });
            }
            else
            {
                return View(characterVM);
            }
        }

        //Returns the edit form to change an existing character
        public async Task<IActionResult> Edit(int id)
        {
            if(User.Identity!.IsAuthenticated)
            {
                var character = await _repo.ReadAsync(id);
                if(character != null)
                {
                    EditCharacterVM characterVM = new EditCharacterVM()
                    {
                        Id = character.Id,
                        Name = character.Name,
                        Race = character.Race,
                        Class = character.Class,
                        Strength = character.Strength,
                        Dexterity = character.Dexterity,
                        Constitution = character.Constitution,
                        Intelligence = character.Intelligence,
                        Wisdom = character.Wisdom,
                        Charisma = character.Charisma,
                        Alignment = character.Alignment,
                        Description = character.Description,
                        Inventory = character.Inventory
                    };

                    return View(characterVM);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //Post method to update a character from a form view
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditCharacterVM characterVM)
        {
            var character = characterVM.GetCharacter();
            if (ModelState.IsValid && User.Identity!.IsAuthenticated && character != null)
            {
                await _repo.UpdateAsync(character.Id, character);
                return RedirectToAction("Details", new { character.Id });
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        //Returns the delete confirmed view to ensure the user wants to delete the character
        public async Task<IActionResult> Delete(int id)
        {
            if (User.Identity!.IsAuthenticated)
            {
                var character = await _repo.ReadAsync(id);
                if (character != null)
                {
                    DeleteCharacterVM characterVM = new DeleteCharacterVM()
                    {
                        Id = character.Id,
                        Name = character.Name
                    };
                    return View(characterVM);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //Calls the repository method to delete a character from a post request
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if(User.Identity!.IsAuthenticated)
            {
                var character = await _repo.ReadAsync(id);
                if (character != null)
                {
                    _repo.DeleteAsync(id);
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}

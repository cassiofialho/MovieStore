using Microsoft.AspNetCore.Mvc;
using MovieStore.Models.Domains;
using MovieStore.Repositories.Abstract;
using MovieStore.Repositories.Implementation;

namespace MovieStore.Controllers
{
    public class GenreController : Controller
    {
        private readonly IGenreServices _genreService;
        public GenreController(IGenreServices genreServices)
        {
            _genreService = genreServices;
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Genre model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = _genreService.Add(model);
            if (result)
            {
                TempData["msg"] = "Adicionado com sucesso";
                return RedirectToAction(nameof(Add));
            }
            else
            {
                TempData["msg"] = "Erro no servidor";
                return View(model);
            }
        }

        public IActionResult Edit(int id)
        {
            var data = _genreService.GetById(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Update(Genre model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = _genreService.Update(model);
            if (result)
            {
                TempData["msg"] = "Adicionar com sucesso";
                return RedirectToAction(nameof(GenreList));
            }
            else
            {
                TempData["msg"] = "Err ono servidor";
                return View(model);
            }
        }

        public IActionResult GenreList()
        {
            var data = this._genreService.GetAll().ToList();
            return View(data);
        }

        public IActionResult Delete(int id)
        {
            var result = _genreService.Delete(id);
            return RedirectToAction(nameof(GenreList));
        }



    }
}

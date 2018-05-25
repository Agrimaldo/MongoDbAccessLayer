using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Rule.Model;

namespace WebApp.Controllers
{
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            List<PersonViewModel> persons = Repository.List<Person>().ConvertAll(PersonViewModel.FromModel);
            return View(persons);
        }

        [HttpGet]
        public IActionResult Create()
        {
            PersonViewModel viewModel = new PersonViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(PersonViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Person person = PersonViewModel.ToModel(viewModel);

                Repository.Add<Person>(person);

                Alert(Rule.Util.AlertType.Success, "New person has be created with success");
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Edit(string Id)
        {
            PersonViewModel viewModel = Repository.List<Person>(p => p.Id.Equals(new MongoDB.Bson.ObjectId(Id))).ConvertAll(PersonViewModel.FromModel).FirstOrDefault();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(PersonViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Person person = PersonViewModel.ToModel(viewModel);

                Repository.Update<Person>(person);

                Alert(Rule.Util.AlertType.Success, "the person has be updated with success");
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Delete(string Id)
        {
            Person person = Repository.List<Person>(p => p.Id.Equals(new MongoDB.Bson.ObjectId(Id))).FirstOrDefault();

            Repository.Delete<Person>(person);

            Alert(Rule.Util.AlertType.Success, "the person has be deleted with success");

            return RedirectToAction("Index");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

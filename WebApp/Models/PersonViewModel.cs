using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Rule.Model;

namespace WebApp.Models
{
    public class PersonViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }
        [MinLength(3), Required]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Carrer { get; set; }
        [Required]
        public bool Active { get; set; }

        public static Person ToModel(PersonViewModel viewModel)
        {
            return new Person
            {
                Id      = string.IsNullOrEmpty(viewModel.Id) ? MongoDB.Bson.ObjectId.Empty : new MongoDB.Bson.ObjectId(viewModel.Id),
                Name    = viewModel.Name,
                Age     = viewModel.Age,
                Carrer  = viewModel.Carrer,
                Active  = viewModel.Active
            };
        }

        public static PersonViewModel FromModel(Person model)
        {
            return new PersonViewModel
            {
                Id = model.Id.ToString(),
                Name = model.Name,
                Age = model.Age,
                Carrer = model.Carrer,
                Active = model.Active
            };
        }
    }
}

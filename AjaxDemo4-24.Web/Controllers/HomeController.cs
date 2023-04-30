using AjaxDemo.Data;
using AjaxDemo4_24.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;

namespace AjaxDemo4_24.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString = @"Data Source=.\sqlexpress;Initial Catalog=People;Integrated Security=true;";

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetPeople()
        {
            var repo = new PeopleRepository(_connectionString);
            List<Person> people = repo.GetAllPeople();
            return Json(people);
        }

        [HttpPost]
        public void AddPerson(Person person)
        {
            var repo = new PeopleRepository(_connectionString);
            repo.Add(person);
        }
        public IActionResult GetPersonById(int id)
        {
            var repo = new PeopleRepository(_connectionString);
            var person = repo.GetPersonById(id);
            return Json(person);
        }
        [HttpPost]
        public void DeletePerson(int id)
        {
            var repo = new PeopleRepository(_connectionString);
            repo.DeletePerson(id);
        }
        [HttpPost]
        public void EditPerson(Person person)
        {
            var repo = new PeopleRepository(_connectionString);
            repo.EditPerson(person);
        }
    }
}
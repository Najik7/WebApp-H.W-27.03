using Dapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class PersonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private string ConnString = @"Data Source=WIN-C6IAG73172R\SQLEXPRESS;Initial Catalog=Alif;Integrated Security=True";
        
        [HttpGet]
        public IActionResult Read(int? Id)
        {
            using (IDbConnection db = new SqlConnection(ConnString))
            {
                if (Id == null)
                    return View(db.Query<Person>("SELECT * FROM Persons").ToList<Person>());
                else
                {
                    return View(db.Query<Person>($"SELECT * FROM Persons Where(Id={Id})").ToList<Person>());
                }
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Person person)
        {
            using (IDbConnection db = new SqlConnection(ConnString))
            {
                db.Execute($"INSERT INTO Persons(LastName,FirstName,MiddleName) VALUES('{person.LastName}','{person.FirstName}','{person.MiddleName}')");
            }
            return RedirectToAction("Read");
        }
        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetResultOfScan(Person person)
        {
            using (IDbConnection db = new SqlConnection(ConnString))
            {
                return View(db.Query<Person>($"SELECT * FROM Persons WHERE (LastName Like '%{person.LastName}%') AND (FirstName Like '%{person.FirstName}%') AND (MiddleName Like '%{person.MiddleName}%')").ToList<Person>());
            }
        }

    }
}

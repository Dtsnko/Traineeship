using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Traineeship.Database;
using Traineeship.Models;

namespace Traineeship.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static  int topicParentId;
        private static TopicModel EditedTopic;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        
        public IActionResult Index()
        {
            DBConnector con = new DBConnector();
            return View(con.SelectTopics());
        }

        [HttpGet]
        public IActionResult CreateTopic()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateTopic(string topicName)
        {
            TopicModel t = new TopicModel() { Name = topicName, Owner = Startup.Username, ParentId = 0 };
            DBConnector con = new DBConnector();
            con.InsertTopic(t);
            return View("Index", con.SelectTopics());
        }

        [HttpGet]
        public IActionResult CreateUnderTopic(int id)
        {
            topicParentId = id;
            return View();
        }
        [HttpPost]
        public IActionResult CreateUnderTopic(string comment)
        {
            TopicModel t = new TopicModel() { Name = comment, Owner = Startup.Username, ParentId = topicParentId };
            DBConnector con = new DBConnector();
            con.InsertTopic(t);
            return View("Index", con.SelectTopics());
        }
        [HttpGet]
        public IActionResult EditTopic(int id)
        {
            DBConnector con = new DBConnector();
            EditedTopic = con.SelectTopics(id).First();
            return View(EditedTopic);
        }
        [HttpPost]
        public IActionResult EditTopic(string comment)
        {
            DBConnector con = new DBConnector();
            con.EditTopic(EditedTopic.Id, comment);
            return View("Index",con.SelectTopics());
        }
        public IActionResult DeleteTopic(int id)
        {
            DBConnector con = new DBConnector();
            con.DeleteTopic(id);
            return View("Index", con.SelectTopics());
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            Startup.SetLogined(username, password);
            return View("Profile");

        }
        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(string username, string password)
        {
                DBConnector con = new DBConnector();

            if (username.Contains('@'))
            {
                if (con.InsertUser(username, password))
                    return View("Login");
                else
                    return View("Error");
            }
            else
                return View("FailedRegister");
        }
        [HttpGet]
        public IActionResult Profile()
        {
            return View();
        }
        //[HttpGet]
        //public IActionResult Profile()
        //{
        //    return View();
        //}

        public IActionResult UnderTopic(int id, string name, string owner)
        {
            TopicModel t = new TopicModel(id, name, 0, owner);
            List<TopicModel> underTopics = new List<TopicModel>();
            DBConnector con = new DBConnector();
            foreach(var c in con.SelectTopics())
            {
                if (c.ParentId == id)
                    underTopics.Add(c);
            }
            ViewBag.UnderTopics = underTopics;
            return View(t);
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

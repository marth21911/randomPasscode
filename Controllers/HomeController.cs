using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using randomPasscode.Models;
using Microsoft.AspNetCore.Http;

namespace randomPasscode.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.Code = HttpContext.Session.GetString("Code");
            return View("Index");
        }
        [HttpGet("GenerateCode")]
        public IActionResult GenerateCode()
        {
            Random randInt = new Random();
            string pwFinal="";
            int[] numbers= new int[7];
            string[] randLetters= new string[7];
            string[] fullLength = new string[14];
            string[] letters = {"a","A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"};
            for(int i = 0; i<7; i++)
            {
                numbers[i] = randInt.Next(0,10);
                randLetters[i] = letters[randInt.Next(1,27)];
            }  
            for(int i=0; i<randLetters.Length;i++)
            {
                string strNum = numbers[i].ToString();
                // Console.WriteLine("Number" + strNum);
                // Console.WriteLine("Letter"+randLetters[i]);
                fullLength[i] = strNum;
                fullLength[i+7] = randLetters[i];
            }
            // Console.WriteLine("fullLength is" + fullLength.Length);
            fullLength = fullLength.OrderBy(x => randInt.Next()).ToArray();
            foreach(string i in fullLength)
            {
                // Console.WriteLine(i);
                pwFinal += i;
            }
            
            HttpContext.Session.SetString("Code", pwFinal );
            ViewBag.Code = HttpContext.Session.GetString("Code");
            Console.WriteLine(ViewBag.Code);
            return RedirectToAction("Index");

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

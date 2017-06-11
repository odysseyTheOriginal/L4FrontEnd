using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using L4ProjectFrontEnd.Models;
using System.Diagnostics;
using System.IO;

namespace L4ProjectFrontEnd.Controllers
{
    public class HomeController : Controller
    {
        [HttpPost]
        [AllowAnonymous]
        public JsonResult AnswerGen(QueryData data)
        {
            // full path of python interpreter
            string python = @"C:\ProgramData\Miniconda3\python.exe";

            // python app to call

            string myPythonApp = @"D:\L4Project\asnwer.py";

            //// dummy parameters to send Python script
            string x = data.QueryEntered;

            // Create new process start info
            ProcessStartInfo myProcessStartInfo = new ProcessStartInfo(python);

            // make sure we can read the output from stdout
            myProcessStartInfo.UseShellExecute = false;
            myProcessStartInfo.RedirectStandardOutput = true;

            // start python app with 3 arguments 
            // 1st arguments is pointer to itself, 2nd and 3rd are actual arguments we want to send
            myProcessStartInfo.Arguments = myPythonApp + " " + x;

            Process myProcess = new Process();
            // assign start information to the process
            myProcess.StartInfo = myProcessStartInfo;

            //Console.WriteLine("Calling Python script with arguments {0} and {1}", x, y);
            // start the process
            myProcess.Start();

            // Read the standard output of the app we called. 
            // in order to avoid deadlock we will read output first and then wait for process terminate:
            StreamReader myStreamReader = myProcess.StandardOutput;
            string myString = myStreamReader.ReadLine();

            /*if you need to read multiple lines, you might use:
                string myString = myStreamReader.ReadToEnd() */

            // wait exit signal from the app we called and then close it.
            myProcess.WaitForExit();
            myProcess.Close();

            //// write the output we got from python app
            Console.WriteLine("Value received from script: " + myString);

            //ViewBag.Message = myString;

            return Json(myString);

        }

        [HttpPost]
        public JsonResult ConceptMapGen(QueryData data)
        {
            //try { 
            // full path of python interpreter
            string python = @"C:\ProgramData\Miniconda3\python.exe";

            // python app to call

            string myPythonApp = @"D:\L4Project\KeyPhrasesNew.py";
            //string myPythonApp = @"D:\L4Project\passlist.py";


            //// dummy parameters to send Python script
            string x = data.QueryEntered;

            // Create new process start info
            ProcessStartInfo myProcessStartInfo = new ProcessStartInfo(python);

            // make sure we can read the output from stdout
            myProcessStartInfo.UseShellExecute = false;
            myProcessStartInfo.RedirectStandardOutput = true;
            myProcessStartInfo.RedirectStandardError = true;

            // start python app with 3 arguments 
            // 1st arguments is pointer to itself, 2nd and 3rd are actual arguments we want to send
            myProcessStartInfo.Arguments = myPythonApp + " " + x;

            //using (Process process = Process.Start(myProcessStartInfo))
            //{
            //    using (StreamReader reader = process.StandardOutput)
            //    {
            //        string stderr = process.StandardError.ReadToEnd(); // Here are the exceptions from our Python script
            //        string result = reader.ReadToEnd(); // Here is the result of StdOut(for example: print "test")
            //        return Json(result);
            //    }
            //}

            Process myProcess = new Process();
            // assign start information to the process
            myProcess.StartInfo = myProcessStartInfo;

            //Console.WriteLine("Calling Python script with arguments {0} and {1}", x, y);
            // start the process
            myProcess.Start();

            // Read the standard output of the app we called. 
            // in order to avoid deadlock we will read output first and then wait for process terminate:
            StreamReader myStreamReader = myProcess.StandardOutput;
            //string myString = myStreamReader.ReadLine();

            string stderr = myProcess.StandardError.ReadToEnd();
            /*if you need to read multiple lines, you might use:*/
            string myString = myStreamReader.ReadToEnd();

            // wait exit signal from the app we called and then close it.
            myProcess.WaitForExit();
            myProcess.Close();

            //// write the output we got from python app
            Console.WriteLine("Value received from script: " + myString);
            //string stderr = myProcess.StandardError.ReadToEnd();
            //string stdout = myProcess.StandardOutput.ReadToEnd();
            //Debug.WriteLine("STDERR: " + stderr);
            //Debug.WriteLine("STDOUT: " + stdout);

            //ViewBag.Message = myString;

            return Json(myString);
            //}
            //    catch(Exception ex)
            //    {
            //        Console.WriteLine(ex);
            //        return Json(ex);
            //    }

        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
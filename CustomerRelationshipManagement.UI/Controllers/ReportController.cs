using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FastReport.Web;
using Microsoft.AspNetCore.Mvc;
using CustomerRelationshipManagement.UI.Models.DBContext;
using CustomerRelationshipManagement.UI.Models;
using Microsoft.AspNetCore.Hosting;
using FastReport.Data;
using System.Configuration;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.IO;
using FastReport.Export.PdfSimple;
using Microsoft.AspNetCore.Authorization;

namespace CustomerRelationshipManagement.UI.Controllers
{
    [Authorize(Roles = "Manager,Employee")]
    public class ReportController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly CustomerRelationshipManagementDBContext _context;
        private readonly IConfiguration _configuration;
        public ReportController(IWebHostEnvironment webHostEnvironment,CustomerRelationshipManagementDBContext context,
            IConfiguration configuration)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CustomerList(string name)
        {
            WebReport web = new WebReport();
            //Load the fast report 
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\CustomerCallList.frx";
            web.Report.Load(path);

            //Passing connectionString fast report
            var mssqlConnection = new MsSqlDataConnection();
            mssqlConnection.ConnectionString = _configuration.GetConnectionString("DBConnection");
            var Conn = mssqlConnection.ConnectionString;
            web.Report.SetParameterValue("CONN", Conn);

            web.Report.SetParameterValue("id", name);

            var CustomerName= _context.CustomerCallList(name);
            var Cusname = CustomerName.CustomerName;
            web.Report.SetParameterValue("CustomerName",Cusname);

            //Render the report pdf
            ////prepare report
            web.Report.Prepare();
            ////save file in stream
            Stream stream = new MemoryStream();
            web.Report.Export(new PDFSimpleExport(), stream);
            stream.Position = 0;
            ////return stream in browser
            //return File(stream, "application/zip", "report.pdf");

            return View(web);
        }
    }
}

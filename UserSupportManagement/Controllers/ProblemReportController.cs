using System.Collections.Generic;
using System.Linq;
using iTextSharp.text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserSupportManagement.Data;
using UserSupportManagement.Models;
using UserSupportManagement.Report;

namespace UserSupportManagement.Controllers
{
    public class ProblemReportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProblemReportController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Report(ProblemReportViewModel problemReportViewModel)
        {
            ProblemReport problemReport = new ProblemReport();
            byte[] abBytes = problemReport.PrepareReport(GetProblemReportViewModels());
            return File(abBytes,"application/pdf");
        }

        public List<ProblemReportViewModel> GetProblemReportViewModels()
        {
            List<ProblemReportViewModel> problemReportViewModels = new List<ProblemReportViewModel>();
            ProblemReportViewModel problemReportViewModel = new ProblemReportViewModel();

            var problem = _context.Problems.ToList();

            foreach (var prob in problem)
            {
                var probName = prob.ProblemName;
                var probType = prob.ProblemType;
                var probDetails = prob.ProblemDetails;

                //var solution = _context.Solutions.FirstOrDefault(x => x.ProblemId == prob.ProblemId);
                //var solDetails = solution.SolutionDetails;

                for (int i = 0; i < 1; i++)
                {
                    problemReportViewModel = new ProblemReportViewModel();
                    problemReportViewModel.ProblemId = i;
                    problemReportViewModel.ProblemName = probName;
                    problemReportViewModel.ProblemTypeName = "ProblemTypeName" ;
                    problemReportViewModel.ProblemDetails = probDetails;
                    problemReportViewModel.SolutionDetails = "solDetails";
                    problemReportViewModels.Add(problemReportViewModel);
                }
            }

            //for (int i = 0; i <= problem.Count; i++)
            //{
            //    problemReportViewModel = new ProblemReportViewModel();
            //    problemReportViewModel.ProblemId = i;
            //    problemReportViewModel.ProblemName = probName;
            //    problemReportViewModel.ProblemTypeName = "ProblemTypeName" + i;
            //    problemReportViewModel.ProblemDetails = "ProblemDetails" + i;
            //    problemReportViewModel.SolutionDetails = "SolutionDetails" + i;
            //    problemReportViewModels.Add(problemReportViewModel);
            //}
            return problemReportViewModels;
        }
    }
}

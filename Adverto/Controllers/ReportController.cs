using Adverto.Domain;
using Adverto.Repositories.ReportRepository;
using Adverto.Routes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adverto.Controllers
{
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportRepo _reportRepo;
        public ReportController(IReportRepo repo)
        {
            _reportRepo = repo;
        }
        [HttpGet(RoutesAPI.Report.GetReports)]
        public async Task<IActionResult> getReports()
        {

            var reports = await _reportRepo.GetReports();


            return Ok(reports);

        }
        [HttpGet(RoutesAPI.Report.GetReport)]
        public async Task<IActionResult> getReport(Guid reportId)
        {
            var report = await _reportRepo.GetReport(reportId);

            return Ok(report);
        }
        [HttpPost(RoutesAPI.Report.Create)]
        public async Task<IActionResult> createReport(Report report)
        {
            var result = await _reportRepo.CreateReport(report);

            if (result == true)
            {
                return Ok(report);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete(RoutesAPI.Report.RemoveReport)]
        public async Task<IActionResult> removeReport([FromRoute]Guid reportId)
        {
            var result = await _reportRepo.DeleteReport(reportId);


            if (result == false)
                return NotFound();

            return Ok();
            
            
        }

        [HttpPut(RoutesAPI.Report.UpdateReport)]
        public async Task<IActionResult> UpdateReport([FromRoute]Guid reportId,[FromBody]Report report)
        {
            var reportFromBase = await _reportRepo.GetReport(reportId);
            reportFromBase.IsCheckedByAdmin = report.IsCheckedByAdmin;

            bool result = await _reportRepo.UpdateReport(reportFromBase);

            if (result)
            {
                return Ok(reportFromBase);
            }
            else
            {
                return NotFound();
            }


        }

    }
}

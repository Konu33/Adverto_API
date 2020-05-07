using Adverto.Data;
using Adverto.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adverto.Repositories.ReportRepository
{
    public class ReportRepo : IReportRepo
    {
        private readonly DataContext _context;
        public ReportRepo(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateReport(Report report)
        {
            report.Id =Guid.NewGuid();
            _context.Reports.Add(report);
            var result = await _context.SaveChangesAsync();


            return result > 0; 
        }

        public async Task<bool> DeleteReport(Guid id)
        {
            Report report = await GetReport(id);


            if(report != null)
            {
                _context.Reports.Remove(report);

                await _context.SaveChangesAsync();
                return true;
            }



            return false;
        }

        public async Task<Report> GetReport(Guid id)
        {
            var report = await _context.Reports.FirstOrDefaultAsync(c => c.Id == id);

            return report;

        }

        public async Task<List<Report>> GetReports()
        {
            var reports = await _context.Reports.ToListAsync();

            return reports;
        }

        public async Task<bool> UpdateReport(Report report)
        {
            if (report == null)
                return false;

            _context.Reports.Update(report);

            int result =await _context.SaveChangesAsync();

            return result > 0;
        }
    }
}

using Adverto.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adverto.Repositories.ReportRepository
{
    public interface IReportRepo
    {
        Task<Report> GetReport(Guid id);
        Task<List<Report>> GetReports();
        Task<bool> CreateReport(Report report);
        Task<bool> DeleteReport(Guid id);
        Task<bool> UpdateReport(Report report);

    }
}

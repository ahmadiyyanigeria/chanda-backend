using Application.Paging;
using Application.Repositories;
using Domain.Entities;
using Infrastructure.Mapping.Extensions;
using Microsoft.EntityFrameworkCore;
using static Application.Queries.GetMemberReport;

namespace Infrastructure.Persistence.Repositories
{
    public class InvoiceItemRepository : IInvoiceItemRepository
    {
        private readonly AppDbContext _context;

        public InvoiceItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<MemberReport> GetMemberReportAsync(Guid? id, string? chandaType, PageRequest request, bool usePaging)
        {
            var query = _context.InvoiceItems.Include(ii => ii.Member).Include(ii => ii.ChandaItems).ThenInclude(ci => ci.ChandaType)
                .Where(ii => ii.Member.Id == id);

            if (!string.IsNullOrEmpty(request.Filter))
            {
                var filter = request.Filter.ToLower().Replace(" ", "");
                query = PeriodicFilter(query, filter, request.StartDate, request.EndDate);
            }
            else
            {
                query = query.Where(ii => ii.CreatedOn.Year == DateTime.Now.Year);
            }

            if (request.IsDescending)
            {
                query = query.OrderByDescending(i => i.CreatedOn);
            }
            var count = await query.CountAsync();

            var noOfMonthsPaid = 0;
            var totalAmountSummary = 0m;
            var invoiceItems = new List<MemberReportResponse>();
            foreach (var obj in query)
            {
                var totalAmountPaid = 0m;
                var chandaItems = new List<Item>();
                if (!string.IsNullOrEmpty(chandaType))
                {
                    var item = obj.ChandaItems.Where(ci => ci.ChandaType.Name.Equals(chandaType, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                    if(item != null)
                    {
                        noOfMonthsPaid++;
                        totalAmountPaid += item.Amount;
                        chandaItems.Add(new Item(item.ChandaType.Name, item.Amount));
                    }
                }
                else
                {
                    noOfMonthsPaid++;
                    foreach (var item in obj.ChandaItems)
                    {                        
                        totalAmountPaid += item.Amount;
                        chandaItems.Add(new Item(item.ChandaType.Name, item.Amount));
                    }
                }
                if(chandaItems.Count > 0)
                {
                    totalAmountSummary += totalAmountPaid;
                    invoiceItems.Add(new MemberReportResponse
                    {
                        MemberId = obj.Member.Id,
                        Name = obj.Member.Name,
                        ChandaNo = obj.Member.ChandaNo,
                        Items = chandaItems,
                        Month = obj.MonthPaidFor,
                        Year = obj.Year,
                        PaymentDate = obj.CreatedOn,
                        RecieptNo = obj.ReceiptNo,
                        ReferenceNo = obj.ReferenceNo,
                        SubTotal = totalAmountPaid
                    });
                }
            }

            var totalCount = invoiceItems.Count();

            if (usePaging)
            {
                var offset = (request.Page - 1) * request.PageSize;

                invoiceItems = (string.IsNullOrWhiteSpace(request.Keyword)) ? invoiceItems.Skip(offset).Take(request.PageSize).ToList() : invoiceItems.SearchByKeyword(request.Keyword).Skip(offset).Take(request.PageSize).ToList();

                var result = invoiceItems.ToPaginatedList(totalCount, request.Page, request.PageSize);
                return new MemberReport
                {
                    TotalAmountSummary = totalAmountSummary,
                    NoOfMonthsPaid = noOfMonthsPaid,
                    ReportResponses = result
                };
            }
            else
            {
                invoiceItems = (string.IsNullOrWhiteSpace(request.Keyword)) ? invoiceItems : [.. invoiceItems.SearchByKeyword(request.Keyword)];
                var result = invoiceItems.ToPaginatedList(totalCount, 1, totalCount);
                return new MemberReport
                {
                    TotalAmountSummary = totalAmountSummary,
                    NoOfMonthsPaid = noOfMonthsPaid,
                    ReportResponses = result
                };
            }
        }

        private IQueryable<InvoiceItem> PeriodicFilter(IQueryable<InvoiceItem> query, string filter, DateTime startDate, DateTime endDate)
        {
            switch (filter)
            {
                case "lastyear":
                    var year = DateTime.Now.Year - 1;
                    query = query.Where(ii => ii.CreatedOn.Year == year);
                    break;

                case "thismonth":
                    query = query.Where(ii => ii.CreatedOn.Month == DateTime.Now.Month);
                    break;

                case "lastmonth":
                    var month = DateTime.Now.Month - 1;
                    query = query.Where(ii => ii.CreatedOn.Month == month);
                    break;
                case "betweendates":
                    query = query.Where(ii => ii.CreatedOn.Date >= startDate.Date && ii.CreatedOn.Date <= endDate.Date);
                    break;

                default:
                    query = query.Where(ii => ii.CreatedOn.Year == DateTime.Now.Year);
                    break;
            }
            return query;
        }

        //private MemberReport 
    }
}

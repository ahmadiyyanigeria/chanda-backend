using Application.Paging;
using Application.Repositories;
using Domain.Constants;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Mapping.Extensions;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.EntityFrameworkCore;
using static Application.Queries.GetCircuitDefaulters;
using static Application.Queries.GetCircuitJamaatsReport;
using static Application.Queries.GetCircuitReport;
using static Application.Queries.GetJamaatDefaulters;
using static Application.Queries.GetJamaatMembersReport;
using static Application.Queries.GetJamaatReport;
using static Application.Queries.GetMemberReport;
using static Application.Queries.GetOverrallSummary;

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
                var filters = request.Filter.ToLower().Replace(" ", "").Split(",");
                foreach (var filter in filters)
                    query = PeriodicFilter(query, filter, request.Year, request.Month, request.StartDate, request.EndDate);
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

        public async Task<JamaatReport> GetJamaatReportAsync(Guid jamaatId, string? chandaType, PageRequest request, bool usePaging)
        {
            var query = _context.InvoiceItems.Include(ii => ii.Member).Include(ii => ii.Jamaat).Include(ii => ii.ChandaItems).ThenInclude(ci => ci.ChandaType)
                .Where(ii => ii.JamaatId == jamaatId);

            if (!string.IsNullOrEmpty(request.Filter))
            {
                var filters = request.Filter.ToLower().Replace(" ", "").Split(",");
                foreach (var filter in filters)
                    query = PeriodicFilter(query, filter, request.Year, request.Month, request.StartDate, request.EndDate);
            }
            else
            {
                query = query.Where(ii => ii.Year == DateTime.Now.Year);
            }

            if (request.IsDescending)
            {
                query = query.OrderByDescending(i => i.CreatedOn);
            }
            var count = await query.CountAsync();

            var queryGrp = query.GroupBy(ii => new { ii.MonthPaidFor, ii.Year });

            var totalAmountSummary = 0m;
            var paidByNoOfMembers = new List<string>();
            var invoiceItems = new List<JamaatReportResponse>();
            foreach (var eachGrp in queryGrp)
            {
                var name = "";
                var totalAmountPaid = 0m;
                var allMembers = new List<string>();
                var chandaItems = new List<ItemObject>();
                foreach (var obj in eachGrp)
                {
                    name = obj.Jamaat.Name;
                    if (!string.IsNullOrEmpty(chandaType))
                    {
                        var item = obj.ChandaItems.Where(ci => ci.ChandaType.Name.Equals(chandaType, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                        if (item != null)
                        {
                            allMembers.Add(obj.PayerId.ToString());
                            totalAmountPaid += item.Amount;
                            if(chandaItems.Any(c => c.ChandaType == item.ChandaType.Name))
                            {
                                chandaItems.Where(c => c.ChandaType == item.ChandaType.Name).First().Amount += item.Amount;
                            }
                            else
                            {
                                chandaItems.Add(new ItemObject { ChandaType = item.ChandaType.Name, Amount = item.Amount });
                            }
                            
                        }
                    }
                    else
                    {
                        allMembers.Add(obj.PayerId.ToString());
                        foreach (var item in obj.ChandaItems)
                        {
                            totalAmountPaid += item.Amount;
                            if (chandaItems.Any(c => c.ChandaType == item.ChandaType.Name))
                            {
                                chandaItems.Where(c => c.ChandaType == item.ChandaType.Name).First().Amount += item.Amount;
                            }
                            else
                            {
                                chandaItems.Add(new ItemObject { ChandaType = item.ChandaType.Name, Amount = item.Amount });
                            }
                        }
                    }
                }
                if (chandaItems.Count > 0)
                {
                    totalAmountSummary += totalAmountPaid;
                    invoiceItems.Add(new JamaatReportResponse
                    {
                        JamaatId = jamaatId,
                        JamaatName = name,
                        Items = chandaItems,
                        Month = eachGrp.Key.MonthPaidFor,
                        Year = eachGrp.Key.Year,
                        NoOfMembers = allMembers.Distinct().Count(),
                        MonthAmount = totalAmountPaid
                    });
                }
                paidByNoOfMembers.AddRange(allMembers);
            }

            var totalCount = invoiceItems.Count();

            if (usePaging)
            {
                var offset = (request.Page - 1) * request.PageSize;

                invoiceItems = (string.IsNullOrWhiteSpace(request.Keyword)) ? invoiceItems.Skip(offset).Take(request.PageSize).ToList() : invoiceItems.SearchByKeyword(request.Keyword).Skip(offset).Take(request.PageSize).ToList();

                var result = invoiceItems.ToPaginatedList(totalCount, request.Page, request.PageSize);
                return new JamaatReport
                {
                    TotalAmountSummary = totalAmountSummary,
                    PaidByNoOfMembers = paidByNoOfMembers.Distinct().Count(),
                    ReportResponses = result
                };
            }
            else
            {
                invoiceItems = (string.IsNullOrWhiteSpace(request.Keyword)) ? invoiceItems : [.. invoiceItems.SearchByKeyword(request.Keyword)];
                var result = invoiceItems.ToPaginatedList(totalCount, 1, totalCount);
                return new JamaatReport
                {
                    TotalAmountSummary = totalAmountSummary,
                    PaidByNoOfMembers = paidByNoOfMembers.Distinct().Count(),
                    ReportResponses = result
                };
            }
        }

        public async Task<JamaatMembersReport> GetJamaatMembersReportAsync(Guid jamaatId, string? chandaType, PageRequest request)
        {
            var query = _context.InvoiceItems.Include(ii => ii.Member).Include(ii => ii.Jamaat).Include(ii => ii.ChandaItems).ThenInclude(ci => ci.ChandaType).Where(ii => ii.JamaatId == jamaatId);

            if (!string.IsNullOrEmpty(request.Filter))
            {
                var filters = request.Filter.ToLower().Replace(" ", "").Split(",");
                foreach (var filter in filters)
                    query = PeriodicFilter(query, filter, request.Year, request.Month, request.StartDate, request.EndDate);
            }
            else
            {
                query = query.Where(ii => ii.Year == DateTime.Now.Year);
            }

            if (request.IsDescending)
            {
                query = query.OrderByDescending(i => i.CreatedOn);
            }
            var count = await query.CountAsync();

            var totalAmountSummary = 0m;
            var paidByNoOfMember = new List<string>();
            var invoiceItems = new List<MemberReportResponse>();
            foreach (var obj in query)
            {
                var totalAmountPaid = 0m;
                var chandaItems = new List<Item>();
                if (!string.IsNullOrEmpty(chandaType))
                {
                    var item = obj.ChandaItems.Where(ci => ci.ChandaType.Name.Equals(chandaType, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                    if (item != null)
                    {
                        paidByNoOfMember.Add(obj.Member.Id.ToString());
                        totalAmountPaid += item.Amount;
                        chandaItems.Add(new Item(item.ChandaType.Name, item.Amount));
                    }
                }
                else
                {
                    paidByNoOfMember.Add(obj.Member.Id.ToString());
                    foreach (var item in obj.ChandaItems)
                    {
                        totalAmountPaid += item.Amount;
                        chandaItems.Add(new Item(item.ChandaType.Name, item.Amount));
                    }
                }
                if (chandaItems.Count > 0)
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

            var offset = (request.Page - 1) * request.PageSize;

            invoiceItems = (string.IsNullOrWhiteSpace(request.Keyword)) ? invoiceItems.Skip(offset).Take(request.PageSize).ToList() : invoiceItems.SearchByKeyword(request.Keyword).Skip(offset).Take(request.PageSize).ToList();

            var result = invoiceItems.ToPaginatedList(totalCount, request.Page, request.PageSize);
            return new JamaatMembersReport
            {
                TotalAmountSummary = totalAmountSummary,
                PaidByNoOfMembers = paidByNoOfMember.Distinct().Count(),
                ReportResponses = result
            };            
        }

        public async Task<CircuitReport> GetCircuitReportAsync(Guid circuitId, string? chandaType, PageRequest request, bool usePaging)
        {
            var query = _context.InvoiceItems.Include(ii => ii.Jamaat).ThenInclude(j => j.Circuit).Include(ii => ii.ChandaItems).ThenInclude(ci => ci.ChandaType).Where(ii => ii.Jamaat.CircuitId == circuitId);

            if (!string.IsNullOrEmpty(request.Filter))
            {
                var filters = request.Filter.ToLower().Replace(" ", "").Split(",");
                foreach (var filter in filters)
                    query = PeriodicFilter(query, filter, request.Year, request.Month, request.StartDate, request.EndDate);
            }
            else
            {
                query = query.Where(ii => ii.Year == DateTime.Now.Year);
            }

            if (request.IsDescending)
            {
                query = query.OrderByDescending(i => i.CreatedOn);
            }
            var count = await query.CountAsync();

            var queryGrp = query.GroupBy(ii => new { ii.MonthPaidFor, ii.Year });

            var totalAmountSummary = 0m;
            var paidByNoOfJamaat = new List<string>();
            var reports = new List<CircuitReportResponse>();
            foreach (var eachGrp in queryGrp)
            {
                var name = "";
                var totalAmountPaid = 0m;
                var allJamaat = new List<string>();
                var chandaItems = new List<ItemObject>();
                foreach (var obj in eachGrp)
                {
                    name = obj.Jamaat.Circuit.Name;
                    if (!string.IsNullOrEmpty(chandaType))
                    {
                        var item = obj.ChandaItems.Where(ci => ci.ChandaType.Name.Equals(chandaType, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                        if (item != null)
                        {
                            allJamaat.Add(obj.JamaatId.ToString());
                            totalAmountPaid += item.Amount;
                            if (chandaItems.Any(c => c.ChandaType == item.ChandaType.Name))
                            {
                                chandaItems.Where(c => c.ChandaType == item.ChandaType.Name).First().Amount += item.Amount;
                            }
                            else
                            {
                                chandaItems.Add(new ItemObject { ChandaType = item.ChandaType.Name, Amount = item.Amount });
                            }
                        }
                    }
                    else
                    {
                        allJamaat.Add(obj.JamaatId.ToString());
                        foreach (var item in obj.ChandaItems)
                        {
                            totalAmountPaid += item.Amount;
                            if (chandaItems.Any(c => c.ChandaType == item.ChandaType.Name))
                            {
                                chandaItems.Where(c => c.ChandaType == item.ChandaType.Name).First().Amount += item.Amount;
                            }
                            else
                            {
                                chandaItems.Add(new ItemObject { ChandaType = item.ChandaType.Name, Amount = item.Amount });
                            }
                        }
                    }
                }
                if (chandaItems.Count > 0)
                {
                    totalAmountSummary += totalAmountPaid;
                    reports.Add(new CircuitReportResponse
                    {
                        CircuitId = circuitId,
                        CircuitName = name,
                        Items = chandaItems,
                        Month = eachGrp.Key.MonthPaidFor,
                        Year = eachGrp.Key.Year,
                        NoOfJamaats = allJamaat.Distinct().Count(),
                        MonthAmount = totalAmountPaid
                    });
                }
                paidByNoOfJamaat.AddRange(allJamaat);
            }

            var totalCount = reports.Count();

            if (usePaging)
            {
                var offset = (request.Page - 1) * request.PageSize;

                reports = (string.IsNullOrWhiteSpace(request.Keyword)) ? reports.Skip(offset).Take(request.PageSize).ToList() : reports.SearchByKeyword(request.Keyword).Skip(offset).Take(request.PageSize).ToList();

                var result = reports.ToPaginatedList(totalCount, request.Page, request.PageSize);
                return new CircuitReport
                {
                    TotalAmountSummary = totalAmountSummary,
                    PaidByNoOfJamaats = paidByNoOfJamaat.Distinct().Count(),
                    ReportResponses = result
                };
            }
            else
            {
                reports = (string.IsNullOrWhiteSpace(request.Keyword)) ? reports : [.. reports.SearchByKeyword(request.Keyword)];
                var result = reports.ToPaginatedList(totalCount, 1, totalCount);
                return new CircuitReport
                {
                    TotalAmountSummary = totalAmountSummary,
                    PaidByNoOfJamaats = paidByNoOfJamaat.Distinct().Count(),
                    ReportResponses = result
                };
            }
        }

        public async Task<CircuitJamaatsReport> GetCircuitJamaatsReportAsync(Guid circuitId, string? chandaType, PageRequest request, bool usePaging)
        {
            var query = _context.InvoiceItems.Include(ii => ii.Jamaat).ThenInclude(j => j.Circuit).Include(ii => ii.ChandaItems).ThenInclude(ci => ci.ChandaType).Where(ii => ii.Jamaat.CircuitId == circuitId);

            if (!string.IsNullOrEmpty(request.Filter))
            {
                var filters = request.Filter.ToLower().Replace(" ", "").Split(",");
                foreach (var filter in filters)
                    query = PeriodicFilter(query, filter, request.Year, request.Month, request.StartDate, request.EndDate);
            }
            else
            {
                query = query.Where(ii => ii.Year == DateTime.Now.Year);
            }

            if (request.IsDescending)
            {
                query = query.OrderByDescending(i => i.CreatedOn);
            }
            var count = await query.CountAsync();

            var queryGrp = query.GroupBy(ii => new { ii.MonthPaidFor, ii.Year, ii.JamaatId });

            var totalAmountSummary = 0m;
            var paidByNoOfJamaat = new List<string>();
            var reports = new List<JamaatReportResponse>();
            foreach (var eachGrp in queryGrp)
            {
                var name = "";
                var totalAmountPaid = 0m;
                var allMembers = new List<string>();
                var chandaItems = new List<ItemObject>();
                foreach (var obj in eachGrp)
                {
                    name = obj.Jamaat.Name;
                    if (!string.IsNullOrEmpty(chandaType))
                    {
                        var item = obj.ChandaItems.Where(ci => ci.ChandaType.Name.Equals(chandaType, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                        if (item != null)
                        {
                            paidByNoOfJamaat.Add(obj.JamaatId.ToString());
                            allMembers.Add(obj.PayerId.ToString());
                            totalAmountPaid += item.Amount;
                            if (chandaItems.Any(c => c.ChandaType == item.ChandaType.Name))
                            {
                                chandaItems.Where(c => c.ChandaType == item.ChandaType.Name).First().Amount += item.Amount;
                            }
                            else
                            {
                                chandaItems.Add(new ItemObject { ChandaType = item.ChandaType.Name, Amount = item.Amount });
                            }
                        }
                    }
                    else
                    {
                        allMembers.Add(obj.PayerId.ToString());
                        paidByNoOfJamaat.Add(obj.JamaatId.ToString());
                        foreach (var item in obj.ChandaItems)
                        {
                            totalAmountPaid += item.Amount;
                            if (chandaItems.Any(c => c.ChandaType == item.ChandaType.Name))
                            {
                                chandaItems.Where(c => c.ChandaType == item.ChandaType.Name).First().Amount += item.Amount;
                            }
                            else
                            {
                                chandaItems.Add(new ItemObject { ChandaType = item.ChandaType.Name, Amount = item.Amount });
                            }
                        }
                    }
                }
                if (chandaItems.Count > 0)
                {
                    totalAmountSummary += totalAmountPaid;
                    reports.Add(new JamaatReportResponse
                    {
                        JamaatId = eachGrp.Key.JamaatId,
                        JamaatName = name,
                        Items = chandaItems,
                        Month = eachGrp.Key.MonthPaidFor,
                        Year = eachGrp.Key.Year,
                        NoOfMembers = allMembers.Distinct().Count(),
                        MonthAmount = totalAmountPaid
                    });
                }
            }

            var totalCount = reports.Count();

            if (usePaging)
            {
                var offset = (request.Page - 1) * request.PageSize;

                reports = (string.IsNullOrWhiteSpace(request.Keyword)) ? reports.Skip(offset).Take(request.PageSize).ToList() : reports.SearchByKeyword(request.Keyword).Skip(offset).Take(request.PageSize).ToList();

                var result = reports.ToPaginatedList(totalCount, request.Page, request.PageSize);
                return new CircuitJamaatsReport
                {
                    TotalAmountSummary = totalAmountSummary,
                    PaidByNoOfJamaats = paidByNoOfJamaat.Distinct().Count(),
                    ReportResponses = result
                };
            }
            else
            {
                reports = (string.IsNullOrWhiteSpace(request.Keyword)) ? reports : [.. reports.SearchByKeyword(request.Keyword)];
                var result = reports.ToPaginatedList(totalCount, 1, totalCount);
                return new CircuitJamaatsReport
                {
                    TotalAmountSummary = totalAmountSummary,
                    PaidByNoOfJamaats = paidByNoOfJamaat.Distinct().Count(),
                    ReportResponses = result
                };
            }
        }

        public async Task<OverrallReport> GetOverrallReportAsync(string? chandaType, PageRequest request, bool usePaging)
        {
            var query = _context.InvoiceItems.Include(ii => ii.Jamaat).ThenInclude(j => j.Circuit).Include(ii => ii.ChandaItems).ThenInclude(ci => ci.ChandaType).AsQueryable();

            if (!string.IsNullOrEmpty(request.Filter))
            {
                var filters = request.Filter.ToLower().Replace(" ", "").Split(",");
                foreach (var filter in filters)
                    query = PeriodicFilter(query, filter, request.Year, request.Month, request.StartDate, request.EndDate);
            }
            else
            {
                query = query.Where(ii => ii.Year == DateTime.Now.Year);
            }

            if (request.IsDescending)
            {
                query = query.OrderByDescending(i => i.CreatedOn);
            }
            var count = await query.CountAsync();

            var queryGrp = query.GroupBy(ii => new { ii.MonthPaidFor, ii.Year, ii.Jamaat.Circuit.Id });

            var totalAmountSummary = 0m;
            var paidByNoOfCircuit = new List<string>();
            var reports = new List<CircuitReportResponse>();
            foreach (var eachGrp in queryGrp)
            {
                Guid id = default;
                var name = "";
                var totalAmountPaid = 0m;
                var allJamaats = new List<string>();
                var chandaItems = new List<ItemObject>();
                foreach (var obj in eachGrp)
                {
                    id = obj.Jamaat.CircuitId;
                    name = obj.Jamaat.Circuit.Name;
                    if (!string.IsNullOrEmpty(chandaType))
                    {
                        var item = obj.ChandaItems.Where(ci => ci.ChandaType.Name.Equals(chandaType, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();
                        if (item != null)
                        {
                            paidByNoOfCircuit.Add(obj.Jamaat.CircuitId.ToString());
                            allJamaats.Add(obj.JamaatId.ToString());
                            totalAmountPaid += item.Amount;
                            if (chandaItems.Any(c => c.ChandaType == item.ChandaType.Name))
                            {
                                chandaItems.Where(c => c.ChandaType == item.ChandaType.Name).First().Amount += item.Amount;
                            }
                            else
                            {
                                chandaItems.Add(new ItemObject { ChandaType = item.ChandaType.Name, Amount = item.Amount });
                            }
                        }
                    }
                    else
                    {
                        allJamaats.Add(obj.JamaatId.ToString());
                        paidByNoOfCircuit.Add(obj.Jamaat.CircuitId.ToString());
                        foreach (var item in obj.ChandaItems)
                        {
                            totalAmountPaid += item.Amount;
                            if (chandaItems.Any(c => c.ChandaType == item.ChandaType.Name))
                            {
                                chandaItems.Where(c => c.ChandaType == item.ChandaType.Name).First().Amount += item.Amount;
                            }
                            else
                            {
                                chandaItems.Add(new ItemObject { ChandaType = item.ChandaType.Name, Amount = item.Amount });
                            }
                        }
                    }
                }
                if (chandaItems.Count > 0)
                {
                    totalAmountSummary += totalAmountPaid;
                    reports.Add(new CircuitReportResponse
                    {
                        CircuitId = id,
                        CircuitName = name,
                        Items = chandaItems,
                        Month = eachGrp.Key.MonthPaidFor,
                        Year = eachGrp.Key.Year,
                        NoOfJamaats = allJamaats.Distinct().Count(),
                        MonthAmount = totalAmountPaid
                    });
                }
            }

            var totalCount = reports.Count();

            if (usePaging)
            {
                var offset = (request.Page - 1) * request.PageSize;

                reports = (string.IsNullOrWhiteSpace(request.Keyword)) ? reports.Skip(offset).Take(request.PageSize).ToList() : reports.SearchByKeyword(request.Keyword).Skip(offset).Take(request.PageSize).ToList();

                var result = reports.ToPaginatedList(totalCount, request.Page, request.PageSize);
                return new OverrallReport
                {
                    TotalAmountSummary = totalAmountSummary,
                    PaidByNoOfCircuits = paidByNoOfCircuit.Distinct().Count(),
                    ReportResponses = result
                };
            }
            else
            {
                reports = (string.IsNullOrWhiteSpace(request.Keyword)) ? reports : [.. reports.SearchByKeyword(request.Keyword)];
                var result = reports.ToPaginatedList(totalCount, 1, totalCount);
                return new OverrallReport
                {
                    TotalAmountSummary = totalAmountSummary,
                    PaidByNoOfCircuits = paidByNoOfCircuit.Distinct().Count(),
                    ReportResponses = result
                };
            }
        }

        public async Task<JamaatDefaulter> GetJamaatDefaulterAsync(Guid jamaatId, string jamaatName, string chandaType, PageRequest request, bool usePaging)
        {
            var year = request.Year <= 0 ? DateTime.Now.Year : request.Year;
            var month = request.Month <= 0 || request.Month > 12 ? DateTime.Now.Month : request.Month;
            var chandaTypes = chandaType.Equals(Constants.ChandaWasiyyat, StringComparison.InvariantCultureIgnoreCase)
                || chandaType.Equals(Constants.ChandaAam, StringComparison.InvariantCultureIgnoreCase)
                ? new string[] { Constants.ChandaAam, Constants.ChandaWasiyyat } : new string[] { chandaType };

            var members = await _context.Members.Include(m => m.Jamaat).Where(m => m.JamaatId == jamaatId).ToListAsync();
            
            var payments =  _context.InvoiceItems.Include(ii => ii.ChandaItems).ThenInclude(ci => ci.ChandaType)
                .Where(ii => ii.MonthPaidFor == (MonthOfTheYear)month && ii.Year == year && ii.JamaatId == jamaatId
                    && ii.ChandaItems.Any(ci => chandaTypes.Contains(ci.ChandaType.Name))).GroupBy(i => i.PayerId)
                .ToDictionary(g => g.Key, g => g.SelectMany(ii => ii.ChandaItems).ToList());

            var defaulters = members.Where(m => !payments.TryGetValue(m.Id, out var memberPay));

            var reports = new List<Defaulter>();
            foreach ( var member in defaulters )
            {
                reports.Add(new Defaulter
                {
                    MemberId = member.Id,
                    MemberName = member.Name,
                    ChandaNo = member.ChandaNo,
                    PhoneNO = member.PhoneNo,
                    Email = member.Email
                });
            }

            var totalCount = reports.Count();

            if (usePaging)
            {
                var offset = (request.Page - 1) * request.PageSize;

                reports = (string.IsNullOrWhiteSpace(request.Keyword)) ? reports.Skip(offset).Take(request.PageSize).ToList() : reports.SearchByKeyword(request.Keyword).Skip(offset).Take(request.PageSize).ToList();

                var result = reports.ToPaginatedList(totalCount, request.Page, request.PageSize);
                return new JamaatDefaulter
                {
                    JamaatId = jamaatId,
                    JamaatName = jamaatName,
                    Month = (MonthOfTheYear)month,
                    Year = year,
                    ChandaType = chandaType,
                    NoOfDefaulters = totalCount,
                    Defaulters = result
                };
            }
            else
            {
                reports = (string.IsNullOrWhiteSpace(request.Keyword)) ? reports : [.. reports.SearchByKeyword(request.Keyword)];
                var result = reports.ToPaginatedList(totalCount, 1, totalCount);
                return new JamaatDefaulter
                {
                    JamaatId = jamaatId,
                    JamaatName = jamaatName,
                    Month = (MonthOfTheYear)month,
                    Year = year,
                    ChandaType = chandaType,
                    NoOfDefaulters = totalCount,
                    Defaulters = result
                };
            }
        }


        public async Task<CircuitDefaulters> GetCircuitDefaulterAsync(Guid circuitId, string circuitName, string chandaType, PageRequest request, bool usePaging)
        {
            var year = request.Year <= 0 ? DateTime.Now.Year : request.Year;
            var month = request.Month <= 0 || request.Month > 12 ? DateTime.Now.Month : request.Month;
            var chandaTypes = chandaType.Equals(Constants.ChandaWasiyyat, StringComparison.InvariantCultureIgnoreCase)
                || chandaType.Equals(Constants.ChandaAam, StringComparison.InvariantCultureIgnoreCase)
                ? new string[] { Constants.ChandaAam, Constants.ChandaWasiyyat } : new string[] { chandaType };

            var members = await _context.Members.Include(m => m.Jamaat).Where(m => m.Jamaat.CircuitId == circuitId).ToListAsync();

            var payments = _context.InvoiceItems.Include(ii => ii.Jamaat).Include(ii => ii.ChandaItems)
                .ThenInclude(ci => ci.ChandaType).Where(ii => ii.MonthPaidFor == (MonthOfTheYear)month 
                    && ii.Year == year && ii.Jamaat.CircuitId == circuitId
                    && ii.ChandaItems.Any(ci => chandaTypes.Contains(ci.ChandaType.Name))).GroupBy(i => i.PayerId)
                .ToDictionary(g => g.Key, g => g.SelectMany(ii => ii.ChandaItems).ToList());

            var defaulters = members.Where(m => !payments.TryGetValue(m.Id, out var memberPay));

            defaulters = defaulters.OrderBy(d => d.Jamaat.Name);

            var name = "";
            var reports = new List<DefaulterMember>();
            foreach (var member in defaulters)
            {
                name = member.Jamaat.Name;
                reports.Add(new DefaulterMember
                {
                    MemberId = member.Id,
                    MemberName = member.Name,
                    ChandaNo = member.ChandaNo,
                    PhoneNO = member.PhoneNo,
                    Email = member.Email,
                    JamaatName = member.Jamaat.Name
                });
            }

            var totalCount = reports.Count();
            if (usePaging)
            {
                var offset = (request.Page - 1) * request.PageSize;
                reports = (string.IsNullOrWhiteSpace(request.Keyword)) ? reports.Skip(offset).Take(request.PageSize).ToList() : reports.SearchByKeyword(request.Keyword).Skip(offset).Take(request.PageSize).ToList();

                var result = reports.ToPaginatedList(totalCount, request.Page, request.PageSize);
                return new CircuitDefaulters
                {
                    CircuitId = circuitId,
                    CircuitName = circuitName,
                    Month = (MonthOfTheYear)month,
                    Year = year,
                    ChandaType = chandaType,
                    NoOfDefaulters = totalCount,
                    DefaulterMembers = result
                };
            }
            else
            {
                reports = (string.IsNullOrWhiteSpace(request.Keyword)) ? reports : [.. reports.SearchByKeyword(request.Keyword)];
                var result = reports.ToPaginatedList(totalCount, 1, totalCount);
                return new CircuitDefaulters
                {
                    CircuitId = circuitId,
                    CircuitName = circuitName,
                    Month = (MonthOfTheYear)month,
                    Year = year,
                    ChandaType = chandaType,
                    NoOfDefaulters = totalCount,
                    DefaulterMembers = result
                };
            }
        }

        private IQueryable<InvoiceItem> PeriodicFilter(IQueryable<InvoiceItem> query, string filter, int year, int month, DateTime startDate, DateTime endDate)
        {
            switch (filter)
            {
                case "lastyear":
                    var latYear = DateTime.Now.Year - 1;
                    query = query.Where(ii => ii.Year == latYear);
                    break;

                case "thismonth":
                    query = query.Where(ii => ii.MonthPaidFor == (MonthOfTheYear)DateTime.Now.Month);
                    break;

                case "lastmonth":
                    var lastMonth = DateTime.Now.Month - 1 == 0 ? 12 : DateTime.Now.Month - 1;                    
                    query = query.Where(ii => ii.MonthPaidFor == (MonthOfTheYear)lastMonth);
                    break;
                case "betweendates":
                    if(startDate != default && endDate != default && startDate < endDate)
                    {
                        query = query.Where(ii => ii.Year >= startDate.Year && ii.Year <= endDate.Year);
                        query = query.Where(ii => ii.MonthPaidFor >= (MonthOfTheYear)startDate.Month && ii.MonthPaidFor <= (MonthOfTheYear)endDate.Month);
                    }
                    break;

                case "month":
                    if(month > 0 && month < 13)
                    {
                        query = query.Where(ii => ii.MonthPaidFor == (MonthOfTheYear) month);
                    }
                    break;

                case "year":
                    if (year > 0)
                    {
                        query = query.Where(ii => ii.Year == year);
                    }
                    break;

                default:
                    query = query.Where(ii => ii.Year == DateTime.Now.Year);
                    break;
            }
            return query;
        }

    }
}

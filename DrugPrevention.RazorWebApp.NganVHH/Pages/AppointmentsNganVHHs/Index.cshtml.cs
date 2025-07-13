using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DrugPrevention.Repositories.NganVHH.Models;
using DrugPrevention.Services.NganVHH;

namespace DrugPrevention.RazorWebApp.NganVHH.Pages.AppointmentsNganVHHs
{
    public class IndexModel : PageModel
    {
        private readonly IAppointmentsNganVHHService _appointmentsNganVHHService;
        public IndexModel(IAppointmentsNganVHHService appointmentsNganVHHService)
        {
            _appointmentsNganVHHService = appointmentsNganVHHService;
        }

        public IList<AppointmentsNganVHH> AppointmentsNganVHH { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchPrimaryIssue { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public int? SearchDuration { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchSpecialization { get; set; } = string.Empty;

        // Pagination properties
        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;
        public int TotalItems { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;

        //public async Task OnGetAsync()
        //{
        //    // Ensure PageNumber is at least 1
        //    if (PageNumber < 1)
        //        PageNumber = 1;

        //    // Nếu có tham số search, thực hiện tìm kiếm với phân trang
        //    if (!string.IsNullOrEmpty(SearchPrimaryIssue) || SearchDuration.HasValue || !string.IsNullOrEmpty(SearchSpecialization))
        //    {
        //        // Get search results with pagination
        //        AppointmentsNganVHH = await _appointmentsNganVHHService.SearchAsync(SearchPrimaryIssue, SearchDuration, SearchSpecialization, PageNumber, PageSize);

        //        // Get total count for search results
        //        TotalItems = await _appointmentsNganVHHService.GetSearchCountAsync(SearchPrimaryIssue, SearchDuration, SearchSpecialization);
        //    }
        //    else
        //    {
        //        // Nếu không có tham số search, hiển thị tất cả với phân trang
        //        AppointmentsNganVHH = await _appointmentsNganVHHService.GetAllAsync(PageNumber, PageSize);

        //        // Get total count for all items
        //        TotalItems = await _appointmentsNganVHHService.GetTotalCountAsync();
        //    }

        //    // Ensure PageNumber is not greater than TotalPages
        //    if (PageNumber > TotalPages && TotalPages > 0)
        //    {
        //        PageNumber = TotalPages;
        //        // Re-fetch data with corrected page number
        //        if (!string.IsNullOrEmpty(SearchPrimaryIssue) || SearchDuration.HasValue || !string.IsNullOrEmpty(SearchSpecialization))
        //        {
        //            AppointmentsNganVHH = await _appointmentsNganVHHService.SearchAsync(SearchPrimaryIssue, SearchDuration, SearchSpecialization, PageNumber, PageSize);
        //        }
        //        else
        //        {
        //            AppointmentsNganVHH = await _appointmentsNganVHHService.GetAllAsync(PageNumber, PageSize);
        //        }
        //    }
        //}

        public async Task OnGetAsync()
        {
            // Ensure PageNumber is at least 1
            if (PageNumber < 1)
                PageNumber = 1;

            // Load data and get total count
            await LoadDataAsync();

            // Handle page correction if needed
            if (PageNumber > TotalPages && TotalPages > 0)
            {
                PageNumber = TotalPages;
                await LoadDataAsync();
            }
        }

        private async Task LoadDataAsync()
        {
            if (HasSearchCriteria())
            {
                AppointmentsNganVHH = await _appointmentsNganVHHService.SearchAsync(
                    SearchPrimaryIssue, SearchDuration, SearchSpecialization, PageNumber, PageSize);
                TotalItems = await _appointmentsNganVHHService.GetSearchCountAsync(
                    SearchPrimaryIssue, SearchDuration, SearchSpecialization);
            }
            else
            {
                AppointmentsNganVHH = await _appointmentsNganVHHService.GetAllAsync(PageNumber, PageSize);
                TotalItems = await _appointmentsNganVHHService.GetTotalCountAsync();
            }
        }

        private bool HasSearchCriteria()
        {
            return !string.IsNullOrEmpty(SearchPrimaryIssue) ||
                   SearchDuration.HasValue ||
                   !string.IsNullOrEmpty(SearchSpecialization);
        }
    }
}

using Date_Management_Project.Services;
using Date_Management_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Date_Management_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly WorkingDaysService _svc;
        public HomeController(WorkingDaysService svc) => _svc = svc;

        [HttpGet]
        public IActionResult Index() =>
            View(new DateRangeViewModel());

        [HttpPost]
        public async Task<IActionResult> Index(DateRangeViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            // 1) pull holidays into VM for rendering checkboxes
            vm.IncludedHolidays = await _svc.GetHolidaysInRangeAsync(
                vm.From, vm.To, vm.CountryCode);

            // 2) compute days passing the two flags + selected holiday IDs
            vm.WorkingDays = await _svc.CountAsync(
                vm.From,
                vm.To,
                vm.CountryCode,
                vm.IncludeSaturday,
                vm.IncludeSunday,
                vm.WorkOnHolidayIds);

            return View(vm);
        }
    }
}

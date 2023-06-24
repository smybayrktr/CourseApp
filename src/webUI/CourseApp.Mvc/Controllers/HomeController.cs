using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CourseApp.Mvc.Models;
using CourseApp.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using CourseApp.DataTransferObject.Responses;
using CourseApp.Mvc.CacheTools;
using Microsoft.Extensions.Options;

namespace CourseApp.Mvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICourseService _courseService;
    private readonly IMemoryCache _memoryCache;

    public HomeController(ILogger<HomeController> logger, ICourseService courseService, IMemoryCache memoryCache)
    {
        _logger = logger;
        _courseService = courseService;
        _memoryCache = memoryCache;
    }

    public IActionResult Index(int pageNo = 1, int? id = null)
    {
        IEnumerable<CourseDisplayResponse> courses = GetCoursesMemCacheOrDb(id);

        /*
         * 1. Sayfa:
         * 0 eleman atla 8 eleman al
         * 
         * 2. Sayfa:
         * 8 eleman atla 8 eleman al
         * 
         * 3. sayfa:
         * 16 eleman atla 8 eleman al
         */
        /*Kursların toplam sayfa sayısı için hangi bilgiler gerekli?
         * 
         * 1. Sayfada kaç kurs olacak? ,
         * 2. Toplam kaç kurs var? 
         */
        var coursePerPage = 4;
        var courseCount = courses.Count();
        var totalPage = Math.Ceiling((decimal)courseCount / coursePerPage);

        var pagingInfo = new PagingInfo
        {
            CurrentPage = pageNo,
            ItemsPerPage = coursePerPage,
            TotalItems = courseCount
        };



        var paginatedCourses = courses.OrderBy(c => c.Id)
                                      .Skip((pageNo - 1) * coursePerPage)
                                      .Take(coursePerPage)
                                      .ToList();

        var model = new PaginationCourseViewModel
        {
            Courses = paginatedCourses,
            PagingInfo = pagingInfo
        };



        return View(model);


    }

    private IEnumerable<CourseDisplayResponse> GetCoursesMemCacheOrDb(int? id)
    {
        if (!_memoryCache.TryGetValue("allCourses", out CacheDataInfo cacheDataInfo))
        {
            //Absolide exp. Mutlak son kullanma, 20 dakika boyunca cachede tutar. Sonra siler. Her 20 dakikada bir bunu yapar.
            //Sliding exp. 5 dakika süre verdik, 4 dk 58 saniye boyunca aynı veriler kullanıldı ve istendiyse 5 dakika daha uzar 

            var options = new MemoryCacheEntryOptions()
                              .SetSlidingExpiration(TimeSpan.FromMinutes(1))
                              .SetPriority(CacheItemPriority.Normal);
            //Cache bellekten ne zaman çıkacak? sunucuda ilk sırada silecekse --> low, cacheden çıkmayacaksa -- never
            //.RegisterPostEvictionCallback()
            //Cacheden data düşünce CallBack fonk. çağırabilirsin
            cacheDataInfo = new CacheDataInfo()
            {
                CacheTime = DateTime.Now,
                Courses = _courseService.GetCourseDisplayResponses()
            };
            _memoryCache.Set("allCourses", cacheDataInfo, options);

        }
        _logger.LogInformation($"{cacheDataInfo.CacheTime.ToLongTimeString()} anındaki cache");
        return id == null ? cacheDataInfo.Courses :
                                        _courseService.GetCoursesByCategory(id.Value);
    }
    [ResponseCache(Duration =70, VaryByQueryKeys = new[] {"id"}, Location =ResponseCacheLocation.Client)]
    //id her değiştiğinde cacheden uçur
    //Location Cache ın nerede saklandığı. Herhangi yer, istemci, saklanmayacak mı? Hangisi?
    public IActionResult Privacy(int id)
    {
        ViewBag.Id = id;
        ViewBag.DateTime = DateTime.Now;
        return View();
    }
    //Cache saklanmasın diye yazılmış
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}


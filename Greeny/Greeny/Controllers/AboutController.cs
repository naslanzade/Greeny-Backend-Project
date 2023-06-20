using Greeny.Data;
using Greeny.Models;
using Greeny.Services.Interface;
using Greeny.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Greeny.Controllers
{
    public class AboutController : Controller
    {

        private readonly IAboutImageService _aboutImageService;
        private readonly IMilestoneService _milestoneService;
        private readonly ITextService _textService;
        private readonly ITeamService _teamService;
        private readonly ITestimonialService _testimonialService;
        private readonly IBgImageService _bgImageService;

        public AboutController(IAboutImageService aboutImageService,
                              IMilestoneService milestoneService,
                              ITextService textService,
                              ITeamService teamService,
                              ITestimonialService testimonialService,
                              IBgImageService bgImageService)
        {
            
            
            _aboutImageService = aboutImageService;
            _milestoneService = milestoneService;
            _textService = textService;
            _teamService = teamService;
            _testimonialService= testimonialService;
            _bgImageService= bgImageService;
        }


        public async Task< IActionResult> Index()
        {
            
            AboutVM aboutVM = new()
            {
                Images= await _aboutImageService.GetAllAsync(),
                Milestones= await _milestoneService.GetAllAsync(),
                Texts= await _textService.GetAllAsync(),
                Teams= await _teamService.GetAllAsync(),
                Testimonials= await _testimonialService.GetAllAsync(),
                BgImage=await _bgImageService.GetAllAsync(),
            };

            return View(aboutVM);
        }
    }
}

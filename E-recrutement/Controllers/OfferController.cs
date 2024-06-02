using E_recrutement.Data;
using E_recrutement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using Microsoft.AspNetCore.Identity;
using E_recrutement.Models.ViewModels;
using Microsoft.IdentityModel.Tokens;

namespace E_recrutement.Controllers
{
    public class OfferController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public OfferController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            List<Offer> offersList = new List<Offer>();
            if (User.IsInRole("Recruteur"))
            {
                var user = await _userManager.GetUserAsync(User);
                foreach (var offer in _db.Offers)
                {
                    if (offer.rectuteurId == user.Id)
                    {
                        offersList.Add(offer);
                    }
                }
                return View(offersList);
            }
            else return View(_db.Offers.ToList());
        }

        public IActionResult Categorie()
        {
            return View();
        }

        [Authorize(Roles = "Recruteur")]
        public async Task<IActionResult> OthersOffers()
        {
            var user = await _userManager.GetUserAsync(User);
            List<Offer> listOffers = new List<Offer>();
            foreach (var item in _db.Offers)
            {
                if(item.rectuteurId != user.Id)
                {
                    listOffers.Add(item);
                }
            }

            return View(listOffers);
        }

        [Authorize(Roles = "Recruteur")]
        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> MyApplications()
        {
            var user = await _userManager.GetUserAsync(User);
            List<CandidatureVM> candidatures = getAppliedForOffers(user.Id);
            return View(candidatures);
        }

        public List<CandidatureVM> getAppliedForOffers(string candidatId)
        {
            List<CandidatureVM> appliedForOffers = new List<CandidatureVM>();

            List<Candidature> listCandidatures = _db.Candidatures.ToList();

            foreach (var candidature in listCandidatures)
            {
                if (candidatId == candidature.CandidatId)
                {
                    CandidatureVM cv = new CandidatureVM();
                    cv.Apply = candidature;
                    cv.ConcernedOffer = _db.Offers.Find(candidature.OfferId);
                    appliedForOffers.Add(cv);
                }
            }

            return appliedForOffers;
        }

        public List<Offer> getCreatedOffers(string recruteurId)
        {
            List<Offer> createdOffers = new List<Offer>();

            List<Offer> listOffers = _db.Offers.ToList();

            foreach (var offer in listOffers)
            {
                if (recruteurId == offer.rectuteurId)
                {
                    createdOffers.Add(offer);
                }
            }

            return createdOffers;
        }

        public async Task<IActionResult> ReceivedApplications()
        {
            var user = await _userManager.GetUserAsync(User);
            List<Offer> createdOffers = getCreatedOffers(user.Id);
            List<Application> receivedApplications = new List<Application>();
            List<Candidature> candidatures = _db.Candidatures.ToList();
            List<ApplicationUser> users = _db.ApplicationUsers.ToList();
            foreach (var offer in createdOffers)
            {
                if (offer.rectuteurId == user.Id)
                {
                    Application app = new Application();
                    app.Canidats = new List<ApplicationUser>();
                    app.ConcernedOffer = offer;

                    foreach (Candidature c in candidatures)
                    {
                        if (offer.Id == c.OfferId)
                        {
                            app.ConcernedOffer = offer;
                            foreach (ApplicationUser u in users)
                            {
                                if (c.CandidatId == u.Id)
                                {
                                    app.Canidats.Add(u);
                                }
                            }
                        }
                    }
                    receivedApplications.Add(app);
                }
            }
            return View(receivedApplications);
        }

        [Authorize(Roles = "Recruteur")]
        [HttpPost]
        public async Task<IActionResult> Create(Offer obj)
        {
            obj.DatePub = DateOnly.FromDateTime(DateTime.Today);
            var user = await _userManager.GetUserAsync(User);
            obj.Remuneration = "$"+ obj.minSalary + " - $"+obj.maxSalary;
            obj.rectuteurId = user.Id;
            obj.Company = user.Company;
            obj.UrlCompanyLogo = user.urlImageCompany;

            _db.Offers.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int? id)
        {
            var obj = _db.Offers.Find(id);
            IsApplied isApplied = new IsApplied();
            isApplied.offer = obj;
            isApplied.isApplied = true;

            if (User.IsInRole("Candidat"))
            {
                var user = await _userManager.GetUserAsync(User);
                if(!isAlreadyApply(obj.Id, user.Id))
                {
                    isApplied.isApplied = false;
                }
            }

            return View(isApplied);
        }

        public Boolean isAlreadyApply(int idO, string idUser)
        {
            foreach (var c in _db.Candidatures.ToList())
            {
                if(c.OfferId == idO && c.CandidatId == idUser)
                {
                    return true;
                }
            }
            return false;
        }

        [Authorize(Roles = "Recruteur")]
        public IActionResult Edit(int? id)
        {
            var offer = _db.Offers.Find(id);
            return View(offer);
        }

        [Authorize(Roles = "Recruteur")]
        [HttpPost]
        public async Task<IActionResult> Edit(Offer obj)
        {
            obj.Remuneration = "$" + obj.minSalary + " - $" + obj.maxSalary;
            var user = await _userManager.GetUserAsync(User);
            obj.rectuteurId = user.Id;
            obj.Company = user.Company;
            obj.UrlCompanyLogo = user.urlImageCompany;

            if (ModelState.IsValid)
            {
                _db.Offers.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else return View();
        }

        [Authorize(Roles = "Recruteur")]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Offer obj = _db.Offers.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [Authorize(Roles = "Recruteur")]
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Offer obj = _db.Offers.Find(id);
            if (obj == null) return NotFound();

            _db.Offers.Remove(obj);

            List<Candidature> candidatures = _db.Candidatures.ToList();
            foreach (var c in candidatures)
            {
                if (c.OfferId == obj.Id)
                {
                    _db.Candidatures.Remove(c);
                }
            }

            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Candidat")]
        [HttpPost]
        public async Task<IActionResult> Apply(int? id)
        {
            var user = await _userManager.GetUserAsync(User);
            Candidature c = new Candidature();
            c.CandidatId = user.Id;
            c.OfferId = (int)id;
            c.MotivationLettre = (string)Request.Form["lettre"];
            c.DateApplication = DateOnly.FromDateTime(DateTime.Today);
            _db.Candidatures.Add(c);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Candidat")]
        public IActionResult ConsulterCandidature(int id)
        {
            CandidatureVM cv = new CandidatureVM();
            cv.Apply = _db.Candidatures.Find(id);
            cv.ConcernedOffer = _db.Offers.Find(cv.Apply.OfferId);

            return View(cv);
        }

        [Authorize(Roles = "Recruteur")]
        public IActionResult ConsulterApplication(string name, int id)
        {
            Postule p = new Postule();
            p.Candidature = new CandidatureVM();
            p.User = new ApplicationUser();

            p.Candidature = GetCandidatureVM(name, id);
            p.User = _db.ApplicationUsers.Find(name);

            return View(p);
        }

        public CandidatureVM GetCandidatureVM(string idC, int idO)
        {
            CandidatureVM cv = new CandidatureVM();
            cv.ConcernedOffer = _db.Offers.Find(idO);
            foreach(var c in _db.Candidatures.ToList())
            {
                if (c.CandidatId == idC && c.OfferId == idO)
                {
                    cv.Apply = c; break;
                }
            }

            return cv;
        }
        
        public List<Offer> SearchOffers(Profile profile, bool withoutProfile, string sector = null, decimal minSalary=0)
        {
            if (withoutProfile)
            {
                var result = _db.Offers.Where(o => (string.IsNullOrEmpty(sector) || o.Secteur.Contains(sector)) && o.minSalary >= minSalary).ToList();
                return result;
            }

            var result1 = _db.Offers.Where(o => (string.IsNullOrEmpty(sector) || o.Secteur.Contains(sector)) && (o.Profil == profile) && o.minSalary >= minSalary).ToList();
            return result1;
        }


        public IActionResult Search()
        {
            string sector = Request.Form["sector"] , profile= Request.Form["profile"], minSalary = Request.Form["minSalary"];
            int minS;
            bool withoutProfile=false;
            bool y = int.TryParse(minSalary, out minS);
            if (!y) { minS = 0; }
            Profile p ;
            switch (profile)
            {
                case "1":
                    p = Profile.Deug;
                    break;
                case "2":
                    p = Profile.Ingenieur;
                    break;
                case "3":
                    p = Profile.Licence;
                    break;
                case "4":
                    p = Profile.Master;
                    break;
                default:
                    p = Profile.Deug;
                    withoutProfile = true;
                    break;
            }

            return View(SearchOffers(p, withoutProfile, sector, minS));
        }
    }
}

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using E_recrutement.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace E_recrutement.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        public ApplicationUser user2 { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
            [Display(Name = "Title")]
            public string Titre { get; set; }
            [Display(Name = "Diplome")]
            public string Diplome { get; set; }
            [Display(Name = "Years of experience")]
            public int NbAnsExp { get; set; }
            [Display(Name = "Upload your resume")]
            public string CV { get; set; }
            [Display(Name = "Company")]
            public string Company { get; set; }
            [Display(Name = "Upload Comany Logo")]
            public string UrlImageCompany { get; set; }
            [Display(Name = "Profile")]
            public Profile? Profil { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var user1 = await _userManager.GetUserAsync(User);
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            int nbAnsExp = user1.NbAnsExp ?? 0;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                Company = user1.Company,
                Diplome = user1.Diplome,
                Titre = user1.Titre,
                NbAnsExp = nbAnsExp,
                Profil = user1.Profil
            };
        }


        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            user2 = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile file = null)
        {
            var user = await _userManager.GetUserAsync(User);
            
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string fileName;

            if (file != null && User.IsInRole("Candidat"))
            {
                fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);//donner un nom 
                string filePath = Path.Combine(wwwRootPath, @"CVs");
                using (var fileStrem = new FileStream(Path.Combine(filePath, fileName),
                FileMode.Create))
                {
                    file.CopyTo(fileStrem);
                }
                user.CV = fileName;
            }

            if (file != null && User.IsInRole("Recruteur"))
            {
                fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);//donner un nom 
                string filePath = Path.Combine(wwwRootPath, @"img");
                using (var fileStrem = new FileStream(Path.Combine(filePath, fileName),
                FileMode.Create))
                {
                    file.CopyTo(fileStrem);
                }
                user.urlImageCompany = fileName;
            }

            user.Titre = Input.Titre;
            user.NbAnsExp = Input.NbAnsExp;
            user.Diplome = Input.Diplome;
            user.Company = Input.Company;
            user.PhoneNumber = Input.PhoneNumber;
            user.Profil = Input.Profil;

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);

            await _userManager.UpdateAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}

using Projekt1.Models;
using Projekt1.Models.BL;
using Projekt1.Models.VM.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Projekt1.Controllers
{
    public class AccountController : Controller
    {
        public AccountBL BL = new AccountBL();

        [AllowAnonymous]
        public ActionResult PatientRegistration()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            ViewBag.FormTitle = "Rejestracja pacjenta";

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PatientRegistration(PatientRegistrationVM model)
        {
            ViewBag.FormTitle = "Rejestracja pacjenta";
            if (ModelState.IsValid)
            {


                // User found in the database
                if (!BL.CheckEmail(model.Email))
                {
                    Guid activationCode = BL.AddPatientWithActivationCode(model);

                    var message = new MailMessage();
                    message.To.Add(new MailAddress(model.Email)); //replace with valid value
                    message.Subject = "Your email subject";
                    string body = "Witaj " + model.Email + ",";
                    body += "<br /><br />Please click the following link to activate your account";
                    body += "<br /><a href = '" + string.Format("{0}://{1}/Account/ActivationAccount?email={3}&activateCode={2}", Request.Url.Scheme, Request.Url.Authority, activationCode, model.Email) + "'>Click here to activate your account.</a>";
                    body += "<br /><br />Thanks";
                    message.Body = body;
                    message.IsBodyHtml = true;
                    try
                    {
                        using (var smtp = new SmtpClient())
                        {
                            await smtp.SendMailAsync(message);
                            return RedirectToAction("RegistrationConfirmed");
                        }

                    }
                    catch (SmtpFailedRecipientException)
                    {
                        ModelState.AddModelError("", "Błedny email");
                        return View(model);


                    }

                }
                else
                {
                    ModelState.AddModelError("", "Email istnieje w bazie");
                }


            }
            return View(model);

        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            ViewBag.FormTitle = "Logowanie";

            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM model, string returnUrl)
        {
            ViewBag.FormTitle = "Logowanie";

            if (ModelState.IsValid)
            {


                // User found in the database
                if (BL.Login(model))
                {
                    if (!BL.IsActive(model.Email))
                    {

                        ModelState.AddModelError("", "Konto nie jest aktywne");
                        return View(model);
                    }

                    FormsAuthentication.SetAuthCookie(model.Email, false);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
               && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }

                }
                else
                {
                    ModelState.AddModelError("", "Nieprawidłowy login lub hasło.");
                }
            }


            // If we got this far, something failed, redisplay form
            return View(model);
        }
        [Authorize]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
        [AllowAnonymous]
        public ActionResult RegistrationConfirmed()
        {

            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [AllowAnonymous]
        public ActionResult ActivationConfirmed()
        {

            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [AllowAnonymous]
        public ActionResult ActivationAccount(string email, Guid activateCode)
        {
            if (BL.ActivationAccount(email, activateCode))


                return RedirectToAction("ActivationConfirmed");
            else
                return View();
        }



    }
}
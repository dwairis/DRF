using DRF.infrastructures;
using DRF.Repositories;
using DRF.Utilities;
using DRF.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Claims;
using System.Text.RegularExpressions;

//namespace DRF.Controllers
//{
//    public class AccountController : Controller
//    {
//        private readonly IAuthService authService;
//        private readonly IConfiguration configuration;
//        private readonly IUsersRepository userRepository;
//        private readonly ICustomEmailSender emailSender;
//        private readonly ISMSService smsService;

//        public AccountController(ISMSService smsService, IAuthService authService, IConfiguration configuration, IUsersRepository userRepository, ICustomEmailSender emailSender)
//        {
//            this.authService = authService;
//            this.configuration = configuration;
//            this.userRepository = userRepository;
//            this.emailSender = emailSender;
//            this.smsService = smsService;
//        }

//        [AllowAnonymous]
//        [HttpGet]
//        public IActionResult Login(string ReturnUrl)
//        {
//            if (!User.Identity.IsAuthenticated)
//            {
//                return View(new LoginPageViewModel()
//                {
//                    FormName = "Login",
//                    ReturnUrl = ReturnUrl
//                });
//            }
//            else
//            {
//                return RedirectToAction("Index", "Home");
//            }
//        }

//        [AllowAnonymous]
//        [HttpPost]
//        public async Task<IActionResult> Login(LoginPageViewModel model)
//        {
//            if (User.Identity.IsAuthenticated)
//                return RedirectToAction("Index", "Home");
//            if (ModelState.IsValid)
//            {
//                string hashedPassword = Helper.ToSHA3(model.Password);
//                var user = userRepository.GetByEmail(model.Email.Trim());
//                if (user != null)
//                {
//                    if (!user.IsLooked)
//                    {
//                        var verified = false;
//                        if (model.TwoFAMethod == "email")
//                        {
//                            var result = await smsService.VerifyEmailOTP(new SMSService.Models.VerifyEmailOTPRequest() { Code = model.Code, Email = (string.IsNullOrEmpty(user.TwoFAEmail) ? user.Email : user.TwoFAEmail) });
//                            verified = result.StatusCode == System.Net.HttpStatusCode.OK;
//                        }
//                        else if (model.TwoFAMethod == "phone")
//                        {
//                            var result = await smsService.VerifyOTP(new SMSService.Models.VerifyOTPRequest() { Code = model.Code, MobileNumber = user.MobileNumber });
//                            verified = result.StatusCode == System.Net.HttpStatusCode.OK;
//                        }

//                        if (verified)
//                        {
//                            if (user.Password.Equals(hashedPassword))
//                            {
//                                if (user.Role == "C" && user.ContractorId.HasValue)
//                                {
//                                    var contractorData = contractorsRepository.GetById(user.ContractorId);
//                                    if (contractorData != null)
//                                    {
//                                        if (!(Helper.Today.Date >= contractorData.StartContractDate && contractorData.EndContractDate >= Helper.Today))
//                                        {
//                                            ModelState.AddModelError("", "Your agreement is expired");
//                                            return View(model);
//                                        }
//                                    }
//                                    else
//                                    {
//                                        ModelState.AddModelError("", "the contractor profile not found");
//                                        return View(model);
//                                    }

//                                }
//                                if (user.IsActive)
//                                {
//                                    user.FailureAttempts = 0;
//                                    if (userRepository.Update(user))
//                                    {
//                                        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
//                                        identity.AddClaim(new Claim("Email", user.Email));
//                                        identity.AddClaim(new Claim("FullName", user.FullName));
//                                        identity.AddClaim(new Claim("UserRole", user.Role));
//                                        identity.AddClaim(new Claim("UserId", user.UserId.ToString()));
//                                        identity.AddClaim(new Claim(ClaimTypes.Role, user.Role));
//                                        identity.AddClaim(new Claim("ContractorId", (user.ContractorId ?? 0).ToString()));
//                                        identity.AddClaim(new Claim("Locations", user.Locations));
//                                        var principal = new ClaimsPrincipal(identity);
//                                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

//                                        if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
//                                        {
//                                            return LocalRedirect(model.ReturnUrl);
//                                        }
//                                        else
//                                        {
//                                            return RedirectToAction("Index", "Home");
//                                        }
//                                    }
//                                    else
//                                    {
//                                        ModelState.AddModelError("", "Unable to login to your account.");
//                                        return View(model);
//                                    }
//                                }
//                                else
//                                {
//                                    ModelState.AddModelError("", "Your account is inactive");
//                                    return View(model);
//                                }
//                            }

//                            else
//                            {
//                                ++user.FailureAttempts;
//                                userRepository.Update(user);
//                                ModelState.AddModelError("", "Unauthorized attempt.");
//                                return View(model);
//                            }
//                        }
//                        else
//                        {


//                            ++user.FailureAttempts;
//                            userRepository.Update(user);
//                            ModelState.AddModelError("", "verification code is incorrect.");
//                            string twoFAMethods = $"<option value='email'>{Helper.MaskEmailAddress(user.Email)}</option>";

//                            if (string.IsNullOrEmpty(user.MobileNumber))
//                            {
//                                twoFAMethods += $"<option value='phone' disabled>{Helper.MaskPhoneNumber(user.MobileNumber)}</option>";
//                            }
//                            else
//                            {
//                                twoFAMethods += $"<option value='phone'>{Helper.MaskPhoneNumber(user.MobileNumber)}</option>";
//                            }
//                            return View(new LoginPageViewModel() { ReturnUrl = model.ReturnUrl, TwoFAMethods = twoFAMethods, FormName = "TwoFAForm", Code = model.Code, Email = model.Email, Password = model.Password, TwoFAMethod = model.TwoFAMethod });
//                        }
//                    }
//                    else
//                    {
//                        ModelState.AddModelError("", "Your account is looked contact system admin to unlock your account.");
//                        return View(model);
//                    }
//                }
//                else
//                {
//                    ModelState.AddModelError("", "Unauthorized attempt.");
//                    return View(model);
//                }
//            }
//            else
//            {
//                return View(model);
//            }

//        }
//        public async Task<IActionResult> Logout()
//        {
//            if (User.Identity.IsAuthenticated)
//            {
//                await HttpContext.SignOutAsync();
//            }
//            return RedirectToAction(nameof(Login));

//        }
//        public IActionResult AccessDenied()
//        {
//            return RedirectToAction("Index", "Home");
//        }

//        [HttpGet]
//        public IActionResult ForgotPassword()
//        {
//            if (User.Identity.IsAuthenticated)
//            {
//                return RedirectToAction("Index", "Home");
//            }
//            else
//            {
//                return View(new ForgotPasswordViewModel());
//            }

//        }
//        [HttpPost]
//        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
//        {

//            if (ModelState.IsValid)
//            {
//                var user = userRepository.GetByEmail(model.EmailAddress.ToLower().Trim());
//                if (user != null)
//                {
//                    var code = Guid.NewGuid();
//                    user.VerificationCode = code;
//                    user.VerificationCodeExpiryDate = Helper.Today.AddDays(1);

//                    if (userRepository.Update(user))
//                    {
//                        string callbackUrl = $"https://{Request.Host}/Account/ResetPassword?code={code}";
//                        Dictionary<string, string> param = new Dictionary<string, string>();
//                        param["ResetLink"] = callbackUrl;
//                        await emailSender.SendHtmlEmail(new string[] { user.Email }, null, null, "UNHCR Tracking System :: Reset Password", HtmlMessageEnum.FORGET_PASSWORD, param);
//                    }

//                }
//                model.EmailAddress = "";
//                model.IsSent = true;
//                return View(model);
//            }
//            else
//            {
//                ModelState.AddModelError(string.Empty, "Missing or invalid input, check your input");
//                return View();
//            }
//        }


//        [HttpGet]
//        public IActionResult ResetPassword()
//        {

//            if (!User.Identity.IsAuthenticated)
//            {
//                if (!string.IsNullOrEmpty(HttpContext.Request.Query["code"].ToString()))
//                {

//                    return View(new ResetPasswordViewModel() { ResetCode = HttpContext.Request.Query["code"].ToString() });
//                }
//                return RedirectToAction("Index", "Home");
//                //ModelState.AddModelError(string.Empty, "the reset password link expired or invalid , please use new reset link by clicking foreget password again.");
//                //return View();
//            }
//            else
//            {
//                return RedirectToAction("Index", "Home");
//            }

//        }

//        [HttpPost]
//        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
//        {
//            Guid codeGuid;
//            if (ModelState.IsValid && Guid.TryParse(model.ResetCode, out codeGuid))
//            {
//                var user = userRepository.GetByResetAttempt(model.EmailAddress, codeGuid);
//                if (user != null)
//                {
//                    if (user.VerificationCodeExpiryDate >= Helper.Today)
//                    {
//                        if (IsComplexPassword(model.Password))
//                        {
//                            user.VerificationCode = null;
//                            user.VerificationCodeExpiryDate = null;
//                            user.IsLooked = false;
//                            user.FailureAttempts = 0;
//                            user.Password = Helper.ToSHA3(model.Password);


//                            if (userRepository.Update(user))
//                            {
//                                Dictionary<string, string> param = new Dictionary<string, string>();
//                                param["FirstName"] = user.FullName;
//                                await emailSender.SendHtmlEmail(new string[] { user.Email }, null, null, "UNHCR Tracking System :: Password Changed successfully", HtmlMessageEnum.CHANGE_PASSWORD, param);
//                                model.IsReset = true;
//                                return View(model);
//                            }
//                            else
//                            {
//                                ModelState.AddModelError(string.Empty, "unable to reset your password.");
//                                return View(model);
//                            }
//                        }
//                        else
//                        {
//                            ModelState.AddModelError(string.Empty, "your password is too week, check the criteria.");
//                            return View(model);
//                        }
//                    }
//                    else
//                    {
//                        ModelState.AddModelError(string.Empty, "The reset password link expired , please use new reset link by clicking foreget password again.");
//                        return View(model);

//                    }

//                }
//            }
//            ModelState.AddModelError(string.Empty, "The reset password link expired or invalid , please use new reset link by clicking foreget password again.");
//            return View(model);
//        }



//        [HttpGet]
//        public IActionResult ChangePassword()
//        {


//            return View(new ChangePasswordViewModel() { });
//        }

//        [HttpPost]

//        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
//        {

//            if (ModelState.IsValid)
//            {
//                var user = userRepository.GetByEmail(model.EmailAddress);
//                if (user != null)
//                {
//                    string hashedPassword = Helper.ToSHA3(model.CurrentPassword);
//                    if (user.Password.Equals(hashedPassword))
//                    {
//                        if (IsComplexPassword(model.Password))
//                        {
//                            user.VerificationCode = null;
//                            user.VerificationCodeExpiryDate = null;
//                            user.IsLooked = false;
//                            user.FailureAttempts = 0;
//                            user.Password = Helper.ToSHA3(model.Password);
//                            if (userRepository.Update(user))
//                            {
//                                Dictionary<string, string> param = new Dictionary<string, string>();
//                                param["FirstName"] = user.FullName;
//                                await emailSender.SendHtmlEmail(new string[] { user.Email }, null, null, "UNHCR Tracking System :: Password Changed successfully", HtmlMessageEnum.CHANGE_PASSWORD, param);
//                                model.IsReset = true;
//                                return View(model);
//                            }
//                            else
//                            {
//                                ModelState.AddModelError(string.Empty, "unable to reset your password.");
//                                return View(model);
//                            }
//                        }
//                        else
//                        {
//                            ModelState.AddModelError(string.Empty, "your password is too week, check the criteria.");
//                            return View(model);
//                        }

//                    }
//                    else
//                    {
//                        ModelState.AddModelError(string.Empty, "your current password is incorrect.");
//                        return View(model);
//                    }

//                }
//            }
//            ModelState.AddModelError(string.Empty, "The reset password link expired or invalid , please use new reset link by clicking foreget password again.");
//            return View(model);
//        }

//        private bool IsComplexPassword(string password)
//        {
//            if (password.Length < 8)
//                return false;
//            if (!Regex.IsMatch(password, @"\d+"))
//                return false;
//            if (!Regex.IsMatch(password, @"[a-z]"))
//                return false;
//            if (!Regex.IsMatch(password, @"[A-Z]"))
//                return false;
//            if (!Regex.IsMatch(password, @"[!@#$%^&*]"))
//                return false;
//            return true;
//        }

//        public IActionResult CheckSignIn([FromBody] LoginPageViewModel model)
//        {
//            if (User.Identity.IsAuthenticated)
//            {
//                return Json(new JsonResponseMessage<object>(System.Net.HttpStatusCode.MultiStatus, null, $"User already logged"));
//            }

//            if (string.IsNullOrEmpty(model.Email))
//            {
//                return Json(new JsonResponseMessage<object>(System.Net.HttpStatusCode.BadRequest, null, $"The Email field is required."));
//            }
//            if (string.IsNullOrEmpty(model.Password))
//            {
//                return Json(new JsonResponseMessage<object>(System.Net.HttpStatusCode.BadRequest, null, $"The Password field is required."));
//            }


//            string hashedPassword = Helper.ToSHA3(model.Password);
//            var user = userRepository.GetByEmail(model.Email.Trim());
//            if (user != null)
//            {
//                if (!user.IsLooked)
//                {

//                    if (user.Password.Equals(hashedPassword))
//                    {
//                        if (user.Role == "C" && user.ContractorId.HasValue)
//                        {
//                            var contractorData = contractorsRepository.GetById(user.ContractorId);
//                            if (contractorData != null)
//                            {
//                                if (!(Helper.Today.Date >= contractorData.StartContractDate && contractorData.EndContractDate >= Helper.Today))
//                                {
//                                    return Json(new JsonResponseMessage<object>(System.Net.HttpStatusCode.BadRequest, null, $"Your agreement is expired."));
//                                }
//                            }
//                            else
//                            {
//                                return Json(new JsonResponseMessage<object>(System.Net.HttpStatusCode.BadRequest, null, $"the contractor profile not found."));

//                            }

//                        }
//                        if (user.IsActive)
//                        {
//                            user.FailureAttempts = 0;
//                            if (userRepository.Update(user))
//                            {
//                                List<LookupItemViewModel> lstMethods = new List<LookupItemViewModel>();

//                                lstMethods.Add(new LookupItemViewModel() { Text = Helper.MaskEmailAddress((string.IsNullOrEmpty(user.TwoFAEmail) ? user.Email : user.TwoFAEmail)), Value = "email", IsActive = true });
//                                if (!string.IsNullOrEmpty(user.MobileNumber))
//                                {
//                                    lstMethods.Add(new LookupItemViewModel() { Text = Helper.MaskPhoneNumber(user.MobileNumber), Value = "phone", IsActive = true });
//                                }

//                                return Json(new JsonResponseMessage<object>(System.Net.HttpStatusCode.OK, lstMethods, $"Success Authentication."));
//                            }
//                            else
//                            {
//                                return Json(new JsonResponseMessage<object>(System.Net.HttpStatusCode.BadRequest, null, $"Unable to login to your account."));
//                            }
//                        }
//                        else
//                        {
//                            return Json(new JsonResponseMessage<object>(System.Net.HttpStatusCode.BadRequest, null, $"Your account is inactive."));
//                        }
//                    }
//                    else
//                    {
//                        ++user.FailureAttempts;
//                        userRepository.Update(user);
//                        return Json(new JsonResponseMessage<object>(System.Net.HttpStatusCode.BadRequest, null, $"Unauthorized attempt."));
//                    }
//                }
//                else
//                {
//                    return Json(new JsonResponseMessage<object>(System.Net.HttpStatusCode.BadRequest, null, $"Your account is looked contact system admin to unlock your account."));
//                }
//            }
//            else
//            {
//                return Json(new JsonResponseMessage<object>(System.Net.HttpStatusCode.BadRequest, null, $"Unauthorized attempt."));

//            }




//        }
//        [EnableRateLimiting("otp")]
//        public async Task<IActionResult> SendOTP([FromBody] LoginPageViewModel model)
//        {
//            if (User.Identity.IsAuthenticated)
//            {
//                return Json(new JsonResponseMessage<object>(System.Net.HttpStatusCode.MultiStatus, null, $"User already logged"));
//            }
//            if (string.IsNullOrEmpty(model.Email))
//            {
//                return Json(new JsonResponseMessage<object>(System.Net.HttpStatusCode.BadRequest, null, $"The Email field is required."));
//            }
//            if (string.IsNullOrEmpty(model.Password))
//            {
//                return Json(new JsonResponseMessage<object>(System.Net.HttpStatusCode.BadRequest, null, $"The Password field is required."));
//            }

//            if (string.IsNullOrEmpty(model.TwoFAMethod))
//            {
//                return Json(new JsonResponseMessage<object>(System.Net.HttpStatusCode.BadRequest, null, $"The Two-Factor Method is required."));
//            }

//            string hashedPassword = Helper.ToSHA3(model.Password);
//            var user = userRepository.GetByEmail(model.Email.Trim());
//            if (user != null && user.Password.Equals(hashedPassword))
//            {
//                if (model.TwoFAMethod == "email")
//                {
//                    var result = await smsService.SendEmailOTP(new SMSService.Models.SendEmailOTPRequest()
//                    {
//                        Language = "EN",
//                        MessageId = 3,
//                        EmailAddress = (string.IsNullOrEmpty(user.TwoFAEmail) ? user.Email : user.TwoFAEmail)
//                    });
//                    if (result.StatusCode == System.Net.HttpStatusCode.OK)
//                    {
//                        return Json(new JsonResponseMessage<object>(System.Net.HttpStatusCode.OK, null, $"The verification code has been sent to the email address."));
//                    }
//                    else
//                    {
//                        return Json(new JsonResponseMessage<object>(System.Net.HttpStatusCode.BadRequest, null, result.Message));
//                    }
//                }
//                else if (model.TwoFAMethod == "phone" && !string.IsNullOrEmpty(user.MobileNumber))
//                {
//                    var result = await smsService.SendOTP(new SMSService.Models.SendOTPRequest()
//                    {

//                        Language = "EN",
//                        MessageId = 3,
//                        MobileNumber = user.MobileNumber

//                    });
//                    if (result.StatusCode == System.Net.HttpStatusCode.OK)
//                    {
//                        return Json(new JsonResponseMessage<object>(System.Net.HttpStatusCode.OK, null, $"The verification code has been sent to the phone number."));
//                    }
//                    else
//                    {
//                        return Json(new JsonResponseMessage<object>(System.Net.HttpStatusCode.BadRequest, null, result.Message));
//                    }
//                }
//                else
//                {
//                    return Json(new JsonResponseMessage<object>(System.Net.HttpStatusCode.BadRequest, null, $"The Two-Factor Method is not available."));
//                }
//            }
//            else
//            {
//                return Json(new JsonResponseMessage<object>(System.Net.HttpStatusCode.BadRequest, null, $"Action rejected, try to login again"));
//            }

//        }
//    }
//}

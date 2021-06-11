using Api.Identity.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using NetDevPack.Identity.Authorization;
using NetDevPack.Identity.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Identity.Controllers
{
    [Authorize]
    [Route("api/actions")]
    public class ActionController : MainController
    {
        private readonly IAspNetUser _user;
        private readonly UserManager<IdentityUser> _userManager;

        public ActionController(IAspNetUser user, UserManager<IdentityUser> userManager)
        {
            _user = user;
            _userManager = userManager;
        }

        [HttpGet("GetById")]
        public async Task<ActionResult> GetById(string id)
        {
            var result = await _userManager.Users.Where(x => x.Id == id).ToListAsync();

            List<RegisterUserView> list = CreateDTOList(result);

            if (list.Any())
            {
                return CustomResponse(list);
            }

            return CustomResponse($"User not fould.");
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            var result = await _userManager.Users.Where(x => x.Id.Length > 0).ToListAsync();

            List<RegisterUserView> list = CreateDTOList(result);

            if (list.Any())
            {
                return CustomResponse(list);
            }

            return CustomResponse($"User not fould.");
        }

        [HttpPut("ChangeEmail")]
        public async Task<ActionResult> ChangeEmail(UpdateEmailRegisterUser updateRegisterUser)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
      
            //Get id
            var userId = _user.GetHttpContext().User.GetUserId();

            //Get user object from db
            var user = await _userManager.FindByIdAsync(userId);

            //Check password
            
            if (await _userManager.CheckPasswordAsync(user, updateRegisterUser.Password))
            {
                user.UserName = updateRegisterUser.NewEmail;
                user.Email = updateRegisterUser.NewEmail;

                //Try to update in db
                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return CustomResponse($"Hey! Your registered email changed from {_user.GetUserEmail()} to {updateRegisterUser.NewEmail}!");
                }

                return BadRequest(result.Errors);
            }

            return CustomResponse("Your password is incorrect.");
            
        }

        [HttpPut("ChangePassword")]
        public async Task<ActionResult> ChangePassword(UpdatePasswordRegisterUser updateRegisterUser)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            //Get id
            var userId = _user.GetHttpContext().User.GetUserId();

            //Get user object from db
            var user = await _userManager.FindByIdAsync(userId);
            
            //Try to update in db
            var result = await _userManager.ChangePasswordAsync(user, updateRegisterUser.CurrentPassword, updateRegisterUser.NewPassword);

            if (result.Succeeded)
            {
                return CustomResponse($"Hey {user.Email}! Your password has been updated!");
            }

            return BadRequest(result.Errors);
        }

        [HttpPut("ForgotPassword")]
        public async Task<ActionResult> ForgotPassword()
        {
            return CustomResponse($"We're going to send you an email with a new password.");
        }

        [HttpPut("ResetPassword")]
        public async Task<ActionResult> ResetPassword()
        {
            return CustomResponse($"Your password has been updated!");
        }

        private List<RegisterUserView> CreateDTOList(List<IdentityUser> list)
        {
            return list.Select(x =>
            {
                var item = new RegisterUserView { Email = x.Email };
                return item;
            }).ToList();
        }

    }
}

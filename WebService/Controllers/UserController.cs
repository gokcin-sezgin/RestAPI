using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebService.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly UserContext _context;


        public UserController(UserContext context)
        {
            _context = context;
            if (_context.UserInfos.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
               

            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserInfo>>> GetUserInfos()
        {
           var users= await _context.UserInfos.ToListAsync();
            foreach (UserInfo user in users)
            {
                user.UserPassword = UnHashPassword(user);
            }
            return users;
                
        }


        [HttpPost]
        public async Task<ActionResult> CreateUserInfo(UserInfo UserInfo)
        {

            if (ModelState.IsValid)
            {
                UserInfo.UserPassword = HashPassword(UserInfo);
                _context.UserInfos.Add(UserInfo);
                _context.SaveChanges();
                return  Ok();
            }
            return  BadRequest("Invalid data.");


        }

        [HttpGet("{id}")]
        public async Task<UserInfo> GetUser(int id)
        {

            var user = await _context.UserInfos.FindAsync(id);
            user.UserPassword = UnHashPassword(user);
            return user;


        }
      
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, UserInfo item)
        {
            if (id != item.UserInfoId)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }



        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            if (ModelState.IsValid)
            {
                var user = _context.UserInfos.SingleOrDefault(x => x.UserInfoId == id);


                if (user == null)
                {
                    return BadRequest("Invalid data.");
                }

                _context.UserInfos.Remove(user);

                _context.SaveChanges();
                return Ok();
            }

            return BadRequest("Invalid data.");

        }

        private static string UnHashPassword(UserInfo user)
        {
            if (user!=null)
            {
                string password = user.UserPassword.Substring((user.UserName.Length + 2), 4) + user.UserPassword.Substring((user.UserName.Length + 9), 2) + user.UserPassword.Substring((user.UserName.Length + 17), user.UserPassword.Length- (user.UserName.Length + 17));
                return password;
            }
            return user.UserPassword;
        }
        private static string HashPassword(UserInfo user) {

            if (user==null)
            {
                return user.UserPassword;
            }
            Random rnd = new Random();
            char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZqwe'!^+%&/()=?~rtyuıopğüasdfghjklşi,<zxcvbnmöç.1234567890".ToCharArray();
            
            string password = user.UserName + alpha[rnd.Next(0, alpha.Length-1)] + alpha[rnd.Next(0, alpha.Length-1)] + user.UserPassword.Substring(0, 4) + alpha[rnd.Next(0, alpha.Length - 1)] + alpha[rnd.Next(0, alpha.Length - 1)] + alpha[rnd.Next(0, alpha.Length - 1)]  + user.UserPassword.Substring(4, 2) + alpha[rnd.Next(0, alpha.Length - 1)] + alpha[rnd.Next(0, alpha.Length - 1)] + alpha[rnd.Next(0, alpha.Length - 1)] + alpha[rnd.Next(0, alpha.Length - 1)] + alpha[rnd.Next(0, alpha.Length - 1)] + alpha[rnd.Next(0, alpha.Length - 1)] + user.UserPassword.Substring(6, user.UserPassword.Length-6);
            //    gökçin123/pgökçğ(=inST!Zu8123
            return password;

          
        }

      
    }
}

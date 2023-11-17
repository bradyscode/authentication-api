﻿using authentication_dot_net.Interfaces;
using authentication_dot_net.Interfaces.UserInterface;
using authentication_dot_net.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace authentication_dot_net.Controllers
{
    public class AuthenticationController : Controller
    {
        private IUserInterface _userInterface;

        public AuthenticationController(IUserInterface userInterface)
        {
            _userInterface = userInterface;
        }

        [HttpGet("/CreateUser")]
        public object CreateUser(string username, string password, int permission)
        {
            if (_userInterface.UsernameExists(username)) throw new Exception("User already exists");
            User user;
            if(permission!=null)
            {
                user = new User(username, password, permission);
            }else
            {
                user = new User(username, password);
            }
            var returnValue = _userInterface.CreateUser(user);
            return returnValue;
        }


        [HttpGet("/AuthenticateUser")]
        public async Task<bool> AuthenticateUserAsync(string username, string password)
        {
            return await _userInterface.AuthenticateUser(username, password);
        }
    }
}

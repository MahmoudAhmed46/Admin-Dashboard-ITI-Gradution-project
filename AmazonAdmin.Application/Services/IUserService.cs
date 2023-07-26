﻿using AmazonAdmin.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonAdmin.Application.Services
{
    public interface IUserService
    {
        Task<bool> RegisterDTO(UserLoginDTO user);
        Task<UserRegisterDTO> Login(string phone);
    }
}
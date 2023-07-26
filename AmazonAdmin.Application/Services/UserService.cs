using AmazonAdmin.Application.Contracts;
using AmazonAdmin.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonAdmin.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserReposatory _repo;
        private readonly IMapper _mapper;
        public UserService(IUserReposatory repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public Task<UserRegisterDTO> Login(string phone)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RegisterDTO(UserLoginDTO user)
        {
            throw new NotImplementedException();
        }
    }
}

using AutoMapper;
using BookMovie2.Models;
using BookMovie2.Data.Contracts;
using BookMovie2.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMovie2.Service.Providers
{
   
    public class UserService : IUserService
    {
        private IUnitOfWork _repository;
        private IMapper _mapper;

        public UserService(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;   
            _mapper = mapper;
        }

        public void AddUser(User user)
        {
           _repository.User.Add(_mapper.Map<Data.User>(user));
            _repository.Save();
        }

        public IEnumerable<User> GetAll()
        {
            var data = _repository.User.GetAll();
           return _mapper.Map<List<User>>(data);
        }

    

    }
}

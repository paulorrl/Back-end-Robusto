using System;
using System.Collections.Generic;
using SpaUserControl.Common.Resources;
using SpaUserControl.Common.Validation;
using SpaUserControl.Domain.Contracts.Repositories;
using SpaUserControl.Domain.Contracts.Services;
using SpaUserControl.Domain.Models;

namespace SpaUserControl.Business.Services
{
    public class UserService: IUserService
    {
        private IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            this._repository = repository;
        }

        public User Authenticate(string email, string password)
        {
            var user = GetByEmail(email);

            if (user.Password != PasswordAssertionConcern.Encrypt(password))
                throw new Exception(Errors.InvalidCredentials);

            return user;
        }

        public User GetByEmail(string email)
        {
            var user = _repository.Get(email);
            if(user == null)
                throw new Exception(Errors.UserNotFound);

            return user;
        }

        public void Register(string name, string email, string password, string confirmPassword)
        {
            var hasUser = _repository.Get(email); // Caso existisse rules, já buscaria no mesmo select (2 selects = menos performance) 
            if (hasUser != null)
                throw new Exception(Errors.DuplicateEmail);

            var user = new User(name, email);
            user.SetPassword(password, confirmPassword);

            _repository.Create(user);    
        }

        public void ChangeInformation(string email, string name)
        {
            var user = GetByEmail(email);
            user.ChanceName(name);

            _repository.Update(user);
        }

        public void ChangePassword(string email, string password, string newPassword, string confirmPassword)
        {
            var user = Authenticate(email, password);
            user.SetPassword(newPassword, confirmPassword);

            _repository.Update(user );
        }

        public string ResetPassword(string email)
        {
            var user = GetByEmail(email);
            var password = user.ResetPassword();

            _repository.Update(user);
            return password;
        }

        public List<User> GetByRange(int skip, int take)
        {
            return _repository.Get(skip, take);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
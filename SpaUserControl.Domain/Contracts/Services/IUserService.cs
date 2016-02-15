using System;
using System.Collections.Generic;
using SpaUserControl.Domain.Models;

namespace SpaUserControl.Domain.Contracts.Services
{
    public interface IUserService: IDisposable
    {
        // O Authenticate poderia estar dentro de uma outra interface exclusiva "AccountService", por exemplo.
        // Mas o exemplo é simples, então, ficará aqui mesmo ... 

        User Authenticate(String email, String password);

        User GetByEmail(String email);

        void Register(String name, String email, String password, String confirmPassword);

        void ChangeInformation(String email, String name);

        void ChangePassword(String email, String password, String newPassword, String confirmPassword);

        String ResetPassword(String email);

        List<User> GetByRange(int skip, int take);
    }
}
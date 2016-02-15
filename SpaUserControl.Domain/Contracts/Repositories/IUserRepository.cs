﻿using SpaUserControl.Domain.Models;
using System;
using System.Collections.Generic;

namespace SpaUserControl.Domain.Contracts.Repositories
{
    public interface IUserRepository: IDisposable
    {
        User Get(String Email);

        User Get(Guid Id);

        List<User> Get(int skip, int take); 

        void Create(User user);

        void Update(User user);

        void Delete(User user);
    }
}
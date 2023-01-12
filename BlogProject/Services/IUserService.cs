﻿using BlogProject.Models.Database.Users;

namespace BlogProject.Services;

public interface IUserService
{
    Task<User[]> GetAllUsers();
    Task<User> GetUser(string guid);
    Task<bool> AddUser(User user);
}
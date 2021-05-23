using BugTrackingApp.utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BugTrackingApp.service.model
{
    class UserUtils
    {
        /// <summary>
        /// Текущий пользователь
        /// </summary>
        public static User currentUser;
        /// <summary>
        /// Пользователь для редактирования
        /// </summary>
        public static User editUser;
        /// <summary>
        /// Пользователь для ассаймента
        /// </summary>
        public static User assigneeUser;
        /// <summary>
        /// Перечисление ролей пользователей
        /// </summary>
        public enum UserRole
        {
            def = 0,        // Пользователь по-умолчанию
            admin = 1,      // Администратор
            dev = 2,        // Разработчик
            test = 3,       // Тестировщик
            manager = 4     // Менеджер
        }
        /// <summary>
        /// Метод для добавления пользователя по-умолчанию (при регистрации) 
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="surname">Фамилия</param>
        /// <param name="email">Имейл</param>
        /// <param name="password">Пароль</param>
        /// <returns>true, если удачно, иначе - false</returns>
        public static bool addUser(string name, string surname, string email, string password)
        {
            return addUser(name, surname, email, password, UserRole.def);
        }

        /// <summary>
        /// Метод для добавления пользователя с указанной ролью
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="surname">Фамилия</param>
        /// <param name="email">Имейл</param>
        /// <param name="password">Пароль</param>
        /// <param name="role">Роль</param>
        /// <returns>true, если удачно, иначе - false</returns>
        public static bool addUser(string name, string surname, string email, string password, UserRole role)
        {
            User user = new User();
            user.name = name;
            user.surname = surname;
            user.email = email;
            user.password = password;
            user.role = (int)role;
            using (BugTrackingEntities context = new BugTrackingEntities())
            {
                try
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Logger.Log.Error("Ошибка добавления пользователя : " + e.Message);
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Метод для обновления пользователя 
        /// </summary>
        /// <param name="user">пользователь</param>
        /// <returns>true, если удачно, иначе - false</returns>
        public static bool updateUser(User user)
        {
            using (BugTrackingEntities context = new BugTrackingEntities())
            {
                try
                {
                    User userToUpdate = context.Users.SingleOrDefault(b => b.Id == user.Id);
                    context.Entry(userToUpdate).Collection(u => u.Comments).Load();
                    context.Entry(userToUpdate).Collection(u => u.Projects).Load();
                    copyUser(userToUpdate, user);
                    if (userToUpdate != null)
                    {
                        context.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    Logger.Log.Error("Ошибка обнавления пользователя : " + e.Message);
                    return false;
                }
            }
         return true;
        }
        /// <summary>
        /// Метод для удаления пользователя
        /// </summary>
        /// <param name="user">Пользователь для удаления</param>
        /// <returns>true, если удачно, иначе - false</returns>
        public static bool removeUser(User user)
        {
            using (BugTrackingEntities context = new BugTrackingEntities())
            {
                try
                {
                    context.Users.Remove(user);
                }
                catch(Exception e)
                {
                    Logger.Log.Error("Ошибка удаления пользователя : " + e.Message);
                    return false;
                }
            }
         return true;
        }
        /// <summary>
        /// Проверка наличия пользователя с имейл/пароль связкой
        /// </summary>
        /// <param name="email">Имейл</param>
        /// <param name="password">пароль</param>
        /// <returns>Пользователя иначе null</returns>
        public static User checkUserLogin(string email, string password)
        {
            User user;
            using (BugTrackingEntities context = new BugTrackingEntities())
            {
                try
                {
                    user = (User)context.Users
                        .Where(u => string.Equals(u.email, email) && string.Equals(u.password, password))
                        .SingleOrDefault();
                    return user;
                }
                catch (Exception e)
                {
                    Logger.Log.Error("Ошибка проверки пользователя : " + e.Message);
                    return null;
                }
            }
        }
        /// <summary>
        /// Метод для получения всех пользователей
        /// </summary>
        /// <returns>Список пользователей или null, в случае ошибки</returns>
        public static List<User> getAllUsers()
        {
            List<User> result =  new List<User>();
            using (BugTrackingEntities context = new BugTrackingEntities())
            {
                try
                {
                    var users = context.Users.ToList();
                    result.AddRange(users);
                }
                catch (Exception e)
                {
                    Logger.Log.Error("Ошибка получения пользователей : " + e.Message);
                    return null;
                }
            }
            return result;
        }
        /// <summary>
        /// Метод для получения пользователей для проекта
        /// </summary>
        /// <param name="projectId">Айди проекта</param>
        /// <returns>Список пользователей или null, в случае ошибки</returns>
        public static List<User> getUsersForProject(int projectId)
        {
            List<User> result = new List<User>();
            using (BugTrackingEntities context = new BugTrackingEntities())
            {
                try
                {
                    Project project = context.Projects.SingleOrDefault(p => p.Id == projectId);
                    context.Entry(project).Collection(p => p.Users).Load();
                    result.AddRange(project.Users);
                }
                catch (Exception e)
                {
                    Logger.Log.Error("Ошибка получения пользователей : " + e.Message);
                    return null;
                }
            }
            return result;
        }
        /// <summary>
        /// Получение пользователя для коментария (автора)
        /// </summary>
        /// <param name="сommentId">Айди комента</param>
        /// <returns>Пользователя или null, в случае ошибки</returns>
        public static User getUserForComment(int сommentId)
        {
            using (BugTrackingEntities context = new BugTrackingEntities())
            {
                try
                {
                    Comment comment = context.Comments.SingleOrDefault(c => c.Id == сommentId);
                    context.Entry(comment).Reference(c => c.User).Load();
                    return comment.User;
                }
                catch (Exception e)
                {
                    Logger.Log.Error("Ошибка получения пользователя : " + e.Message);
                    return null;
                }
            }
        }
        /// <summary>
        /// Получение пользователя по Айди
        /// </summary>
        /// <param name="userId">Айди пользователя</param>
        /// <returns>Пользователя или null, в случае ошибки</returns>
        public static User getUserById(int userId)
        {
            User user;
            using (BugTrackingEntities context = new BugTrackingEntities())
            {
                try
                {
                    user = context.Users.SingleOrDefault(b => b.Id == userId);
                }
                catch (Exception e)
                {
                    Logger.Log.Error("Ошибка удаления пользователя : " + e.Message);
                    return null;
                }
            }
            return user;
        }
        /// <summary>
        /// Метод для копирования свойств пользователя
        /// </summary>
        /// <param name="userTo">Пользователь, куда копировать</param>
        /// <param name="userFrom">Пользователь из которого копировать</param>
        private static void copyUser(User userTo, User userFrom)
        {
            userTo.name = userFrom.name;
            userTo.password = userFrom.password;
            userTo.surname = userFrom.surname;
            userTo.role = userFrom.role;
            userTo.email = userFrom.email;
        }
    }
}

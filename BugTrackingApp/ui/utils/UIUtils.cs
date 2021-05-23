using BugTrackingApp.service.model;
using BugTrackingApp.ui.model;
using BugTrackingApp.utils;
using System.Collections.Generic;
using System.Windows.Controls;

namespace BugTrackingApp.ui.utils
{
    /// <summary>
    /// Утильный класс для работы с юай формами
    /// </summary>
    class UIUtils
    {
        public enum BackWindow{
            addTicket,
            viewTicket
        }
        public static BackWindow backWindow;
        /// <summary>
        /// Метод получения пользователей для ассаймента на проект
        /// </summary>
        /// <param name="project">Проект</param>
        /// <returns>Список всех пользователей в виде юай модели</returns>
        public static List<UserAssignmentCbListItem> getUsersForProjectAssignment(Project project)
        {
            List<User> users = UserUtils.getAllUsers();
            List<UserAssignmentCbListItem> result = new List<UserAssignmentCbListItem>();
            
            if(project == null || users == null)
            {
                Logger.Log.Error("Проект или список пользователей пуст!!!");
                return null;
            }
            foreach(User user in users)
            {
                if(isUserinList(project.Users, user))
                {
                    result.Add(new UserAssignmentCbListItem(true, user));
                }
                else
                {
                    result.Add(new UserAssignmentCbListItem(false, user));
                }
            }
            return result;
        }
        /// <summary>
        /// Заасайнить пользователей на проект
        /// </summary>
        /// <param name="project">Проект</param>
        /// <param name="items">модель для добавления</param>
        public static void assignUsersToProject(Project project, ItemCollection items)
        {
            project.Users.Clear();
            foreach (UserAssignmentCbListItem userItem in items)
            {
                if (userItem.Checked)
                {
                    project.Users.Add(userItem.User);
                    ProjectUtils.updateProject(project);
                }
            }
        }
        /// <summary>
        /// Описание пользователя по айди
        /// </summary>
        /// <param name="userId">айди пользователя</param>
        /// <returns>Текст описания пользователя</returns>
        public static string getUserDescrById(int userId)
        {
            User user = UserUtils.getUserById(userId);
            if (user != null)
            {
                return getUserDescr(user);
            }
            else return "";
        }
        /// <summary>
        /// Описание пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns>Текст описания пользователя</returns>
        public static string getUserDescr(User user)
        {
            if (user != null)
            {
                return user.name + " " + user.surname + "(" + user.email + ")";
            }
            else return "";
        }
        /// <summary>
        /// Проверка пользователя в списке
        /// </summary>
        /// <param name="users">Список пользователей</param>
        /// <param name="user">Пользователь для проверки</param>
        /// <returns>true, если есть, иначе - false</returns>
        private static bool isUserinList(ICollection<User> users, User user)
        {
            foreach(User userToCheck in users)
            {
                if(userToCheck.Id == user.Id)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

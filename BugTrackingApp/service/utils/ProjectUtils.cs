using BugTrackingApp.utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BugTrackingApp.service.model
{
    class ProjectUtils
    {
        /// <summary>
        /// Ссылка на проект для редактирования
        /// </summary>
        public static Project editProject = null;
        /// <summary>
        /// Ссылка на проект для ассаймента
        /// </summary>
        public static Project assignProject = null;
        /// <summary>
        /// Метод добавления проекта
        /// </summary>
        /// <param name="name">название проекта</param>
        /// <param name="description">описание проекта</param>
        /// <returns>true, если добавлен, иначе - false</returns>
        public static bool addProject(string name, string description)
        {
            Project project = new Project();
            project.name = name;
            project.description = description;
            using (BugTrackingEntities context = new BugTrackingEntities())
            {
                try
                {
                    context.Projects.Add(project);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Logger.Log.Error("Ошибка добавления проекта : " + e.Message);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Метод для обновления проекта
        /// </summary>
        /// <param name="project">обнавлённый проект</param>
        /// <returns>true, если удачно, иначе - false</returns>
        public static bool updateProject(Project project)
        {
            using (BugTrackingEntities context = new BugTrackingEntities())
            {
                try
                {
                    if (project != null)
                    {
                        Project projectToUpdate = context.Projects.SingleOrDefault(b => b.Id == project.Id);
                        //context.Entry(projectToUpdate).Collection(p => p.Users).Load();
                        projectToUpdate.Users.Clear();
                        foreach(User user in project.Users)
                        {
                            projectToUpdate.Users.Add(context.Users.SingleOrDefault(u => u.Id == user.Id));
                        }
                        context.Entry(projectToUpdate).Collection(p => p.Tickets).Load();
                        copyProjects(projectToUpdate, project);
                        context.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    Logger.Log.Error("Ошибка обновления проекта : " + e.Message);
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Метод для удаления проекта
        /// </summary>
        /// <param name="project">Проект для удаления</param>
        /// <returns>ttrue, если удачно, иначе - false</returns>
        public static bool removeProject(Project project)
        {
            using (BugTrackingEntities context = new BugTrackingEntities())
            {
                try
                {
                    if (project != null)
                    {
                        context.Projects.Remove(project);
                        context.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    Logger.Log.Error("Ошибка удаления проекта : " + e.Message);
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Возвращает все проекты для пользователя
        /// </summary>
        /// <param name="user">пользователь</param>
        /// <returns>список проектов</returns>
        public static List<Project> getUserProjects(User user)
        {
            List<Project> result = new List<Project>();
            using (BugTrackingEntities context = new BugTrackingEntities())
            {
                try
                {
                    User userFromDb = context.Users.SingleOrDefault(u => u.Id == user.Id);
                    context.Entry(userFromDb).Collection(u => u.Projects).Load();
                    result.AddRange(userFromDb.Projects);
                }
                catch (Exception e)
                {
                    Logger.Log.Error("Ошибка получения проектов : " + e.Message);
                    return null;
                }
            }
                return result;
        }
        /// <summary>
        /// Вспомогательный метод для копирование свойств проекта
        /// </summary>
        /// <param name="projectTo">проект в который кпировать</param>
        /// <param name="projectFrom">проект из которого копирование</param>
        private static void copyProjects(Project projectTo, Project projectFrom)
        {
            projectTo.name = projectFrom.name;
            projectTo.description = projectFrom.description;
        }
    }
}

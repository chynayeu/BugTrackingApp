using BugTrackingApp.utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BugTrackingApp.service.model
{
    class TicketUtils
    {
        /// <summary>
        /// Ссылка на текущий тикет
        /// </summary>
        public static Ticket currentTicket;
        /// <summary>
        /// Перечисление типов тикетов 
        /// </summary>
        public enum TicketType
        {
            bug,
            story,
            query
        }

        /// <summary>
        /// Перечисление приоритетов тикета
        /// </summary>
        public enum TicketPriority
        {
            p1,
            p2,
            p3,
            p4
        }
        /// <summary>
        /// Перечисление статусов тикета
        /// </summary>
        public enum TicketStatus
        {
            open,
            inProgress,
            readyForTest,
            inTest,
            closed
        }
        /// <summary>
        /// Метод для создания тикета
        /// </summary>
        /// <param name="ticket">тикет для добавления в базу</param>
        /// <returns>true, если удачно, иначе - false</returns>
        public static bool creatTicket(Ticket ticket)
        {
            using (BugTrackingEntities context = new BugTrackingEntities())
            {
                try
                {
                    ticket.Project = context.Projects.SingleOrDefault(b => b.Id == ProjectUtils.assignProject.Id);
                    context.Tickets.Add(ticket);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Logger.Log.Error("Ошибка добавления тикета " + e.Message);
                    return false;
                }

            }
            return true;
        }
        /// <summary>
        /// Метод для обновления тикета
        /// </summary>
        /// <param name="ticket">тикет для обновления</param>
        /// <returns>true, если удачно, иначе - false</returns>
        public static bool updateTicket(Ticket ticket)
        {
            using (BugTrackingEntities context = new BugTrackingEntities())
            {
                try
                {
                    Ticket ticketToUpdate = context.Tickets.SingleOrDefault(b => b.Id == ticket.Id);
                    context.Entry(ticketToUpdate).Reference(t => t.Project).Load();
                    context.Entry(ticketToUpdate).Collection(t => t.Comments).Load();
                    copyTicket(ticketToUpdate, ticket);
                    if(ticketToUpdate != null)
                    {
                        context.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    Logger.Log.Error("Ошибка обноления тикета " + e.Message);
                    return false;
                }

            } 
         return true;
        }
        /// <summary>
        /// Метод для получения тикетов для проекта
        /// </summary>
        /// <param name="projectId">Айди проекта для поиска</param>
        /// <returns>Список тикетов для проекта</returns>
        public static List<Ticket> getTicketsByProjectId(int projectId)
        {
            List<Ticket> result = new List<Ticket>();
            using (BugTrackingEntities context = new BugTrackingEntities())
            {
                try
                {
                    //Explicit load of tickets
                    Project project = context.Projects.FirstOrDefault(p => p.Id == projectId);
                    context.Entry(project).Collection(p => p.Tickets).Load();
                    if(project.Tickets != null)
                    {
                        result.AddRange(project.Tickets);
                    }
                }
                catch (Exception e)
                {
                    Logger.Log.Error("Ошибка обноления тикета " + e.Message);
                    return null;
                }

            }
            return result;
        }
        /// <summary>
        /// Метод для добавление коментария к тикету
        /// </summary>
        /// <param name="text">текст комментария</param>
        /// <returns>true, если удачно, иначе - false</returns>
        public static bool addComment(string text)
        {
            using (BugTrackingEntities context = new BugTrackingEntities())
            {
                try
                {
                    Comment comment = new Comment();
                    comment.text = text;
                    comment.User = context.Users.SingleOrDefault(u => u.Id == UserUtils.currentUser.Id);
                    comment.Ticket = context.Tickets.SingleOrDefault(t => t.Id == currentTicket.Id);
                    comment.date = DateTime.Now;
                    context.Comments.Add(comment);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Logger.Log.Error("Ошибка добавления кометария " + e.Message);
                    return false;
                }

            }
            return true;
        }
        /// <summary>
        /// Метод для получения комментариев для тикета
        /// </summary>
        /// <param name="ticket">тикет для поиска коментариев</param>
        /// <returns>список комментариев для тикета</returns>
        public static List<Comment> getTicketComments(Ticket ticket)
        {
            List<Comment> result = new List<Comment>();
            using (BugTrackingEntities context = new BugTrackingEntities())
            {
                try
                {
                    Ticket ticketFromdb = context.Tickets.SingleOrDefault(t => t.Id == ticket.Id);
                    context.Entry(ticketFromdb).Collection(t => t.Comments).Load();
                    result.AddRange(ticketFromdb.Comments);
                }
                catch (Exception e)
                {
                    Logger.Log.Error("Ошибка получения коментвриев для тикета " + e.Message);
                    return null;
                }

            }
            return result;
        }
        /// <summary>
        /// Метод для копирования тикета
        /// </summary>
        /// <param name="ticketTo">тикет в который копируют</param>
        /// <param name="ticketFrom">тикет из которого копируют</param>
        private static void copyTicket(Ticket ticketTo, Ticket ticketFrom)
        {
            ticketTo.name = ticketFrom.name;
            ticketTo.priority = ticketFrom.priority;
            ticketTo.status = ticketFrom.status;
            ticketTo.type = ticketFrom.type;
            ticketTo.assigned = ticketFrom.assigned;
            ticketTo.created = ticketFrom.created;
            ticketTo.description = ticketFrom.description;
        }
    }
}

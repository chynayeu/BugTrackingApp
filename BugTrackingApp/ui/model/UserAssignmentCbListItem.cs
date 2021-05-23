using BugTrackingApp.service.model;
using BugTrackingApp.ui.utils;

namespace BugTrackingApp.ui.model
{
    /// <summary>
    /// UI Модель для ассаймента пользователя
    /// </summary>
    class UserAssignmentCbListItem
    {
        public bool Checked { get; set; }
        public User User { get; set; }
        public string Text
            {
            get
            {
                return UIUtils.getUserDescr(User);
            }
            set { }
            }

        public UserAssignmentCbListItem(bool isChecked, User user)
        {
            Checked = isChecked;
            User = user;
        }
    }
}

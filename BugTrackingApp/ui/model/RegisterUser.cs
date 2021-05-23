namespace BugTrackingApp.ui.model
{
    /// <summary>
    /// UI Модель для регистарции пользователя
    /// </summary>
    class RegisterUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public void Reset()
        {
            Name = "";
            Surname = "";
            Email = "";
        }
    }
}

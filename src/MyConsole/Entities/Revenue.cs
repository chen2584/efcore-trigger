using System;

namespace MyConsole.Entities
{
    public class Revenue
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public float Amount { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
namespace Shlyapnikova_lr.Models
{
    public class Student
    {   
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public DateTime CheckInStart { get; set; }
        public DateTime CheckInEnd { get; set; }
        public DateTime CheckInTime { get; set; }
        public float StudentPhone { get; set; }
        public int Status { get; set; }
    }
}

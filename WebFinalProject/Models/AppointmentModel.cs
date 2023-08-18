using System;
namespace WebFinalProject.Models
{
	public class AppointmentModel
	{
        public string FacultyId { get; set; }
        public DateTime AppointmentStartTime { get; set; }
        public DateTime AppointmentEndTime { get; set; }
        public string StudentId { get; set; }
        public string Name { get; set; }
    }
}


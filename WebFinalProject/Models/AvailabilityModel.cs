using System;
namespace WebFinalProject.Models
{
	public class AvailabilityModel
	{
        public string Id { get; set; }
        public string FacultyId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}


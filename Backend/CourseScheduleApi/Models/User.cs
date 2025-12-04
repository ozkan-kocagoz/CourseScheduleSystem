namespace CourseScheduleApi.Models
{
	public class User
	{
		public int Id { get; set; }
		public string FullName { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string PasswordHash { get; set; } = string.Empty; // Dokümanda password_hash olarak geçiyor
		public string Role { get; set; } = string.Empty; // admin, dean, department_rep [cite: 84-87]
		public int? DepartmentId { get; set; } // Hangi bölüme baðlý? (Opsiyonel)
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	}
}
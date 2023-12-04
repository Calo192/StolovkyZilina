using StolovkyZilina.Models.Domain;

namespace StolovkyZilina.Models.ViewModels
{
	public class AdminMessageViewModel
	{
		public Guid Id { get; set; }
		public string Message { get; set; }
		public string Subject { get; set; }
		public string Author { get; set; }
		public int Status { get; set; }
		public int Type { get; set; }
		public DateTime Date { get; set; }

		public AdminMessageViewModel()
		{

		}

		public AdminMessageViewModel(AdminMessage adminMessage)
		{
			Id = adminMessage.Id;
			Message = adminMessage.Message;
			Subject = adminMessage.Subject;
			Author = adminMessage.Author;
			Status = adminMessage.Status;
			Type = adminMessage.Type;
			Date = adminMessage.Date;
		}

		public AdminMessage GetMessage()
		{
			var message = new AdminMessage();

			message.Id = Id;
			message.Message = Message;
			message.Subject = Subject;
			message.Author = Author;
			message.Status = Status;
			message.Type = Type;
			message.Date = Date;

			return message;
		}
	}
}

namespace StolovkyZilina.Models.Requests
{
    public class AddRatingRequest
    {
        public Guid ContentId { get; set; }
        public Guid UserId { get; set; }
        public int Rating { get; set; }
    }
}

namespace LibertyVilla_Server.Data.Model
{
    public class Room
    {
        public int Id { get; set; }
        public string RoomName { get; set; }
        public double Price { get; set; }
        public bool IsActive { get; set; }
        public List<RoomProps> RoomProps { get; set; }

    }
}

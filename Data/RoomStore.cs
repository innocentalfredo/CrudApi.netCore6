using test.Models.Dto;

namespace test.Data
{
    public static class RoomStore
    {
        public static List<RoomDTO> rooms = new List<RoomDTO>
            {
                new RoomDTO{Id = 1, Name = "Luxury Room"},
                new RoomDTO{Id =2, Name = "Delux Room"}
            };
    }
}


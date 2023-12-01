using BE_Shop.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BE_Shop.Data
{
    [Table("Message")]
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public ChatLine FromUser { get; set; }
        public int ToRoomId { get; set; }
        public Room ToRoom { get; set; }
    }
    [Table("Room")]
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ChatLine Admin { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}   

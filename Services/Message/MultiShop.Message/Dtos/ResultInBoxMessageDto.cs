namespace MultiShop.Message.Dtos
{
    public class ResultInBoxMessageDto
    {
        public int UserMessageId { get; set; }
        public string SenderId { get; set; }
        public string ReceivedId { get; set; }
        public string Subject { get; set; }
        public string MessageDetail { get; set; }
        public bool IsRead { get; set; }
        public DateTime MessageDate { get; set; }
    }
}

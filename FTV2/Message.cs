namespace FTV2
{
    public class Message<T>
    {
        public string Key { get; set; }
        public T Value { get; set; }
        public string Date { get; set; }
        public Message() { }

        public override string ToString()
        {
            return $"Key:{Key} V:{Value} [{Date}]";
        }
    }
}

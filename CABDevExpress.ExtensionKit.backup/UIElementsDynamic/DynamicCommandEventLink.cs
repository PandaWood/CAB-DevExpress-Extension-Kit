namespace CABDevExpress.UIElements
{
    public class DynamicCommandEventLink
    {
        public DynamicCommandEventLink(string eventTopicName, object data)
        {
            this.EventTopicName = eventTopicName;
            this.Data = data;
        }

        private string eventTopicName;
        public string EventTopicName
        {
            get { return eventTopicName; }
            set { eventTopicName = value; }
        }

        private object data;
        public object Data
        {
            get { return data; }
            set { data = value; }
        }
    }
}

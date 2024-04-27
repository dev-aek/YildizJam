namespace EventBus
{
    public delegate void EventListener<in TEvent>(TEvent eventToPublish);
}
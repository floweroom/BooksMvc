using BooksMvc.Interface;

namespace BooksMvc.Repository
{
    public class SimpleService :ITimeService
    {
        public string GetTime()
        {
            
            return System.DateTime.Now.ToString("hh:mm:ss");
        }
    }
}

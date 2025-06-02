namespace ProjectAPI.Event
{
    public delegate void GetProductProcessHandle();
    public class ProcessGetProduct
    {
        public event GetProductProcessHandle OnGetProductProcess;

        public void ProcessHandle()
        {
            OnGetProductProcess?.Invoke();
        }
    }
}

namespace WebUI.Service
{
    public class Service : IService
    {
        public string Name {  get; set; }

        public Service()
        {
            Name = Guid.NewGuid().ToString();
        }

    }
}

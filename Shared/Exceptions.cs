namespace ProjectApi.Shared
{
    public class Exceptions
    {
        public class IdNotFound : Exception
        {
            public IdNotFound(string message) : base(message){}
        }

        public class CustomerIsAlready : Exception
        {
            public CustomerIsAlready(string message) : base(message){}
        }
    }
}
using System;

namespace TrainScheduler.Model.Exceptions
{
    public class ObjectNotFoundException : Exception
    {
        public ObjectNotFoundException(string message)
            : base(message)
        {

        }
    }
}

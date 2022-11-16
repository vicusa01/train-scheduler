using System.Collections.Generic;

namespace TrainScheduler.Model.Models
{
    public class AuthResult
    {
        public AuthResult(bool succeeded)
        {
            Succeeded = succeeded;
            Errors = new List<string>();
        }

        public bool Succeeded { get; set; }

        public List<string> Errors { get; set; }
    }
}

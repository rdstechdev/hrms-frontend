using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceOne.ViewModels
{
    public class ProcessResult
    {
        public bool IsSucceed { get; set; }
        public string Message { get; set; }

        public int id { get; set; }

        public string user_id { get; set; }

        public void InsertSucceed()
        {
            this.IsSucceed = true;
            this.Message = "Data has been saved";
        }

        public void UpdateSucceed()
        {
            this.IsSucceed = true;
            this.Message = "Data has been saved";
        }

        public void DeleteSucceed()
        {
            this.IsSucceed = true;
            this.Message = "Data has been deleted";
        }

        public void ProcessSucceed(string message)
        {
            this.IsSucceed = true;
            this.Message = message;
        }

        public void ProcessFailed(string message)
        {
            this.IsSucceed = true;
            this.Message = message;
        }
    }
}
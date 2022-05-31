using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace TraineProject.FunctionApp
{
    public class Function1
    {
        [FunctionName("Function1")]
        public void Run([BlobTrigger("traineeprojectblobstorage/{name}", Connection = "StorageAccountConnectionFunctionApp")]Stream myBlob, string name, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
            ProcessLog(myBlob);
        }

        public void ProcessLog(Stream log)
        {
          using(var reader = new StreamReader(log)) {
                string line = "";
                while((line = reader.ReadLine()) != null)
                {

                } 
          }  
        }
    }
}
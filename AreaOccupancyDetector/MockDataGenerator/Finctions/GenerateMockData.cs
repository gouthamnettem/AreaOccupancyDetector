using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Extensions.Configuration;
using MockDataGenerator.ServiceHelper;

namespace MockDataGenerator.Finctions
{
    public class GenerateMockData
    {
        private readonly IGenerator _generator;
        public GenerateMockData(IGenerator generator)
        {
            this._generator = generator;   
        }

        [FunctionName("GenerateMockData")]
        public async Task Run(
            [TimerTrigger("%cornExpression%"
                #if DEBUG
                 , RunOnStartup =true 
                #endif
            )]TimerInfo myTimer, ILogger log)
        {
            _generator.GenerateData();
        }
    }
}

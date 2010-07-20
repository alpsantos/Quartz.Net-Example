using System;
using System.Collections.Specialized;
using Common.Logging;
using Quartz.Impl;

namespace Quartz
{
    class Program
    {
        static void Main(string[] args)
        {
            var properties = new NameValueCollection();
            properties["quartz.threadPool.threadCount"] = "1";
            
            const string cronExpression = "0/30 * * * * ?";

            var scheduler = new StdSchedulerFactory(properties).GetScheduler();
            
            var job = new JobDetail("NomeJob", "GrupoJob", typeof(BillingJob));

            var trigger = new CronTrigger("NomeTrigger", "GrupoTrigger", "NomeJob", "GrupoJob", cronExpression);

            scheduler.ScheduleJob(job, trigger);

            scheduler.Start();

            Console.ReadKey();

            scheduler.Shutdown();
        }
    }

    internal class BillingJob : IJob
    {
        public void Execute(JobExecutionContext context)
        {
            var log = LogManager.GetLogger(typeof(Program));
            log.Info(string.Format("Executado em : {0}", DateTime.Now));
        }
    }
}

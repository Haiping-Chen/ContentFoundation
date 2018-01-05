using CustomEntityFoundation;
using EntityFrameworkCore.BootKit;
using Microsoft.Data.Sqlite;
using Quartz;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace ContentFoundation.Core
{
    public abstract class ScheduleJobBase : IScheduleJob, IJob
    {
        protected Database dc { get; set; }

        public ScheduleJobBase()
        {
            dc = InitDc();
        }

        /// <summary>
        /// http://www.quartz-scheduler.net/documentation/quartz-2.x/tutorial/crontriggers.html
        /// </summary>
        /// <param name="context"></param>
        public abstract Task Execute(IJobExecutionContext context);

        /// <summary>
        /// 继续执行上一次中断的Job
        /// </summary>
        public virtual void ResumeJob()
        {

        }

        private static Database InitDc()
        {
            var dc = new Database();

            string db = CefOptions.Configuration.GetSection("Database:Default").Value;
            string connectionString = CefOptions.Configuration.GetSection("Database:ConnectionStrings")[db];

            if (db.Equals("SqlServer"))
            {
                dc.BindDbContext<IDbRecord, DbContext4SqlServer>(new DatabaseBind
                {
                    MasterConnection = new SqlConnection(connectionString),
                    CreateDbIfNotExist = true,
                    AssemblyNames = CefOptions.Assembles
                });
            }
            else if (db.Equals("Sqlite"))
            {
                connectionString = connectionString.Replace("|DataDirectory|\\", CefOptions.ContentRootPath + "\\App_Data\\");
                dc.BindDbContext<IDbRecord, DbContext4Sqlite>(new DatabaseBind
                {
                    MasterConnection = new SqliteConnection(connectionString),
                    CreateDbIfNotExist = true,
                    AssemblyNames = CefOptions.Assembles
                });
            }

            return dc;
        }
    }

    public interface IScheduleJob
    {
        /// <summary>
        /// 继续执行上一次中断的Job
        /// </summary>
        void ResumeJob();
    }
}

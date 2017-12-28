using Quickflow.Core.Interfacess;
using System;
using System.Collections.Generic;
using System.Text;
using Quickflow.Core.Entities;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Linq;
using Quickflow.Core;
using Microsoft.EntityFrameworkCore;
using EntityFrameworkCore.BootKit;
using CustomEntityFoundation.Bundles;

namespace Quickflow.ActivityRepository.CustomEntityFoundation
{
    public class InsertBundleEntityActivity : IWorkflowActivity
    {
        public Task Run(Database dc, Workflow wf, ActivityInWorkflow activity, ActivityInWorkflow preActivity)
        {
            var bundle = dc.Table<Bundle>().Include(x => x.Fields).FirstOrDefault(x => x.Id == activity.GetOptionValue("bundleId"));
            var entity = bundle.AddRecord(dc, JObject.FromObject(activity.Input.Data));
            activity.Output.Data = entity.ToBusinessObject(dc, bundle.EntityName);
            return Task.CompletedTask;
        }
    }
}

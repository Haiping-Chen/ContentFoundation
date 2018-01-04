using ContentFoundation.Core.Viewers;
using CustomEntityFoundation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ContentFoundation.RestApi.Viewers
{
    [AllowAnonymous]
    public class ViewerController : CoreController
    {
        [HttpGet("{viewerId}")]
        public Object GetViewer([FromRoute] String viewerId)
        {
            var viewer = dc.Table<Viewer>().Find(viewerId);
            viewer.LoadDetail();

            return viewer;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;
using Sitecore.Speak.Applications;
using Sitecore.Web;

namespace Sitecore.Support.Speak.Applications
{
    public class UploadMediaDialog : Sitecore.Speak.Applications.UploadMediaDialog
    {
        public override void  Initialize()
        {
            string queryString = WebUtil.GetQueryString("ro");
            string folder = WebUtil.GetQueryString("fo");
            bool showFullPath = Sitecore.Speak.Applications.SelectMediaDialog.GetShowFullPath(folder);
            switch (WebUtil.GetQueryString("ref"))
            {
                case "tree":
                    base.BackButton.Parameters["Click"] = "javascript:window.location.assign('/sitecore/client/applications/Dialogs/SelectMediaViaTreeDialog?ro={0}&fo={1}&hasUploaded=0&showFullPath={2}&du=');";
                    break;

                case "list":
                    base.BackButton.Parameters["Click"] = "javascript:window.location.assign('/sitecore/client/applications/Dialogs/SelectMediaDialog?ro={0}&fo={1}&hasUploaded=0&showFullPath={2}&du=');";
                    break;
            }
            base.BackButton.Parameters["Click"] = string.Format(base.BackButton.Parameters["Click"], HttpUtility.UrlEncode(queryString), HttpUtility.UrlEncode(folder), showFullPath);
            var rootItem = Sitecore.Speak.Applications.SelectMediaDialog.GetMediaItemFromQueryString(queryString) ?? ClientHost.Items.GetItem("/sitecore/media library");
            Sitecore.Speak.Applications.SelectMediaDialog.GetMediaItemFromQueryString(folder);
            string str4 = null;
            string str5 = str4 ?? rootItem.Paths.Path;
            SelectMediaDialogTree.SetTreeRootAndSelectedItem(base.TreeView, rootItem, null);
            base.DestinationValueText.Parameters["Text"] = Sitecore.Speak.Applications.SelectMediaDialog.GetDisplayPath(rootItem.Paths.Path, str4, showFullPath);
            base.Uploader.Parameters["DestinationUrl"] = str5;
        }

    }
}
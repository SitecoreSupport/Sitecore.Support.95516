using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Configuration;
using Sitecore.Diagnostics;
using Sitecore.Events.Hooks;
using Sitecore.SecurityModel;

namespace Sitecore.Support.Hooks
{
    public class UpdateUploadMediaDialog : IHook
    {
        public void Initialize()
        {
            using (new SecurityDisabler())
            {
                var databaseName = "core";
                var itemPath = "/sitecore/client/Applications/Dialogs/UploadMediaDialog";
                var fieldName = "__Renderings";

                // protects from refactoring-related mistakes
                var type = typeof(Sitecore.Support.Speak.Applications.UploadMediaDialog);

                var assemblyName = type.Assembly.GetName().Name;

                var database = Factory.GetDatabase(databaseName);
                var item = database.GetItem(itemPath);
                var fieldValue = item[fieldName].Replace("PageCodeTypeName=Sitecore.Speak.Applications.UploadMediaDialog%2c+Sitecore.Speak.Applications", "PageCodeTypeName=Sitecore.Support.Speak.Applications.UploadMediaDialog%2c+Sitecore.Support.95516");
                
                if (string.Equals(item[fieldName], fieldValue, StringComparison.Ordinal))
                {
                    // already installed
                    return;
                }

                Log.Info($"Installing {assemblyName} to update {item.Name}", this);
                item.Editing.BeginEdit();
                item[fieldName] = fieldValue;
                item.Editing.EndEdit();
            }
        }
    }
}
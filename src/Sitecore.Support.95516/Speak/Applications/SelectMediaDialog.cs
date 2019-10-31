using System.Web;
using Sitecore.Data.Items;
using Sitecore.Web;
using Sitecore.Configuration;

namespace Sitecore.Support.Speak.Applications
{
    public class SelectMediaDialog : Sitecore.Speak.Applications.SelectMediaDialog
    {
        public override void Initialize()
        {
            string str4;
            string str5;
            string str6;
            string str7;
            SelectMediaDialog.RedirectOnItembucketsDisabled(ClientHost.Items.GetItem("{16227E67-F9CB-4FB7-9928-7FF6A529708E}"));
            string queryString = WebUtil.GetQueryString("ro");
            string folder = WebUtil.GetQueryString("fo");
            bool showFullPath = SelectMediaDialog.GetShowFullPath(folder);
            string str3 = WebUtil.GetQueryString("hasUploaded");
            if (!(string.IsNullOrEmpty(str3) || (str3 != "1")))
            {
                base.DataSource.Parameters["SearchConfigItemId"] = "{1E723604-BFE0-47F6-B7C5-3E2FA6DD70BD}";
                base.Menu.Parameters["DefaultSelectedItemId"] = "{BE8CD31C-2A01-4ED6-9C83-E84C2275E429}";
            }
            Item item = SelectMediaDialog.GetMediaItemFromQueryString(queryString) ?? ClientHost.Items.GetItem("sitecore / media library");
            if (item != null)
            {
                base.MediaResultsListControl.Parameters["ContentLanguage"] = item.Language.ToString();
                base.MediaResultsListControl.Parameters["DefaultSelectedItemId"] = item.ID.ToString();
                base.DataSource.Parameters["RootItemId"] = item.ID.ToString();
                base.MediaFolderValueText.Parameters["Text"] = SelectMediaDialog.GetDisplayPath(item.Paths.Path, null, showFullPath);
            }
            base.TreeViewToggleButton.Parameters["Click"] = string.Format(base.TreeViewToggleButton.Parameters["Click"], HttpUtility.UrlEncode(queryString), HttpUtility.UrlEncode(folder), showFullPath);
            SelectMediaDialog.FillCommandParts(base.UploadButton.Parameters["Click"], out str4, out str5, out str6, out str7);
            string str8 = SelectMediaDialog.SetUrlContentDatabase(str4, WebUtil.GetQueryString("sc_content"));
            string format = str5 + str7 + str8 + str7 + str6;
            base.UploadButton.Parameters["Click"] = string.Format(format, HttpUtility.UrlEncode(queryString), HttpUtility.UrlEncode(folder), showFullPath);
        }
    }
}
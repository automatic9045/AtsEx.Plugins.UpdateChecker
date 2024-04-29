using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Markdig;

using Automatic9045.AtsEx.UpdateChecker.Data;
using Automatic9045.AtsEx.UpdateChecker.Hosting.GitHub;

namespace Automatic9045.AtsEx.UpdateChecker.Hosting
{
    internal static class UpdateChecker
    {
        public static void CheckUpdates()
        {
            DateTime stopNotifyUntil = new DateTime(Config.Instance.StopNotifyUntil);
            DateTime now = DateTime.Now;
            if (now < stopNotifyUntil)
            {
                Config.Instance.StopNotifyUntil = 0;
                Config.Save();
            }
            else if (now - stopNotifyUntil < new TimeSpan(12, 0, 0))
            {
                return;
            }

            try
            {
                ReleaseInfo latestRelease;
                switch (Config.Instance.Source.Value)
                {
                    case GitHubRepository repository:
                    {
                        RepositoryHost repositoryHost = new RepositoryHost(repository.Owner, repository.Name);
                        latestRelease = repositoryHost.GetLatestReleaseAsync().Result;
                        break;
                    }

                    case HttpRequest httpRequest:
                    {
                        using (HttpClient httpClient = new HttpClient())
                        {
                            string versionText = httpClient.GetStringAsync(httpRequest.VersionUrl).Result;
                            Version version = new Version(versionText);
                            latestRelease = new ReleaseInfo(version, httpRequest.UpdateInfoUrl);
                        }
                        break;
                    }

                    default:
                        throw new NotSupportedException();
                }

                Version currentVersion = new Version(Config.Instance.Version);
                if (currentVersion < latestRelease.Version)
                {
                    UpdateInfoDialog dialog = new UpdateInfoDialog(currentVersion, latestRelease.Version, GetUpdateDetailsHtmlAsync().Result);

                    DialogResult confirm = dialog.ShowDialog();
                    if (confirm == DialogResult.OK)
                    {
                        Process.Start(Config.Instance.DownloadUrl);
                    }

                    if (dialog.DoNotShowAgain)
                    {
                        Config.Instance.StopNotifyUntil = now.Ticks;
                        Config.Save();
                    }


                    async Task<string> GetUpdateDetailsHtmlAsync()
                        => await Task.Run(() =>
                        {
                            string updateDetailsMarkdown = "【詳細の取得に失敗しました】";
                            try
                            {
                                using (HttpClient httpClient = new HttpClient())
                                {
                                    updateDetailsMarkdown = httpClient.GetStringAsync(latestRelease.UpdateInfoUrl).Result;
                                }
                            }
                            catch { }

                            string updateDetailsHtml = Markdown.ToHtml(updateDetailsMarkdown);
                            return updateDetailsHtml;
                        }).ConfigureAwait(false);
                }
            }
            catch (Exception ex) { }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Octokit;

namespace Automatic9045.AtsEx.UpdateChecker.Hosting.GitHub
{
    internal sealed class RepositoryHost
    {
        private readonly string RepositoryOwner;
        private readonly string RepositoryName;

        public RepositoryHost(string repositoryOwner, string repositoryName)
        {
            RepositoryOwner = repositoryOwner;
            RepositoryName = repositoryName;
        }

        public async Task<ReleaseInfo> GetLatestReleaseAsync()
        {
            GitHubClient gitHubClient = new GitHubClient(new ProductHeaderValue("atsex-update-checker"));
            IReadOnlyList<Release> releases = await gitHubClient.Repository.Release.GetAll(RepositoryOwner, RepositoryName).ConfigureAwait(false);

            if (releases.Count == 0) throw new Exception("リリースが見つかりません。");

            IEnumerable<Release> stableReleases = releases.Where(r => !r.Prerelease);
            Release latestRelease = stableReleases.Any() ? stableReleases.First() : releases.First();

            string latestVersionText = latestRelease.TagName.TrimStart('v');
            Version latestVersion = Version.Parse(latestVersionText);

            IEnumerable<ReleaseAsset> updateInfoAssets = latestRelease.Assets.Where(asset => asset.Name.StartsWith("UpdateInfo.", StringComparison.Ordinal));
            ReleaseAsset updateInfoAsset = updateInfoAssets.FirstOrDefault(asset => asset.Name.EndsWith(".md", StringComparison.Ordinal)) ?? updateInfoAssets.First();

            return new ReleaseInfo(latestVersion, updateInfoAsset.BrowserDownloadUrl);
        }
    }
}

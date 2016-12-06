using System.Collections.Specialized;
using System.IO;
using System.Web;
using Lucene.Net.Analysis;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Umbraco.Core;
using Umbraco.Core.IO;
using Directory = System.IO.Directory;

namespace UmbracoExamine.VariableStorage
{
    internal class UmbracoVariableStorageHelper
    {
        public Lucene.Net.Store.Directory LuceneDirectory { get; private set; }
        private readonly object directoryLock = new object();
        public SnapshotDeletionPolicy Snapshotter { get; private set; }

        public UmbracoVariableStorageHelper()
        {
            IndexDeletionPolicy policy = new KeepOnlyLastCommitDeletionPolicy();
            Snapshotter = new SnapshotDeletionPolicy(policy);
        }

        public void Initialize(NameValueCollection config, string configuredPath, Lucene.Net.Store.Directory baseLuceneDirectory, Analyzer analyzer)
        {
            lock (directoryLock)
            {
                string path = Utilities.GetPathWithTokensReplaced(configuredPath);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                LuceneDirectory = FSDirectory.Open(new DirectoryInfo(path));
            }
        }
    }
}
using System.IO;
using System.Web;
using Examine.LuceneEngine.Config;
using Lucene.Net.Store;
using Umbraco.Core;
using ClientDependency.Core;

namespace UmbracoExamine.VariableStorage
{
    public class UmbracoVariableStorageSearcher : UmbracoExamineSearcher
    {
        private volatile Lucene.Net.Store.Directory _directory;
        private readonly object directoryLock = new object();
        private string _directoryPath;

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            base.Initialize(name, config);

            var indexSet = IndexSets.Instance.Sets[IndexSetName];
            string configuredPath = Utilities.GetPathWithTokensReplaced(indexSet.IndexPath);
            _directoryPath = configuredPath;
        }

        protected override Lucene.Net.Store.Directory GetLuceneDirectory()
        {
            if (_directory == null)
            {
                lock (directoryLock)
                {
                    if (_directory == null)
                    {
                        _directoryPath = Utilities.GetPathWithTokensReplaced(_directoryPath);
                        //not syncing just use a normal lucene directory
                        _directory = FSDirectory.Open(new DirectoryInfo(_directoryPath));
                    }
                }
            }

            return _directory;
        }
    }
}
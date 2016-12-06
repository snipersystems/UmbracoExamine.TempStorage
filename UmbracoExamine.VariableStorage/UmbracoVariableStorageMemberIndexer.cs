using System;
using System.Collections.Specialized;
using Examine.LuceneEngine.Config;
using Lucene.Net.Index;
using System.Linq;

namespace UmbracoExamine.VariableStorage
{
    public class UmbracoVariableStorageMemberIndexer : UmbracoMemberIndexer
    {
        //private readonly UmbracoVariableStorageHelper _helper = new UmbracoVariableStorageHelper();

        public UmbracoVariableStorageMemberIndexer()
        {
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            IndexSet set = null;
            if (name.EndsWith("Indexer"))
            {
                var setNameByConvension = name.Remove(name.LastIndexOf("Indexer")) + "IndexSet";
                //check if we can assign the index set by naming convention
                set = IndexSets.Instance.Sets.Cast<IndexSet>()
                    .Where(x => x.SetName == setNameByConvension)
                    .SingleOrDefault();

                if (set == null)
                {
                    throw new NullReferenceException("Could not find index set named " + setNameByConvension);
                }
            }

            var configuredPath = Utilities.GetPathWithTokensReplaced(set.IndexPath);
            set.IndexPath = configuredPath;

            base.Initialize(name, config);
        }
    }
}
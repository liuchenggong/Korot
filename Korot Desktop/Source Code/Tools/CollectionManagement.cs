using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Korot
{
    public class CollectionManagement
    {
        public string Collections = null;
        public bool readCollections(string collectionFile)
        {
            Collections = FileSystem2.ReadFile(collectionFile, Encoding.UTF8);
            return true;
        }
        public bool writeCollections(string collectionFile)
        {
            FileSystem2.WriteFile(collectionFile, Collections, Encoding.UTF8);
            return true;
        }
    }
}

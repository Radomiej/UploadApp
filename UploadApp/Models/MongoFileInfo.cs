using System;
using System.Collections.Generic;

namespace RepozytoriumKlient
{
    public class MongoFileInfo
    {
        public string id;
        public MongoFileMetadata metadata;
        public string filename;
        public string[] aliases;
        public long chunkSize;
        public string uploadDate;
        public long length;
        public string contentType;
        public string md5;
    }

    public class MongoFileMetadata
    {
        public Dictionary<String, String> values;
    }
}
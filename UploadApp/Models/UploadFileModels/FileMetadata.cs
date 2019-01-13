using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepozytoriumKlient.Models.UploadFileModels
{
    public class FileMetadata
    {
        public Dictionary<string, string> values = new Dictionary<string, string>();

        public void SetMetadata(string metadataKey, string metadataValue)
        {
            if (values.ContainsKey(metadataKey))
            {
                values[metadataKey] = metadataValue;
            }
            else
            {
                values.Add(metadataKey, metadataValue);
            }
        }
    }
}

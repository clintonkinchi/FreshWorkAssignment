using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FreshWorksDataStore.Models
{
    [XmlRoot("freshworks_datastore")]
    public class DataStore
    {       
        [XmlArray("Records",IsNullable =true)]
        public List<Record> Records { get; set; }
    }
}

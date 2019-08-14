using System;

namespace Entidades
{
    public class CustomField
    {
        public String encodedKey { get; set; }
        public String id { get; set; }
        public String name { get; set; }
        public String type { get; set; }
        public String datatype { get; set; }
        public String valueLength { get; set; }
        public String linkedEntityKeyValue { get; set; }
        public String customFieldSetGroupIndex { get; set; }

        public CustomFieldSet customFieldSet { get; set; }
    }
}
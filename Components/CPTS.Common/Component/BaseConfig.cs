using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using CPTS.Common.Utilities;

namespace CPTS.Common.Component
{
    public abstract class BaseConfig : IConfigurationSectionHandler
    {
        public abstract object Create(object parent, object configContext, XmlNode section);
        protected IList<T> attributeValueArray<T>(XmlNode node, string name, string sper = "|")
        {
            if (node == null) return null;
            var att = node.Attributes[name];
            if (att != null && !string.IsNullOrWhiteSpace(att.Value))
            {
                var arr = Regex.Split(att.Value, sper);
                if (arr.Length > 0)
                {
                    var list = new List<T>();
                    foreach (var item in arr)
                    {
                        var obj = item.ParseType<T>();
                        if (obj != null)
                            list.Add(obj);
                    }
                    return list;
                }
            }
            return null;
        }
        protected T attributeValue<T>(XmlNode node, string name)
        {
            if (node == null) return default(T);
            var att = node.Attributes[name];
            if (att != null && !string.IsNullOrWhiteSpace(att.Value))
                return att.Value.ParseType<T>();
            return default(T);
        }
    }
}

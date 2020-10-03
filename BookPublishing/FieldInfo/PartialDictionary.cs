using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace BookPublishing.FieldInfo
{
    public interface ILoadPartialDictionary<TKey, TValue>
    {
        void LoadProperties(PartialDictionary<TKey, TValue> properties);
    }

    [Serializable]
    public class PartialDictionary<TKey, TValue> : Dictionary<TKey, TValue>, 
        IEnumerable<KeyValuePair<TKey, TValue>>, ISerializable, IXmlSerializable
    {
        public bool IsLoaded = false;
        protected ILoadPartialDictionary<TKey, TValue> _reference = null;

        protected PartialDictionary(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            IsLoaded = (bool)info.GetValue("IsLoaded", typeof(bool));
            _reference = (ILoadPartialDictionary<TKey, TValue>)info.GetValue("_reference", typeof(ILoadPartialDictionary<TKey, TValue>));
        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("IsLoaded", IsLoaded);
            if (_reference != null) info.AddValue("_reference", _reference);
            base.GetObjectData(info, context);
        }

        public void ForceLoad()
        {
            if (!IsLoaded && _reference != null) _reference.LoadProperties(this);
        }

        public PartialDictionary()
        {
            _reference = null;
            IsLoaded = true;
        }

        public PartialDictionary(ILoadPartialDictionary<TKey, TValue> reference, bool isLoaded)
        {
            _reference = reference;
            IsLoaded = isLoaded;
        }

        public new TValue this[TKey key]
        {
            get
            {
                lock (this)
                {
                    if (base.ContainsKey(key)) return (base[key]);
                    else if (IsLoaded == false && _reference != null) { _reference.LoadProperties(this); return base[key]; }
                    else return default(TValue);
                }
            }
            set
            {
                lock (this)
                {
                    base[key] = value;
                }
            }
        }

        public new IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            if (!IsLoaded && _reference != null) _reference.LoadProperties(this);
            return base.GetEnumerator();
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

            bool wasEmpty = reader.IsEmptyElement;
            reader.Read();

            if (wasEmpty)
                return;

            while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
            {
                reader.ReadStartElement("item");

                reader.ReadStartElement("key");
                TKey key = (TKey)keySerializer.Deserialize(reader);
                reader.ReadEndElement();

                reader.ReadStartElement("value");
                TValue value = (TValue)valueSerializer.Deserialize(reader);
                reader.ReadEndElement();

                this.Add(key, value);

                reader.ReadEndElement();
                reader.MoveToContent();
            }
            reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

            foreach (TKey key in this.Keys)
            {
                writer.WriteStartElement("item");

                writer.WriteStartElement("key");
                keySerializer.Serialize(writer, key);
                writer.WriteEndElement();

                writer.WriteStartElement("value");
                TValue value = this[key];
                valueSerializer.Serialize(writer, value);
                writer.WriteEndElement();

                writer.WriteEndElement();
            }
        }
    }

}

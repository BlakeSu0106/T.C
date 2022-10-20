using System.Xml.Serialization;

namespace Telligent.Core.Domain.Notifications;

[XmlRoot(ElementName = "Data")]
public class SmsPushResult
{
    [XmlElement(ElementName = "ID")] public string Id { get; set; }

    [XmlElement(ElementName = "Result")] public string Result { get; set; }
}
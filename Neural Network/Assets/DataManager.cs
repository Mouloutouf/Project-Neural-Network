using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using UnityEngine;

[XmlRoot("Data")]
public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public string path;

    XmlSerializer serializer = new XmlSerializer(typeof(Data));
    Encoding encoding = Encoding.GetEncoding("UTF-8");

    void Awake()
    {
        instance = this;
        SetPath();
    }

    public void Save(List<NeuralNetwork> _nets)
    {
        StreamWriter streamWriter = new StreamWriter(path, false, encoding);

        Data data = new Data { nets = _nets };

        serializer.Serialize(streamWriter, data);
    }

    public Data Load()
    {
        if (File.Exists(path))
        {
            FileStream fileStream = new FileStream(path, FileMode.Open);

            return serializer.Deserialize(fileStream) as Data;
        }
        else return null;
    }

    public void SetPath()
    {
        path = Path.Combine(Application.persistentDataPath, "Data.xml");
    }
}

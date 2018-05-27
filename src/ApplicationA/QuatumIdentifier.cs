using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace ApplicationA
{
    class QuatumIdentifier
    {
        static List<RecordModel> taskList = new List<RecordModel>();
        internal static void Start(String Url)
        {
            using (XmlReader reader = XmlReader.Create(Url))
            {
                reader.MoveToContent();
                var i = 1;
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.Name == "record")
                        {
                            XElement el = XNode.ReadFrom(reader) as XElement;
                            if (el != null)
                            {
                                taskList.Add(new RecordModel
                                {
                                    Id = i++,
                                    Value = (string)el.Attribute("value"),
                                    Type = ((string)el.Attribute("type")).ToEnum<ClientType>(),
                                    Status = RecordStatus.Pending
                                });
                            }
                        }
                    }
                }
                Process().Wait();
            }
        }
        internal static async Task Process()
        {
            JobDitributer _ditributer = new JobDitributer();
            int _total = taskList.Count;
            while (taskList.Any(x => x.Status == RecordStatus.Pending))
            {
                var _ids = taskList.Where(x => x.Status == RecordStatus.Pending).Select(x => x.Id);
                foreach (var id in _ids)
                {
                    var _task = taskList.FirstOrDefault(x => x.Id == id);
                    _task.Status = RecordStatus.Processing;
                    if (await _ditributer.AssignAsync(_task))
                    {
                        Console.WriteLine(String.Format("Processed: Id: {0}, Type: {1}, Value: {2}", _task.Id, _task.Type, _task.Value));
                        _task.Status = RecordStatus.Processed;
                    }
                    else
                    {
                        _task.Status = RecordStatus.Pending;
                    }

                    Thread.Sleep(1000);
                }
            }
        }
    }
}

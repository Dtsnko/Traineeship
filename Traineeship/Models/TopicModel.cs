using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Traineeship.Models
{
    public class TopicModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public string Owner { get; set; }
        public TopicModel(int id, string name, int parentId, string owner)
        {
            Id = id;
            Name = name;
            ParentId = parentId;
            Owner = owner;
        }
        public TopicModel()
        {

        }
    }
}

using ButodoProject.Core.Model.FixType;
using System;
using System.Collections.Generic;
using System.Text;

namespace ButodoProject.Core.Service.Dto
{
    public class MessageDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid TaskId { get; set; }

    }
}

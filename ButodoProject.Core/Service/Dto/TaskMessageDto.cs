using System;
using System.Collections.Generic;
using System.Text;

namespace ButodoProject.Core.Service.Dto
{
    public class TaskMessageDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string TaskTableName { get; set; }
        public Guid TaskTableId { get; set; }
        public DateTime CreatedAt { get; set; }
        public IEnumerable<TaskMessageDto> TaskMessageList { get; set; }
        public IEnumerable<TaskTableDto> TaskTableList { get; set; }
        public PersonalListDto PersonalListDto { get; set; }
    }
}

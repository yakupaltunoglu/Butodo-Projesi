using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ButodoProject.Core.Service.Dto
{
    public class SpendTimeDto
    {
        public Guid Id { get; set; }
        public Guid PersonalId { get; set; }
        public Guid TaskTableId { get; set; }

        public int Minute { get; set; }
        public DateTime CreatedAt { get; set; }
        public string TaskTableName { get; set; }
        public string PersonalName { get; set; }
        public TaskTableDto TaskTableDto { get; set; }
        public IList<TaskTableDto> TaskTableList { get; set; }
        public PersonalDto PersonalDto { get; set; }
        public IList<PersonalDto> PersonalList { get; set; }
    }

    

}
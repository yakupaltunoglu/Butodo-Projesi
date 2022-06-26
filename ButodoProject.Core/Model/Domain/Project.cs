using System;

namespace ButodoProject.Model.Domain
{
    public class Project:EntityBase
    {
        public virtual string ProjectName { get; set; }
        public virtual string FullProjectName { get; set; }
        public virtual Company Company { get; set; }

        public virtual int Leftx { get; set; }
        public virtual int Rightx { get; set; }
        public virtual int Depth { get; set; }
    }
}

using System;

namespace CWB.CommonUtils.Common
{
    public class BaseEntity
    {
        public long Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string Creator { get; set; }
        public string LastModifier { get; set; }
    }
}

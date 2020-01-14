using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Model.Entity.Repository
{
    public class Repository
    {
        // Kho ID
        public int RepositoryId { get; set; }
        // Tên Kho
        public string RepositoryName { get; set; }
        // Người tạo
        public int Created { get; set; }
        // Ngày tạo
        public DateTime CreateTime { get; set; }
        // Người cập nhật
        public int Updated { get; set; }
        // Người udpate
        public DateTime UpdateTime { get; set; }
        public int Status { get; set; }
        public float Acreage { get; set; }

    }
}

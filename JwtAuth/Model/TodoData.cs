using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtAuth.Model
{
    public class TodoData
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public bool Done { get; set; }
    }
}

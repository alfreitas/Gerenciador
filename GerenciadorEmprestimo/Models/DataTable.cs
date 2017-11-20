using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GerenciadorEmprestimo.Models
{
    public class DataTable<T>
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public T[] data { get; set; }
    }
}
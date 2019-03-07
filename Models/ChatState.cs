using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmlakBot.Models
{
    class ChatState
    {
        public int State { get; set; }
        public string PrimaryCommand { get; set; }
        public string SubCommand { get; set; }
    }
}

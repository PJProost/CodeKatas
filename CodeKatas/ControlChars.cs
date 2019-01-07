using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeKatas
{
    public class ControlChars
    {
        public enum Chars
        {
            ENQ = 0x05, //enquire
            SOH = 0x01, //start of heading
            ETB = 0x17, //end of transmission block
            STX = 0x02, //start of text
            ETX = 0x03, //end of text
            EOT = 0x04  //end of transmission
        }
    }
}

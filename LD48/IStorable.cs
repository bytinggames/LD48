using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LD48
{
    public interface IStorable
    {
        public object[] GetConstructorValues();
    }
}

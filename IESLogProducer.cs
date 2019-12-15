using System;
using System.Collections.Generic;
using System.Text;

namespace DataStreaming
{
    public interface IESLogProducer
    {
        void Produce(string message);
    }
}

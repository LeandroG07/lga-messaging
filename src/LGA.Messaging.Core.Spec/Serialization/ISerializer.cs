using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGA.Messaging.Core.Spec.Serialization
{
    public interface ISerializer
    {

        string Serialize<T>(T model);

        T Deserealize<T>(string data);

    }
}

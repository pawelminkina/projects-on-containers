using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Architecture.DDD;

namespace Issues.Application.Services.Files
{
    public interface ICsvFileReader
    {
        IEnumerable<T> ReadEntity<T>(byte[] content) where T : EntityBase;
    }
}

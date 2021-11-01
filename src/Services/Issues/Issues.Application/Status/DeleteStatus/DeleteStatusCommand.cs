using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Issues.Application.Status.DeleteStatus
{
    public class DeleteStatusCommand : IRequest
    {
        public DeleteStatusCommand(string statusId)
        {

        }
    }
}

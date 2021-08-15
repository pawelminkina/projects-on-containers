using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tasks.API.Extensions;
using Tasks.API.Protos;
using Tasks.Application.Tasks.CreateTask;
using Tasks.Application.Tasks.DeleteTask;
using Tasks.Application.Tasks.GetTask;
using Tasks.Application.Tasks.GetTasks;
using Tasks.Application.Tasks.UpdateTask;

namespace Tasks.API.GrpcServices
{
    public class GrpcTaskService : TaskService.TaskServiceBase
    {
        private readonly IMediator _mediator;

        public GrpcTaskService(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        public override async Task<CreateTaskResponse> CreateTask(CreateTaskRequest request, ServerCallContext context)
        {
            var taskId = await _mediator.Send(new CreateTaskCommand(request.Name, request.Content, context.GetHttpContext().User.GetUserId()));
            return new CreateTaskResponse() { Id = taskId };
        }

        public override async Task<DeleteTaskResponse> DeleteTask(DeleteTaskRequest request, ServerCallContext context)
        {
            await _mediator.Send(new DeleteTaskCommand(request.Id, context.GetHttpContext().User.GetUserId()));
            return new DeleteTaskResponse();
        }

        public override async Task<GetTaskResponse> GetTask(GetTaskRequest request, ServerCallContext context)
        {
            var task = await _mediator.Send(new GetTaskQuery(request.TaskId, context.GetHttpContext().User.GetUserId()));
            if (task is null)
                throw new RpcException(new Grpc.Core.Status(StatusCode.NotFound, $"Task {request.TaskId} was not found"));
            
            return new GetTaskResponse() { Task = MapToProtoTask(task) };
        }

        public override async Task<GetTasksResponse> GetTasks(GetTasksRequest request, ServerCallContext context)
        {
            var tasks = await _mediator.Send(new GetTasksQuery(context.GetHttpContext().User.GetUserId()));
            if (tasks is null)
                return new GetTasksResponse();

            var responseToReturn = new GetTasksResponse();
            responseToReturn.Tasks.AddRange(tasks.Select(task => MapToProtoTask(task)));
            return responseToReturn;
        }

        public override async Task<UpdateTaskResponse> UpdateTask(UpdateTaskRequest request, ServerCallContext context)
        {
            await _mediator.Send(new UpdateTaskCommand(request.Id, request.Name, context.GetHttpContext().User.GetUserId()));
            return new UpdateTaskResponse();
        }

        public override async Task<UpdateTaskStatusResponse> UpdateTaskStatus(UpdateTaskStatusRequest request, ServerCallContext context)
        {
            throw new NotImplementedException();
        }

        private Protos.Task MapToProtoTask(Tasks.Domain.Tasks.Task task)
        {
            return new Protos.Task()
            {
                Content = task.Content,
                Id = task.Id,
                Name = task.Name,
                TimeOfCreation = task.TimeOfCreation.ToTimestamp()
            };
        }
    }
}

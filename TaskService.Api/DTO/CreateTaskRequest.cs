namespace TaskService.Api.DTO;

public class CreateTaskRequest
{
    public required string Title { get; set;}
}

// Frontend (HTTP request) -> API/Controller (Mapper JSON -> C# Objekt) -> CreateTaskRequest.cs -> TaskService -> Repository -> Database
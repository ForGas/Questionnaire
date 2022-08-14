using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/v{version:apiVersion}/[controller]/[action]")]
[ApiController]
public class ApiController : ControllerBase { }

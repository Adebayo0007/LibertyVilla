using Microsoft.AspNetCore.Mvc;
namespace LibertyVilla_WebApi.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
public class VersionedApiController : BaseApiController
{
}

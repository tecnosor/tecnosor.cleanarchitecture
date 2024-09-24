using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tecnosor.cleanarchitecture.common.domain.maps;
using tecnosor.cleanarchitecture.common.domain.match;
using stolenCars.publication.domain;

namespace stolenCars.restApi.controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class PublicationController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapperService _mapperService;

    public PublicationController(ISender sender, IMapperService mapperService)
    {
        _sender = sender;
        _mapperService = mapperService;
    }

    [HttpGet]
    public IActionResult Find([FromQuery(Name = "filters")] List<RestFilter> filters,
        [FromQuery(Name = "orderby")] string orderBy,
        [FromQuery(Name = "order")] string order,
        [FromQuery(Name = "page")] int page,
        [FromQuery(Name = "size")] int size)
    {
        List<Filter<Publication>> domainFilters = _mapperService.Map<List<Filter<Publication>>>(filters);

        _sender.Send(null);
        return Ok();
    }

    [HttpGet("{id}")]
    public IActionResult Get(long id)
    {
        // TODO
        _sender.Send(null);
        return Ok();
    }

    [HttpPost()]
    public IActionResult Create(long id)
    {
        // TODO
        _sender.Send(null);
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult Modify(long id)
    {
        // TODO
        _sender.Send(null);
        return Ok();
    }

    [HttpPatch("{id}")]
    public IActionResult PartialModification(long id)
    {
        // TODO
        _sender.Send(null);
        return Ok();
    }

}

using Application.UseCases.Resales.Create;
using Application.UseCases.Resales.GetByFilter;
using Application.UseCases.Resales.GetById;
using Application.UseCases.Resales.Updade;
using Application.Validators;
using Dto.Orders.Reponses;
using Dto.Resales.Requests;
using Dto.Resales.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace Resale.Api.Controllers
{
    [ExcludeFromCodeCoverage]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("resales/")]
    [Produces("application/json")]
    public class ResalesController : ControllerBase
    {
        private readonly ActivitySource _activitySource;

        public ResalesController(ActivitySource activitySource)
        {
            _activitySource = activitySource;
        }


        /// <summary>
        /// Cadastro de Revenda.
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        ///
        ///     POST /{
        ///             "fantasyName": "Distribuidora da esquina",
        ///             "phone": "23999994323",
        ///             "contactName": "Heitor Machado",
        ///             "email": "machado.loureiro@gmail.com",
        ///             "cnpj": "17373221000108", 
        ///             "address": {
        ///               "street": "Av sete",
        ///               "zipCode": "29100200",
        ///               "number": "s/n",
        ///               "city": "Vila Velha"
        ///             }
        ///            }
        /// </remarks>
        /// <param name="createRequestDto">Dto de requisição de cadastro de revenda.</param>
        /// <param name="createUseCase">Caso de uso para criação da revenda</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Codigo da revenda</returns> 
        /// <response code="201">Returns OrderResponseDto</response>
        [ProducesResponseType(typeof(OrderResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<ActionResult<OrderResponseDto>> Create(
           [FromServices] IResalesCreateUseCase createUseCase,
           ResalesRequestDto createRequestDto,
           CancellationToken cancellationToken)
        {

            using var activity = _activitySource.StartActivity("CreateResale");
            activity?.AddEvent(new ActivityEvent("Create Resale - Started"));
            activity?.SetTag("payload.request", createRequestDto);

            var validator = new ResalesRequestValidator();
            var validationResult = validator.Validate(createRequestDto);

            if (!validationResult.IsValid)
            {
                activity?.AddEvent(new ActivityEvent("Request Invalided"));
                activity?.SetTag("payload.validationError", validationResult.Errors.Distinct());
                return BadRequest(validationResult.Errors);
            }

            var result = await createUseCase.Execute(createRequestDto, cancellationToken);
            if (result.IsError)
            {
                activity?.AddEvent(new ActivityEvent($"Error: CreateUseCase"));
                activity?.SetTag("payload.usecaseError", result.Errors.Distinct());
                return BadRequest(result.Errors);
            }

            activity?.SetTag("payload.response", result.Value);
            activity?.AddEvent(new ActivityEvent("Create Resale - Finalized"));
            return Ok(result.Value);

        }


        /// <summary>
        /// Atualização de dados da Revenda.
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        ///
        ///     POST /{
        ///             "id": "67f16a6bce05e946b18f02e2",
        ///             "fantasyName": "Distribuidora da rua de trás",
        ///             "phone": "23999994323",
        ///             "contactName": "Heitor Machado",
        ///             "email": "machado.loureiro@gmail.com",
        ///             "cnpj": "17373221000108", 
        ///             "address": {
        ///               "street": "Av sete",
        ///               "zipCode": "29100200",
        ///               "number": "s/n",
        ///               "city": "Vila Velha"
        ///             }
        ///            }
        /// </remarks>
        /// <param name="id">identificador da revenda a ser atualizada.</param>
        /// <param name="requestDto">Dto de atualização de dados da revenda.</param>
        /// <param name="updateResaleUseCase">Caso de uso para atualização da revenda</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>retorna  ResalesResponseDto</returns> 
        /// <response code="201">Returns ResalesResponseDto</response>
        [ProducesResponseType(typeof(ResalesResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{id}")]
        public async Task<ActionResult<ResalesResponseDto>> Update(
            string id,
            ResaleUpdateRequestDto requestDto,
            [FromServices] IUpdateResaleUseCase updateResaleUseCase,
            CancellationToken cancellationToken)
        {

            using var activity = _activitySource.StartActivity("UpdateResale");
            activity?.AddEvent(new ActivityEvent("UpdateResale - Started"));
            activity?.SetTag("payload.id", id);
            activity?.SetTag("payload.request", JsonSerializer.Serialize(requestDto));

            var validator = new ResalesUpdatRequestDtoValidator();
            var validationResult = validator.Validate(requestDto);

            if (!validationResult.IsValid)
            {
                activity?.AddEvent(new ActivityEvent("Request Invalided"));
                activity?.SetTag("payload.validationError", JsonSerializer.Serialize(validationResult.Errors.Distinct()));
                return BadRequest(validationResult.Errors);
            }

            var result = await updateResaleUseCase.Execute(id, requestDto, cancellationToken);
            if (result.IsError)
            {
                activity?.AddEvent(new ActivityEvent("Error: UpdateUseCase"));
                activity?.SetTag("payload.usecaseError", JsonSerializer.Serialize(result.Errors.Distinct()));
                return BadRequest(result.Errors);
            }

            activity?.SetTag("payload.response", JsonSerializer.Serialize(result.Value));
            activity?.AddEvent(new ActivityEvent("UpdateResale - Finalized"));
            return Ok(result.Value);
        }



        /// <summary>
        /// Listagem de revendas cadastradas.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <param name="getAllUseCase">Caso de uso para obtenção das revendas</param>
        /// <returns>Lista de revendas cadastradas</returns> 
        /// <response code="201">Returns ResalesResponseDto</response>
        [ProducesResponseType(typeof(List<ResalesResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<ActionResult<List<ResalesResponseDto>>> GetAll(
            [FromServices] IGetAllResaleUseCase getAllUseCase,
            CancellationToken cancellationToken)
        {
            using var activity = _activitySource.StartActivity("GetAllResale");

            activity?.AddEvent(new ActivityEvent("GetAllResale - Started"));

            var result = await getAllUseCase.Execute(cancellationToken);
            if (result.IsError)
                return NoContent();

            activity?.AddEvent(new ActivityEvent("GetAllResale - Finalized"));
            return Ok(result.Value);
        }


        /// <summary>
        /// Busca por revenda atraves do codigo de identificação.
        /// </summary>
        /// <param name="id">identificador da revenda a ser atualizada.</param>
        /// <param name="getResaleByIdUseCase">Caso de uso para obtenção de revenda por Id</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A revenda cadastrada</returns> 
        /// <response code="201">Returns ResalesResponseDto</response>
        [ProducesResponseType(typeof(ResalesResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ResalesResponseDto>> GetById(
            string id,
            [FromServices] IGetResaleByIdUseCase getResaleByIdUseCase,
            CancellationToken cancellationToken)
        {
            using var activity = _activitySource.StartActivity("GetByIdResale");

            activity?.AddEvent(new ActivityEvent("GetByIdResale - Started"));
            activity?.SetTag("payload.request", id);

            var result = await getResaleByIdUseCase.Execute(id, cancellationToken);
            if (result.IsError)
                return NoContent();

            activity?.SetTag("payload.response", JsonSerializer.Serialize(result.Value));
            activity?.AddEvent(new ActivityEvent("GetByIdResale - Finalized"));
            return Ok(result.Value);
        }



    }
}

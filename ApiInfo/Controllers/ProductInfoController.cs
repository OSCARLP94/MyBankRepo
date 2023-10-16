using ApiInfo.DTOs;
using ApiInfo.Mappers;
using BusinessDomain;
using BusinessDomain.Contracts;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ApiInfo.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    //[Authorize]
    public class ProductInfoController : ControllerBase
    {
        private readonly ILogger<ProductInfoController> _logger;
        private readonly IInformationDomain _infoCheckingAccountDomain;

        public ProductInfoController(ILogger<ProductInfoController> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _infoCheckingAccountDomain = serviceProvider.GetServices<IInformationDomain>()?.FirstOrDefault(x=>x.GetType() == typeof(CheckingAccountDomain));
        }

        /// <summary>
        /// Lista productos asociados a un cliente
        /// </summary>
        /// <param name="idClient"></param>
        /// <returns></returns>
        [HttpGet, Route(nameof(ProductInfoController.InfoProductsByCient))]
        [ProducesResponseType(typeof(ExceptionLib.Response), StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtiene información de productos asociados a un cliente.")]
        public async Task<dynamic> InfoProductsByCient(string userClient)
        {
            try
            {
                return await _infoCheckingAccountDomain.GetProductsByClient(userClient);
            }
            catch (Exception ex)
            {
                return ExceptionLib.Response.WithErrorException(ex);
            }
        }

        //detalle producto cliente

        //transacciones producto cliente
        [HttpPost, Route(nameof(ProductInfoController.ListTransactionsClient))]
        [ProducesResponseType(typeof(ExceptionLib.Response), StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(Summary = "Obtiene información de transacciones de cliente")]
        public async Task<dynamic> ListTransactionsClient(ReqTransactionsClientDTO reqDTO)
        {
            try
            {
                return await _infoCheckingAccountDomain.GetTransactionsClient(InfoTransactionsMapper.FromInfoTransacReqClientToInfoReqDomain(reqDTO));
            }
            catch (Exception ex)
            {
                return ExceptionLib.Response.WithErrorException(ex);
            }
        }
    }
}
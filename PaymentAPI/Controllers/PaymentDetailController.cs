using Microsoft.AspNetCore.Mvc;
using PaymentAPI.Models;
using PaymentAPI.Services;
using System.Threading.Tasks.Dataflow;

namespace PaymentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentDetailController : ControllerBase
    {
        public PaymentService _paymentService;
        private readonly ILogger<PaymentDetailController> _logger;

        public PaymentDetailController(PaymentService paymentService, ILogger<PaymentDetailController> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        [HttpGet] //GET api/PaymentDetail
        public async Task< ActionResult<List<PaymentDetail>>> Get()
        {
            try
            {
                return Ok(await _paymentService.Get());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving payment details.");
                return StatusCode(500, "An error occurred while retrieving payment details.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDetail>> Get(string id) {
            try
            {
                return await _paymentService.Get(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving payment detail with ID: {0}", id);
                return StatusCode(500, "An error occurred while retrieving the payment detail.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<PaymentDetail>> CreateAsync(PaymentDetail paymentDetail)
        {
            try
            {
                await _paymentService.Create(paymentDetail);
                return Ok(await _paymentService.Get());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating payment detail.");
                return StatusCode(500, "An error occurred while creating the payment detail.");
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update(PaymentDetail paymentDetail)
        {
            try
            {
                await _paymentService.Update(paymentDetail.PaymentDetailId, paymentDetail);
                return Ok(await _paymentService.Get());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating payment detail with ID: {0}", paymentDetail.PaymentDetailId);
                return StatusCode(500, "An error occurred while updating the payment detail.");
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                await _paymentService.Delete(id);
                return Ok(await _paymentService.Get());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting payment detail with ID: {0}", id);
                return StatusCode(500, "An error occurred while deleting the payment detail.");
            }
        }
    }
}

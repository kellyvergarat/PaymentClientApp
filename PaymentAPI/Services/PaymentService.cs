using MongoDB.Driver;
using PaymentAPI.Models;

namespace PaymentAPI.Services
{
    //CRUD to mongodb 

    public class PaymentService
    {
        private IMongoCollection<PaymentDetail> _payments;

        public PaymentService(IPaymentSettings settings)
        {
            //Getting values from injection
            var client = new MongoClient(settings.Server);
            var database = client.GetDatabase(settings.Database);
            _payments = database.GetCollection<PaymentDetail>(settings.Collection);
        }

        public async Task<List<PaymentDetail>> Get()
        {
            var cursor = await _payments.FindAsync(d => true);
            return await cursor.ToListAsync();
        }

        public async Task<PaymentDetail> Get(string id)
        {
            //return _payments.Find(d=> d.PaymentDetailId == id).FirstOrDefault();

            var filter = Builders<PaymentDetail>.Filter.Eq(d => d.PaymentDetailId, id);
            var cursor = await _payments.FindAsync(filter);
            return await cursor.FirstOrDefaultAsync();
        }

        public async Task<PaymentDetail> Create(PaymentDetail paymentDetail)
        {
            var options = new InsertOneOptions() { BypassDocumentValidation = true };
            await _payments.InsertOneAsync(paymentDetail, options);
            return paymentDetail;
        }

        public async Task Update(string id, PaymentDetail paymentDetail)
        {
            await _payments.ReplaceOneAsync(d => d.PaymentDetailId == id, paymentDetail);
        }

        public async Task Delete(string id)
        {
            await _payments.DeleteOneAsync(d => d.PaymentDetailId == id);
        }
    }
}

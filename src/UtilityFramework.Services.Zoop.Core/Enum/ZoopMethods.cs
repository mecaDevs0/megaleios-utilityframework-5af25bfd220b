namespace UtilityFramework.Services.Zoop.Core.Enum
{
    public class ZoopMethods
    {

        public const string BaseUrl = "https://api.zoop.ws";
        /// <summary>
        /// marketplace_id = id do market place
        /// </summary>
        /// <value></value>
        public const string CreateCardToken = "/v1/marketplaces/{{marketplace_id}}/cards/tokens";
        /// <summary>
        /// marketplace_id = id do market place
        /// </summary>
        /// <value></value>
        public const string CreateBankToken = "/v1/marketplaces/{{marketplace_id}}/bank_accounts/tokens";
        /// <summary>
        /// token_id = token do cartão ou conta bancaria
        /// marketplace_id = id do market place
        /// </summary>
        /// <value></value>
        public const string GetInfoToken = "/v1/marketplaces/{{marketplace_id}}/tokens/{{token_id}}";
        /// <summary>
        /// marketplace_id = id do market place
        /// </summary>
        /// <value></value>
        public const string RegisterBuyer = "/v1/marketplaces/{{marketplace_id}}/buyers";
        /// <summary>
        /// marketplace_id = id do market place
        /// </summary>
        /// <value></value>
        public const string ListBuyerByMarketPlace = "/v1/marketplaces/{{marketplace_id}}/buyers";
        /// <summary>
        /// marketplace_id = id do market place
        /// </summary>
        /// <value></value>
        public const string AssociateCreaditCard = "/v1/marketplaces/{{marketplace_id}}/cards";
        /// <summary>
        /// marketplace_id = id do market place
        /// </summary>
        /// <value></value>
        public const string AssociateBankAccount = "v1/marketplaces/{{marketplace_id}}/bank_accounts";
        /// <summary>
        /// marketplace_id = id do market place
        /// card_id = id do cartão
        /// </summary>
        /// <value></value>
        public const string DeleteCredidCard = "/v1/marketplaces/{{marketplace_id}}/cards/{{card_id}}";
        /// <summary>
        /// marketplace_id = id do market place
        /// buyer_id = id do comprador
        /// </summary>
        /// <value></value>
        public const string GetBuyerById = "/v1/marketplaces/{{marketplace_id}}/buyers/{{buyer_id}}";
        /// <summary>
        /// marketplace_id = id do market place
        /// card_id = id do cartão
        /// </summary>
        public const string GetCreditCardById = "/v1/marketplaces/{{marketplace_id}}/cards/{{card_id}}";
        /// <summary>
        /// marketplace_id = id do market place
        /// transaction_id = id da transação
        /// </summary>
        /// <value></value>
        public const string ReverseTransaction = "/v1/marketplaces/{{marketplace_id}}/transactions/{{transaction_id}}/void";
        /// <summary>
        /// marketplace_id = id do market place
        /// </summary>
        /// <value></value>
        public const string CreateTransaction = "/v1/marketplaces/{{marketplace_id}}/transactions";
        /// <summary>
        /// marketplace_id = id do market place
        /// id = id do vendedor
        /// </summary>
        /// <value></value>
        public const string DeleteSeller = "/v1/marketplaces/{{marketplace_id}}/sellers/{{id}}";
        /// <summary>
        /// marketplace_id = id do market place
        /// id = id do vendedor
        /// </summary>
        /// <value></value>
        public const string GetSellerById = "/v1/marketplaces/{{marketplace_id}}/sellers/{{id}}";
        /// <summary>
        /// marketplace_id = id do market place
        /// </summary>
        /// <value></value>
        public const string RegisterSellerIndividual = "/v1/marketplaces/{{marketplace_id}}/sellers/individuals";
        /// <summary>
        /// marketplace_id = id do market place
        /// id = id do vendedor
        /// </summary>
        /// <value></value>
        public const string UpdateSellerIndividual = "/v1/marketplaces/{{marketplace_id}}/sellers/individuals/{{id}}";
        /// <summary>
        /// marketplace_id = id do market place
        /// </summary>
        /// <value></value>
        public const string RegisterSellerBusiness = "/v1/marketplaces/{{marketplace_id}}/sellers/businesses";
        /// <summary>
        /// marketplace_id = id do market place
        /// id = id do vendedor
        /// </summary>
        /// <value></value>
        public const string UpdateSellerBusiness = "/v1/marketplaces/{{marketplace_id}}/sellers/businesses/{{id}}";
        /// <summary>
        /// marketplace_id = id do market place
        /// </summary>
        /// <value></value>
        public const string ListSellers = "/v1/marketplaces/{{marketplace_id}}/sellers";
        /// <summary>
        /// marketplace_id = id do market place
        /// buyer_id = id do comprador
        /// </summary>
        public const string DeleteBuyer = "/v1/marketplaces/{{marketplace_id}}/buyers/{{buyer_id}}";
        /// <summary>
        /// LISTAR CATEGORIAS DE VENDEDOR  INDIVIDUAL | EMPRESA 
        /// </summary>
        public const string ListMerchantCategoryCode = "/v1/merchant_category_codes";

        /// <summary>
        /// marketplace_id = id do market place
        /// </summary>
        /// <value></value>
        public const string ListCard = "/v1/marketplaces/{{marketplace_id}}/cards";
        /// <summary>
        /// marketplace_id = id do market place
        /// boleto_id = id do boleto
        /// </summary>
        /// <value></value>
        public const string GetBankSlip = "/v1/marketplaces/{{marketplace_id}}/boletos/{{boleto_id}}";
        /// <summary>
        /// marketplace_id = id do market place
        /// cpfOrCnpj = cpf ou cnpj do comprador
        /// </summary>
        /// <value></value>
        public const string GetBuyerByCpfOrCnpj = "/v1/marketplaces/{{marketplace_id}}/buyers/search?taxpayer_id={{cpfOrCnpj}}";
        /// <summary>
        /// marketplace_id = id do market place
        /// webhook_id = id do webhook
        ///  </summary>
        /// <value></value>
        public const string WebHookById = "/v1/marketplaces/{{marketplace_id}}/webhooks/{{webhook_id}}";

        /// <summary>
        /// marketplace_id = id do market place
        /// </summary>
        /// <value></value>
        public const string RegisterWebHook = "/v1/marketplaces/{{marketplace_id}}/webhooks";
        /// <summary>
        /// marketplace_id = id do market place
        /// </summary>
        /// <value></value>
        public const string GetBankAccounts = "/v1/marketplaces/{{marketplace_id}}/bank_accounts";
        /// <summary>
        /// marketplace_id = id do market place
        /// bank_account_id = id dos dados bancários 
        /// </summary>
        /// <value></value>
        public const string GetBankById = "/v1/marketplaces/{{marketplace_id}}/bank_accounts/{{bank_account_id}}";
        /// <summary>
        /// marketplace_id = id do market place
        /// seller_id = id do vendedor
        /// </summary>
        /// <value></value>
        public const string GetBankBySellerId = "/v1/marketplaces/{{marketplace_id}}/sellers/{{seller_id}}/bank_accounts";
        /// <summary>
        /// marketplace_id = id do market place
        /// seller_id = id do vendedor
        /// </summary>
        public const string RegisterDocuments = "/v1/marketplaces/{{marketplace_id}}/sellers/{{seller_id}}/documents";
        /// <summary>
        /// marketplace_id = id do market place
        /// </summary>
        /// <value></value>
        public const string GetSellerByCpfOrCnpj = "/v1/marketplaces/{{marketplace_id}}/sellers/search";
        /// <summary>
        /// marketplace_id = id do market place
        /// seller_id = id do seller
        /// </summary>
        /// <value></value>
        public const string ChangeAutomaticTransfer = "/v1/marketplaces/{{marketplace_id}}/sellers/{{seller_id}}/receiving_policy";
        /// <summary>
        /// marketplace_id = id do market place
        /// owner = id do vendedor que está realizando a transferencia xx
        /// receiver = id do recebedor da transferencia 
        /// </summary>
        /// <value></value>
        public const string RequestInternalTransfer = "/v2/marketplaces/{{marketplace_id}}/transfers/{{owner}}/to/{{receiver}}";
        /// <summary>
        /// marketplace_id = id do market place
        /// bank_account_id = id da conta bancária que ira receber a transferencia 
        /// </summary>
        /// <value></value>
        public const string RequestTransfer = "/v1/marketplaces/{{marketplace_id}}/bank_accounts/{{bank_account_id}}/transfers";
    }
}
using PayPalMvc.Enums;
using System.Collections.Generic;
using System.Globalization;
namespace PayPalMvc
{
    /// <summary>
    /// Represents a transaction registration that is sent to PayPal. 
    /// This should be serialized using the HttpPostSerializer.
    /// </summary>
    public class SetExpressCheckoutRequest : CommonRequest
    {
        readonly PaymentAction paymentAction;
        
        readonly string currencyCode;
        readonly decimal totalAmount;
        readonly decimal deliveryAmount;
        readonly string paymentDescription;
        readonly string trackingReference;

        readonly List<ExpressCheckoutItem> items;

        readonly string serverURL;
        readonly string email;

        readonly string fullName;
        readonly string firstLine;
        readonly string secondLine;
        readonly string city;
        readonly string state;
        readonly string postcode;
        readonly string countrycode;

        // See ITransactionRegister for parameter descriptions
        public SetExpressCheckoutRequest(string currencyCode, decimal totalAmount, decimal deliveryAmount, string paymentDescription, string trackingReference, string serverURL, string fullName, string firstLine, string city, string postcode, string countrycode, List<ExpressCheckoutItem> purchaseItems = null, string userEmail = null, string state = null, string secondLine = null)
        {
            base.method = RequestType.SetExpressCheckout;
            this.paymentAction = PaymentAction.Sale;

            this.currencyCode = currencyCode;
            this.totalAmount = totalAmount;
            this.deliveryAmount = deliveryAmount;
            this.paymentDescription = paymentDescription;
            this.trackingReference = trackingReference;

            this.serverURL = serverURL;
            this.items = purchaseItems;
            this.email = userEmail;

            this.fullName = fullName;
            this.firstLine = firstLine;
            this.secondLine = secondLine;
            this.city = city;
            this.state = state;
            this.postcode = postcode;
            this.countrycode = countrycode;
        }

        public string PAYMENTREQUEST_0_PAYMENTACTION
        {
            get { return paymentAction.ToString(); }
        }

        public string PAYMENTREQUEST_0_CURRENCYCODE
        {
            get { return currencyCode; }
        }
        public string PAYMENTREQUEST_0_ITEMAMT
        {
            get { return totalAmount.ToString(new CultureInfo("en-US", false)); }
        }
        public string PAYMENTREQUEST_0_AMT
        {
            get { return (totalAmount+deliveryAmount).ToString(new CultureInfo("en-US", false)); }
        }
        public string PAYMENTREQUEST_0_SHIPPINGAMT
        {
            get { return deliveryAmount.ToString(new CultureInfo("en-US", false)); }
        }
        public string PAYMENTREQUEST_0_DESC
        {
            get { return paymentDescription; }
        }

        public string PAYMENTREQUEST_0_INVNUM
        {
            get { return trackingReference; }
        }

        [Optional]
        public string EMAIL
        {
            get { return email; }
        }

        public string RETURNURL
        {
            get { return serverURL + Configuration.Current.ReturnAction; }
        }

        public string CANCELURL
        {
            get { return serverURL + Configuration.Current.CancelAction; }
        }
        public int REQCONFIRMSHIPPING
        {
            get { return 0; }
        }
        public int NOSHIPPING
        {
            get { return 2; }
        }
        public int ADDROVERRIDE
        {
            get { return 1; }
        }

        public string PAYMENTREQUEST_0_SHIPTONAME
        {
            get { return fullName; }
        }
        public string PAYMENTREQUEST_0_SHIPTOSTREET
        {
            get { return firstLine; }
        }
        public string PAYMENTREQUEST_0_SHIPTOSTREET2
        {
            get { return secondLine; }
        }
        public string PAYMENTREQUEST_0_SHIPTOCITY
        {
            get { return city; }
        }
        public string PAYMENTREQUEST_0_SHIPTOSTATE
        {
            get { return state; }
        }
        public string PAYMENTREQUEST_0_SHIPTOZIP
        {
            get { return postcode; }
        }
        public string PAYMENTREQUEST_0_SHIPTOCOUNTRYCODE
        {
            get { return countrycode; }
        }

        //Optional List of Items in this purchase
        [Optional]
        public List<ExpressCheckoutItem> Items
        {
            get { return this.items; }
        }
    }
}
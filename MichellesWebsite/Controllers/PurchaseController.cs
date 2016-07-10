using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MichellesWebsite;
using MichellesWebsite.Models;
using PayPalMvc;
using PayPalMvc.Enums;
using Microsoft.AspNet.Identity;

namespace SampleMVC3WebApplication.Controllers
{
    public class PurchaseController : Controller
    {
        private static TransactionService transactionService = new TransactionService();
        private ApplicationDbContext db = new ApplicationDbContext();
        #region Set Express Checkout and Get Checkout Details

        public ActionResult PayPalExpressCheckout(string Addresses)
        {
            if (!String.IsNullOrEmpty(Addresses))
            {
                int addressId = int.Parse(Addresses);
                if (db.Addresses.Where(x => x.id == addressId).Count() == 1)
                {
                    string userId = User.Identity.GetUserId();
                    if (db.Addresses.Single(x => x.id == addressId).userId == userId)
                    {
                        WebUILogging.LogMessage("Express Checkout Initiated");
                        // SetExpressCheckout
                        ApplicationCart cart = (ApplicationCart)Session["Cart"];
                        cart.Address = addressId;
                        Guid saleId = Guid.NewGuid();
                        cart.SaleId = saleId.ToString();
                        Session["Cart"] = cart;

                        // Initiate sale in database
                        
                        db.SaleModels.Add(new SaleModel
                        {
                            SaleId = saleId,
                            AddressId = cart.Address,
                            Amount = cart.TotalPrice,
                            CustomerId = User.Identity.GetUserId(),
                            ts = DateTime.UtcNow,
                            Products = cart.Items.Select(x => new SaleProductModel
                            {
                                PriceId = db.ProductPrices.Where(y => y.productID == x.ProductId).Single(y => y.dateTo == null).ID,
                                ProductId = x.ProductId,
                                Quantity = x.Quantity
                            }).ToList(),
                            status = Status.Ordering
                        });
                        db.SaveChanges();
                        

                        string serverURL = HttpContext.Request.Url.GetLeftPart(UriPartial.Authority) + VirtualPathUtility.ToAbsolute("~/");

                        SetExpressCheckoutResponse transactionResponse = transactionService.SendPayPalSetExpressCheckoutRequest(cart, serverURL);
                        // If Success redirect to PayPal for user to make payment
                        if (transactionResponse == null || transactionResponse.ResponseStatus != PayPalMvc.Enums.ResponseType.Success)
                        {
                            SetUserNotification("Sorry there was a problem with initiating a PayPal transaction. Please try again and contact an Administrator if this still doesn't work.");
                            string errorMessage = (transactionResponse == null) ? "Null Transaction Response" : transactionResponse.ErrorToString;
                            WebUILogging.LogMessage("Error initiating PayPal SetExpressCheckout transaction. Error: " + errorMessage);
                            return RedirectToAction("Error", "Purchase");
                        }
                        return Redirect(string.Format(PayPalMvc.Configuration.Current.PayPalRedirectUrl, transactionResponse.TOKEN));
                    }
                }
            }

            TempData["ErrorMessage"] = "You must configure an address first in your settings";
            return RedirectToAction("Error");
        }

        public ActionResult PayPalExpressCheckoutAuthorisedSuccess(string token, string PayerID) // Note "PayerID" is returned with capitalisation as written
        {
            // PayPal redirects back to here
            WebUILogging.LogMessage("Express Checkout Authorised");
            // GetExpressCheckoutDetails
            TempData["token"] = token;
            TempData["payerId"] = PayerID;
            ApplicationCart cart = (ApplicationCart)Session["Cart"];
            GetExpressCheckoutDetailsResponse transactionResponse = transactionService.SendPayPalGetExpressCheckoutDetailsRequest(token, cart);
            if (transactionResponse == null || transactionResponse.ResponseStatus != PayPalMvc.Enums.ResponseType.Success)
            {
                SetUserNotification("Sorry there was a problem with initiating a PayPal transaction. Please try again and contact an Administrator if this still doesn't work.");
                string errorMessage = (transactionResponse == null) ? "Null Transaction Response" : transactionResponse.ErrorToString;
                WebUILogging.LogMessage("Error initiating PayPal GetExpressCheckoutDetails transaction. Error: " + errorMessage);
                return RedirectToAction("Error", "Purchase");
            }
            return RedirectToAction("ConfirmPayPalPayment");
        }

        #endregion

        #region Confirm Payment

        public ActionResult ConfirmPayPalPayment()
        {
            WebUILogging.LogMessage("Express Checkout Confirmation");
            ApplicationCart cart = (ApplicationCart)Session["Cart"];
            return View(cart);
        }

        [HttpPost]
        public ActionResult ConfirmPayPalPayment(bool confirmed = true)
        {
            WebUILogging.LogMessage("Express Checkout Confirmed");
            ApplicationCart cart = (ApplicationCart)Session["Cart"];
            SaleModel sale = db.SaleModels.Single(x => x.SaleId.ToString() == cart.SaleId);
            sale.status = Status.Ordered;
            db.SaveChanges();
            Session["Cart"] = cart;
            // DoExpressCheckoutPayment
            string token = TempData["token"].ToString();
            string payerId = TempData["payerId"].ToString();
            DoExpressCheckoutPaymentResponse transactionResponse = transactionService.SendPayPalDoExpressCheckoutPaymentRequest(cart, token, payerId);

            if (transactionResponse == null || transactionResponse.ResponseStatus != PayPalMvc.Enums.ResponseType.Success)
            {
                if (transactionResponse != null && transactionResponse.L_ERRORCODE0 == "10486")
                {
                    // Redirect user back to PayPal in case of Error 10486 (bad funding method)
                    // https://www.x.com/developers/paypal/documentation-tools/how-to-guides/how-to-recover-funding-failure-error-code-10486-doexpresscheckout
                    WebUILogging.LogMessage("Redirecting User back to PayPal due to 10486 error (bad funding method - typically an invalid or maxed out credit card)");
                    return Redirect(string.Format(PayPalMvc.Configuration.Current.PayPalRedirectUrl, token));
                }
                SetUserNotification("Sorry there was a problem with taking the PayPal payment, so no money has been transferred. Please try again and contact an Administrator if this still doesn't work.");
                string errorMessage = (transactionResponse == null) ? "Null Transaction Response" : transactionResponse.ErrorToString;
                WebUILogging.LogMessage("Error initiating PayPal DoExpressCheckoutPayment transaction. Error: " + errorMessage);
                sale.status = Status.Failed;
                db.SaveChanges();
                return RedirectToAction("Error", "Purchase");
            }

            if (transactionResponse.PaymentStatus == PaymentStatus.Completed)
            {
                List<int> products = cart.Items.Select(x => x.ProductId).ToList();
                List<ProductModel> productModels = db.ProductModels.Where(x => products.Contains(x.ID)).ToList();
                foreach(ProductModel pm in productModels)
                {
                    pm.stock = pm.stock - (int)cart.Items.Where(x => x.ProductId == pm.ID).Sum(x => x.Quantity);
                };
                sale.status = Status.Ordered;
                db.SaveChanges();
                return RedirectToAction("PostPaymentSuccess");
            }
            else
            {
                // Something went wrong or the payment isn't complete
                sale.status = Status.Failed;
                db.SaveChanges();
                WebUILogging.LogMessage("Error taking PayPal payment. Error: " + transactionResponse.ErrorToString + " - Payment Error: " + transactionResponse.PaymentErrorToString);
                TempData["TransactionResult"] = transactionResponse.PAYMENTREQUEST_0_LONGMESSAGE;
                return RedirectToAction("PostPaymentFailure");
            }
        }

        #endregion

        #region Post Payment and Cancellation

        public ActionResult PostPaymentSuccess()
        {
            WebUILogging.LogMessage("Post Payment Result: Success");
            ApplicationCart cart = (ApplicationCart)Session["Cart"];
            ViewBag.TrackingReference = cart.Id;
            ViewBag.Description = cart.PurchaseDescription;
            ViewBag.TotalCost = cart.TotalPrice;
            ViewBag.Currency = cart.Currency;
            
            return View();
        }

        public ActionResult PostPaymentFailure()
        {
            WebUILogging.LogMessage("Post Payment Result: Failure");
            ViewBag.ErrorMessage = TempData["TransactionResult"];
            return View();
        }

        public ActionResult CancelPayPalTransaction()
        {
            ApplicationCart cart = (ApplicationCart)Session["Cart"];
            SaleModel sale = db.SaleModels.Single(x => x.SaleId.ToString() == cart.SaleId);
            sale.status = Status.Cancelled;
            db.SaveChanges();
            return View();
        }

        #endregion

        #region Transaction Error

        private void SetUserNotification(string notification)
        {
            TempData["ErrorMessage"] = notification;
        }

        public ActionResult Error()
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            return View();
        }

        #endregion

    }
}

using System;
using System.Collections.Specialized;

namespace Memories.Models
{
    public class Merchant
    {
        public Boolean Debug { get; set; }
        public Boolean UseSsl { get; set; }
        public Boolean IgnoreSslErrors { get; set; }

        public String GatewayHost { get; set; }
        public String Version { get; set; }
        public String GatewayUrl { get; set; }

        public Boolean UseProxy { get; set; }
        public String ProxyHost { get; set; }
        public String ProxyUser { get; set; }
        public String ProxyPassword { get; set; }
        public String ProxyDomain { get; set; }

        public String MerchantId { get; set; }
        public String Password { get; set; }
        public String Username { get; set; }

        public Merchant()
        {
            //NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;


            this.Debug = false;
            this.UseSsl = true;
            this.IgnoreSslErrors = false;

            this.GatewayHost = "https://ap-gateway.mastercard.com";
            this.Version = "66";

            this.UseProxy = false;
            this.ProxyHost = "";
            this.ProxyUser = "";
            this.ProxyPassword = "";
            this.ProxyDomain = "";

            this.MerchantId = "TESTBEINTRACK"; 
            this.Username = "merchant.TESTBEINTRACK";
            this.Password = "b2a492d0275ca4e00bc26a9e45f2865c";
        }
    }
}

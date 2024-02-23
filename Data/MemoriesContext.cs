using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Memories.Models;

#nullable disable

namespace Memories.Data
{
    public partial class MemoriesContext : DbContext
    {
        public MemoriesContext()
        {
        }

        public MemoriesContext(DbContextOptions<MemoriesContext> options)
            : base(options)
        {
        }
        public virtual DbSet<PaymentModel> Payments { get; set; }
        public virtual DbSet<Gateway> Gateways { get; set; }
        public virtual DbSet<LanguageModel> Languages { get; set; }
        public virtual DbSet<PaymentStatus> PaymentStatus { get; set; }

        //public virtual DbSet<PaymentGetway> PaymentGetways { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gateway>().HasData(new Gateway { GateWayId = 1, Title = "MasterCard-NBK", TestURL = "https://nbkpayment.gateway.mastercard.com/api/rest", UserName = "merchant.900185001", Password = "937cd5c82ab0949b7afad77cd8958122", MerchantId = "900185001", version = "65" });
            modelBuilder.Entity<Gateway>().HasData(new Gateway { GateWayId = 2, Title = "KNET-CBK", TestURL = "https://pg.cbk.com/ePay/pg/epay?_v=", UserName = "27387462", Password = "-B76ERtLowiNGtH8fYTTU8yqkeAii9O99bP8lhD6xh81", ENCRP_KEY = "FbwZvfx-xudBGOszQa-nkarUVel9jDSqm7MKZKoJ9KyybsEXb9hfiP7gaJ3--BSL78VK-k2rd6tTeISdpCnRu9gSlspqr0jU90C1h-k3yXs1", AuthUrl = "https://pg.cbk.com/ePay/api/cbk/online/pg/merchant/Authenticate", MerchantPaymentRef = "test", MerchantPaymentLang = "en", MerchReturnUrl = "https://memories.beintrackpay.com/Response" });
            modelBuilder.Entity<Gateway>().HasData(new Gateway { GateWayId = 3, Title = "MasterCard-CBK", TestURL = "https://ap-gateway.mastercard.com/api/rest", UserName = "merchant.BEINTRACK", Password = "12aa799d8ad04626fb7f739550674868", MerchantId = "BEINTRACK", version = "65" });
            modelBuilder.Entity<PaymentStatus>().HasData(new PaymentStatus { PaymentStatusId = 1, Title = "Waiting" });
            modelBuilder.Entity<PaymentStatus>().HasData(new PaymentStatus { PaymentStatusId = 2, Title = "Success" });
            modelBuilder.Entity<PaymentStatus>().HasData(new PaymentStatus { PaymentStatusId = 3, Title = "Failed" });
            modelBuilder.Entity<PaymentStatus>().HasData(new PaymentStatus { PaymentStatusId = 4, Title = "Not Paied" });
            //modelBuilder.Entity<Gateway>().HasData(new Gateway { GateWayId = 3,Title="Knet" });
            //modelBuilder.Entity<LanguageModel>().HasData(new LanguageModel { LanguageModelId = 1, Title = "English" });
            //modelBuilder.Entity<LanguageModel>().HasData(new LanguageModel { LanguageModelId = 2, Title = "Arabic" });
            //modelBuilder.Entity<PaymentModel>().HasIndex(u => u.OrderNumber).IsUnique();
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

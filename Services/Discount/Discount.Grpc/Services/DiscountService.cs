using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly DiscountDbContext dbContext;

        public DiscountService(DiscountDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x=>x.ProductName == request.ProductName);
            if (coupon == null)
            {
                coupon = new Coupon { ProductName = "No Discount", Description = "Wrong Coupon Details for Discount", Amount = 0 };
                var couponModel1 = coupon.Adapt<CouponModel>();
                return couponModel1;
            }
            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }

        public override async Task<CouponModel> CreatetDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.Coupons.FindAsync(request.Coupon.Id);
            if(coupon != null)
            {
                coupon =  new Coupon { ProductName = "Coupon Applied", Description = "Coupon Already Applied", Amount = 0 };
                var couponModel1 = coupon.Adapt<CouponModel>();
                return couponModel1;
            }
            var couponModel = coupon.Adapt<CouponModel>();
            await dbContext.Coupons.AddAsync(coupon);
            return couponModel;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.Coupons.FindAsync(request.Coupon.Id);
            if (coupon == null)
            {
                coupon = new Coupon { ProductName = "Coupon Not found", Description = "Coupon Not found in db", Amount = 0 };
                var couponModel1 = coupon.Adapt<CouponModel>();
                return couponModel1;
            }
            coupon.Amount = request.Coupon.Amount;
            coupon.Description = request.Coupon.Description;
            coupon.ProductName = request.Coupon.ProductName;
            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.Coupons.FindAsync(request.ProdutName);
            if (coupon == null)
            {
                var response1= new DeleteDiscountResponse { Success = false };
                return response1;
            }
            dbContext.Coupons.Remove(coupon);
            var response = new DeleteDiscountResponse { Success = true };
            return response;
        }
    }
}

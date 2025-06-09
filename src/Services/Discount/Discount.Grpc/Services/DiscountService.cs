using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
	public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger) : Discount.DiscountBase
	{
		//private readonly ILogger<DiscountService> _logger;
		//public DiscountService(ILogger<DiscountService> logger)
		//{
		//	_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		//}
		public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
		{
			var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
			if (coupon == null)
			{
				coupon = new Models.Coupon { Amount = 0, Description = "No Discount", ProductName = "No Discount" };	
			}

			var couponModel = coupon.Adapt<CouponModel>();

			return couponModel;

		}
		public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
		{
			var coupon = request.Coupon.Adapt<Coupon>();
			if (coupon == null)
			{
				throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid coupon"));
			}

			coupon.Id = 0;

			dbContext.Coupons.Add(coupon);
			await dbContext.SaveChangesAsync();

			logger.LogInformation("Discount {ProductName} is successfully created", coupon.ProductName);

			var couponModel = coupon.Adapt<CouponModel>();
			return couponModel;
			
		}
		public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
		{
			var coupon = request.Coupon.Adapt<Coupon>();
			if (coupon == null)
			{
				throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid coupon"));
			}

			dbContext.Coupons.Update(coupon);
			await dbContext.SaveChangesAsync();

			logger.LogInformation("Discount {ProductName} is successfully updated", coupon.ProductName);

			var couponModel = coupon.Adapt<CouponModel>();
			return couponModel;
		}
		public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
		{
			var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
			if (coupon == null)
			{
				logger.LogInformation("Discount {ProductName} is not found", coupon.ProductName);
				return new DeleteDiscountResponse
				{
					Success = false
				};
			}			
				
			dbContext.Coupons.Remove(coupon);
			await dbContext.SaveChangesAsync();

			logger.LogInformation("Discount {ProductName} is successfully deleted", coupon.ProductName);

			return new DeleteDiscountResponse
			{
				Success = true
			};

		}
	}

}

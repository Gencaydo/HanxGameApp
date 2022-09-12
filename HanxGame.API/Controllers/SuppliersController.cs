using AutoMapper;
using HanxGame.Core.DTOs;
using HanxGame.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace HanxGame.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SuppliersController : CustomBaseController
    {
        private readonly IMapper mapper;
        private readonly IApplicationReadDbService applicationReadDbService;
        private readonly IApplicationWriteDbService applicationWriteDbService;

        public SuppliersController(IApplicationReadDbService applicationReadDbService, IApplicationWriteDbService applicationWriteDbService, IMapper mapper)
        {
            this.mapper = mapper;
            this.applicationReadDbService = applicationReadDbService;
            this.applicationWriteDbService = applicationWriteDbService;
        }

        [HttpPost]
        public async Task<IActionResult> GetAllSuppliers(SupplierDto supplierDto)
        {
            try
            {
                string query = "SELECT a.ID,a.NAME,a.EMAIL,a.GAMEID,b.NAME as GAMENAME,a.BUYINGPRICE,a.SELLINGPRICE,a.CREATEDATE,a.UPDATEDATE FROM SUPPLIERS a,GAMES b WHERE a.GAMEID = b.ID AND a.STATUSID IN (1,3)";

                var result = await applicationReadDbService.QueryAsync<SupplierDto>(query);

                if (result == null)
                {
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "Select Query Result Not Found any Data"));
                }

                var suppliersResultDto = mapper.Map<List<SupplierDto>>(result);
                return CreateActionResult(CustomResponseDto<List<SupplierDto>>.Success(200, suppliersResultDto));
            }
            catch (Exception ex)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddSupplier(SupplierDto supplierDto)
        {
            try
            {
                string selectquery = "SELECT * FROM SUPPLIERS WHERE NAME = @NAME AND GAMEID = @GAMEID";

                var exist = await applicationReadDbService.QueryAsync<SupplierDto>(selectquery, new SupplierDto { Name = supplierDto.Name, GameId = supplierDto.GameId });


                if (exist.Where(x => x.Name == supplierDto.Name).Count() > 0) return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(302, "Same Game Exist for Supplier, Please Check!"));


                string insertquery = "INSERT INTO SUPPLIERS VALUES (@NAME,@Email,NULL,NULL,NULL,NULL,NULL,NULL,NULL,0,NULL,0,@BuyingPrice,@SellingPrice,@GAMEID,1,@CREATEUSERID,CONVERT(VARCHAR, GETDATE(), 120),NULL,CONVERT(VARCHAR, GETDATE(), 120))";

                var result = await applicationWriteDbService.ExecuteAsync(insertquery, new SupplierDto { Name = supplierDto.Name, 
                                                                                                         Email = supplierDto.Email,
                                                                                                         BuyingPrice = supplierDto.BuyingPrice,
                                                                                                         SellingPrice = supplierDto.SellingPrice,
                                                                                                         GameId = supplierDto.GameId,
                                                                                                         CreateUserId = supplierDto.CreateUserId });

                if (result == 0)
                {
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "Insert Query Executed Error"));
                }

                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(200));
            }
            catch (Exception ex)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, ex.Message));
            }
        }
    }
}

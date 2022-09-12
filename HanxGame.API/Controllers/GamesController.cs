using AutoMapper;
using HanxGame.Core.DTOs;
using HanxGame.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HanxGame.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GamesController : CustomBaseController
    {
        private readonly IMapper mapper;
        private readonly IApplicationReadDbService applicationReadDbService;
        private readonly IApplicationWriteDbService applicationWriteDbService;

        public GamesController(IApplicationReadDbService applicationReadDbService, IApplicationWriteDbService applicationWriteDbService, IMapper mapper)
        {
            this.mapper = mapper;
            this.applicationReadDbService = applicationReadDbService;
            this.applicationWriteDbService = applicationWriteDbService;
        }
        [HttpPost]
        public async Task<IActionResult> GetAllGames(GameDto gameDto)
        {
            try
            {
                string query = "SELECT * FROM GAMES WHERE STATUSID IN (@STATUSID)";

                var result = await applicationReadDbService.QueryAsync<GameDto>(query,new GameDto { StatusId = 1 });

                if (result == null)
                {
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "Select Query Result Not Found any Data"));
                }

                var result1 = await applicationReadDbService.QueryAsync<GameDto>(query, new GameDto { StatusId = 3 });

                if (result1 == null)
                {
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "Select Query Result Not Found any Data"));
                }

                var gamesResultDto = mapper.Map<List<GameDto>>(result).Concat(result1).ToList();
                return CreateActionResult(CustomResponseDto<List<GameDto>>.Success(200, gamesResultDto));
            }
            catch (Exception ex)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, ex.Message));
            }
           

        }

        [HttpPost]
        public async Task<IActionResult> AddGame(GameDto gameDto)
        {
            try
            {
                string selectquery = "SELECT * FROM GAMES WHERE NAME = @NAME";

                var exist = await applicationReadDbService.QueryAsync<GameDto>(selectquery, new GameDto { Name = gameDto.Name });


                if (exist.Where(x => x.Name == gameDto.Name).Count() > 0) return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(302, "Same Game Name Found, Please Check!"));


                string insertquery = "INSERT INTO GAMES VALUES (@NAME,0,@DefaultBuyingPrice,@DefaultSellingPrice,NULL,@KEYTYPEID,1,@CREATEUSERID,CONVERT(VARCHAR, GETDATE(), 120),NULL,CONVERT(VARCHAR, GETDATE(), 120))";

                var result = await applicationWriteDbService.ExecuteAsync(insertquery,new GameDto { Name = gameDto.Name,
                                                                                                    DefaultBuyingPrice = gameDto.DefaultBuyingPrice,
                                                                                                    DefaultSellingPrice = gameDto.DefaultSellingPrice,
                                                                                                    KeyTypeId = gameDto.KeyTypeId,
                                                                                                    CreateUserId = gameDto.CreateUserId});

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

        [HttpPost]
        public async Task<IActionResult> UpdateGame(GameDto gameDto)
        {
            bool pricechanged = false;
            bool namechanged = false;
            try
            {
                string selectquery = "SELECT * FROM GAMES WHERE ID = @Id";

                var exist = await applicationReadDbService.QueryAsync<GameDto>(selectquery,new GameDto { Id = gameDto.Id });

                if (exist.Where(x => x.DefaultBuyingPrice != gameDto.DefaultBuyingPrice).Count() > 0 || exist.Where(x => x.DefaultSellingPrice != gameDto.DefaultSellingPrice).Count() > 0) pricechanged = true;

                string selectquery1 = "SELECT * FROM GAMES WHERE NAME = @NAME";

                var exist1 = await applicationReadDbService.QueryAsync<GameDto>(selectquery1, new GameDto { Name = gameDto.Name });

                if (exist1.Count() == 0) namechanged = true;

                if (!pricechanged) return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(302, "Dont Change Prices, Please Check!"));
                if (!namechanged) return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(302, "Same Name Found, Please Check!"));


                string query = "UPDATE GAMES SET STATUSID = 3,NAME=@NAME,DEFAULTBUYINGPRICE=@DefaultBuyingPrice,DEFAULTSELLINGPRICE=@DefaultSellingPrice,UPDATEUSERID=@UPDATEUSERID,UPDATEDATE = GETDATE() WHERE ID=@ID";

                var result = await applicationReadDbService.QueryAsync<GameDto>(query,new GameDto { Name= gameDto.Name,
                                                                                                    DefaultBuyingPrice = gameDto.DefaultBuyingPrice,
                                                                                                    DefaultSellingPrice = gameDto.DefaultSellingPrice,
                                                                                                    UpdateUserId = gameDto.UpdateUserId,
                                                                                                    Id = gameDto.Id});

                if (result == null)
                {
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "Remove Query Executed Error"));
                }

                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(200));
            }
            catch (Exception ex)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveGame(GameDto gameDto)
        {
            try
            {
                string query = "UPDATE GAMES SET STATUSID = 4,UPDATEUSERID=@UPDATEUSERID,UPDATEDATE = GETDATE() WHERE ID=@ID";

                var result = await applicationReadDbService.QueryAsync<GameDto>(query,new GameDto { UpdateUserId = gameDto.UpdateUserId,
                                                                                                    Id = gameDto.Id});

                if (result == null)
                {
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "Remove Query Executed Error"));
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

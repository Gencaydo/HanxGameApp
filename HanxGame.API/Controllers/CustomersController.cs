using AutoMapper;
using HanxGame.Core.DTOs;
using HanxGame.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HanxGame.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomersController : CustomBaseController
    {
        private readonly IMapper mapper;
        private readonly IApplicationReadDbService applicationReadDbService;
        private readonly IApplicationWriteDbService applicationWriteDbService;

        public CustomersController(IApplicationReadDbService applicationReadDbService, IApplicationWriteDbService applicationWriteDbService, IMapper mapper)
        {
            this.mapper = mapper;
            this.applicationReadDbService = applicationReadDbService;
            this.applicationWriteDbService = applicationWriteDbService;
        }
        [HttpPost]
        public async Task<IActionResult> GetAllCustomers(CustomerDto customerDto)
        {
            try
            {
                string query = "SELECT * FROM CUSTOMERS WHERE STATUSID IN (1,3)";

                var result = await applicationReadDbService.QueryAsync<CustomerDto>(query);

                if (result == null)
                {
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "Select Query Result Not Found any Data"));
                }

                var customersResultDto = mapper.Map<List<CustomerDto>>(result);
                return CreateActionResult(CustomResponseDto<List<CustomerDto>>.Success(200, customersResultDto));
            }
            catch (Exception ex)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer(CustomerDto customerDto)
        {
            try
            {
                string selectquery = "SELECT * FROM CUSTOMERS WHERE NAME = '" + customerDto.Name + "'";

                var exist = await applicationReadDbService.QueryAsync<CustomerDto>(selectquery);

                if (exist.Count > 0)
                {
                    if (exist.Where(x => x.StatusId == 1).Count() > 0) return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(302, "Same Customer Name Found, Please Search Customer!"));
                    else if (exist.Where(x => x.StatusId == 4).Count() > 0) return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(302, "Same Customer Name Found but Status Deleted, Please Contant with IT!"));
                }

                string insertquery = "INSERT INTO CUSTOMERS VALUES ('" + customerDto.Name
                                                      + "','" 
                                                      + customerDto.Email
                                                      + "','"
                                                      + customerDto.Description
                                                      + "','"
                                                      + customerDto.AddressLine1
                                                      + "','"
                                                      + customerDto.AddressLine2
                                                      + "','"
                                                      + customerDto.MobilePhoneNumber
                                                      + "','"
                                                      + customerDto.PhoneNumber
                                                      + "','"
                                                      + customerDto.Town
                                                      + "','"
                                                      + customerDto.State
                                                      + "'," 
                                                      + customerDto.PostCode
                                                      + ",'"
                                                      + customerDto.Country
                                                      + "','"
                                                      + customerDto.IsBillingAddress
                                                      + "','"
                                                      + customerDto.ResponseUserId 
                                                      + "',"
                                                      + "1" //StatusID Set Active
                                                      + ",'"
                                                      + customerDto.CreateUserId
                                                      + "',"
                                                      + "CONVERT(VARCHAR, GETDATE(), 120)"
                                                      + ",NULL"
                                                      + ",CONVERT(VARCHAR, GETDATE(), 120))";

                var result = await applicationWriteDbService.ExecuteAsync(insertquery);

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
        public async Task<IActionResult> UpdateCustomer(CustomerDto customerDto)
        {
            bool ınfochanged = false;
            bool namechanged = false;
            try
            {
                string selectquery = "SELECT * FROM CUSTOMERS WHERE ID = @ID";

                var exist = await applicationReadDbService.QueryAsync<CustomerDto>(selectquery, new CustomerDto { Id = customerDto.Id });

                if (exist.Where(x => x.Email != customerDto.Email).Count() > 0 || exist.Where(x => x.Description != customerDto.Description).Count() > 0) ınfochanged = true;

                string selectquery1 = "SELECT * FROM CUSTOMERS WHERE NAME = @NAME";

                var exist1 = await applicationReadDbService.QueryAsync<CustomerDto>(selectquery1, new CustomerDto { Name = customerDto.Name });

                if (exist1.Count() == 0) namechanged = true;

                if (!ınfochanged) return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(302, "Dont Change Nothing, Please Check!"));
                if (!namechanged) return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(302, "Same Name Found, Please Check!"));

                string updatequery = "UPDATE CUSTOMERS SET STATUSID = 3, NAME=@NAME, EMAIL=@EMAIL, DESCRIPTION=@DESCRIPTION, UPDATEUSERID=@UPDATEUSERID, UPDATEDATE=GETDATE() WHERE ID=@ID";

                var result = await applicationReadDbService.QueryAsync<CustomerDto>(updatequery, new CustomerDto { Id = customerDto.Id,
                                                                                                                   Name = customerDto.Name, 
                                                                                                                   Email = customerDto.Email,
                                                                                                                   Description = customerDto.Description,
                                                                                                                   UpdateUserId = customerDto.UpdateUserId});

                if (result == null)
                {
                    return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, "Update Query Executed Error"));
                }

                return CreateActionResult(CustomResponseDto<NoContentDto>.Success(200));
            }
            catch (Exception ex)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404, ex.Message));
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoveCustomer(CustomerDto CustomerDto)
        {
            try
            {


                var result = await applicationReadDbService.QueryAsync<CustomerDto>("UPDATE CUSTOMERS SET STATUSID = 4, " +
                                                                                    "UPDATEUSERID = '" + CustomerDto.UpdateUserId + "', " +
                                                                                    "UPDATEDATE = GETDATE() WHERE id = " + CustomerDto.Id);

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

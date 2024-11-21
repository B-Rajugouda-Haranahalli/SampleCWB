using CWB.BusinessAquisition.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CWB.BusinessAquisition.Services
{
    public interface IBAService
    {
        Task<CustomerOrderVM> CustomerOrder(CustomerOrderVM customerOrderVM);
        Task<bool> AddSalesOrders(long tenantId, long customerOrderId);
        Task<IEnumerable<CustomerOrderVM>> GetCustomerOrders(long tenantId);
        Task<IEnumerable<POLogVM>>  GetPOLogs(long tenantId, long customerOrderId);
        Task<IEnumerable<SalesOrderVM>> GetSalesOrders(long tenantId, long customerOrderId);
        Task<SalesOrderVM> GetSingleSalesOrder(long tenantId, long salesOrderId);
        Task<IEnumerable<SalesOrderVM>> AllSalesOrders(long tenantId);
        Task<IEnumerable<DeliveryScheduleVM>> GetSchedules(long tenantId, long customerOrderId);
        string HelloWorld();
        Task<SalesOrderVM> SalesOrder(SalesOrderVM salesOrderVm);
        Task<POLogVM> POLog(POLogVM poLogVm);
        Task<POLogVM> SOLog(POLogVM poLogVm);
        Task<DeliveryScheduleVM> DeliverySchedule(DeliveryScheduleVM deliveryScheduleVM);
        Task<OrderStatusVM> OrderStatus(OrderStatusVM customerOrderStatusVM);
        Task<bool> RemoveSalesOrder(long tenantId, long salesOrderId);
        Task<bool> RemoveCustomerOder(long tenantId, long customerOrderId);
        Task<bool> RemoveDeliverySchedule(long tenantId, long scheduleId);
        Task<bool> RemoveOrderStatus(long tenantId, object orderStatusId);

        Task<SOAggregateVM> SOAggregate(SOAggregateVM aggregateVM);
        Task<SOAggregateVM> GetSOAggregate(long tenantId, long customerOrderId);
        Task<IEnumerable<SOAggregateVM>> GetSOAggregates(long tenantId);

        bool CheckPartNo(long partId);
        Task<BAStatusVM> GetBAStatus(long Id);
        //Task<List<SalesOrderVM>> UpdateWoInSo(List<SalesOrderVM> sales);
        /*IEnumerable<CompanyTypeVM> GetCompanyTypes();


Task<IEnumerable<CompaniesVM>> GetCompaniesByTenant(long tenantID);

Task<IEnumerable<CompaniesVM>> GetCompaniesByCompanyNTenant(long companyID, long tenantID);
Task<CompanyVM> Company(CompanyVM companyVM);

Task<bool> DeleteCompany(long companyID, long tenantId);
Task<bool> DeleteDivision(long divisionID, long tenantId);
bool CheckIfCompanyExisit(CheckCompanyVM checkCompanyVM);
bool CheckIfDivisionExisit(CheckDivisionVM checkDivisionVM);
Task<CompanyVM> GetCompany(long companyID,long tenantId);
Task<long> GetCompanyId(string co);*/

    }
}

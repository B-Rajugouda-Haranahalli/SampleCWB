using AutoMapper;
using CWB.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using CWB.BusinessAquisition.Repositories;
using CWB.BusinessAquisition.Infrastructure;
using CWB.BusinessAquisition.ViewModels;
using CWB.BusinessAquisition.Domain;

namespace CWB.BusinessAquisition.Services
{
    public class BAService : IBAService
    {
        enum OrdStatus
        {
            [Description("Not Planned")]
            NOTPlanned = 1,
            [Description("Planned")]
            Planned,
            [Description(" Matl Recd")]
            MatlRecd,
            [Description("WIP")]
            WIP,
            [Description("Complete")]
            Complete,
            [Description("On Hold")]
            OnHold,
            [Description("Deleted")]
            Deleted
        }

        public String GetStatus(int status)
        {
            if (status == (int)OrdStatus.NOTPlanned) return ""+OrdStatus.NOTPlanned;
            else if (status == (int)OrdStatus.Planned) return "" + OrdStatus.Planned;
            else if (status == (int)OrdStatus.MatlRecd) return "" + OrdStatus.MatlRecd;
            else if (status == (int)OrdStatus.WIP) return "" + OrdStatus.WIP;
            else if (status == (int)OrdStatus.Complete) return "" + OrdStatus.Complete;
            else if (status == (int)OrdStatus.OnHold) return "" + OrdStatus.OnHold;
            else  return "" + OrdStatus.Deleted;
        }


        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPOLogRepository _pOLogRepository;
        private readonly ICustomerOrderRepository _customerOrderRepository;
        private readonly ISalesOrderRepository _salesOrderRepository;
        private readonly ISOAggregateRepository _sOAggregateRepository;
        private readonly IDeliveryScheduleRepository _deliveryScheduleRepository;
        private readonly IBAStatusRepository _baStatusRepository;

        public BAService(ILoggerManager logger, IMapper mapper, IUnitOfWork unitOfWork
            ,IPOLogRepository pOLogRepository
            ,ICustomerOrderRepository customerOderRepository
            ,ISalesOrderRepository salesOrderRepository
            ,ISOAggregateRepository sOAggregateRepository
            ,IBAStatusRepository bAStatusRepository
            , IDeliveryScheduleRepository deliveryScheduleRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _pOLogRepository = pOLogRepository;
            _customerOrderRepository = customerOderRepository;
            _salesOrderRepository = salesOrderRepository;
            _sOAggregateRepository = sOAggregateRepository;
            _baStatusRepository = bAStatusRepository;
            _deliveryScheduleRepository = deliveryScheduleRepository;
        }

        public async Task<CustomerOrderVM> CustomerOrder(CustomerOrderVM customerOrderVM)
        {
            var customerOrder = _mapper.Map<CustomerOrder>(customerOrderVM);
            if (customerOrder.Id == 0)
            {
                customerOrder.Plan = 0;
                customerOrder.Matl = 0;
                customerOrder.Hold = false;
                customerOrder.Done = false;
                customerOrder.Status = (int)OrdStatus.NOTPlanned;
                customerOrder.LineNo = "";

                await _customerOrderRepository.AddAsync(customerOrder);
                await _unitOfWork.CommitAsync();
                customerOrderVM.CustomerOrderId = customerOrder.Id;
                await POLog(LogPOEntry(customerOrder));
            }
            else
            {
                customerOrder = await _customerOrderRepository.SingleOrDefaultAsync(x => x.Id == customerOrder.Id);
                if(customerOrder == null) { return customerOrderVM; }
                
                customerOrder.PONumber = customerOrderVM.PONumber;
                customerOrder.PODate = customerOrderVM.PODate;
                customerOrder.DirectEntryDetails = customerOrderVM.DirectEntryDetails;
                customerOrder.Comment = customerOrderVM.Comment;
                customerOrder.OrderType = customerOrderVM.OrderType;

                customerOrder = await _customerOrderRepository.UpdateAsync(customerOrder.Id, customerOrder);
                await _unitOfWork.CommitAsync();
                customerOrderVM.CustomerOrderId = customerOrder.Id;
                await POLog(LogPOEdit(customerOrder));
            }
            return customerOrderVM;
        }

        public async Task<bool> AddSalesOrders(long tenantId,long customerOrderId)
        {
            try
            {
                var salesOrders = GetSalesOrders(tenantId, customerOrderId).Result.ToList();
                var deliverySchedules = _deliveryScheduleRepository.GetRangeAsync(m => m.TenantId == tenantId && m.CustomerOrderId == customerOrderId);
                foreach (var delSchedule in deliverySchedules)
                {
                    bool alreadyAdded = false;
                    foreach(SalesOrderVM ord in salesOrders)
                    {
                        //if(ord.ScheduleId == delSchedule.Id)
                        {
                            alreadyAdded = true;
                            break;
                        }
                    }
                    if(alreadyAdded)
                    {
                        continue;
                    }
                    var salesOrder = _mapper.Map<SalesOrder>(delSchedule);
                    salesOrder.CustomerOrderId = customerOrderId;
                    //salesOrder.ScheduleId = delSchedule.Id;
                    salesOrder.Done = false;
                    salesOrder.Hold = false;
                    salesOrder.Plan = 0;
                    salesOrder.Matl = 0;
                    salesOrder.Status = (int)OrdStatus.NOTPlanned;//Scheduled
                    salesOrder.WorkOrderNo = "";
                    salesOrder.WorkOrderId = 0;
                    salesOrder.SODate = DateTime.Now;
                    salesOrder.SONumber = "SO_" + salesOrder.SODate.Value.ToString("yyyyMMddHHmmss");


                    await _salesOrderRepository.AddAsync(salesOrder);
                    //await POLog(LogSOEntry(salesOrder));
                }
                await _unitOfWork.CommitAsync();
                
                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<DeliveryScheduleVM> DeliverySchedule(DeliveryScheduleVM deliveryScheduleVM)
        {
            var salesOrder = _mapper.Map<SalesOrder>(deliveryScheduleVM);
            if (salesOrder.Id == 0)
            {
                salesOrder.Done = false;
                salesOrder.Hold = false;
                salesOrder.Plan = 0;
                salesOrder.Matl = 0;
                salesOrder.Status = (int)OrdStatus.NOTPlanned;//Scheduled
                salesOrder.WorkOrderNo = "";
                salesOrder.WorkOrderId = 0;
                salesOrder.SODate = DateTime.Now;
                salesOrder.SONumber = "SO_"+salesOrder.SODate.Value.ToString("yyyyMMddHHmmss");
                try
                {
                    await _salesOrderRepository.AddAsync(salesOrder);
                }catch(Exception ex)
                {
                    Exception exa = ex.InnerException;
                    string msg = ex.Message;
                }
            }
            else
            {
                salesOrder = null;
                salesOrder = await _salesOrderRepository.SingleOrDefaultAsync(m=>m.Id == deliveryScheduleVM.ScheduleId);
                if (salesOrder != null)
                {
                    salesOrder.RequiredByDate = deliveryScheduleVM.RequiredByDate;
                    salesOrder.RequiredQuantity = deliveryScheduleVM.RequiredQuantity;
                    salesOrder.Comment = deliveryScheduleVM.Comment;
                    salesOrder = await _salesOrderRepository.UpdateAsync(salesOrder.Id, salesOrder);
                    await POLog(LogSOEdit(salesOrder));
                }
                //await _deliveryScheduleRepository.UpdateAsync(delSchedule.Id, delSchedule);
            }
            try
            {
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                Exception exa = ex.InnerException;
                string msg = ex.Message;
            }
            deliveryScheduleVM.ScheduleId = salesOrder.Id;
            return deliveryScheduleVM;
        }

        public async Task<IEnumerable<CustomerOrderVM>> GetCustomerOrders(long tenantId)
        {
            var custOrders = _customerOrderRepository.GetRangeAsync(d => d.TenantId == tenantId);
           /* var salesOrders = _salesOrderRepository.GetRangeAsync(d => d.TenantId == tenantId);

            foreach (var customerOrder in custOrders)
            {
                foreach (var sos in salesOrders)
                {
                   if(customerOrder.Id == sos.CustomerOrderId)
                    {
                        customerOrder.Plan += sos.RequiredQuantity;
                    }
                }
            }*/
            return _mapper.Map<IEnumerable<CustomerOrderVM>>(custOrders);
        }
        public async Task<IEnumerable<SOAggregateVM>> GetSOAggregates(long tenantId)
        {
            var agregates = _sOAggregateRepository.GetRangeAsync(d => d.TenantId == tenantId);
            return _mapper.Map<IEnumerable<SOAggregateVM>>(agregates);
        }

        public async Task<IEnumerable<POLogVM>> GetPOLogs(long tenantId,long customerOrderId)
        {
            var poLogs = _pOLogRepository.GetAllAsync().Result.ToList(); //d => d.TenantId == tenantId && d.CustomerOrderId == customerOrderId);
            var salesOrders = _salesOrderRepository.GetRangeAsync(d => d.TenantId == tenantId && d.CustomerOrderId == customerOrderId);
            List<POLog> pvLogList = new List<POLog>();
            foreach(POLog polog in poLogs)
            {
                if (polog.CustomerOrderId != customerOrderId)
                    continue;
                pvLogList.Add(polog);
            }
            pvLogList.Reverse();
            return _mapper.Map<IEnumerable<POLogVM>>(pvLogList);
        }

        public async Task<IEnumerable<SalesOrderVM>> GetSalesOrders(long tenantId,long customerOrderId)
        {
            var salesOrders = _salesOrderRepository.GetRangeAsync(d => d.TenantId == tenantId && d.CustomerOrderId == customerOrderId);
            return _mapper.Map<IEnumerable<SalesOrderVM>>(salesOrders);
        }

        public async Task<SalesOrderVM> GetSingleSalesOrder(long tenantId, long salesOrderId)
        {
            var salesOrders =await _salesOrderRepository.SingleOrDefaultAsync(d => d.Id == salesOrderId);
            if (salesOrders != null)
            {
            return _mapper.Map<SalesOrderVM>(salesOrders);
            }
            return new SalesOrderVM { SalesOrderId = -1 };
        }
        public async Task<IEnumerable<SalesOrderVM>> AllSalesOrders(long tenantId)
        {
            var salesOrders = _salesOrderRepository.GetRangeAsync(d => d.TenantId == tenantId);
            return _mapper.Map<IEnumerable<SalesOrderVM>>(salesOrders);
        }

        public async Task<IEnumerable<DeliveryScheduleVM>> GetSchedules(long tenantId,long customerOrderId)
        {
            try
            {
                // var delSchedules = _deliveryScheduleRepository.GetRangeAsync(d => d.TenantId == tenantId && d.CustomerOrderId == customerOrderId);
                var delSchedules = _salesOrderRepository.GetRangeAsync(d => d.TenantId == tenantId && d.CustomerOrderId == customerOrderId);
                return _mapper.Map<IEnumerable<DeliveryScheduleVM>>(delSchedules);
            }catch(Exception ex)
            {
                Exception exa = ex.InnerException;
                string msg = ex.Message;
            }
            return new List<DeliveryScheduleVM>();
        }


        public string HelloWorld()
        {
            return "Hello World";
        }

        public Task<OrderStatusVM> OrderStatus(OrderStatusVM customerOrderStatusVM)
        {
            throw new NotImplementedException();
        }

        public async Task<POLogVM> SOLog(POLogVM poLogVm)
        {
            try
            {
                var poLog = _mapper.Map<POLog>(poLogVm);
                if (poLog.Id == 0)
                {
                    var salesOrder = await _salesOrderRepository.SingleOrDefaultAsync(m => m.Id == poLog.SalesOrderId);
                    if (salesOrder != null)
                    {
                        poLog.SalesOrderId = salesOrder.Id;
                        if (poLog.Event.Equals("Delete"))
                        {
                            if (RemoveSalesOrder(poLog.TenantId,salesOrder.Id).Result)
                            {
                                poLog.OldValue = GetStatus(salesOrder.Status);
                                poLog.NewValue = GetStatus((int)OrdStatus.Deleted);
                            }
                        }
                        if (poLog.Event.Equals("HoldResume"))
                        {
                            poLog.OldValue = GetStatus(salesOrder.Status);
                            if (salesOrder.Hold)
                            {
                                salesOrder.Status = (int)OrdStatus.NOTPlanned;
                                poLog.NewValue = GetStatus((int)OrdStatus.NOTPlanned);
                            }
                            else
                            {
                                salesOrder.Status = (int)OrdStatus.OnHold;
                                poLog.NewValue = GetStatus(salesOrder.Status);
                            }
                            salesOrder.Hold = !salesOrder.Hold;

                            await _salesOrderRepository.UpdateAsync(salesOrder.Id, salesOrder);
                        }
                        poLog.PartId = salesOrder.PartId;
                        await _pOLogRepository.AddAsync(poLog);
                    }

                }
                else
                {
                    poLog = await _pOLogRepository.UpdateAsync(poLog.Id, poLog);
                }
                await _unitOfWork.CommitAsync();
                poLogVm.CustomerOrderId = poLog.Id;

            }
            catch (Exception ex)
            {
                Exception exa = ex.InnerException;
                string message = exa.Message;
            }
            return poLogVm;
        }


        private void CascadeDownPoLog(long tenantId, long customerOrderId)
        {
            var salesOrders = GetSalesOrders(tenantId, customerOrderId).Result.ToList();
            foreach(SalesOrderVM som in salesOrders)
            {

            }
        }
        

        public async Task<POLogVM> POLog(POLogVM poLogVm)
        {
            try {

                var poLog = _mapper.Map<POLog>(poLogVm);
                poLog.User = "-";
                if (poLog.Id == 0)
                {
                   // if(poLog.SalesOrderId>0)
                    {
                     //   return await SOLog(poLogVm);
                    }
                    //else
                    { 
                        var customerOrder = await _customerOrderRepository.SingleOrDefaultAsync(m => m.Id == poLog.CustomerOrderId);
                        if (customerOrder != null)
                        {
                            if (poLog.Event.Equals("Delete"))
                            {
                                _customerOrderRepository.Remove(customerOrder);
                                poLog.OldValue = GetStatus(customerOrder.Status);
                                poLog.NewValue = GetStatus((int)OrdStatus.Deleted);

                            }
                            if (poLog.Event.Equals("HoldResume"))
                            {
                                poLog.OldValue = GetStatus(customerOrder.Status);
                                if (customerOrder.Hold)
                                {
                                    customerOrder.Status = (int)OrdStatus.NOTPlanned;
                                    poLog.NewValue = GetStatus((int)OrdStatus.NOTPlanned);
                                }
                                else
                                {
                                    customerOrder.Status = (int)OrdStatus.OnHold;
                                    poLog.NewValue = GetStatus(customerOrder.Status);
                                }
                                customerOrder.Hold = !customerOrder.Hold;

                                await _customerOrderRepository.UpdateAsync(customerOrder.Id, customerOrder);
                            }
                            await _pOLogRepository.AddAsync(poLog);
                        }
                    }

                }
                else
                {
                    poLog = await _pOLogRepository.UpdateAsync(poLog.Id, poLog);
                }
                await _unitOfWork.CommitAsync();
                poLogVm.POLogId = poLog.Id;

            } catch(Exception ex)
            {
                Exception exa =ex.InnerException;
                string message = exa.Message;
            }
            
            return poLogVm;
        }

        public async Task<bool> RemoveCustomerOder(long tenantId, long customerOrderId)
        {
            var salesOrders = _salesOrderRepository.GetRangeAsync(m => m.CustomerOrderId == customerOrderId);
            var customerOrder = await _customerOrderRepository.SingleOrDefaultAsync(m=>m.Id == customerOrderId);
            foreach(var salesOrder in salesOrders)
            {
                await RemoveSalesOrder(tenantId, salesOrder.Id);
            }

            if(customerOrder != null)
            {
                _customerOrderRepository.Remove(customerOrder);
                await _unitOfWork.CommitAsync();
                await POLog(LogPODelete(customerOrder));
                return true;
            }

            return false;
        }

        private POLogVM LogPOEdit(CustomerOrder order)
        {
            POLogVM poLog = new POLogVM();
            poLog.CustomerOrderId = order.Id;
            poLog.OldValue = GetStatus(order.Status);
            poLog.NewValue = GetStatus((int)OrdStatus.NOTPlanned);
            poLog.Event = "POEdit";
            poLog.User = "-";
            poLog.Comment = order.CustomerName + "/" + order.Comment + "/" + order.OrderType + "/" + order.DirectEntryDetails + "/" + order.PONumber+"/"+order.PODate;
            return poLog;
        }
        private POLogVM LogPOEntry(CustomerOrder order)
        {
            POLogVM poLog = new POLogVM();
            poLog.CustomerOrderId = order.Id;
            poLog.OldValue = GetStatus(order.Status);
            poLog.NewValue = GetStatus((int)OrdStatus.NOTPlanned);
            poLog.Event = "POEntry";
            poLog.User = "-";
            poLog.Comment = order.CustomerName + "/" + order.Comment + "/" + order.OrderType + "/" + order.DirectEntryDetails + "/" + order.PONumber + "/" + order.PODate;
            return poLog;
        }

        private POLogVM LogSOEntry(SalesOrder order)
        {
            POLogVM poLog = new POLogVM();
            poLog.CustomerOrderId = order.CustomerOrderId;
            poLog.SalesOrderId = order.Id;
            poLog.PartId = order.PartId;
            poLog.OldValue = GetStatus(order.Status);
            poLog.NewValue = GetStatus((int)OrdStatus.NOTPlanned);
            poLog.Event = "POEntry/SalesOrder";
            poLog.User = "-";
            poLog.Comment = order.SONumber + "/" + order.Comment + "/" + order.RequiredQuantity + "/" + order.RequiredByDate + "/" + order.SODate;
            return poLog;
        }
        private POLogVM LogSOEdit(SalesOrder order)
        {
            POLogVM poLog = new POLogVM();
            poLog.CustomerOrderId = order.CustomerOrderId;
            poLog.SalesOrderId = order.Id;
            poLog.PartId = order.PartId;
            poLog.OldValue = GetStatus(order.Status);
            poLog.NewValue = GetStatus((int)OrdStatus.NOTPlanned);
            poLog.Event = "POEdit/SalesOrder";
            poLog.User = "-";
            poLog.Comment = order.SONumber + "/" + order.Comment + "/" + order.RequiredQuantity + "/" + order.RequiredByDate + "/" + order.SODate;
            return poLog;
        }

        private POLogVM LogSODelete(SalesOrder order)
        {
            POLogVM poLog = new POLogVM();
            poLog.CustomerOrderId = order.CustomerOrderId;
            poLog.SalesOrderId = order.Id;
            poLog.PartId = order.PartId;
            poLog.OldValue = GetStatus(order.Status);
            poLog.NewValue = GetStatus((int)OrdStatus.Deleted);
            poLog.Event = "Deleted/SO";
            poLog.User = "Admin";
            poLog.Comment = order.SONumber + "/" + order.Comment + "/" + order.RequiredQuantity + "/" + order.RequiredByDate + "/" + order.SODate;
            return poLog;
        }
        private POLogVM LogPODelete(CustomerOrder order)
        {
            POLogVM poLog = new POLogVM();
            poLog.CustomerOrderId = order.Id;
            poLog.OldValue = GetStatus(order.Status);
            poLog.NewValue = GetStatus((int)OrdStatus.Deleted);
            poLog.Event = "Deleted";
            poLog.User = "-";
            poLog.Comment = order.CustomerName + "/" + order.Comment + "/" + order.OrderType + "/" + order.DirectEntryDetails + "/" + order.PONumber + "/" + order.PODate;
            return poLog;
        }

        public async Task<bool> RemoveDeliverySchedule(long tenantId, long scheduleId)
        {
            var delSchedule = await _deliveryScheduleRepository.SingleOrDefaultAsync(m => m.Id == scheduleId);
            var salesOrder = await _salesOrderRepository.SingleOrDefaultAsync(m => m.Id == scheduleId);
            if(salesOrder != null)
            {
                try
                {
                    _salesOrderRepository.Remove(salesOrder);
                    await _unitOfWork.CommitAsync();
                    /*if (delSchedule != null)
                    {
                        _deliveryScheduleRepository.Remove(delSchedule);
                        await _unitOfWork.CommitAsync();
                        return true;
                    }*/
                }
                catch (Exception ex)
                {
                    Exception exa = ex.InnerException;
                    string message = exa.Message;
                }

            }
            return false;
        }

        public async Task<bool> RemoveOrderStatus(long tenantId, object orderStatusId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveSalesOrder(long tenantId, long salesOrderId)
        {
            var salesOrder = await _salesOrderRepository.SingleOrDefaultAsync(m => m.Id == salesOrderId);

            if (salesOrder != null)
            {
                _salesOrderRepository.Remove(salesOrder);
                await _unitOfWork.CommitAsync();
                await POLog(LogSODelete(salesOrder));
                return true;
              /*  var delSchedule = await _deliveryScheduleRepository.SingleOrDefaultAsync(m => m.Id ==salesOrder.ScheduleId);
                if (delSchedule != null)
                {
                    _deliveryScheduleRepository.Remove(delSchedule);
                    await _unitOfWork.CommitAsync();
                    return true;
                }*/
            }
            return false;
        }

        public async Task<SalesOrderVM> SalesOrder(SalesOrderVM salesOrderVm)
        {
            var salesOrder = _mapper.Map<SalesOrder>(salesOrderVm);
            if (salesOrder.Id == 0)
            {
                await _salesOrderRepository.AddAsync(salesOrder);
            }
            else
            {
                var so = await _salesOrderRepository.SingleOrDefaultAsync(x => x.Id == salesOrder.Id);
                if (so == null)
                {
                    return salesOrderVm;
                }
                so.WorkOrderId = salesOrder.WorkOrderId;
                so.WorkOrderNo = salesOrder.WorkOrderNo;
                so.BalanceSOQty = salesOrder.BalanceSOQty;
                salesOrder = await _salesOrderRepository.UpdateAsync(salesOrder.Id, so);
            }
            try
            {
            await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {

            }
            salesOrderVm.CustomerOrderId = salesOrder.Id;
            return salesOrderVm;
        }

        public async Task<SOAggregateVM> SOAggregate(SOAggregateVM aggregateVM)
        {
            var aggregate = _mapper.Map<SOAggregate>(aggregateVM);
            if (aggregate.Id == 0)
            {
                await _sOAggregateRepository.AddAsync(aggregate);
            }
            else
            {
                aggregate = await _sOAggregateRepository.UpdateAsync(aggregate.Id, aggregate);
            }
            await _unitOfWork.CommitAsync();
            aggregateVM.SOAggregateId = aggregate.Id;
            return aggregateVM;
        }

        public async Task<SOAggregateVM> GetSOAggregate(long tenantId, long customerOrderId)
        {
            var aggregate = _sOAggregateRepository.GetRangeAsync(m=>m.TenantId == tenantId && m.CustomerOrderId == customerOrderId).OrderBy(m=>m.CreationDate).LastOrDefault();

            if(aggregate == null)
            {
                return new SOAggregateVM { SOAggregateId=-1,CustomerOrderId=customerOrderId,Comment=""};
            }
            return  _mapper.Map<SOAggregateVM>(aggregate);
        }

        public bool CheckPartNo(long partId)
        {
            var manufPs = _salesOrderRepository.GetRangeAsync(c => c.PartId == partId);
            if (!manufPs.Any())
            {
                return false;
            }
            return true;
        }

        public async Task<BAStatusVM> GetBAStatus(long Id)
        {
            var bastatus = await _baStatusRepository.SingleOrDefaultAsync(d => d.Id == Id);
            if (bastatus != null)
            {
                return _mapper.Map<BAStatusVM>(bastatus);
            }
            return new BAStatusVM { StatusId = -1 };
        }

    }
}

using AutoMapper;
using CWB.Logging;
using CWB.ProductionPlanWO.Domain;
using CWB.ProductionPlanWO.Infrastructure;
using CWB.ProductionPlanWO.Repositories;
using CWB.ProductionPlanWO.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CWB.ProductionPlanWO.Services
{
    public class WOService :IWOService
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IWOSORepository _wosoRepository;
        private readonly IBOMTempRepository _bOMTempRepository;
        private readonly IProcPlanRepository _procPlanRepository;
        private readonly IBOMListRepository _bOMListRepository;
        private readonly IProductionPlan_WORepository _productionPlan_WORepository;
        private readonly IWOStatusRepository _wOStatusrepository;
        private readonly IChildWoRelRepository _childWoRelRepository;
        private readonly IMcTimeListRepository _mcTimeListRepository;
        private readonly IPODetailsRepository _poDetailsRepository;
        private readonly IPOHeaderRepository _poHeaderRepository;
        private readonly IPOStatusRepository _poStatusRepository;

        public WOService(
            ILoggerManager logger, IMapper mapper, IUnitOfWork unitOfWork
            , IWorkOrderRepository workOrderRepository
            , IProcPlanRepository procPlanRepository, IWOSORepository woso, IBOMTempRepository bOMTempRepository, IBOMListRepository bOMListRepository,
            IProductionPlan_WORepository productionPlan_WORepository, IWOStatusRepository wOStatus, IChildWoRelRepository childWoRelRepository
            , IMcTimeListRepository mcTimeListRepository, IPODetailsRepository pODetailsRepository,IPOHeaderRepository pOHeaderRepository,IPOStatusRepository pOStatusRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _workOrderRepository = workOrderRepository;
            _wosoRepository = woso;
            _bOMTempRepository = bOMTempRepository;
            _procPlanRepository = procPlanRepository;
            _bOMListRepository = bOMListRepository;
            _productionPlan_WORepository = productionPlan_WORepository;
            _wOStatusrepository = wOStatus;
            _childWoRelRepository = childWoRelRepository;
            _mcTimeListRepository = mcTimeListRepository;
            _poDetailsRepository = pODetailsRepository;
            _poHeaderRepository = pOHeaderRepository;
            _poStatusRepository = pOStatusRepository;
        }

        public string HelloWorld()
        {
            return "Hello World";
        }

        public async Task<WorkOrdersVM> WorkOrder(WorkOrdersVM workOrdersVM)
        {
            var wo = _mapper.Map<WorkOrders>(workOrdersVM);
            if (wo.SalesOrderId > 0)
            {
                if (wo.Id == 0)
                {
                    wo.WODate = DateTime.Now;
                    wo.WONumber = "WO_" + wo.WODate.Value.ToString("yyyyMMddHHmmssffff");
                    wo.TestData = 'Y';
                    wo.Status = 1;
                    wo.Active = 1;
                    try
                    {
                        await _workOrderRepository.AddAsync(wo);
                    }
                    catch (Exception ex)
                    {
                        Exception exa = ex.InnerException;
                        string msg = ex.Message;
                    }
                }
                else
                {
                    //wo.WODate = DateTime.Now;
                    var wkord = await _workOrderRepository.SingleOrDefaultAsync(x => x.Id == wo.Id);
                    if(wkord == null)
                    {
                        return workOrdersVM;
                    }
                    wkord.CalcWOQty = wo.CalcWOQty;
                    wkord.PlanCompletionDate = wo.PlanCompletionDate;
                    wkord.BuildToStock = wo.BuildToStock;
                    wkord.Parentlevel = wo.Parentlevel;
                    wkord.Status = wo.Status;
                    wkord.RoutingId = wo.RoutingId;
                    wkord.StartingOpNo = wo.StartingOpNo;
                    wkord.EndingOpNo = wo.EndingOpNo;
                    wkord.ReloadOption = wo.ReloadOption;
                    wkord.Active = wo.Active;
                    wo = await _workOrderRepository.UpdateAsync(wo.Id, wkord);
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
                workOrdersVM.WOID = wo.Id;
                workOrdersVM.WONumber = wo.WONumber;
                workOrdersVM.TestData = wo.TestData;
                return workOrdersVM;
            }

            return workOrdersVM;
        }

        public async Task<List<WorkOrdersVM>> MultipleWorkOrder(List<WorkOrdersVM> workOrdersVM)
        {
            foreach (WorkOrdersVM item in workOrdersVM)
            {
                var wo = _mapper.Map<WorkOrders>(item);
                if (wo.SalesOrderId > 0)
                {
                    if (wo.Id == 0)
                    {
                        wo.WODate = DateTime.Now;
                        wo.WONumber = "WO_" + wo.WODate.Value.ToString("yyyyMMddHHmmssffff");
                        wo.Status = 1;
                        wo.TestData = 'Y';
                        try
                        {
                            await _workOrderRepository.AddAsync(wo);
                        }
                        catch (Exception ex)
                        {
                            Exception exa = ex.InnerException;
                            string msg = ex.Message;
                        }
                    }
                    else
                    {
                       
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
                }
                item.WOID = wo.Id;
                item.WONumber = wo.WONumber;
                item.TestData = wo.TestData;
                item.Status = wo.Status;
            }
            return workOrdersVM;
        }

        public async Task<List<WorkOrdersVM>> UpdateMultipleWorkOrder(List<WorkOrdersVM> workOrdersVM)
        {
            foreach (WorkOrdersVM item in workOrdersVM)
            {
                var wo = _mapper.Map<WorkOrders>(item);
                if (wo.SalesOrderId > 0)
                {
                    if (wo.Id == 0)
                    {
                        //wo.WODate = DateTime.Now;
                        //wo.WONumber = "WO_" + wo.WODate.Value.ToString("yyyyMMddHHmmssffff");
                        //wo.Status = 1;
                        //wo.TestData = 'Y';
                        //try
                        //{
                        //    await _workOrderRepository.AddAsync(wo);
                        //}
                        //catch (Exception ex)
                        //{
                        //    Exception exa = ex.InnerException;
                        //    string msg = ex.Message;
                        //}
                    }
                    else
                    {
                        var wkord = await _workOrderRepository.SingleOrDefaultAsync(x => x.Id == wo.Id);
                        if (wkord == null)
                        {
                            return workOrdersVM;
                        }
                        wkord.PPStatus = "PP";
                        wkord.Status = wo.Status;
                        wo = await _workOrderRepository.UpdateAsync(wo.Id, wkord);
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
                }
            }
            return workOrdersVM;
        }

        public async Task<List<WOSOVM>> PostWOSO(List<WOSOVM> woso)
        {
            foreach (WOSOVM item in woso)
            {
                var wosorel = _mapper.Map<WOSO>(item);
                if (wosorel.SalesOrderId > 0)
                {
                    if (wosorel.Id == 0)
                    {
                        try
                        {
                            await _wosoRepository.AddAsync(wosorel);
                        }
                        catch (Exception ex)
                        {
                            Exception exa = ex.InnerException;
                            string msg = ex.Message;
                        }
                    }
                    else
                    {
                        var wkord = await _wosoRepository.SingleOrDefaultAsync(x => x.Id == wosorel.Id);
                        if (wkord == null)
                        {
                            return woso;
                        }
                        wkord.Active = wosorel.Active;
                        wosorel = await _wosoRepository.UpdateAsync(wosorel.Id, wkord);
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
                }
            }
            return woso;
        }

        public async Task<List<BOMTempVM>> BOMTempPost(List<BOMTempVM> bomVm)
        {
            foreach (BOMTempVM item in bomVm)
            {
                var bom = _mapper.Map<BOMTemp>(item);
                if(bom.Id == 0)
                {
                    try
                    {
                        await _bOMTempRepository.AddAsync(bom);
                    }catch (Exception ex)
                    {
                        Exception exa = ex.InnerException;
                        string msg = ex.Message;
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
                }
            }
            return bomVm;
        }

        public async Task<List<ProcPlanVM>> PostProcPlan(List<ProcPlanVM> proc)
        {
            foreach (ProcPlanVM item in proc)
            {
                var pp = _mapper.Map<ProcPlan>(item);
                if(pp.Id == 0)
                {
                    DateTime dt = DateTime.Now;
                    pp.Reference = "PP_" + dt.ToString("yyyyMMddHHmmssffff");
                    pp.TestData = 'Y';
                    pp.Plan_Proc_Qnty = item.Calc_Proc_Qnty - item.OtyOnHand;
                    try
                    {
                        await _procPlanRepository.AddAsync(pp);
                    }
                    catch (Exception ex)
                    {
                        Exception exa = ex.InnerException;
                        string msg = ex.Message;
                    }
                   
                }
                else
                {
                    var wkord = await _procPlanRepository.SingleOrDefaultAsync(x => x.Id == pp.Id);
                    if (wkord == null)
                    {
                        return proc;
                    }
                    wkord.Plan_Proc_Qnty = pp.Plan_Proc_Qnty;
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
                item.ProcPlanId = pp.Id;
            }
            return proc;
        }

        public async Task<List<BOMListVM>> PostBomList(List<BOMListVM> bomlist)
        {
            foreach (BOMListVM item in bomlist)
            {
                var bom = _mapper.Map<BOMList>(item);
                if (bom.Id == 0)
                {
                    //bom.TestData = 'Y';
                    try
                    {
                        await _bOMListRepository.AddAsync(bom);
                    }
                    catch (Exception ex)
                    {
                        Exception exa = ex.InnerException;
                        string msg = ex.Message;
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
                }
                item.BomListId = bom.Id;
            }
            return bomlist;
        }

        public async Task<IEnumerable<WorkOrdersVM>> AllWorkOrders(long tenantId)
        {
            var allwo =  _workOrderRepository.GetRangeAsync(d => d.TenantId == tenantId);
            return _mapper.Map<IEnumerable<WorkOrdersVM>>(allwo);
        }

        public async Task<IEnumerable<WorkOrdersVM>> AllParentChildWo(long parentWoId, long tenantId)
        {
            var allwo = _workOrderRepository.GetRangeAsync(d => d.ParentWoId == parentWoId && d.TenantId == tenantId);
            return _mapper.Map<IEnumerable<WorkOrdersVM>>(allwo);
        }

        public async Task<WorkOrdersVM> GetSingleWO(long Id, long tenantId)
        {
            var singlewo =await _workOrderRepository.SingleOrDefaultAsync(d=>d.Id == Id);
            if (singlewo != null)
            {
              return _mapper.Map<WorkOrdersVM>(singlewo);
            }
            return new WorkOrdersVM { WOID = -1 };
        }

        public async Task<IEnumerable<WOSOVM>> GetSoWo(long workOrderId)
        {
            var so = _wosoRepository.GetRangeAsync(s => s.WorkOrderId == workOrderId).OrderBy(s => s.Id);
            try
            {
                return _mapper.Map<IEnumerable<WOSOVM>>(so);
            }
            catch (Exception ex)
            {
                return new List<WOSOVM>();                
            }
        }

        public async Task<IEnumerable<ProcPlanVM>> AllProcPlan(long tenantId)
        {
            var allprocplan =_procPlanRepository.GetRangeAsync(d => d.TenantId == tenantId);
            return _mapper.Map<IEnumerable<ProcPlanVM>>(allprocplan);
        }
        public async Task<IEnumerable<BOMListVM>> AllBomList(long tenantId)
        {
            var allbomlist = _bOMListRepository.GetRangeAsync(d => d.TenantId == tenantId);
            return _mapper.Map<IEnumerable<BOMListVM>>(allbomlist);
        }

        public async Task<List<ProductionPlan_WOVM>> PostProductionPlan_Wo(List<ProductionPlan_WOVM> productions)
        {
            foreach (ProductionPlan_WOVM item in productions)
            {
                var pp = _mapper.Map<ProductionPlan_WO>(item);
                if (pp.SalesOrderId > 0)
                {
                    if (pp.Id == 0)
                    {
                        pp.WODate = DateTime.Now;
                        pp.PPNumber = "PP_" + pp.WODate.Value.ToString("yyyyMMddHHmmssffff");
                        if(pp.WONumber == null)
                        {
                            pp.WONumber = "WO_" + pp.WODate.Value.ToString("yyyyMMddHHmmssffff");
                        }
                        pp.Status = 1;
                        pp.TestData = 'Y';
                        try
                        {
                            await _productionPlan_WORepository.AddAsync(pp);
                        }
                        catch (Exception ex)
                        {
                            Exception exa = ex.InnerException;
                            string msg = ex.Message;
                        }
                    }
                    else
                    {
                        var upp = await _productionPlan_WORepository.SingleOrDefaultAsync(x => x.Id == pp.Id);
                        if (upp == null)
                        {
                            return productions;
                        }
                        upp.CalcWOQty = pp.CalcWOQty;
                        upp.PlanCompletionDate = pp.PlanCompletionDate;
                        upp.BuildToStock = pp.BuildToStock;
                        upp.Parentlevel = pp.Parentlevel;
                        upp.Status = pp.Status;
                        upp.RoutingId = pp.RoutingId;
                        upp.StartingOpNo = pp.StartingOpNo;
                        upp.EndingOpNo = pp.EndingOpNo;
                        upp.ReloadOption = pp.ReloadOption;
                        upp.Active = pp.Active;
                        pp = await _productionPlan_WORepository.UpdateAsync(pp.Id, upp);
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
                }
                item.ProductionPlanId = pp.Id;
                item.PPNumber = pp.PPNumber;
                item.WONumber = pp.WONumber;
                item.TestData = pp.TestData;
            }
            return productions;
        }

        public async Task<IEnumerable<ProductionPlan_WOVM>> AllProductionWo(long tenantId)
        {
            var allpp = _productionPlan_WORepository.GetRangeAsync(d => d.TenantId == tenantId);
            return _mapper.Map<IEnumerable<ProductionPlan_WOVM>>(allpp);
        }

        public async Task<WOStatusVM> GetWOStatus(long Id)
        {
            var allpp =await _wOStatusrepository.SingleOrDefaultAsync(d => d.Id == Id);
            if (allpp != null)
            {
                return _mapper.Map<WOStatusVM>(allpp);
            }
            return new WOStatusVM { StatusId = -1 };
        }

        public async Task<List<ChildWoRelVM>> PostChildWoRel(List<ChildWoRelVM> childWos)
        {
            foreach (ChildWoRelVM item in childWos)
            {
                var cwo = _mapper.Map<ChildWoRel>(item);
                if (cwo.WoId > 0)
                {
                    if (cwo.Id == 0)
                    {
                        try
                        {
                            await _childWoRelRepository.AddAsync(cwo);
                        }
                        catch (Exception ex)
                        {
                            Exception exa = ex.InnerException;
                            string msg = ex.Message;
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
                    }
                }
            }
            return childWos;
        }

        public async Task<List<McTimeListVM>> PostMcTimeList(List<McTimeListVM> mcTimeLists)
        {
            foreach (McTimeListVM item in mcTimeLists)
            {
                var cwo = _mapper.Map<McTimeList>(item);
                //if (cwo.WoId > 0)
                //{
                    if (cwo.Id == 0)
                    {
                        try
                        {
                            await _mcTimeListRepository.AddAsync(cwo);
                        }
                        catch (Exception ex)
                        {
                            Exception exa = ex.InnerException;
                            string msg = ex.Message;
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
                    }
                //}
                item.McTimeListId = cwo.Id;
            }
            return mcTimeLists;
        }

        public async Task<IEnumerable<McTimeListVM>> GetAllMcTimeListVMs(long tenantId)
        {
            var allpp = _mcTimeListRepository.GetRangeAsync(d => d.TenantId == tenantId);
            return _mapper.Map<IEnumerable<McTimeListVM>>(allpp);
        }


        public async Task<POStatusVM> GetPOStatus(long Id)
        {
            var allpp = await _poStatusRepository.SingleOrDefaultAsync(d => d.Id == Id);
            if (allpp != null)
            {
                return _mapper.Map<POStatusVM>(allpp);
            }
            return new POStatusVM { StatusId = -1 };
        }

        public async Task<List<PODetailsVM>> MultiplePODetails(List<PODetailsVM> pODetailsVM)
        {
            foreach (PODetailsVM item in pODetailsVM)
            {
                var po = _mapper.Map<PODetails>(item);
                    if (po.Id == 0)
                    {
                        po.PoDate = DateTime.Now;
                        po.POReference = "PO_" + po.PoDate.ToString("yyyyMMddHHmmssffff");
                        po.Status = 1;
                        try
                        {
                            await _poDetailsRepository.AddAsync(po);
                        }
                        catch (Exception ex)
                        {
                            Exception exa = ex.InnerException;
                            string msg = ex.Message;
                        }
                    }
                    else
                    {

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
                item.PoDetailsId = po.Id;
                item.POReference = po.POReference;
                item.Status = po.Status;
            }
            return pODetailsVM;
        }

        public async Task<List<POHeaderVM>> MultiplePOHeaders(List<POHeaderVM> pOHeaderVMs)
        {
            foreach (POHeaderVM item in pOHeaderVMs)
            {
                var po = _mapper.Map<POHeader>(item);
                if (po.Id == 0)
                {
                    try
                    {
                        await _poHeaderRepository.AddAsync(po);
                    }
                    catch (Exception ex)
                    {
                        Exception exa = ex.InnerException;
                        string msg = ex.Message;
                    }
                }
                else
                {

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
                item.PoDetailsId = po.Id;
            }
            return pOHeaderVMs;
        }
    }
}

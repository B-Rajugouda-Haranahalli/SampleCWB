using AutoMapper;
using CWB.CommonUtils.Common;
using CWB.Logging;
using CWB.Masters.Domain;
using CWB.Masters.Infrastructure;
using CWB.Masters.MastersUtils;
using CWB.Masters.MastersUtils.ItemMaster;
using CWB.Masters.Repositories.Company;
using CWB.Masters.Repositories.Routings;
using CWB.Masters.ViewModels.Company;
using CWB.Masters.ViewModels.ItemMaster;
using CWB.Masters.ViewModels.Routings;
using CWB.Masters.Domain.Routings;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CWB.Masters.Services.Routings
{
    public class RoutingService : IRoutingService
    {
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        
        private readonly IRoutingRepository _routingRepository;
        private readonly IRoutingStatusLogRepository _routingStatusLogRepository;
        private readonly IRoutingStepRepository _routingStepRepository;
        private readonly IRoutingStepPartRepository _routingStepPartRepository;
        private readonly IRoutingStepMachineRepository _routingStepMachineRepository;
        private readonly IRoutingStepSupplierRepository _routingStepSupplierRepository;
        private readonly ISubConDetailsRepository _subConDetailsRepository;
        private readonly ISubConWorkStepDetailsRepository _subConWorkStepDetailsRepository;


        public RoutingService(ILoggerManager logger, IMapper mapper, IUnitOfWork unitOfWork
            ,IRoutingRepository routingRepository, IRoutingStepRepository routingStepRepository 
            ,IRoutingStepPartRepository routingStepPartRepository
            ,IRoutingStepMachineRepository routingStepMachineRepository
            , IRoutingStepSupplierRepository routingStepSupplierRepository
            , ISubConDetailsRepository subConDetailsRepository
            , ISubConWorkStepDetailsRepository subConWorkStepDetailsRepository,
            IRoutingStatusLogRepository routingStatusLogRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _routingRepository = routingRepository;
            _routingStepRepository = routingStepRepository;
            _routingStepPartRepository = routingStepPartRepository;
            _routingStepMachineRepository = routingStepMachineRepository;
            _routingStepSupplierRepository = routingStepSupplierRepository;
            _subConDetailsRepository = subConDetailsRepository;
            _subConWorkStepDetailsRepository = subConWorkStepDetailsRepository;
            _routingStatusLogRepository = routingStatusLogRepository;
        }

        public IEnumerable<RoutingVM> GetRoutingsForManufId(int manufId)
        {
            var routings = _routingRepository.GetRangeAsync(m => m.ManufacturedPartId == manufId).OrderBy(m=>m.Id);
            try
            {
                return _mapper.Map<IEnumerable<RoutingVM>>(routings);
            }
            catch (Exception ex) {
                return new List<RoutingVM>();
            }
        }
        public IEnumerable<RoutingStepVM> GetStepsForRoutingId(int routingId)
        {
            try
            {
                var routingsteps = _routingStepRepository.GetRangeAsync(m => m.RoutingId == routingId).OrderBy(m => m.Id);
                return _mapper.Map<IEnumerable<RoutingStepVM>>(routingsteps);
            } catch(Exception ex)
            {
                string msg = ex.InnerException.Message;
                string src = ex.InnerException.Source;
                return new List<RoutingStepVM>();
            }
        }
        public async Task<RoutingStepVM> GetStep(int stepId)
        {
            RoutingStep step = await _routingStepRepository.SingleOrDefaultAsync(m=>m.Id == stepId);
            if(step == null)
            {
                return new RoutingStepVM { StepId = -1 };
            }
            return _mapper.Map <RoutingStepVM>(step);
        }
        public async Task<RoutingStepSupplierVM> GetStepSupplier(int stepId, int supplierId)
        {
            RoutingStepSupplier step = await _routingStepSupplierRepository.SingleOrDefaultAsync(m => m.RoutingStepId == stepId && m.SupplierId == supplierId);
            if (step == null)
            {
                return new RoutingStepSupplierVM { RoutingStepId = -1,SupplierId=-1 };
            }
            return _mapper.Map<RoutingStepSupplierVM>(step);
        }

        public async Task<RoutingStepPartVM> GetStepPart(int stepId, int stepPartId)
        {
            RoutingStepPart step = await _routingStepPartRepository.SingleOrDefaultAsync(m => m.RoutingStepId == stepId && m.Id == stepPartId);
            if (step == null)
            {
                return new RoutingStepPartVM { RoutingStepId = -1, StepPartId = -1 };
            }
            return _mapper.Map<RoutingStepPartVM>(step);
        }

        public async Task<RoutingStepMachineVM> GetStepMachine(int stepId, int machineId)
        {
            RoutingStepMachine step = await _routingStepMachineRepository.SingleOrDefaultAsync(m => m.RoutingStepId == stepId && m.Id == machineId);
            if (step == null)
            {
                return new RoutingStepMachineVM { RoutingStepId = -1, MachineId = -1 };
            }
            return _mapper.Map<RoutingStepMachineVM>(step);
        }


        public async Task<bool> DeleteStep(int stepId)
        {
            try
            {
                var step = await GetStep(stepId);
                RoutingStep routingStep = _mapper.Map<RoutingStep>(step);


                var subConwsDetails = _subConWorkStepDetailsRepository.GetRangeAsync(m=>m.RoutingStepId == stepId);
                var subcons = _subConDetailsRepository.GetRangeAsync(m => m.RoutingStepId == stepId);
                var stepParts = _routingStepPartRepository.GetRangeAsync(m => m.RoutingStepId == stepId).OrderByDescending(m => m.Id);
                var stepmachines = _routingStepMachineRepository.GetRangeAsync(m => m.RoutingStepId == stepId);
                if (routingStep.Id > 0)
                {
                    if (subConwsDetails != null && subConwsDetails.Count()>0)
                    {
                        foreach (SubConWorkStepDetails swd in subConwsDetails)
                        {
                            await DeleteSubConWSDetails((int)swd.Id);
                        }
                    }
                    if (subcons != null && subcons.Count()>0)
                    {
                        foreach (SubConDetails swd in subcons)
                        {
                            await DeleteSubConDetails((int)swd.Id, stepId);
                        }
                    }
                    if (stepParts != null && stepParts.Count() > 0)
                    {
                        foreach (RoutingStepPart swd in stepParts)
                        {
                            await DeleteStepPart(stepId, (int)swd.Id);
                        }
                    }
                    if (stepmachines != null && stepmachines.Count()>0)
                    {
                        foreach (RoutingStepMachine swd in stepmachines)
                        {
                            await DeleteStepMachine(stepId, (int)swd.Id);
                        }
                    }
                    _routingStepRepository.DetachEntry(routingStep);
                    _routingStepRepository.Remove(routingStep);
                    await _unitOfWork.CommitAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                string msg = ex.InnerException.Message;
                return false;
            }
        }

        public async Task<bool> DeleteStepPart(int stepId, int stepPartId)
        {
            try
            {
                var step = await GetStepPart(stepId, stepPartId);
                RoutingStepPart stepPart = _mapper.Map<RoutingStepPart>(step);
                if (stepPart.Id > 0)
                {
                    _routingStepPartRepository.DetachEntry(stepPart);
                    _routingStepPartRepository.Remove(stepPart);
                    await _unitOfWork.CommitAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                string msg = ex.InnerException.Message;
                return false;
            }
        }

        public async Task<bool> DeleteStepMachine(int stepId,int machineId)
        {
            try
            {
                var step = await GetStepMachine(stepId,machineId);
                RoutingStepMachine routingStepMachine = _mapper.Map<RoutingStepMachine>(step);
                if (routingStepMachine.Id > 0)
                {
                    _routingStepMachineRepository.DetachEntry(routingStepMachine);
                    _routingStepMachineRepository.Remove(routingStepMachine);
                    await _unitOfWork.CommitAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                string msg = ex.InnerException.Message;
                return false;
            }
        }

        public async Task<bool> DeleteStepSupplier(int stepId, int supplierId)
        {
            try
            {
                var step = await GetStepSupplier(stepId, supplierId);
                RoutingStepSupplier routingStepSupplier = _mapper.Map<RoutingStepSupplier>(step);
                if (routingStepSupplier.Id > 0)
                {
                    //_routingStepMachineRepository.(routingStepMachine);
                    _routingStepSupplierRepository.Remove(routingStepSupplier);
                    await _unitOfWork.CommitAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                string msg = ex.InnerException.Message;
                return false;
            }
        }
        


        public IEnumerable<RoutingStepPartVM> GetPartsForStepId(int stepId)
        {
            try
            {
                var stepParts = _routingStepPartRepository.GetRangeAsync(m => m.RoutingStepId == stepId).OrderByDescending(m => m.Id);
                return _mapper.Map<IEnumerable<RoutingStepPartVM>>(stepParts);
            }
            catch (Exception ex)
            {
                string msg = ex.InnerException.Message;
                string src = ex.InnerException.Source;
                return new List<RoutingStepPartVM>();
            }

        }

        public async Task<IEnumerable<CWB.Masters.Domain.Routings.Routing>> GetAllRoutings()
        {
            var routings = _routingRepository.GetRangeAsync(m=>m.Deleted==0);
            return routings;

        }

        public IEnumerable<RoutingStepPartVM> GetPartsForManufId(int manufID)
        {
            try
            {
                var stepParts = _routingStepPartRepository.GetRangeAsync(m => m.ManufacturedPartId == manufID).OrderBy(m => m.Id);
                return _mapper.Map<IEnumerable<RoutingStepPartVM>>(stepParts);
            }
            catch (Exception ex)
            {
                string msg = ex.InnerException.Message;
                string src = ex.InnerException.Source;
                return new List<RoutingStepPartVM>();
            }

        }

        private async Task<RoutingVM> GetRouting(int routingId)
        {
            var routing =  await _routingRepository.SingleOrDefaultAsync(m=>m.Id== routingId);
            if(routing == null)
            {
                return new RoutingVM { RoutingId = -1 };
            }
            return _mapper.Map<CWB.Masters.ViewModels.Routings.RoutingVM>(routing);
        }
        public async Task<RoutingVM> DeleteRouting(int routingId)
        {
            RoutingVM routingVM = await GetRouting(routingId);
            var routing = _mapper.Map<CWB.Masters.Domain.Routings.Routing>(routingVM);
            try
            {
                if (routing.Id <= 0)
                {
                }
                else
                {
                    routing.Deleted = 1;
                    routing = await _routingRepository.UpdateAsync(routing.Id,routing);
                }
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex) { }
            routingVM.RoutingId = (int)routing.Id;
            return routingVM;
        }

        public async Task<RoutingVM> Routing(RoutingVM routingVM)
        {
            var routing = _mapper.Map<CWB.Masters.Domain.Routings.Routing>(routingVM);
            if (routing.Id == 0)
            {
                await _routingRepository.AddAsync(routing);
            }
            else
            {
                var frouting = await _routingRepository.SingleOrDefaultAsync(m=>m.Id==routing.Id);
                if(frouting.Status != routing.Status)
                {
                    RoutingStatusLog routingStatusLog = new RoutingStatusLog();
                    routingStatusLog.UpdatedBy = frouting.TenantId;
                    routingStatusLog.RoutingId = frouting.Id;
                    routingStatusLog.TenantId = frouting.TenantId;
                    routingStatusLog.PrevStatus = frouting.Status;
                    routingStatusLog.ChangedStatus = routing.Status;
                    routingStatusLog.UpdatedDate = DateTime.Now;
                    routingStatusLog.Reason = routing.StatusChangeReason;
                    await _routingStatusLogRepository.AddAsync(routingStatusLog);
                }
                routing = await _routingRepository.UpdateAsync(routing.Id, routing);
            }
            await _unitOfWork.CommitAsync();
            routingVM.RoutingId = (int)routing.Id;
            return routingVM;
        }


        
        
        

        

        public async Task<RoutingVM> AltRouting(RoutingVM routingVM)
        {
            var routing = _mapper.Map<CWB.Masters.Domain.Routings.Routing>(routingVM);
            if (routing.Id == 0)
            {
                await _routingRepository.AddAsync(routing);
             //   await CopySteps(routingVM.OrigRoutingId, (int)routing.Id);
            }
            else
            {
                routing = await _routingRepository.UpdateAsync(routing.Id, routing);
            }
            await _unitOfWork.CommitAsync();
            routingVM.RoutingId = (int)routing.Id;
            return routingVM;
        }



        public async Task<RoutingVM> PreferredRouting(RoutingVM routingVM)
        {
            var routing = _mapper.Map<CWB.Masters.Domain.Routings.Routing>(routingVM);
            if (routing.Id != 0)
            {
                List<RoutingVM> lst = GetRoutingsForManufId((int)routing.ManufacturedPartId).ToList();
                foreach(RoutingVM rv in lst)
                {
                    var lRouting = _mapper.Map<CWB.Masters.Domain.Routings.Routing>(rv);
                    lRouting.PreferredRouting = 0;
                    await _routingRepository.UpdateAsync(lRouting.Id, lRouting);
                }
                routing = await _routingRepository.UpdateAsync(routing.Id, routing);
            }
            await _unitOfWork.CommitAsync();
            routingVM.RoutingId = (int)routing.Id;
            return routingVM;
        }

        public async Task<IEnumerable<RoutingStepVM>> ChangeSequence(IEnumerable<RoutingStepVM> routingStepVM)
        {
            try
            {   
                foreach (var item in routingStepVM)
                {

                    var routingStep = _mapper.Map<RoutingStep>(item);
                    if (routingStep.Id == 0)
                    {
                        //await _routingStepRepository.AddAsync(routingStep);
                    }
                    else
                    {
                        var routingStepDb = await _routingStepRepository.SingleOrDefaultAsync(x=>x.Id == routingStep.Id);
                        if(routingStepDb != null)
                        {
                            routingStepDb.RoutingStepSequence = routingStep.RoutingStepSequence;
                            routingStep = await _routingStepRepository.UpdateAsync(routingStep.Id, routingStepDb);
                        }
                    }
                    await _unitOfWork.CommitAsync();
                    item.StepId = (int)routingStep.Id;
                }
                return routingStepVM;
            }catch(Exception ex)
            {
                string msg = ex.InnerException.Message;
                string src = ex.InnerException.Source;
            }
            return routingStepVM;
        }
        public async Task<RoutingStatusLogVM> PostRoutingStatusLog(RoutingStatusLogVM routingStatusLogVM)
        {
            try
            {
                var routingStep = _mapper.Map<RoutingStatusLog>(routingStatusLogVM);
                if (routingStep.Id == 0)
                {
                    await _routingStatusLogRepository.AddAsync(routingStep);
                }
                else
                {
                    routingStep = await _routingStatusLogRepository.UpdateAsync(routingStep.Id, routingStep);
                }
                await _unitOfWork.CommitAsync();
                routingStatusLogVM.RoutingStatusLogId = (int)routingStep.Id;
                return routingStatusLogVM;
            }catch(Exception ex)
            {
                string msg = ex.InnerException.Message;
                string src = ex.InnerException.Source;
            }
            return routingStatusLogVM;
        }
        public IEnumerable<RoutingStatusLogVM> GetRoutingStatusLog(long routingId,long tenantId)
        {
            try
            {
                var stepParts = _routingStatusLogRepository.GetRangeAsync(m => m.TenantId == tenantId && m.RoutingId == routingId);
                return _mapper.Map<IEnumerable<RoutingStatusLogVM>>(stepParts);
            }
            catch (Exception ex)
            {
                string msg = ex.InnerException.Message;
                string src = ex.InnerException.Source;
                return new List<RoutingStatusLogVM>();
            }

        }
        public async Task<RoutingStepVM> RoutingStep(RoutingStepVM routingStepVM)
        {
            try
            {
                var routingStep = _mapper.Map<RoutingStep>(routingStepVM);
                if (routingStep.Id == 0)
                {
                    await _routingStepRepository.AddAsync(routingStep);
                }
                else
                {
                    routingStep = await _routingStepRepository.UpdateAsync(routingStep.Id, routingStep);
                }
                await _unitOfWork.CommitAsync();
                routingStepVM.StepId = (int)routingStep.Id;
                return routingStepVM;
            }catch(Exception ex)
            {
                string msg = ex.InnerException.Message;
                string src = ex.InnerException.Source;
            }
            return routingStepVM;
        }

        public async Task<RoutingStepPartVM> RoutingStepPart(RoutingStepPartVM routingStepPartVM)
        {
            try
            {
                var routingStepPart = _mapper.Map<RoutingStepPart>(routingStepPartVM);
                if (routingStepPart.Id == 0)
                {
                    await _routingStepPartRepository.AddAsync(routingStepPart);
                }
                else
                {
                    routingStepPart = await _routingStepPartRepository.UpdateAsync(routingStepPart.Id, routingStepPart);
                }
                await _unitOfWork.CommitAsync();
                routingStepPartVM.StepPartId = (int)routingStepPart.Id;
            }catch(Exception ex)
            {
                string msg = ex.InnerException.Message;
                string src = ex.InnerException.Source;
            }
            return routingStepPartVM;
        }

        public async Task<IEnumerable<RoutingStepMachineVM>> StepMachines(int stepId)
        {
            try
            {
                var stepMachines = _routingStepMachineRepository.GetRangeAsync(m => m.RoutingStepId == stepId).OrderBy(m => m.Id);
                return _mapper.Map<IEnumerable<RoutingStepMachineVM>>(stepMachines);
            }
            catch (Exception ex)
            {
                string msg = ex.InnerException.Message;
                string src = ex.InnerException.Source;
                return new List<RoutingStepMachineVM>();
            }
        }

        public IEnumerable<SubConDetailsVM> AllSubCons(int stepId)
        {
            try
            {
                var subcons = _subConDetailsRepository.GetRangeAsync(m => m.RoutingStepId == stepId && m.Deleted==0).OrderBy(m => m.Id);
                return _mapper.Map<IEnumerable<SubConDetailsVM>>(subcons);
            }
            catch (Exception ex)
            {
                string msg = ex.InnerException.Message;
                string src = ex.InnerException.Source;
                return new List<SubConDetailsVM>();
            }
        }

        public async Task<IEnumerable<RoutingStepSupplierVM>> StepSuppliers(int stepId)
        {
            try
            {
                var stepSuppliers = _routingStepSupplierRepository.GetRangeAsync(m => m.RoutingStepId == stepId).OrderBy(m => m.Id);
                return _mapper.Map<IEnumerable<RoutingStepSupplierVM>>(stepSuppliers);
            }
            catch (Exception ex)
            {
                string msg = ex.InnerException.Message;
                string src = ex.InnerException.Source;
                return new List<RoutingStepSupplierVM>();
            }
        }
        public async Task<SubConDetailsVM> PreferredSubCon(int subConDetailsId)
        {
            SubConDetailsVM subConDetailsVM = new SubConDetailsVM { };
            try
            {
                var subConDetails = await _subConDetailsRepository.SingleOrDefaultAsync(m => m.Id == Convert.ToInt64(subConDetailsId));
                if (subConDetails == null)
                    return new SubConDetailsVM { SubConDetailsId = -1 };
                if (subConDetails.Id != 0)
                {
                    var subcons = AllSubCons((int)subConDetails.RoutingStepId);
                    foreach (var subCon in subcons)
                    {
                        var rm = _mapper.Map<SubConDetails>(subCon);
                        rm.PreferredSubCon = 0;
                        await _subConDetailsRepository.UpdateAsync(rm.Id, rm);
                    }
                    subConDetails.PreferredSubCon = 1;
                    subConDetails = await _subConDetailsRepository.UpdateAsync(subConDetails.Id, subConDetails);
                    subConDetailsVM = _mapper.Map<SubConDetailsVM>(subConDetails);
                }
                await _unitOfWork.CommitAsync();
                subConDetailsVM.SubConDetailsId = (int)subConDetails.Id;
            }
            catch (Exception ex)
            {
                string msg = ex.InnerException.Message;
                string src = ex.InnerException.Source;
            }
            return subConDetailsVM;
        }


        public int GetMaxMachinCount(int stepId)
        {
            var step = _routingStepRepository.SingleOrDefaultAsync(m=>m.Id == stepId);
            if(step == null)
            {
                return 1;
            }
            RoutingStepVM stepVM = _mapper.Map<RoutingStepVM>(step);
            return stepVM.NumberOfSimMachines;
        }

        public async Task<RoutingStepMachineVM> PreferredStepMachine(string routingStepMachineId, string routingStepId, int maxMachineCount)
        {
            RoutingStepMachineVM routingStepMachineVM = new RoutingStepMachineVM { };
            try
            {
                var routingStepMachine = await _routingStepMachineRepository.SingleOrDefaultAsync(m=>m.Id == Convert.ToInt64(routingStepMachineId));
                if (routingStepMachine == null)
                    return new RoutingStepMachineVM { RoutingStepMachineId=-1};
                if (routingStepMachine.Id != 0)
                {
                    var stepmachines = StepMachines((int)routingStepMachine.RoutingStepId).Result.ToList();
                    int prefCount = 0;
                    foreach (var stepmachine in stepmachines)
                    {
                        var rm = _mapper.Map<RoutingStepMachine>(stepmachine);
                        if(rm.PreferredMachine == 1)
                        {
                            prefCount++;
                        }
                    }
                    if (prefCount >= maxMachineCount)
                    {
                        foreach (var stepmachine in stepmachines)
                        {
                            var rm = _mapper.Map<RoutingStepMachine>(stepmachine);
                            if (rm.PreferredMachine == 1)
                            {
                                rm.PreferredMachine = 0;
                                await _routingStepMachineRepository.UpdateAsync(rm.Id, rm);
                                break;
                            }
                        }
                    }
                    routingStepMachine.PreferredMachine = 1;
                    routingStepMachine = await _routingStepMachineRepository.UpdateAsync(routingStepMachine.Id, routingStepMachine);
                    routingStepMachineVM = _mapper.Map<RoutingStepMachineVM>(routingStepMachine);
                }
                await _unitOfWork.CommitAsync();
                routingStepMachineVM.RoutingStepMachineId = (int)routingStepMachine.Id;
            }
            catch (Exception ex)
            {
                string msg = ex.InnerException.Message;
                string src = ex.InnerException.Source;
            }
            return routingStepMachineVM;
        }
        public async Task<RoutingStepMachineVM> RoutingStepMachine(RoutingStepMachineVM routingStepMachineVM)
        {
            try
            {
                var routingStepMachine = _mapper.Map<RoutingStepMachine>(routingStepMachineVM);
                if (routingStepMachine.Id == 0)
                {
                      await _routingStepMachineRepository.AddAsync(routingStepMachine);
                  //  await PreferredStepMachine(routingStepMachine.RoutingStepId.ToString(),routingStepMachine.MachineId.ToString(), GetMaxMachinCount((int)routingStepMachine.RoutingStepId));
                }
                else
                {
                    routingStepMachine = await _routingStepMachineRepository.UpdateAsync(routingStepMachine.Id, routingStepMachine);
                }
                await _unitOfWork.CommitAsync();
                routingStepMachineVM.RoutingStepMachineId = (int)routingStepMachine.Id;
            }
            catch (Exception ex)
            {
                string msg = ex.InnerException.Message;
                string src = ex.InnerException.Source;
            }
            return routingStepMachineVM;
        }

        public async Task<RoutingStepSupplierVM> RoutingStepSupplier(RoutingStepSupplierVM routingStepSupplierVM)
        {
            try
            {
                var routingStepSupplier = _mapper.Map<RoutingStepSupplier>(routingStepSupplierVM);
                if (routingStepSupplier.Id == 0)
                {
                    await _routingStepSupplierRepository.AddAsync(routingStepSupplier);
                }
                else
                {
                    routingStepSupplier = await _routingStepSupplierRepository.UpdateAsync(routingStepSupplier.Id, routingStepSupplier);
                }
                await _unitOfWork.CommitAsync();
                routingStepSupplierVM.RoutingStepSupplierId = (int)routingStepSupplier.Id;
            }
            catch (Exception ex)
            {
                string msg = ex.InnerException.Message;
                string src = ex.InnerException.Source;
            }
            return routingStepSupplierVM;
        }

        public async Task<SubConDetailsVM> GetSubConDetails(int subConDetailsId, int stepId)
        {
            SubConDetails subConDetails = await _subConDetailsRepository.SingleOrDefaultAsync(m => m.RoutingStepId == stepId && m.Id == subConDetailsId);
            if (subConDetails == null)
            {
                return new SubConDetailsVM { RoutingStepId = -1, SubConDetailsId = -1 };
            }
            return _mapper.Map<SubConDetailsVM>(subConDetails);
        }

        public async Task<bool> DeleteSubConDetails(int subConDetailsId,int stepId)
        {
            try
            {
                var step = await GetSubConDetails(subConDetailsId, stepId);
                SubConDetails subConDetails = _mapper.Map<SubConDetails>(step);
                if (subConDetails.Id > 0)
                {
                    subConDetails.Deleted = 1;
                    //_routingStepMachineRepository.(routingStepMachine);
                    subConDetails = await _subConDetailsRepository.UpdateAsync(subConDetails.Id, subConDetails);
                    await _unitOfWork.CommitAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                string msg = ex.InnerException.Message;
                return false;
            }
        }
        public async Task<SubConDetailsVM> AddSubConDetails(SubConDetailsVM subConDetailsVM)
        {
            try
            {
                var subConDetails = _mapper.Map<SubConDetails>(subConDetailsVM);
                if (subConDetails.Id == 0)
                {
                    await _subConDetailsRepository.AddAsync(subConDetails);
                }
                else
                {
                    subConDetails = await _subConDetailsRepository.UpdateAsync(subConDetails.Id, subConDetails);
                }
                await _unitOfWork.CommitAsync();
                subConDetailsVM.SubConDetailsId = (int)subConDetails.Id;
            }
            catch (Exception ex)
            {
                string msg = ex.InnerException.Message;
                string src = ex.InnerException.Source;
            }
            return subConDetailsVM;
        }

        public async Task<SubConWorkStepDetailsVM> GetSubConWSDetails(int subConWSId)
        {
            SubConWorkStepDetails subConWSDetails = await _subConWorkStepDetailsRepository.SingleOrDefaultAsync(m => m.Id == subConWSId);
            if (subConWSDetails == null)
            {
                return new SubConWorkStepDetailsVM { RoutingStepId = -1, SubConWSDetailsId = -1 , SubConDetailsId=-1};
            }
            return _mapper.Map<SubConWorkStepDetailsVM>(subConWSDetails);
        }

        public async Task<bool> DeleteSubConWSDetails(int subConWSId)
        {
            try
            {
                var step = await GetSubConWSDetails(subConWSId);
                SubConWorkStepDetails subConWSDetails = _mapper.Map<SubConWorkStepDetails>(step);
                if (subConWSDetails.Id > 0)
                {
                    _subConWorkStepDetailsRepository.DetachEntry(subConWSDetails);
                    _subConWorkStepDetailsRepository.Remove(subConWSDetails);
                    await _unitOfWork.CommitAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                string msg = ex.InnerException.Message;
                return false;
            }
        }
        public async Task<SubConWorkStepDetailsVM> AddSubConWorkStepDetails(SubConWorkStepDetailsVM subConWSDetailsVM)
        {
            try
            {
                var subConWSDetails = _mapper.Map<SubConWorkStepDetails>(subConWSDetailsVM);
                if (subConWSDetails.Id == 0)
                {
                    await _subConWorkStepDetailsRepository.AddAsync(subConWSDetails);
                }
                else
                {
                    subConWSDetails = await _subConWorkStepDetailsRepository.UpdateAsync(subConWSDetails.Id, subConWSDetails);
                }
                await _unitOfWork.CommitAsync();
                subConWSDetailsVM.SubConWSDetailsId = (int)subConWSDetails.Id;
            }
            catch (Exception ex)
            {
                string msg = ex.InnerException.Message;
                string src = ex.InnerException.Source;
            }
            return subConWSDetailsVM;
        }

        
        public async Task<IEnumerable<SubConDetailsVM>> SubCons(int stepId)
        {
            try
            {
                var subCons = _subConDetailsRepository.GetRangeAsync(m => m.RoutingStepId == stepId).OrderBy(m => m.Id);
                return _mapper.Map<IEnumerable<SubConDetailsVM>>(subCons);
            }
            catch (Exception ex)
            {
                string msg = ex.InnerException.Message;
                string src = ex.InnerException.Source;
                return new List<SubConDetailsVM>();
            }

        }
        public async Task<IEnumerable<SubConWorkStepDetailsVM>> SubConWSS(int stepId,int subConDetailsId)
        {
            if (subConDetailsId == -1)
            {
                try
                {
                    var subCons = _subConWorkStepDetailsRepository.GetRangeAsync(m => m.RoutingStepId == stepId);
                    return _mapper.Map<IEnumerable<SubConWorkStepDetailsVM>>(subCons);
                }
                catch (Exception ex)
                {
                    string msg = ex.InnerException.Message;
                    string src = ex.InnerException.Source;
                    return new List<SubConWorkStepDetailsVM>();
                }
            }
            else
            {
                try
                {
                    var subCons = _subConWorkStepDetailsRepository.GetRangeAsync(m => m.RoutingStepId == stepId && m.SubConDetailsId == subConDetailsId).OrderBy(m => m.Id);
                    return _mapper.Map<IEnumerable<SubConWorkStepDetailsVM>>(subCons);
                }
                catch (Exception ex)
                {
                    string msg = ex.InnerException.Message;
                    string src = ex.InnerException.Source;
                    return new List<SubConWorkStepDetailsVM>();
                }
            }

        }
        public async Task CopySubConsWSs(int fromRoutingId, int toRoutingId)
        {
            List<RoutingStepVM> steps = GetStepsForRoutingId(fromRoutingId).ToList();
            List<RoutingStepVM> tosteps = GetStepsForRoutingId(toRoutingId).ToList();
            if (tosteps.Count() == 0)
                return;
            foreach (RoutingStepVM rs in steps)
            {
                foreach (RoutingStepVM tors in tosteps)
                {
                    if (rs.StepId.Equals(tors.OrigStepId)) { }
                    else continue;
                    List<SubConDetailsVM> origSubCons =  SubCons((int)rs.StepId).Result.ToList();
                    List<SubConDetailsVM> alsubcons = SubCons((int)tors.StepId).Result.ToList();
                    foreach (SubConDetailsVM origsubcon in origSubCons)
                    {
                        foreach (SubConDetailsVM altsubcon in alsubcons)
                        {
                            if (origsubcon.SubConDetailsId == altsubcon.OrigSubConId)
                            { }
                            else continue;
                            List<SubConWorkStepDetailsVM> subconwss = SubConWSS((int)origsubcon.RoutingStepId,(int)origsubcon.SubConDetailsId).Result.ToList();
                            foreach (SubConWorkStepDetailsVM scwsvm in subconwss)
                            {
                                SubConWorkStepDetails scws = _mapper.Map<SubConWorkStepDetails>(scwsvm);
                                scws.RoutingStepId = altsubcon.RoutingStepId;
                                scws.SubConDetailsId = altsubcon.SubConDetailsId;
                                scws.OrigSubConWSId = scws.Id;
                                scws.Id = 0;
                                await _subConWorkStepDetailsRepository.AddAsync(scws);
                                await _unitOfWork.CommitAsync();
                            }
                        }
                    }
                }
            }
        }
        
        public async Task CopySubCons(int fromRoutingId, int toRoutingId)
        {
            List<RoutingStepVM> steps = GetStepsForRoutingId(fromRoutingId).ToList();
            List<RoutingStepVM> tosteps = GetStepsForRoutingId(toRoutingId).ToList();
            if (tosteps.Count() == 0)
                return;

            foreach (RoutingStepVM rs in steps)
            {
                foreach (RoutingStepVM tors in tosteps)
                {
                    if (rs.StepId.Equals(tors.OrigStepId)) { }
                    else continue;
                    rs.RoutingId = tors.RoutingId;
                    int origStepId = (int)rs.StepId;
                    if (rs.RoutingId > 0 && tors.RoutingId > 0)
                    {
                        int toStepId = (int)tors.StepId;
                        List<SubConDetailsVM> subcons = SubCons(origStepId).Result.ToList();
                        foreach (SubConDetailsVM rsm in subcons)
                        {
                            rsm.RoutingStepId = toStepId;
                            rsm.OrigSubConId = rsm.SubConDetailsId;
                            rsm.SubConDetailsId = 0;
                            var subCon = _mapper.Map<SubConDetails>(rsm);
                            if (subCon.Id == 0)
                            {
                                await _subConDetailsRepository.AddAsync(subCon);
                                //  await PreferredStepMachine(routingStepMachine.RoutingStepId.ToString(),routingStepMachine.MachineId.ToString(), GetMaxMachinCount((int)routingStepMachine.RoutingStepId));
                            }
                        }
                        await _unitOfWork.CommitAsync();
                    }
                }
            }
        }

        
        public async Task CopyStepMachines(int fromRoutingId, int toRoutingId)
        {
            List<RoutingStepVM> steps = GetStepsForRoutingId(fromRoutingId).ToList();
            List<RoutingStepVM> tosteps = GetStepsForRoutingId(toRoutingId).ToList();
            if (tosteps.Count == 0)
                return;
            foreach (RoutingStepVM rs in steps)
            {
                foreach (RoutingStepVM tors in tosteps)
                {
                    if (rs.StepId.Equals(tors.OrigStepId)) { }
                    else continue;
                    rs.RoutingId = tors.RoutingId;
                    int origStepId = (int)rs.StepId;
                    if (rs.RoutingId > 0 && tors.RoutingId > 0)
                    {
                        int toStepId = (int)tors.StepId;
                        List<RoutingStepMachineVM> stepmachines = StepMachines(origStepId).Result.ToList();
                        foreach (RoutingStepMachineVM rsm in stepmachines)
                        {
                            rsm.RoutingStepId = toStepId;
                            rsm.OrigStepMachineId = rsm.RoutingStepMachineId;
                            rsm.RoutingStepMachineId = 0;
                            var routingStepMachine = _mapper.Map<RoutingStepMachine>(rsm);
                            if (routingStepMachine.Id == 0)
                            {
                                await _routingStepMachineRepository.AddAsync(routingStepMachine);
                                //  await PreferredStepMachine(routingStepMachine.RoutingStepId.ToString(),routingStepMachine.MachineId.ToString(), GetMaxMachinCount((int)routingStepMachine.RoutingStepId));
                            }
                        }
                        await _unitOfWork.CommitAsync();
                    }
                }
            }
        }

        public async Task CopyStepParts(int fromRoutingId, int toRoutingId)
        {
            List<RoutingStepVM> steps = GetStepsForRoutingId(fromRoutingId).ToList();
            List<RoutingStepVM> tosteps = GetStepsForRoutingId(toRoutingId).ToList();
            if (tosteps.Count == 0)
                return;
            foreach (RoutingStepVM rs in steps)
            {
                foreach (RoutingStepVM tors in tosteps)
                {
                    if (rs.StepId.Equals(tors.OrigStepId)) { }
                    else continue;
                    rs.RoutingId = tors.RoutingId;
                    int origStepId = (int)rs.StepId;
                    if (rs.RoutingId > 0 && tors.RoutingId > 0)
                    {
                        int toStepId = (int)tors.StepId;
                        List<RoutingStepPartVM> stepparts = GetPartsForStepId(origStepId).ToList();
                        foreach (RoutingStepPartVM rsm in stepparts)
                        {
                            rsm.RoutingStepId = toStepId;
                            rsm.OrigStepPartId = rsm.StepPartId;
                            rsm.StepPartId = 0;
                            var routingStepPArt = _mapper.Map<RoutingStepPart>(rsm);
                            if (routingStepPArt.Id == 0)
                            {
                                await _routingStepPartRepository.AddAsync(routingStepPArt);
                                //  await PreferredStepMachine(routingStepMachine.RoutingStepId.ToString(),routingStepMachine.MachineId.ToString(), GetMaxMachinCount((int)routingStepMachine.RoutingStepId));
                            }
                        }
                        await _unitOfWork.CommitAsync();
                    }
                }
            }
        }

        public async Task CopySteps(int fromRoutingId, int toRoutingId)
        {
            List<RoutingStepVM> steps = GetStepsForRoutingId(fromRoutingId).ToList();
            foreach (RoutingStepVM rs in steps)
            {
                rs.RoutingId = toRoutingId;
                int origStepId = (int)rs.StepId;
                rs.StepId = 0;
                rs.OrigStepId = origStepId;
                var routingStep = _mapper.Map<RoutingStep>(rs);
                if (routingStep.Id == 0)
                {
                    await _routingStepRepository.AddAsync(routingStep);
                    await _unitOfWork.CommitAsync();
                    //     await CopyStepMachines(origStepId,(int)routingStep.Id);
                    //     await CopySubCons(origStepId,(int)routingStep.Id);
                }
            }
        }
    }
}

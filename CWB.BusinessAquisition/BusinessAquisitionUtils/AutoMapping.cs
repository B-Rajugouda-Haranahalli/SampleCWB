using AutoMapper;
using CWB.BusinessAquisition.Domain;
using CWB.BusinessAquisition.ViewModels;
using CWB.CommonUtils.Common;

namespace CWB.BusinessAquisition.Utils
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<OrderStatus, OrderStatusVM>()
               .ForMember(m => m.StatusId, m => m.MapFrom(src => src.Id))
               .ForMember(m => m.StatusId, m => m.MapFrom(src => src.StatusId))
               .ForMember(m => m.Status, m => m.MapFrom(src => src.Status));

            CreateMap<OrderStatusVM, OrderStatus>()
                .ForMember(m => m.Id, m => m.MapFrom(src => src.StatusId))
                .ForMember(m => m.StatusId, m => m.MapFrom(src => src.StatusId))
               .ForMember(m => m.Status, m => m.MapFrom(src => src.Status));

            CreateMap<SalesOrder, DeliveryScheduleVM>()
               .ForMember(m => m.ScheduleId, m => m.MapFrom(src => src.Id))
               .ForMember(m => m.RequiredQuantity, m => m.MapFrom(src => src.RequiredQuantity))
               .ForMember(m => m.CustomerOrderId, m => m.MapFrom(src => src.CustomerOrderId))
               .ForMember(m => m.RequiredByDate, m => m.MapFrom(src => src.RequiredByDate))
               .ForMember(m => m.DSPartId, m => m.MapFrom(src => src.PartId))
               .ForMember(m => m.Comment, m => m.MapFrom(src => src.Comment));
            CreateMap<DeliveryScheduleVM, SalesOrder>()
                .ForMember(m => m.Id, m => m.MapFrom(src => src.ScheduleId))
               .ForMember(m => m.RequiredQuantity, m => m.MapFrom(src => src.RequiredQuantity))
               .ForMember(m => m.CustomerOrderId, m => m.MapFrom(src => src.CustomerOrderId))
               .ForMember(m => m.RequiredByDate, m => m.MapFrom(src => src.RequiredByDate))
               .ForMember(m => m.PartId, m => m.MapFrom(src => src.DSPartId))
               .ForMember(m=>m.ActQuantity,m=>m.MapFrom(src=>src.RequiredQuantity))
               .ForMember(m => m.ActCompletedDate, m => m.MapFrom(src => src.RequiredByDate))
               .ForMember(m => m.Comment, m => m.MapFrom(src => src.Comment));

            CreateMap<SalesOrder, DeliverySchedule>()
               .ForMember(m => m.RequiredQuantity, m => m.MapFrom(src => src.RequiredQuantity))
               .ForMember(m => m.CustomerOrderId, m => m.MapFrom(src => src.CustomerOrderId))
               .ForMember(m => m.RequiredByDate, m => m.MapFrom(src => src.RequiredByDate))
               .ForMember(m => m.PartId, m => m.MapFrom(src => src.PartId))
               .ForMember(m => m.RequiredQuantity, m => m.MapFrom(src => src.ActQuantity))
               .ForMember(m => m.RequiredByDate, m => m.MapFrom(src => src.ActCompletedDate))
               .ForMember(m => m.Comment, m => m.MapFrom(src => src.Comment));
            CreateMap<DeliverySchedule, SalesOrder>()
               .ForMember(m => m.RequiredQuantity, m => m.MapFrom(src => src.RequiredQuantity))
               .ForMember(m => m.CustomerOrderId, m => m.MapFrom(src => src.CustomerOrderId))
               .ForMember(m => m.RequiredByDate, m => m.MapFrom(src => src.RequiredByDate))
               .ForMember(m => m.PartId, m => m.MapFrom(src => src.PartId))
               .ForMember(m => m.ActQuantity, m => m.MapFrom(src => src.RequiredQuantity))
               .ForMember(m => m.ActCompletedDate, m => m.MapFrom(src => src.RequiredByDate))
               .ForMember(m => m.Comment, m => m.MapFrom(src => src.Comment));
            CreateMap<SOAggregate, SOAggregateVM>()
             .ForMember(m => m.SOAggregateId, m => m.MapFrom(src => src.Id))
             .ForMember(m => m.CustomerOrderId, m => m.MapFrom(src => src.CustomerOrderId))
             .ForMember(m => m.PartId, m => m.MapFrom(src => src.PartId))
             .ForMember(m => m.Comment, m => m.MapFrom(src => src.Comment));
            CreateMap<SOAggregateVM, SOAggregate>()
                .ForMember(m => m.Id, m => m.MapFrom(src => src.SOAggregateId))
               .ForMember(m => m.CustomerOrderId, m => m.MapFrom(src => src.CustomerOrderId))
               .ForMember(m => m.PartId, m => m.MapFrom(src => src.PartId))
               .ForMember(m => m.Comment, m => m.MapFrom(src => src.Comment));

            CreateMap<POLog, POLogVM>()
                .ForMember(m => m.POLogId, m => m.MapFrom(src => src.Id))
                .ForMember(m => m.PODate, m => m.MapFrom(src => src.CreationDate))
                .ForMember(m => m.CustomerOrderId, m => m.MapFrom(src => src.CustomerOrderId))
                .ForMember(m => m.SalesOrderId, m => m.MapFrom(src => src.SalesOrderId))
                .ForMember(m => m.PartId, m => m.MapFrom(src => src.PartId))
                .ForMember(m => m.User, m => m.MapFrom(src => src.User))
                .ForMember(m => m.NewValue, m => m.MapFrom(src => src.NewValue))
                .ForMember(m => m.OldValue, m => m.MapFrom(src => src.OldValue))
                .ForMember(m => m.Comment, m => m.MapFrom(src => src.Comment))
                .ForMember(m => m.Event, m => m.MapFrom(src => src.Event));
            CreateMap<POLogVM, POLog>()
                .ForMember(m => m.Id, m => m.MapFrom(src => src.POLogId))
                .ForMember(m => m.CustomerOrderId, m => m.MapFrom(src => src.CustomerOrderId))
                .ForMember(m => m.SalesOrderId, m => m.MapFrom(src => src.SalesOrderId))
                .ForMember(m => m.PartId, m => m.MapFrom(src => src.PartId))
                .ForMember(m => m.User, m => m.MapFrom(src => src.User))
                .ForMember(m => m.NewValue, m => m.MapFrom(src => src.NewValue))
                .ForMember(m => m.OldValue, m => m.MapFrom(src => src.OldValue))
                .ForMember(m => m.Comment, m => m.MapFrom(src => src.Comment))
                .ForMember(m => m.Event, m => m.MapFrom(src => src.Event));

            CreateMap<CustomerOrder, CustomerOrderVM>()
               .ForMember(m => m.CustomerOrderId, m => m.MapFrom(src => src.Id))
               .ForMember(m => m.CustomerId, m => m.MapFrom(src => src.CustomerId))
               .ForMember(m => m.CustomerName, m => m.MapFrom(src => src.CustomerName))
               .ForMember(m => m.OrderType, m => m.MapFrom(src => src.OrderType))
               .ForMember(m => m.PONumber, m => m.MapFrom(src => src.PONumber))
               .ForMember(m => m.PODate, m => m.MapFrom(src => src.PODate))
               .ForMember(m => m.DirectEntryDetails, m => m.MapFrom(src => src.DirectEntryDetails))
               .ForMember(m => m.Comment, m => m.MapFrom(src => src.Comment))
               .ForMember(m => m.LineNo, m => m.MapFrom(src => src.LineNo))
               .ForMember(m => m.Status, m => m.MapFrom(src => src.Status))
               .ForMember(m => m.Plan, m => m.MapFrom(src => src.Plan))
               .ForMember(m => m.Matl, m => m.MapFrom(src => src.Matl))
               .ForMember(m => m.Hold, m => m.MapFrom(src => src.Hold))
               .ForMember(m => m.Done, m => m.MapFrom(src => src.Done))
               .ForMember(m => m.WIP, m => m.MapFrom(src => src.WIP));

            CreateMap<CustomerOrderVM, CustomerOrder>()
              .ForMember(m => m.Id, m => m.MapFrom(src => src.CustomerOrderId))
              .ForMember(m => m.CustomerId, m => m.MapFrom(src => src.CustomerId))
              .ForMember(m => m.CustomerName, m => m.MapFrom(src => src.CustomerName))
              .ForMember(m => m.PONumber, m => m.MapFrom(src => src.PONumber))
              .ForMember(m => m.PODate, m => m.MapFrom(src => src.PODate))
              .ForMember(m => m.DirectEntryDetails, m => m.MapFrom(src => src.DirectEntryDetails))
              .ForMember(m => m.Comment, m => m.MapFrom(src => src.Comment))
              .ForMember(m => m.LineNo, m => m.MapFrom(src => src.LineNo))
              .ForMember(m => m.Status, m => m.MapFrom(src => src.Status))
              .ForMember(m => m.Plan, m => m.MapFrom(src => src.Plan))
              .ForMember(m => m.Matl, m => m.MapFrom(src => src.Matl))
              .ForMember(m => m.Hold, m => m.MapFrom(src => src.Hold))
              .ForMember(m => m.Done, m => m.MapFrom(src => src.Done))
              .ForMember(m => m.WIP, m => m.MapFrom(src => src.WIP));


            CreateMap<SalesOrder, SalesOrderVM>()
                .ForMember(m => m.SalesOrderId, m => m.MapFrom(src => src.Id))
                .ForMember(m => m.CustomerOrderId, m => m.MapFrom(src => src.CustomerOrderId))
                .ForMember(m => m.WorkOrderId, m => m.MapFrom(src => src.WorkOrderId))
                .ForMember(m => m.PartId, m => m.MapFrom(src => src.PartId))
                .ForMember(m => m.SONumber, m => m.MapFrom(src => src.SONumber))
                .ForMember(m => m.WorkOrderNo, m => m.MapFrom(src => src.WorkOrderNo))
                .ForMember(m => m.SODate, m => m.MapFrom(src => src.SODate))
                .ForMember(m => m.Comment, m => m.MapFrom(src => src.Comment))
                .ForMember(m => m.Status, m => m.MapFrom(src => src.Status))
                //.ForMember(m => m.ScheduleId, m => m.MapFrom(src => src.ScheduleId))
                .ForMember(m => m.Plan, m => m.MapFrom(src => src.Plan))
                .ForMember(m => m.Matl, m => m.MapFrom(src => src.Matl))
                .ForMember(m => m.Hold, m => m.MapFrom(src => src.Hold))
                .ForMember(m => m.Done, m => m.MapFrom(src => src.Done))
                .ForMember(m => m.WIP, m => m.MapFrom(src => src.WIP))
                .ForMember(m => m.BalanceSOQty, m => m.MapFrom(src => src.BalanceSOQty))
                .ForMember(m => m.RequiredQuantity, m => m.MapFrom(src => src.RequiredQuantity))
                .ForMember(m => m.RequiredByDate, m => m.MapFrom(src => src.RequiredByDate));

            CreateMap<SalesOrderVM, SalesOrder>()
               .ForMember(m => m.Id, m => m.MapFrom(src => src.SalesOrderId))
               .ForMember(m => m.CustomerOrderId, m => m.MapFrom(src => src.CustomerOrderId))
               .ForMember(m => m.WorkOrderId, m => m.MapFrom(src => src.WorkOrderId))
               .ForMember(m => m.PartId, m => m.MapFrom(src => src.PartId))
               .ForMember(m => m.SONumber, m => m.MapFrom(src => src.SONumber))
               .ForMember(m => m.WorkOrderNo, m => m.MapFrom(src => src.WorkOrderNo))
               .ForMember(m => m.Comment, m => m.MapFrom(src => src.Comment))
               .ForMember(m => m.SODate, m => m.MapFrom(src => src.SODate))
               .ForMember(m => m.Status, m => m.MapFrom(src => src.Status))
               //.ForMember(m => m.ScheduleId, m => m.MapFrom(src => src.ScheduleId))
               .ForMember(m => m.Plan, m => m.MapFrom(src => src.Plan))
               .ForMember(m => m.Matl, m => m.MapFrom(src => src.Matl))
               .ForMember(m => m.Hold, m => m.MapFrom(src => src.Hold))
               .ForMember(m => m.Done, m => m.MapFrom(src => src.Done))
               .ForMember(m => m.WIP, m => m.MapFrom(src => src.WIP))
               .ForMember(m => m.RequiredQuantity, m => m.MapFrom(src => src.RequiredQuantity))
               .ForMember(m => m.RequiredByDate, m => m.MapFrom(src => src.RequiredByDate));


            CreateMap<BAStatusVM, BAStatus>()
              .ForMember(m => m.Id, m => m.MapFrom(src => src.StatusId))
              .ForMember(m => m.Status, m => m.MapFrom(src => src.Status));

            CreateMap<BAStatus, BAStatusVM>()
               .ForMember(m => m.StatusId, m => m.MapFrom(src => src.Id))
               .ForMember(m => m.Status, m => m.MapFrom(src => src.Status));
        }
    }
}
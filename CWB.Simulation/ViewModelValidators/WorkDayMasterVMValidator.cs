using CWB.Simulation.ViewModels;
using CWB.Simulation.ViewModelValidatorsMessage;
using FluentValidation;
using System;

namespace CWB.Simulation.ViewModelValidators
{
    public class WorkDayMasterVMValidator : AbstractValidator<WorkDayMasterVM>
    {
        public WorkDayMasterVMValidator()
        {
            RuleFor(c => c.WeeklyOff)
                .NotEmpty().WithMessage(WorkDayMasterVMValidatorMessage.EmptyWeeklyOff);

            RuleFor(c => c.NoOfShifts)
                .NotEmpty().WithMessage(WorkDayMasterVMValidatorMessage.EmptyNoOfShifts);

            RuleFor(c => c.FirstShiftStartTime)
                .Must(date => date != default(TimeSpan)).WithMessage(WorkDayMasterVMValidatorMessage.EmptyFirstShiftStartTime);

            RuleFor(c => c.FirstShiftDuration)
                .Must(date => date != default(TimeSpan)).WithMessage(WorkDayMasterVMValidatorMessage.EmptyFirstShiftDuration);

            RuleFor(c => c.SecondShiftDuration)
                .Must(date => date != default(TimeSpan)).WithMessage(WorkDayMasterVMValidatorMessage.EmptySecondShiftDuration);

            RuleFor(c => c.TenantId)
                .NotEmpty().WithMessage(WorkDayMasterVMValidatorMessage.EmptyTenantId);
                
        }
    }
}

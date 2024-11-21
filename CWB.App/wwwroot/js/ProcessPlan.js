function landingPage() {
    const today = new Date();
    api.getbulk("/WorkOrder/AllSalesOrders").then((data) => {
        const count = data.length;
        const woPendingCount = data.filter((salesOrder) => salesOrder.status === 1).length;
        const soOnHoldCount = data.filter((salesOrder) => salesOrder.hold === 1).length;
        //const soInWipPastDueCount = data.filter((salesOrder) => Date.parse(salesOrder.requiredByDate) < today.getTime()).length;
        //const soInWipOnTrackCount = data.filter((salesOrder) => Date.parse(salesOrder.requiredByDate) >= today.getTime()).length;
        $('#1stSoWoPending').text(woPendingCount);
        $('#SoWoPending').text(woPendingCount);
        $('#noOfSoHold').text(soOnHoldCount);
        $('#SoInPastDue').text(0);
        $('#SoOntrack').text(0);
        $('#totalSO').text(count);
    }).catch((error) => {
    });
    api.getbulk("/WorkOrder/AllWorkOrders").then((data) => {
        const workOrdersWithStatus1 = data.filter((workOrder) => workOrder.status >= 1);
        const workOrdersWithStatusHold = data.filter((workOrder) => workOrder.status === 8);
        const count = workOrdersWithStatus1.length;
        const woonhold = workOrdersWithStatusHold.length;

        //const woInWipPastDueCount = data.filter((workOrder) => Date.parse(workOrder.planCompletionDate) < today.getTime()).length;
        //const woInWipOnTrackCount = data.filter((workOrder) => Date.parse(workOrder.planCompletionDate) >= today.getTime()).length;

        $('#totalWO').text(count);
        $('#WoOnHold').text(woonhold);
        $('#WoInPastDue').text(0);
        $('#WoOnTrack').text(0);
    }).catch((error) => {
    });
}






$(document).ready(function () {

    landingPage();
});

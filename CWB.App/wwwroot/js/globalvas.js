//For loading Company / Supplier
let newCo = "";
let newCoId = "";
let CURRENT_TAB = "TabMPMain";
let CURDLG = "";
let AddOrEdit = "add";
function SetOp(op) {
    AddOrEdit = op;
};
function IsAddOpCalled() {
    return AddOrEdit == "add";
};
$(function () {
    $('.select2').each(function () {
        $(this).select2({ dropdownParent: $(this).parent() });
    })
});
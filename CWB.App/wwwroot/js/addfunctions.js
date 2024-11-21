
$(function () {
    
    

});

function AddUOM() {
    alert("AddUOM");
}




function showEditUOMDialog() {
    var dlgComp = $("#dialog-EditUOM");
    dlgComp.dialog({
        width: 300,
        height: 200,
        modal: true,
        title: "Edit UOM"
    });
}

$("#myInput").on("change", function () {
    var value = $(this).val().toLowerCase();
    $("#myTable tbody tr").filter(function () {
        $(this).toggle($(this.children[1]).text().toLowerCase().indexOf(value) > -1)
    });
});
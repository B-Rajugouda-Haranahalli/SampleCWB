let dataMPDList = "";
let partType = "ManufacturedPart";
let status = "Active";

$(function () {

    $("#master_co").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#mptable tbody tr").filter(function () {
            $(this).toggle($(this.children[0]).text().toLowerCase().indexOf(value) > -1)
        });
    });

    $("#master_partno").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#mptable tbody tr").filter(function () {
            $(this).toggle($(this.children[1]).text().toLowerCase().indexOf(value) > -1)
        });
    });

    $("#master_description").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#mptable tbody tr").filter(function () {
            $(this).toggle($(this.children[2]).text().toLowerCase().indexOf(value) > -1)
        });
    });
    $("#manfWithOutRM").click(function (event) {
        var data = "No";
        var value = data.toLowerCase();
        $("#mptable tbody tr").filter(function () {
            $(this).toggle($(this.children[5]).text().toLowerCase().indexOf(value) > -1)
        });
    });
    $("#AssWithOuutBOM").click(function (event) {
        var data = "No";
        var value = data.toLowerCase();
        $("#mptable tbody tr").filter(function () {
            $(this).toggle($(this.children[4]).text().toLowerCase().indexOf(value) > -1)
        });
    });
    $("#PurcSupplier").click(function (event) {
        var data = "No";
        var value = data.toLowerCase();
        $("#mptable tbody tr").filter(function () {
            $(this).toggle($(this.children[6]).text().toLowerCase().indexOf(value) > -1)
        });
    });
    $("#MandatoryNotUp").click(function (event) {
        var data = "No";
        var value = data.toLowerCase();
        $("#mptable tbody tr").filter(function () {
            $(this).toggle($(this.children[7]).text().toLowerCase().indexOf(value) > -1)
        });
    });
    $("#finalepartSold").click(function (event) {
        var data = "Y";
        var value = data.toLowerCase();
        $("#mptable tbody tr").filter(function () {
            $(this).toggle($(this.children[9]).text().toLowerCase().indexOf(value) > -1)
        });
    });

    $("#Status").change(function () {
        loadMPDList();
    });

    var RawMaterialMadeType = 0;
    //////debugger;
    // Document is ready
    /** $("#RawMaterialTypeId").select2();
     loadRMTypes("RawMaterialTypeId");
 
     $("#BaseRawMaterialId").select2();
     loadBaseRMs("BaseRawMaterialId");
 
     $("#Standard").select2();
     loadRMStandards("Standard");
 
     $("#MaterialSpecId").select2();
     loadRMSpecs("MaterialSpecId");
 
     $("#SupplierId").select2();
     loadSuppliers("SupplierId");*/
    loadEditParts();

    $('#VMStatus').change(function () {
        var selectedValue = $(this).val();
        if (selectedValue == "1") {
            var data = "Active";
            var value = data.toLowerCase();
            $("#VMOneGrid tbody tr").filter(function () {
                $(this).toggle($(this.children[3]).text().toLowerCase().indexOf(value) > -1)
            });
        } else if (selectedValue == "2") {
            var data = "Inactive";
            var value = data.toLowerCase();
            $("#VMOneGrid tbody tr").filter(function () {
                $(this).toggle($(this.children[3]).text().toLowerCase().indexOf(value) > -1)
            });
        } else {
            $("#VMOneGrid tbody tr").show();
        }
    });
    $('#P5Status').change(function () {
        var selectedValue = $(this).val();
        if (selectedValue == "1") {
            var data = "Active";
            var value = data.toLowerCase();
            $("#P5Grid tbody tr").filter(function () {
                $(this).toggle($(this.children[3]).text().toLowerCase().indexOf(value) > -1)
            });
        } else if (selectedValue == "2") {
            var data = "Inactive";
            var value = data.toLowerCase();
            $("#P5Grid tbody tr").filter(function () {
                $(this).toggle($(this.children[3]).text().toLowerCase().indexOf(value) > -1)
            });
        } else {
            $("#P5Grid tbody tr").show();
        }
    });
    $("#P5Company").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#P5Grid tbody tr").filter(function () {
            $(this).toggle($(this.children[1]).text().toLowerCase().indexOf(value) > -1)
        });
    });
    $("#P5Source").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#P5Grid tbody tr").filter(function () {
            $(this).toggle($(this.children[2]).text().toLowerCase().indexOf(value) > -1)
        });
    });
    $("#P5MatlSpec").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#P5Grid tbody tr").filter(function () {
            $(this).toggle($(this.children[6]).text().toLowerCase().indexOf(value) > -1)
        });
    });
    $("#P5BaseRm").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#P5Grid tbody tr").filter(function () {
            $(this).toggle($(this.children[5]).text().toLowerCase().indexOf(value) > -1)
        });
    });
    $("#P5RmType").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#P5Grid tbody tr").filter(function () {
            $(this).toggle($(this.children[4]).text().toLowerCase().indexOf(value) > -1)
        });
    });
    $("#P5PartNo").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#P5Grid tbody tr").filter(function () {
            $(this).toggle($(this.children[0]).text().toLowerCase().indexOf(value) > -1)
        });
    });
    $("#P3RmType").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#P3Grid tbody tr").filter(function () {
            $(this).toggle($(this.children[1]).text().toLowerCase().indexOf(value) > -1)
        });
    });
    $("#P3BaseRm").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#P3Grid tbody tr").filter(function () {
            $(this).toggle($(this.children[4]).text().toLowerCase().indexOf(value) > -1)
        });
    });
    $("#P9PartSearch").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#P9Grid tbody tr").filter(function () {
            $(this).toggle($(this.children[0]).text().toLowerCase().indexOf(value) > -1)
        });
    });
    $("#P9CompSearch").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#P9Grid tbody tr").filter(function () {
            $(this).toggle($(this.children[1]).text().toLowerCase().indexOf(value) > -1)
        });
    });
    $('#P3StatusSr').change(function () {
        var selectedValue = $(this).val();
        if (selectedValue == "1") {
            var data = "Active";
            var value = data.toLowerCase();
            $("#P3Grid tbody tr").filter(function () {
                $(this).toggle($(this.children[2]).text().toLowerCase().indexOf(value) > -1)
            });
        } else if (selectedValue == "2") {
            var data = "Inactive";
            var value = data.toLowerCase();
            $("#P3Grid tbody tr").filter(function () {
                $(this).toggle($(this.children[2]).text().toLowerCase().indexOf(value) > -1)
            });
        } else {
            $("#P3Grid tbody tr").show();
        }
    });
    $('#P7Status').change(function () {
        var selectedValue = $(this).val();
        if (selectedValue == "1") {
            var data = "Active";
            var value = data.toLowerCase();
            $("#P7Grid tbody tr").filter(function () {
                $(this).toggle($(this.children[1]).text().toLowerCase().indexOf(value) > -1)
            });
        } else if (selectedValue == "2") {
            var data = "Inactive";
            var value = data.toLowerCase();
            $("#P7Grid tbody tr").filter(function () {
                $(this).toggle($(this.children[1]).text().toLowerCase().indexOf(value) > -1)
            });
        } else {
            $("#P7Grid tbody tr").show();
        }
    });
    $('#P9StatusSearch').change(function () {
        var selectedValue = $(this).val();
        if (selectedValue == "1") {
            var data = "Active";
            var value = data.toLowerCase();
            $("#P9Grid tbody tr").filter(function () {
                $(this).toggle($(this.children[3]).text().toLowerCase().indexOf(value) > -1)
            });
        } else if (selectedValue == "2") {
            var data = "Inactive";
            var value = data.toLowerCase();
            $("#P9Grid tbody tr").filter(function () {
                $(this).toggle($(this.children[3]).text().toLowerCase().indexOf(value) > -1)
            });
        } else {
            $("#P9Grid tbody tr").show();
        }
    });
    $('#P7BofType').change(function () {
        var selectedValue = $(this).val();
        if (selectedValue == "1") {
            var data = "Standard";
            var value = data.toLowerCase();
            $("#P7Grid tbody tr").filter(function () {
                $(this).toggle($(this.children[2]).text().toLowerCase().indexOf(value) > -1)
            });
        } else if (selectedValue == "2") {
            var data = "Catalog";
            var value = data.toLowerCase();
            $("#P7Grid tbody tr").filter(function () {
                $(this).toggle($(this.children[2]).text().toLowerCase().indexOf(value) > -1)
            });
        } else if (selectedValue == "3") {
            var data = "Made to Print";
            var value = data.toLowerCase();
            $("#P7Grid tbody tr").filter(function () {
                $(this).toggle($(this.children[2]).text().toLowerCase().indexOf(value) > -1)
            });
        } else {
            $("#P7Grid tbody tr").show();
        }
    });
    $('#P9BofType').change(function () {
        var selectedValue = $(this).val();
        if (selectedValue == "1") {
            var data = "Standard";
            var value = data.toLowerCase();
            $("#P9Grid tbody tr").filter(function () {
                $(this).toggle($(this.children[4]).text().toLowerCase().indexOf(value) > -1)
            });
        } else if (selectedValue == "2") {
            var data = "Catalog";
            var value = data.toLowerCase();
            $("#P9Grid tbody tr").filter(function () {
                $(this).toggle($(this.children[4]).text().toLowerCase().indexOf(value) > -1)
            });
        } else if (selectedValue == "3") {
            var data = "Made to Print";
            var value = data.toLowerCase();
            $("#P9Grid tbody tr").filter(function () {
                $(this).toggle($(this.children[4]).text().toLowerCase().indexOf(value) > -1)
            });
        } else {
            $("#P9Grid tbody tr").show();
        }
    });
    $("#P9Supp1").click(function (event) {
        var isChecked = $(this).prop("checked");
        if (isChecked) {
            var data = "1";
            var value = data.toLowerCase();
            $("#P9Grid tbody tr").filter(function () {
                $(this).toggle($(this.children[5]).text().toLowerCase().indexOf(value) > -1)
            });
        } else {
            $("#P9Grid tbody tr").show();
        }

    });
    $("#P9Supp2").click(function (event) {
        var isChecked = $(this).prop("checked");
        if (isChecked) {
            var data = "2";
            var value = data.toLowerCase();
            $("#P9Grid tbody tr").filter(function () {
                $(this).toggle($(this.children[5]).text().toLowerCase().indexOf(value) > -1)
            });
        } else {
            $("#P9Grid tbody tr").show();
        }
    });
    $("#btnP5CancelFields").click(function (event) {
        $("#P5PartNo").val('');
        $("#P5Company").val('');
        $("#P5Source").val('');
        $("#P5RmType").val('');
        $("#P5BaseRm").val('');
        $("#P5MatlSpec").val('');
        $("#P5Status").val(0);
        $("#P5Supplier2").prop('checked', false);
        $("#P5SuppWith1").prop('checked', false);
        $("#P5Grid tbody tr").show();
    });
    $("#P9BtnClearFields").click(function (event) {
        $("#P9PartSearch").val('');
        $("#P9CompSearch").val('');
        $("#P9SourceSearch").val('');
        $("#P9StatusSearch").val(0);
        $("#P9BofType").val(0);
        $("#P9Supp2").prop('checked', false);
        $("#P9Supp1").prop('checked', false);
        $("#P9Grid tbody tr").show();
    });
    $("#P5Supplier2").click(function (event) {
        var isChecked = $(this).prop("checked");
        var data = "2";
        var value = data.toLowerCase();
        if (isChecked) {
            $("#P5Grid tbody tr").filter(function () {
                $(this).toggle($(this.children[7]).text().toLowerCase().indexOf(value) > -1)
            });
        } else {
            $("#P5Grid tbody tr").show(); // Show all rows if the checkbox is not checked
        }
    });
    $("#P5SuppWith1").click(function (event) {
        var isChecked = $(this).prop("checked");
        if (isChecked) {
            var data = "1";
            var value = data.toLowerCase();
            $("#P5Grid tbody tr").filter(function () {
                $(this).toggle($(this.children[7]).text().toLowerCase().indexOf(value) > -1)
            });
        } else {
            $("#P5Grid tbody tr").show();
        }

    });
    $('#VMSearchPartTypr').change(function () {
        var selectedValue = $(this).val();
        if (selectedValue == "1") {
            var data = "ManufacturedPart";
            var value = data.toLowerCase();
            $("#VMOneGrid tbody tr").filter(function () {
                $(this).toggle($(this.children[2]).text().toLowerCase().indexOf(value) > -1)
            });
        } else if (selectedValue == "2") {
            var data = "Assembly";
            var value = data.toLowerCase();
            $("#VMOneGrid tbody tr").filter(function () {
                $(this).toggle($(this.children[2]).text().toLowerCase().indexOf(value) > -1)
            });
        } else {
            $("#VMOneGrid tbody tr").show();    
        }
    });
    $("#BtnClearFlieds").click(function (event) {
        $("#VMOneGrid tbody tr").show();
    });
    $("#VMManfWithoutRouting").click(function (event) {
        var isChecked = $(this).prop("checked");
        if (isChecked) {
            var data = "No";
            var value = data.toLowerCase();
            $("#VMOneGrid tbody tr").filter(function () {
                $(this).toggle($(this.children[8]).text().toLowerCase().indexOf(value) > -1)
            });
        } else {
            $("#VMOneGrid tbody tr").show();
        }
    });
    $("#VMManfWithoutDoc").click(function (event) {
        var isChecked = $(this).prop("checked");
        if (isChecked) {
            var data = "No";
            var value = data.toLowerCase();
            $("#VMOneGrid tbody tr").filter(function () {
                $(this).toggle($(this.children[7]).text().toLowerCase().indexOf(value) > -1)
            });
        } else {
            $("#VMOneGrid tbody tr").show();
        }
    });
    $("#VMRoutingWithoutDoc").click(function (event) {
        var isChecked = $(this).prop("checked");
        if (isChecked) {
            var data = "No";
            var value = data.toLowerCase();
            $("#VMOneGrid tbody tr").filter(function () {
                $(this).toggle($(this.children[9]).text().toLowerCase().indexOf(value) > -1)
            });
        } else {
            $("#VMOneGrid tbody tr").show();
        }
    });
    $("#VMAssWithoutBOm").click(function (event) {
        var isChecked = $(this).prop("checked");
        if (isChecked) {
            var data = "No";
            var value = data.toLowerCase();
            $("#VMOneGrid tbody tr").filter(function () {
                $(this).toggle($(this.children[6]).text().toLowerCase().indexOf(value) > -1)
            });
        } else {
            $("#VMOneGrid tbody tr").show();
        }

    });
    $("#VMFinalPart").click(function (event) {
        var isChecked = $(this).prop("checked");
        if (isChecked) {
            var data = "No";
            var value = data.toLowerCase();
            $("#VMOneGrid tbody tr").filter(function () {
                $(this).toggle($(this.children[1]).text().toLowerCase().indexOf(value) > -1)
            });
        } else {
            $("#VMOneGrid tbody tr").show();
        }
    });
    $("#VMManfWithoutRM").click(function (event) {
        var isChecked = $(this).prop("checked");
        if (isChecked) {
            var data = "No";
            var value = data.toLowerCase();
            $("#VMOneGrid tbody tr").filter(function () {
                $(this).toggle($(this.children[5]).text().toLowerCase().indexOf(value) > -1)
            });
        } else {
            $("#VMOneGrid tbody tr").show();
        }
    });
    $('select[name="MasterPart"]').change(function () {
        var selectedValue = $(this).val();
        if (selectedValue == "1") {
            partType = "ManufacturedPart";
            loadMPDList();
        } else if (selectedValue == "2") {
            partType = "Assembly";
            loadMPDList();
        } else if (selectedValue == "3") {
            partType = "BOF";
            loadMPDList();
        } else if (selectedValue == "4") {
            partType = "RawMaterial";
            dataMPDList = "";
            loadMPDList();
        } else {
            loadEditParts();
        }
    });
    $("#ClearSearchFields").on("click", function () {
        $("#mptable tbody tr").show();
        $("#master_co").val('');
        $("#cars").prop('checked', false);
        document.getElementById("manfWithOutRM").checked = false;
        document.getElementById("AssWithOuutBOM").checked = false;
        document.getElementById("PurcSupplier").checked = false;
        document.getElementById("MandatoryNotUp").checked = false;
        document.getElementById("finalepartSold").checked = false;

        var SearchFileExtn = $('#MasterPart');
        SearchFileExtn.val(0).trigger('change');
    });

    $("#AddToMasterDocList").click(function (event) {
        var mkID = parseInt($("#SetPreferedMKId").val());
        var DocTypeName = parseInt($("#DocTypeName").val());
        var MasterContent = parseInt($("#MasterContent").val());
        var ItemDocId = parseInt($("#ItemDocId").val());
        var mandatory = 'N';
        if ($("#DocUploadMandatory").prop("checked") == true) {
            mandatory = 'Y';
        }
        if (MasterContent == 0) {
            var newNamevalidate = document.getElementById('MasterContent');
            newNamevalidate.style.border = '2px solid red';
            return false;
        } else {
            var newNamevalidate = document.getElementById('MasterContent');
            newNamevalidate.style.border = '';
        }
        if (DocTypeName == 0) {
            var newNamevalidate = document.getElementById('DocTypeName');
            newNamevalidate.style.border = '2px solid red';
            return false;
        } else {
            var newNamevalidate = document.getElementById('DocTypeName');
            newNamevalidate.style.border = '';
        }
        var today = new Date();
        today.setDate(today.getDate());
        var deletionDate = today.toISOString().split('T')[0];
        var rowData = {
            ItemMasterDocListId: ItemDocId,
            ContentId: MasterContent,
            DocumentTypeId: DocTypeName,
            Mandatory: mandatory,
            UpdatedOn: deletionDate
        };
        api.getbulk("/masters/CheckDocumentTypeInItemMaster?documentTypeId=" + DocTypeName + "&contentId=" + MasterContent).then((data) => {
            if (data) {
                api.post("/masters/PostItemMasterDocList", rowData).then((data) => {
                    $("#Text-Error").text("");
                    var DocTypeName = $("#DocTypeName");
                    var MasterContent = $("#MasterContent");
                    $("#ItemDocId").val(0);
                    DocTypeName.find("option[value='" + 0 + "']").prop('selected', true);
                    MasterContent.find("option[value='" + 0 + "']").prop('selected', true);
                    $("#DocUploadMandatory").prop("checked", false);

                    loadItemMasterDocList();
                }).catch((error) => {
                    AppUtil.HandleError("FormEditMakeFrom", error);
                });

            } else {
                $("#Text-Error").text("This Entry Is Already in The List").css('color', 'red');
            }
        }).catch((error) => {
        });
    });
    $('#adtimc').on('hidden.bs.modal', function (event) {
        var DocTypeName = $("#DocTypeName");
        var MasterContent = $("#MasterContent");
        $("#ItemDocId").val(0);
        DocTypeName.find("option[value='" + 0 + "']").prop('selected', true);
        MasterContent.find("option[value='" + 0 + "']").prop('selected', true);
        $("#DocUploadMandatory").prop("checked", false);
        $("#Text-Error").text("");
    });
    $('#adtimc').on('show.bs.modal', function (event) {
        loadItemMasterDocList();
        loadDocTypes();
        loadMasterContent();
    });
    $('#popup6').on('show.bs.modal', function (event) {
        BofSupplier();
    });
    $('#popup5').on('show.bs.modal', function (event) {
        RMList();
    });
    $('#popup1').on('show.bs.modal', function (event) {
        loadManufAssem();
    });
    let vmpartid = 0;
    $('#viewDoc').on('show.bs.modal', function (event) {
        $('#fileViewer').attr('src', '');
        var newNamevalidate = document.getElementById('SelectDocType');
        newNamevalidate.style.border = '';
        var relatedTarget = $(event.relatedTarget);
        var customername = relatedTarget.data("customername");
        var partid = relatedTarget.data("partid");
        var partno = relatedTarget.data("partno");
        var partdesc = relatedTarget.data("partdesc");
        var parttype = relatedTarget.data("parttype");
        vmpartid = partid;
        $("#VPPartNo").text(partno);
        $("#VPPartDesc").text(partdesc);
        $("#VPCustomer").text(customername);
        loadViewDocumentType(parttype);
    });
    $("#BtnViewDoc").on("click", function () {
        var documenttype = $("#SelectDocType").val();
        if (parseInt(documenttype) == 0) {
            var newNamevalidate = document.getElementById('SelectDocType');
            newNamevalidate.style.border = '2px solid red';
            return false;
        }
        api.getbulk("/DocumentManagement/GetAllDocList").then((data) => {
            data = data.filter(item => item.partId === vmpartid && item.documentTypeId === parseInt(documenttype));
            if (data.length > 0) {
                $("#VPDocType").text(data[0].documentTypeName);
                $("#VPRetention").text(data[0].retentionDateStr);
                $("#VPUploadedOn").text(data[0].updatedOnStr);
                $("#VPUploadedBy").text(data[0].uploadedBy);
                ViewFile(data[0].fileName);
            }
        }).catch((error) => {
            console.log(error);
        });
    });
    $('#ViewManufPartPopup').on('show.bs.modal', function (event) {
        var relatedTarget = $(event.relatedTarget);
        var customername = relatedTarget.data("customername");
        $("#VmPopComp").text(customername);
        loadManufAssemComp(customername);
    });
    $('#popup8').on('show.bs.modal', function (event) {
        var relatedTarget = $(event.relatedTarget);
        var partid = relatedTarget.data("partid");
        var partno = relatedTarget.data("partno");
        var partdesc = relatedTarget.data("partdesc");
        var customername = relatedTarget.data("customername");
        $("#P8BofNo").text(partno);
        $("#P8BofDesc").text(partdesc);
        $("#P8Comp").text(customername);
        LoadPartByBof(partid);
    });
    $('#popup9').on('show.bs.modal', function (event) {
        BofList();
    });
    $('#popup2').on('show.bs.modal', function (event) {
        RmSupplier();
    });
    $('#popup3').on('show.bs.modal', function (event) {
        var relatedTarget = $(event.relatedTarget);
        var customername = relatedTarget.data("customername");
        $("#P3CustSupp").text(" ");
        $("#P3Comp").text(customername);
        P2RMList(customername);
    });
    $('#popup7').on('show.bs.modal', function (event) {
        var relatedTarget = $(event.relatedTarget);
        var customername = relatedTarget.data("customername");
        $("#P7Cust").text(" ");
        $("#P7Comp").text(customername);
        P7BofList(customername);
    });
    $('#popup4').on('show.bs.modal', function (event) {
        var relatedTarget = $(event.relatedTarget);
        var partid = relatedTarget.data("partid");
        var partno = relatedTarget.data("partno");
        var partdesc = relatedTarget.data("partdesc");
        var customername = relatedTarget.data("customername");
        $("#P4RmPartNo").text(partno);
        $("#P4RmDesc").text(partdesc);
        $("#P4Comp").text(customername);
        LoadPartByRM(partid);
    });
});
function BofSupplier() {
    var tablebody = $("#P6Grid tbody");
    $(tablebody).html("");//empty tbody

    api.getbulk("/masters/BofByComp").then((data) => {
        for (i = 0; i < data.length; i++) {
            var tBody = ProcessTemplateDataNew("P6GridRow", data[i]);
            $(tablebody).append(tBody);
        }
    }).catch((error) => {
    });
}
function RmSupplier() {
    var tablebody = $("#P2Grid tbody");
    $(tablebody).html("");//empty tbody

    api.getbulk("/masters/RmByComp").then((data) => {
        for (i = 0; i < data.length; i++) {
            var tBody = ProcessTemplateDataNew("P2GridRow", data[i]);
            $(tablebody).append(tBody);
        }
    }).catch((error) => {
    });
}
function LoadPartByBof(partid) {
    var tablebody = $("#P8Grid tbody");
    $(tablebody).html("");//empty tbody

    api.getbulk("/masters/GetAllAssemByBof?partid="+partid).then((data) => {
        for (i = 0; i < data.length; i++) {
            var tBody = ProcessTemplateDataNew("P8GridRow", data[i]);
            $(tablebody).append(tBody);
        }
    }).catch((error) => {
    });
}
function LoadPartByRM(partid) {
    var tablebody = $("#Popup4Grid tbody");
    $(tablebody).html("");//empty tbody

    api.getbulk("/masters/GetAllManufByRM?partid="+partid).then((data) => {
        for (i = 0; i < data.length; i++) {
            var tBody = ProcessTemplateDataNew("P4GridRow", data[i]);
            $(tablebody).append(tBody);
        }
    }).catch((error) => {
    });
}
function ViewFile(filename) {

    var xhr = new XMLHttpRequest();
    xhr.open('GET', '/masters/ViewFile?fileName=' + filename, true);
    xhr.responseType = 'arraybuffer';
    xhr.onload = function (e) {
        if (this.status == 200) {
            var blob = new Blob([this.response], { type: "application/pdf" });

            const objectElement = document.getElementById('fileViewer');
            const url = URL.createObjectURL(blob);
            objectElement.src = url;
            //objectElement.width = '1000px';
            //objectElement.height = '1000px';
            //objectElement.type = 'text/plain';
            //var link = document.createElement('a');
            //link.href = window.URL.createObjectURL(blob);
            //link.download = "Report_" + new Date() + ".pdf";
            //link.click();
        }
    };
    xhr.send();
}
function loadItemMasterDocList() {
    var tablebody = $("#MasterDocList tbody");
    $(tablebody).html("");//empty tbody

    api.getbulk("/masters/GetAllItemMasterDocLists").then((data) => {
        for (i = 0; i < data.length; i++) {
            var tBody = ProcessTemplateDataNew("MasterDocRow", data[i]);
            $(tablebody).append(tBody);
            //console.log(tBody);
        }
    }).catch((error) => {
    });
}
function loadMasterContent() {
    var selElem = $('#MasterContent');
    selElem.html('');
    api.getbulk("/masters/ItemMasterContent").then((data) => {

        div_data = "<option value='" + 0 + "'>" + "--Select--" + "</option>";
        selElem.append(div_data);
        for (i = 0; i < data.length; i++) {
            div_data = "<option value='" + data[i].itemMasterContentId + "'>" + data[i].contentDesc + "</option>";
            selElem.append(div_data);
        }
    });
}
function loadDocTypes() {
    var selElem = $('#DocTypeName');
    selElem.html('');
    api.getbulk("/masters/DocTypes").then((data) => {

        div_data = "<option value='" + 0 + "'>" + "--Select--" + "</option>";
        selElem.append(div_data);
        for (i = 0; i < data.length; i++) {
            div_data = "<option value='" + data[i].documentTypeId + "'>" + data[i].documentName + "</option>";
            selElem.append(div_data);
        }
    });
}
function DeleteItemMasterDoc(element) {
    var relatedTarget = $(element);
    var masterdocid = relatedTarget.data("masterdocid");
    var doctypeId = relatedTarget.data("docname");
    api.get("/masters/CheckDocTypeInDocList?docTypeid=" + doctypeId).then((data) => {
        // loadDocType(); 
        if (data) {
            let confirmval = confirm("Are your sure you want to delete this ?", "Yes", "No");
            if (confirmval) {
                api.get("/masters/DeleteItemMasterDocList?itemMasterDocListId=" + masterdocid).then((data) => {
                    loadItemMasterDocList();
                }).catch((error) => {

                });
            }
        } else {
            alert("Deletion of This can be done after files with the Extn are deleted from the System");
        }
    }).catch((error) => {

    });
}
function EditItemMasterDoc(element) {
    var relatedTarget = $(element);
    var docname = relatedTarget.data("docname");
    var mandatory = relatedTarget.data("mandatory");
    var mastercontent = relatedTarget.data("mastercontent");
    var masterdocid = relatedTarget.data("masterdocid");
    var DocTypeName=$("#DocTypeName");
    var MasterContent = $("#MasterContent");
    $("#ItemDocId").val(masterdocid);
    DocTypeName.find("option[value='" + docname + "']").prop('selected', true);
    MasterContent.find("option[value='" + mastercontent + "']").prop('selected', true);
    if (mandatory == 'Y') {
        $("#DocUploadMandatory").prop("checked", true);
    }

}

function ProcessTemplateDataNew(templateId, dataObj) {
    //debugger;
    var templateElement = $("#" + templateId).html();
    ////console.log(templateId);
    templateElement = templateElement.replaceAll("{partType}", partType)
    for (var key in dataObj) {
        ////console.log(key + " " + dataObj[key]);
        templateElement = templateElement.replaceAll("{" + key + "}", dataObj[key])
    }
    // console.log(templateElement);
    return templateElement;
}

function loadMPDList() {
    var tablebody = $("#mptable tbody");
    $(tablebody).html("");//empty tbody
    //UpdatePurchaseDetailsTableFromPostData
    let i = 0;
    var strActive = $("#Status").val();
    if (dataMPDList.length > 2) {
        //console.log(partType);
        //console.log("================");
        let data = dataMPDList;
        for (i = 0; i < data.length; i++) {
            if (!(data[i]['masterPartType'] == partType))
                continue;
            if (!(data[i]['status'] == strActive))
                continue;
            var tBody = ProcessTemplateDataNew("MasterDetaiTemplate", data[i]);
            $(tablebody).append(tBody);
            //console.log(tBody);
        }
    }
    else {
        api.getbulk("/masters/masterparts").then((data) => {
            dataMPDList = data;
            for (i = 0; i < data.length; i++) {
                /*for (var key in data[i]) {
                    console.log(key);
                    console.log(data[i][key]);
                    console.log("*****");
                }*/
                //console.log(partType);
                //console.log("================");
                if (!(data[i]['masterPartType'] == partType))
                    continue;
                if (!(data[i]['status'] == strActive))
                    continue;
                var tBody = ProcessTemplateDataNew("MasterDetaiTemplate", data[i]);
                $(tablebody).append(tBody);
                //console.log(tBody);
            }
        }).catch((error) => {
        });
    }
}

function loadEditParts() {
    var tablebody = $("#mptable tbody");
    $(tablebody).html("");//empty tbody
    api.getbulk("/masters/masterparts").then((data) => {
        dataMPDList = data;
        for (i = 0; i < data.length; i++) {
           
            //if (!(data[i]['status'] == strActive))
            //    continue;
            var tBody = ProcessTemplateDataNew("MasterDetaiTemplate", data[i]);
            $(tablebody).append(tBody);
            //console.log(tBody);
        }
    }).catch((error) => {
    });
}

function loadManufAssem() {
    var tablebody = $("#Popup1Grid tbody");
    $(tablebody).html("");//empty tbody
    api.getbulk("/masters/ManufAssemCompLinq").then((data) => {
        let totalFinalPartsSold = 0;
        let totalManfActive = 0;
        let totalAssemActive = 0;
        let totalManfInactive = 0;
        let totalAssemInactive = 0;
        let totalManfWithoutRM = 0;
        let totalAssemWithoutBOM = 0;
        let totalMandatoryDocsNotUploaded = 0;
        let totalRoutingNotAvl = 0;

        for (let i = 0; i < data.length; i++) {
            const item = data[i];

            // Accumulate totals (assuming these fields exist and are numbers)
            totalFinalPartsSold += parseInt(item.finalPart) || 0;
            totalManfActive += parseInt(item.noOfManufActive) || 0;
            totalAssemActive += parseInt(item.noOfAssemblyActive) || 0;
            totalManfInactive += parseInt(item.noOfManufInActive) || 0;
            totalAssemInactive += parseInt(item.noOfAssemblyInActive) || 0;
            totalManfWithoutRM += parseInt(item.rmAvl) || 0;
            totalAssemWithoutBOM += parseInt(item.bomAvl) || 0;
            totalMandatoryDocsNotUploaded += parseInt(item.mandocAvl) || 0;
            totalRoutingNotAvl += parseInt(item.routingNotAvl) || 0;


            // Append the row data for each company
            $(tablebody).append(AppUtil.ProcessTemplateData("Popup1GridRow", item));
        }

        // Create the totals row with accumulated values
        const totalsRow = `
        <tr>
            <td>Total</td>
            <td>${totalFinalPartsSold}</td>
            <td>${totalManfActive}</td>
            <td>${totalAssemActive}</td>
            <td>${totalManfInactive}</td>
            <td>${totalAssemInactive}</td>
            <td>${totalManfWithoutRM}</td>
            <td>${totalAssemWithoutBOM}</td>
            <td>${totalMandatoryDocsNotUploaded}</td>
            <td>${totalRoutingNotAvl}</td>
            <td></td>
        </tr>
    `;

        // Append the totals row to the table body
        $(tablebody).append(totalsRow);
    }).catch((error) => {
    });
}
function loadManufAssemComp(company) {
    var tablebody = $("#VMOneGrid tbody");
    $(tablebody).html("");//empty tbody
    api.getbulk("/masters/ManufAssemlist?company=" + company).then((data) => {
        for (i = 0; i < data.length; i++) {
            $(tablebody).append(AppUtil.ProcessTemplateData("VMGridRow", data[i]));
        }
    }).catch((error) => {
    });
}
function BofList() {
    var tablebody = $("#P9Grid tbody");
    $(tablebody).html("");//empty tbody
    api.getbulk("/masters/AllBofList").then((data) => {
        for (i = 0; i < data.length; i++) {
            $(tablebody).append(AppUtil.ProcessTemplateData("P9GridRow", data[i]));
        }
    }).catch((error) => {
    });
}
function P7BofList(company) {
    var tablebody = $("#P7Grid tbody");
    $(tablebody).html("");//empty tbody
    api.getbulk("/masters/BofSumByComp?company=" + company).then((data) => {
        data = data.filter(item => item.company === company);
        for (i = 0; i < data.length; i++) {
            $(tablebody).append(AppUtil.ProcessTemplateData("P7GridRow", data[i]));
        }
    }).catch((error) => {
    });
}
function P2RMList(company) {
    var tablebody = $("#P3Grid tbody");
    $(tablebody).html("");//empty tbody
    api.getbulk("/masters/RmSupplierList?company=" + company).then((data) => {
        data = data.filter(item => item.company === company);
        for (i = 0; i < data.length; i++) {
            $(tablebody).append(AppUtil.ProcessTemplateData("P3GridRow", data[i]));
        }
    }).catch((error) => {
    });
}
function RMList() {
    var tablebody = $("#P5Grid tbody");
    $(tablebody).html("");//empty tbody
    api.getbulk("/masters/RMList").then((data) => {
        for (i = 0; i < data.length; i++) {
            $(tablebody).append(AppUtil.ProcessTemplateData("P5GridRow", data[i]));
        }
    }).catch((error) => {
    });
}
function loadViewDocumentType(partType) {
    api.getbulk("/masters/GetAllItemMasterDocLists").then((data) => {
        if (partType == "ManufacturedPart") {
            data = data.filter(item => item.contentId === 1);
            var selElem = $('#SelectDocType');
            selElem.html('');
            var rdiv_data = "<option value='" + 0 + "'>" + "--Select--" + "</option>";
            selElem.append(rdiv_data);
            for (i = 0; i < data.length; i++) {
                rdiv_data = "<option value='" + data[i].documentTypeId + "'>" + data[i].documentTypeName + "</option>";
                selElem.append(rdiv_data);
            }
        }
        if (partType == "RawMaterial") {
            data = data.filter(item => item.contentId === 4);
            var selElem = $('#SelectDocType');
            selElem.html('');
            var rdiv_data = "<option value='" + 0 + "'>" + "--Select--" + "</option>";
            selElem.append(rdiv_data);
            for (i = 0; i < data.length; i++) {
                rdiv_data = "<option value='" + data[i].documentTypeId + "'>" + data[i].documentTypeName + "</option>";
                selElem.append(rdiv_data);
            }
        }
        if (partType == "Standard BOF") {
            data = data.filter(item => item.contentId === 6);
            var selElem = $('#SelectDocType');
            selElem.html('');
            var rdiv_data = "<option value='" + 0 + "'>" + "--Select--" + "</option>";
            selElem.append(rdiv_data);
            for (i = 0; i < data.length; i++) {
                rdiv_data = "<option value='" + data[i].documentTypeId + "'>" + data[i].documentTypeName + "</option>";
                selElem.append(rdiv_data);
            }
        }
        if (partType == "BOF") {
            data = data.filter(item => item.contentId === 7);
            var selElem = $('#SelectDocType');
            selElem.html('');
            var rdiv_data = "<option value='" + 0 + "'>" + "--Select--" + "</option>";
            selElem.append(rdiv_data);
            for (i = 0; i < data.length; i++) {
                rdiv_data = "<option value='" + data[i].documentTypeId + "'>" + data[i].documentTypeName + "</option>";
                selElem.append(rdiv_data);
            }
        }
        if (partType == "Own Purchased RM") {
            data = data.filter(item => item.contentId === 4 || item.contentId ==5);
            var selElem = $('#SelectDocType');
            selElem.html('');
            var rdiv_data = "<option value='" + 0 + "'>" + "--Select--" + "</option>";
            selElem.append(rdiv_data);
            for (i = 0; i < data.length; i++) {
                rdiv_data = "<option value='" + data[i].documentTypeId + "'>" + data[i].documentTypeName + "</option>";
                selElem.append(rdiv_data);
            }
        }
    }).catch((error) => {
    });
}
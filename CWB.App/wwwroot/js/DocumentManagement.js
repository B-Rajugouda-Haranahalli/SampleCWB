var gobaldoctypename = "";
var gobalExtnName = "";
function loadDocCategory() {
    var selElem = $('#DTDDocCat');
    selElem.html('');
    api.getbulk("/DocumentManagement/GetAllDocCategory").then((data) => {

        div_data = "<option value='" + 0 + "'>" + "--Select--" + "</option>";
        selElem.append(div_data);
        for (i = 0; i < data.length; i++) {
            div_data = "<option value='" + data[i].docCategoryId + "'>" + data[i].docCategoryDesc + "</option>";
            selElem.append(div_data);
        }
    });
}
function loadFileExtnSelect() {
    var FileSelElem = $('#DTDFileExtn');
    var SearchFileExtn = $('#SearchFileExtn');
    FileSelElem.html("");
    SearchFileExtn.html("");
    //SearchFileExtn.html("");
    var tablebody = $("#fileExtensionGrid tbody");
    $(tablebody).html("");//empty tbody
    api.getbulk("/DocumentManagement/GetAllFileExtn").then((data) => {

        div_data = "<option value='" + 0 + "'>" + "--Select--" + "</option>";
        FileSelElem.append(div_data);
        SearchFileExtn.append(div_data);
        for (i = 0; i < data.length; i++) {
            div_data = "<option value='" + data[i].extnId + "'>" + data[i].extnName + "</option>";
            FileSelElem.append(div_data);
            SearchFileExtn.append(div_data);
            //console.log(data);
            //for (j = 0; j < data.length; j++) {
                $(tablebody).append(AppUtil.ProcessTemplateData("FileExtensionGridRow", data[i]));
            //}
        }
    });
}

function loadDocType() {
    api.getbulk("/DocumentManagement/GetAllDocumentType").then((data) => {
        //data = data.filter(item => item.status !== 6);
        var tablebody = $("#DocTypeGrid tbody");
        $(tablebody).html("");//empty tbody
        //console.log(data);
        for (i = 0; i < data.length; i++) {
            $(tablebody).append(AppUtil.ProcessTemplateData("DocumentTypeRow", data[i]));
        }
    }).catch((error) => {
    });
}

function LoadDepartment() {
    api.getbulk("/DocumentManagement/GetDepartMent").then((data) => {
        //data = data.filter(item => item.status !== 6);
        var tablebody = $("#DepartmentUpload tbody");
        $(tablebody).html("");//empty tbody
        var tablebody2 = $("#DepartmentView tbody");
        $(tablebody2).html("");//empty tbody
        //console.log(data);
        for (i = 0; i < data.length; i++) {
            $(tablebody).append(AppUtil.ProcessTemplateData("DepartmentUploadRow", data[i]));
            $(tablebody2).append(AppUtil.ProcessTemplateData("DepartmentViewRow", data[i]));
        }
    }).catch((error) => {
    });
}

function loadCustRetData() {
    api.getbulk("/DocumentManagement/GetAllCustRet").then((data) => {
        //data = data.filter(item => item.status !== 6);
        var tablebody = $("#rpdcCustomerRetGrid tbody");
        $(tablebody).html("");//empty tbody
        //console.log(data);
        for (i = 0; i < data.length; i++) {
            $(tablebody).append(AppUtil.ProcessTemplateData("CustomerRetentionRow", data[i]));
        }
    }).catch((error) => {
    });
}

function SelectDocType() {
    var selElem = $('#CDRPDocTypeName');
    var CDRPRetPerYear = $('#CDRPRetPerYear');
    var CDRPRetPerMon = $('#CDRPRetPerMon');
    var CDRPCustomerSelElem = $('#CDRPCustomer');
    api.getbulk("/DocumentManagement/GetAllDocumentType").then((data) => {
        selElem.html('');
        div_data = "<option value='" + 0 + "'>" + "--Select--" + "</option>";
        selElem.append(div_data);
        for (i = 0; i < data.length; i++) {
            div_data = "<option value='" + data[i].documentTypeId + "'>" + data[i].documentName + "</option>";
            selElem.append(div_data);
            //Yeardiv_data = "<option value='" + data[i].defaultRetPerYear + "'>" + data[i].defaultRetPerYear + "</option>";
            //CDRPRetPerYear.html('');
            //CDRPRetPerYear.append(Yeardiv_data);
            //MonYeardiv_data = "<option value='" + data[i].defaultRetPerMon + "'>" + data[i].defaultRetPerMon + "</option>";
            //CDRPRetPerMon.html('');
            //CDRPRetPerMon.append(MonYeardiv_data);
        }
    });
    api.getbulk("/Contacts/Companies").then((data) => {
        CDRPCustomerSelElem.html('');
        div_data = "<option value='" + 0 + "'>" + "--Select--" + "</option>";
        CDRPCustomerSelElem.append(div_data);

        for (i = 0; i < data.length; i++) {
            div_data = "<option value='" + data[i].companyId + "'>" + data[i].companyName + "</option>";
            CDRPCustomerSelElem.append(div_data);
        }
    });
}


$(document).ready(function () {
    loadDocType();
    loadFileExtnSelect();
    loadDocCategory();
    $("#ClearSearchFields").on("click", function () {
        $("#DocViewListGrid tbody tr").show();
        $("#SeacrchUploaddept").val('');
        $("#SearchDataEntryFormat").prop('checked', false);
        $("#SearchDataEntryShare").prop('checked', false);
        var SearchFileExtn = $('#SearchFileExtn');
        SearchFileExtn.val(0).trigger('change');
    });
    $("#SearchFileExtn").select2();
    $("#DTDFileExtn").select2({
        dropdownParent: $("#doc-type-detail")
    });
    $("#DTDDocCat").select2({
        dropdownParent: $("#doc-type-detail")
    });
    $("#rpdcCustomerSelect").select2({
        dropdownParent: $("#rpdcCustomerPopUp")
    });
    $("#CDRPDocTypeName").select2({
        dropdownParent: $("#CDRP")
    });
    $("#CDRPCustomer").select2({
        dropdownParent: $("#CDRP")
    });
    //rpdcDocTypeName
    $("#SearchDocTypeName").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#DocTypeGrid tbody tr").filter(function () {
            $(this).toggle($(this.children[1]).text().toLowerCase().indexOf(value) > -1)
        });
    });
    $("#SeacrchUploaddept").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#DocTypeGrid tbody tr").filter(function () {
            $(this).toggle($(this.children[4]).text().toLowerCase().indexOf(value) > -1)
        });
    });

    $("#DTDDocCat").on("change", function () {
        var selectedValue = $(this).val().toLowerCase();
        if (selectedValue == "1" || selectedValue =="0") {
            $("#RetPeriodDiv").hide();
           // $("#RetPeriodDiv").hide();
            $("#DTDDataCustDiv").hide();
        } else {
            $("#DTDDataCustDiv").show();
        }
       
    });
    $("#SearchFileExtn").on("change", function () {
        var selectedValue = $(this).val().toLowerCase();
        if (selectedValue == "0") {
            $("#DocTypeGrid tbody tr").show();
            return;
        }
        $("#DocTypeGrid tbody tr").filter(function () {
            $(this).toggle($(this.children[11]).text().toLowerCase().indexOf(selectedValue) > -1)
        });// show only the filtered rows
    });

    $('#SearchDataEntryFormat').on('click', function () {
        if ($(this).is(':checked')) {
            var v = 'Y';
            var value = v.toLowerCase();
            $("#DocTypeGrid tbody tr").filter(function () {
                $(this).toggle($(this.children[2]).text().toLowerCase().indexOf(value) > -1)
            });
        } else {
            $("#DocTypeGrid tbody tr").show(); // show all rows when checkbox is unchecked
        }
    });
    $('#SearchDataEntryShare').on('click', function () {
        if ($(this).is(':checked')) {
            var v = 'Y';
            var value = v.toLowerCase();
            $("#DocTypeGrid tbody tr").filter(function () {
                $(this).toggle($(this.children[3]).text().toLowerCase().indexOf(value) > -1)
            });
        } else {
            $("#DocTypeGrid tbody tr").show(); // show all rows when checkbox is unchecked
        }
    });




    $("#rpdcDocTypeName").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#rpdcCustomerRetGrid tbody tr").filter(function () {
            $(this).toggle($(this.children[0]).text().toLowerCase().indexOf(value) > -1)
        });
    });

    $("#rpdcCustomerSelect").on("change", function () {
        var selectedValue = $(this).val().toLowerCase();
        if (selectedValue == "0") {
            $("#rpdcCustomerRetGrid tbody tr").show();
            return;
        }
        $("#rpdcCustomerRetGrid tbody tr").filter(function () {
            $(this).toggle($(this.children[1]).text().toLowerCase().indexOf(selectedValue) > -1)
        });// show only the filtered rows
    });

    $('#CDRP').on('hidden.bs.modal', function (event) {
        var selElem = $('#CDRPDocTypeName');
        var CDRPRetPerYear = $('#CDRPRetPerYear');
        var CDRPRetPerMon = $('#CDRPRetPerMon');
        var CDRPCustomerSelElem = $('#CDRPCustomer');
        selElem.html('');
        CDRPCustomerSelElem.html('');
        CDRPRetPerYear.val(0);
        //CDRPRetPerYear.find("option[value='']").prop('selected', true);
        CDRPRetPerMon.find("option[value='']").prop('selected', true);
        document.getElementById('rpdcCustomerPopUp').style.filter = 'none';

    });

    $('#CDRP').on('shown.bs.modal', function (event) {
        //SelectDocType();
        document.getElementById('rpdcCustomerPopUp').style.filter = 'blur(5px)';
        var relatedTarget = $(event.relatedTarget);
        var custretnid = relatedTarget.data("custretnid");
        var doctypename = relatedTarget.data("doctypename");
        var customerid = relatedTarget.data("customerid");
        var doctypenameid = relatedTarget.data("doctypenameid");
        var retperyear = relatedTarget.data("retperyear");
        var retpermon = relatedTarget.data("retpermon");
        if (custretnid) {
            $("#CDRPCustRetId").val(custretnid);
            var selElem = $('#CDRPDocTypeName');
            var CDRPRetPerYear = $('#CDRPRetPerYear');
            var CDRPRetPerMon = $('#CDRPRetPerMon');
            var CDRPCustomerSelElem = $('#CDRPCustomer');
            api.getbulk("/DocumentManagement/GetAllDocumentType").then((data) => {
                selElem.html('');
                div_data = "<option value='" + 0 + "'>" + "--Select--" + "</option>";
                selElem.append(div_data);
                for (i = 0; i < data.length; i++) {
                    div_data = "<option value='" + data[i].documentTypeId + "'>" + data[i].documentName + "</option>";
                    selElem.append(div_data);
                }
                //CDRPRetPerYear.find("option[value='" + retperyear + "']").prop('selected', true);
                CDRPRetPerMon.find("option[value='" + retpermon + "']").prop('selected', true);
                selElem.find("option[value='" + doctypenameid + "']").prop('selected', true);
                $("#CDRPRetPerYear").val(retperyear);
                //$("#CDRPRetPerMon").val(retpermon);
            });
            api.getbulk("/Contacts/Companies").then((data) => {
                CDRPCustomerSelElem.html('');
                div_data = "<option value='" + 0 + "'>" + "--Select--" + "</option>";
                CDRPCustomerSelElem.append(div_data);
                for (i = 0; i < data.length; i++) {
                    div_data = "<option value='" + data[i].companyId + "'>" + data[i].companyName + "</option>";
                    CDRPCustomerSelElem.append(div_data);
                    CDRPCustomerSelElem.find("option[value='" + customerid + "']").prop('selected', true);
                }
            });
        } else {
            SelectDocType();
            var CDRPRetPerMon = $('#CDRPRetPerMon');
            CDRPRetPerMon.find("option[value='" + 6 + "']").prop('selected', true);
        }

    });

    $('#rpdcCustomerPopUp').on('shown.bs.modal', function (event) {
        loadCustRetData();
        var rpdcCustomerSelect = $('#rpdcCustomerSelect');
        rpdcCustomerSelect.html('');
        api.getbulk("/Contacts/Companies").then((data) => {
            div_data = "<option value='" + 0 + "'>" + "--Select--" + "</option>";
            rpdcCustomerSelect.append(div_data);
            for (i = 0; i < data.length; i++) {
                div_data = "<option value='" + data[i].companyName + "'>" + data[i].companyName + "</option>";
                rpdcCustomerSelect.append(div_data);
            }
        });
    });

    $('#doc-type-detail').on('hidden.bs.modal', function (event) {
        $("#DTDTypeName").val('');
        $("#DTDDocTypeId").val('');
        $("#DTDDocCat").val('');
        $("#DTDRetYear").val(0);
        $("#DTDRetMonth").val('');
        $("#DTDFileExtn").val('');
        var newNamevalidate = document.getElementById('fileExtensionNameEdit');
        newNamevalidate.style.border = '';
        var newDTDTypeNameNamevalidate = document.getElementById('DTDTypeName');
        newDTDTypeNameNamevalidate.style.border = '';

        var DTDRetYear = document.getElementById('DTDRetYear');
        DTDRetYear.style.border = '';

        var DTDRetMonth = document.getElementById('DTDRetMonth');
        DTDRetMonth.style.border = '';
        gobaldoctypename = "";
        var tablebody = $("#DocTypePopupExtngrid tbody");
        $(tablebody).html("");
    });
    // Capture the checkbox click event for a checkbox with ID "myCheckbox"
    $("#DTDDataCust").click(function () {
        if (this.checked) {
            $("#RetPeriodDiv").show();
        } else {
            $("#RetPeriodDiv").hide();
        }
    });
    $('#doc-type-detail').on('shown.bs.modal', function (event) {
        $("#AddDocType").prop('disabled', false);

        var relatedTarget = $(event.relatedTarget);
        var doctypeid = relatedTarget.data("doctypeid");
        var doctypename = relatedTarget.data("doctypename");
        var docdatareqdcust = relatedTarget.data("docdatareqdcust");
        var doccatid = relatedTarget.data("doccatid");
        var fileextnid = relatedTarget.data("fileextnid");
        var retperyear = relatedTarget.data("retperyear");
        var retpermon = relatedTarget.data("retpermon");
        var tabledata = [];
        if (doctypeid) {
            $("#DocTypePopupExtngrid").show();
            LoadDepartment();
            $("#DTDDocTypeId").val(doctypeid);
            $("#DTDTypeName").val(doctypename);
            if (docdatareqdcust == "N") {
                $("#DTDDataCust").prop('checked', false);
                $("#RetPeriodDiv").hide();
            } else {
                $("#DTDDataCust").prop('checked', true);
                $("#RetPeriodDiv").show();
            }
            if (doccatid == 1) {
                $("#RetPeriodDiv").hide();
                // $("#RetPeriodDiv").hide();
                $("#DTDDataCustDiv").hide();
            } else {
                $("#DTDDataCustDiv").show();
            }
            var DTDDocCat = $('#DTDDocCat');
            var DTDFileExtn = $('#DTDFileExtn');
            var DTDRetMonth = $('#DTDRetMonth');
            var filenamedata = "";
            DTDFileExtn.html("");
            DTDDocCat.html("");
            api.getbulk("/DocumentManagement/GetAllFileExtn").then((data) => {

                div_data = "<option value='" + 0 + "'>" + "--Select--" + "</option>";
                DTDFileExtn.append(div_data);
                for (i = 0; i < data.length; i++) {
                    if (data[i].extnId === fileextnid) {
                        filenamedata = data[i].extnName;
                    }
                    div_data = "<option value='" + data[i].extnId + "'>" + data[i].extnName + "</option>";
                    DTDFileExtn.append(div_data);
                }
                DTDFileExtn.find("option[value='" + fileextnid + "']").prop('selected', true);
                var rowData = {
                    documentTypeId: doctypeid,
                    documentName: doctypename,
                    fileExtnName: filenamedata,
                    extnId: fileextnid
                };
                tabledata.push(rowData);
                var tablebody = $("#DocTypePopupExtngrid tbody");
                $(tablebody).html("");//empty tbody
                //console.log(data);
                for (i = 0; i < tabledata.length; i++) {
                    $(tablebody).append(AppUtil.ProcessTemplateData("ExtnGridRow", tabledata[i]));
                }
                api.get("/DocumentManagement/NoFilesExtn?extn=" + filenamedata).then((data) => {
                    // loadDocType(); 
                    if (!data) {
                        $("#delete-extn-link").css("pointer-events", "auto");
                        $("#delete-extn-link").css("color", "");

                    } else {
                        // Disable the anchor tag
                        $("#delete-extn-link").css("pointer-events", "none");
                        $("#delete-extn-link").css("color", "grey");
                    }
                }).catch((error) => {

                });
            });
            api.getbulk("/DocumentManagement/GetAllDocCategory").then((data) => {

                div_data = "<option value='" + 0 + "'>" + "--Select--" + "</option>";
                DTDDocCat.append(div_data);
                for (i = 0; i < data.length; i++) {
                    div_data = "<option value='" + data[i].docCategoryId + "'>" + data[i].docCategoryDesc + "</option>";
                    DTDDocCat.append(div_data);
                }
                DTDDocCat.find("option[value='" + doccatid + "']").prop('selected', true);

            });

            api.getbulk("/DocumentManagement/GetAllDocViewDepartMent?docTypeId="+doctypeid).then((vdata) => {
                var departmentIds = vdata;
                $('#DepartmentView tbody tr').each(function () {
                    var departmentId = $(this).find('td:eq(2)').text();
                    if (departmentIds.some(obj => obj.departmentId === parseInt(departmentId))) {
                        $(this).find('input[type="checkbox"]').prop('checked', true);
                    }
                });
            });

            api.getbulk("/DocumentManagement/GetAllDocUploadDepartMent?docTypeId=" + doctypeid).then((udata) => {
                var departmentIds = udata;
                $('#DepartmentUpload tbody tr').each(function () {
                    var departmentId = $(this).find('td:eq(2)').text();
                    if (departmentIds.some(obj => obj.departmentId === parseInt(departmentId))) {
                        $(this).find('input[type="checkbox"]').prop('checked', true);
                    }
                });
            });
            $('#DTDRetYear').val(retperyear);
            DTDRetMonth.find("option[value='" + retpermon + "']").prop('selected', true);

        }
        else {
            loadDocCategory();
            loadFileExtnSelect();
            LoadDepartment();
            var DTDRetMonth = $('#DTDRetMonth');
            DTDRetMonth.find("option[value='" + 6 + "']").prop('selected', true);
            $("#DocTypePopupExtngrid").hide();
        }
        gobaldoctypename = doctypename;
    });

    $("#AddDocType").on("click", function () {
        var DocTypeName = $("#DTDTypeName").val();
        var DTDDocTypeId = parseInt($("#DTDDocTypeId").val());
        var DTDDocCat = parseInt($("#DTDDocCat").val());
        var DTDRetYear = parseInt($("#DTDRetYear").val());
        var DTDRetMonth = parseInt($("#DTDRetMonth").val());
        var DTDFileExtn = parseInt($("#DTDFileExtn").val());
        var checkbox = document.getElementById("DTDDataCust");
        var DataReqdByCust = 'N';
        if (DocTypeName.length <= 0) {
            var newNamevalidate = document.getElementById('DTDTypeName');
            newNamevalidate.style.border = '2px solid red';
            return false;
        }
        if (isNaN(DTDDocCat) || DTDDocCat === 0) {
            var newNamevalidate = $('#DTDDocCat').next('.select2-container');
            newNamevalidate.css('border', '2px solid red');
            return false;
        } else {
            var newNamevalidate = $('#DTDDocCat').next('.select2-container');
            newNamevalidate.css('border', '');
        }
        if (DTDDocCat === 1) {
            DTDRetMonth = 0;
            DTDRetYear = 0;
        }
        if (isNaN(DTDRetYear)) {
            var newNamevalidate = document.getElementById('DTDRetYear');
            newNamevalidate.style.border = '2px solid red';
            return false;
        }
        if (isNaN(DTDFileExtn) || DTDFileExtn === 0) {
            //document.getElementById('DTDFileExtn-error').textContent = 'This field is required.';
            var newNamevalidate = $('#DTDFileExtn').next('.select2-container');
            newNamevalidate.css('border', '2px solid red');
            return false;
        } else {
            //document.getElementById('DTDFileExtn-error').textContent = '';
            var newNamevalidate = $('#DTDFileExtn').next('.select2-container');
            newNamevalidate.css('border', '');
        }
        if (isNaN(DTDDocTypeId)) {
            DTDDocTypeId = 0;
        }
        if (checkbox.checked) {
            DataReqdByCust = 'Y';
        } else {
            DTDRetMonth = 6;
            DTDRetYear = 0;
        }
        if (isNaN(DTDRetMonth)) {
            var newNamevalidate = document.getElementById('DTDRetMonth');
            newNamevalidate.style.border = '2px solid red';
            return false;
        }
        var rowData = {
            documentTypeId: DTDDocTypeId,
            documentName: DocTypeName,
            extnId: DTDFileExtn,
            allowDelete:'N',
            docuCategory: DTDDocCat,
            dataReqdByCust: DataReqdByCust,
            defaultRetPerMon: DTDRetMonth,
            defaultRetPerYear: DTDRetYear,
            retentionDays:0
        };
        api.getbulk("/DocumentManagement/CheckDocTypeName?docTypeName=" + DocTypeName).then((data) => {
            //console.log(data);
            // if (DTDDocTypeId === 0) {
            if (!data || gobaldoctypename === DocTypeName) {
                $("#DTDTypeName-error").text("");
                    $.ajax({
                        type: "POST",
                        url: '/DocumentManagement/PostDocumentType',
                        contentType: "application/json; charset=utf-8",
                        headers: { 'Content-Type': 'application/json' },
                        data: JSON.stringify(rowData),
                        dataType: "json",
                        success: function (result) {
                            var doctypeId = result.documentTypeId;
                            $("#DTDDocTypeId").val(doctypeId);
                            var newDTDTypeNameNamevalidate = document.getElementById('DTDTypeName');
                            newDTDTypeNameNamevalidate.style.border = '';

                            var DTDRetYear = document.getElementById('DTDRetYear');
                            DTDRetYear.style.border = '';

                            var DTDRetMonth = document.getElementById('DTDRetMonth');
                            DTDRetMonth.style.border = '';
                            loadDocType();

                            alert("Document Type Has Added To List");
                            // $('#doc-type-detail').modal('hide');
                            $("#AddDocType").prop('disabled', true);
                            var tabledata = [];
                            var filenamedata = $("#DTDFileExtn option:selected").text();
                            var rowData = {
                                documentTypeId: DocTypeName,
                                documentName: DocTypeName,
                                fileExtnName: filenamedata,
                                extnId: DTDFileExtn
                            };
                            $("#DocTypePopupExtngrid").show();
                            tabledata.push(rowData);
                            var tablebody = $("#DocTypePopupExtngrid tbody");
                            $(tablebody).html("");//empty tbody
                            for (i = 0; i < tabledata.length; i++) {
                                $(tablebody).append(AppUtil.ProcessTemplateData("ExtnGridRow", tabledata[i]));
                            }
                            api.get("/DocumentManagement/NoFilesExtn?extn=" + filenamedata).then((data) => {
                                // loadDocType(); 
                                if (!data) {
                                    $("#delete-extn-link").css("pointer-events", "auto");
                                    $("#delete-extn-link").css("color", "");

                                } else {
                                    // Disable the anchor tag
                                    $("#delete-extn-link").css("pointer-events", "none");
                                    $("#delete-extn-link").css("color", "grey");
                                }
                            }).catch((error) => {

                            });
                        }
                    });
                } else {
                $("#DTDTypeName-error").text("This Document Type Name Already Exists. Please Enter A Different Name");
                }
        });
    });

    $("#FileExtensionSave").on("click", function () {
        var fileExtensionEditId = parseInt($("#fileExtensionEditId").val());
        var fileExtensionNameEdit = $("#fileExtensionNameEdit").val();

        if (fileExtensionNameEdit.length <= 0) {
            var newNamevalidate = document.getElementById('fileExtensionNameEdit');
            newNamevalidate.style.border = '2px solid red';
            return false;
        }
        if (isNaN(fileExtensionEditId)) {
            fileExtensionEditId = 0;
        }
        var rowData = {
            extnId: fileExtensionEditId,
            extnName: fileExtensionNameEdit
        };
        api.getbulk("/DocumentManagement/CheckExtnName?extnName=" + fileExtensionNameEdit).then((data) => {
            //console.log(data);
            // if (DTDDocTypeId === 0) {
            if (!data) {
                $.ajax({
                    type: "POST",
                    url: '/DocumentManagement/PostFileExtn',
                    contentType: "application/json; charset=utf-8",
                    headers: { 'Content-Type': 'application/json' },
                    data: JSON.stringify(rowData),
                    dataType: "json",
                    success: function (result) {
                        $("#fileExtensionEditId").val('');
                        $("#fileExtensionNameEdit").val('');
                        loadFileExtnSelect();
                    }
                });
            } else {
                $("#FileName-error").text("This File Extension Already Exists. Please Enter A Different File Extension");
                $("#fileExtensionEditId").val(0);
            }
        });

    });

    $("#CDRPCustRetSave").on("click", function () {
        var CDRPCustRetId = parseInt($("#CDRPCustRetId").val());
        var CDRPDocTypeName = parseInt($("#CDRPDocTypeName").val());
        var CDRPCustomer = parseInt($("#CDRPCustomer").val());
        var CDRPRetPerYear = parseInt($("#CDRPRetPerYear").val());
        var CDRPRetPerMon = parseInt($("#CDRPRetPerMon").val());
        if (isNaN(CDRPCustRetId)) {
            CDRPCustRetId = 0;
        }
        if (isNaN(CDRPDocTypeName) || CDRPDocTypeName === 0) {
            var newNamevalidate = $('#CDRPDocTypeName').next('.select2-container');
            newNamevalidate.css('border', '2px solid red');
            return false;
        } else {
            var newNamevalidate = $('#CDRPDocTypeName').next('.select2-container');
            newNamevalidate.css('border', '');
        }
        if (isNaN(CDRPCustomer) || CDRPCustomer == 0) {
            var newNamevalidate = $('#CDRPCustomer').next('.select2-container');
            newNamevalidate.css('border', '2px solid red');
            return false;
        } else {
            var newNamevalidate = $('#CDRPCustomer').next('.select2-container');
            newNamevalidate.css('border', '');
        }
        if (isNaN(CDRPRetPerYear)) {
            var newNamevalidate = document.getElementById('CDRPRetPerYear');
            newNamevalidate.style.border = '2px solid red';
            return false;
        }
        if (isNaN(CDRPRetPerMon)) {
            var newNamevalidate = document.getElementById('CDRPRetPerMon');
            newNamevalidate.style.border = '2px solid red';
            return false;
        }
        var rowData = {
            custRetnDataId: CDRPCustRetId,
            documentTypeId: CDRPDocTypeName,
            comapanyId: CDRPCustomer,
            retPerMon: CDRPRetPerMon,
            retPerYear: CDRPRetPerYear
        };
        $.ajax({
            type: "POST",
            url: '/DocumentManagement/PostCustRetndata',
            contentType: "application/json; charset=utf-8",
            headers: { 'Content-Type': 'application/json' },
            data: JSON.stringify(rowData),
            dataType: "json",
            success: function (result) {
                var CustRetnDataId = result.custRetnDataId;
                $("#CDRPCustRetId").val(CustRetnDataId);
                var newNamevalidateYr = document.getElementById('CDRPRetPerYear');
                var newNamevalidateMn = document.getElementById('CDRPRetPerMon');
                newNamevalidateYr.style.border = '';
                newNamevalidateMn.style.border = '';
                loadCustRetData();
                loadDocType();
                $('#CDRP').modal('hide');
            }
        });
    });

    $("#DocUploadSave").on("click", function () {
        var selectedRowsData = {};
        var selectedRowsDataView = {};
        var deptupload = {};
        var deptView = {};
        var doctypeId = $("#DTDDocTypeId").val();
        if (doctypeId === "" || isNaN(doctypeId)) {
            alert("Please Save Document Type.");
            return false;
        } 
        var DeptUploadcheckboxes = $("#DepartmentUpload tbody input[type='checkbox']:checked");
        var DeptViewcheckboxes = $("#DepartmentView tbody input[type='checkbox']:checked");
        DeptUploadcheckboxes.each(function (index, checkbox) {
            var row = checkbox.parentNode.parentNode;
            var rowData = {
                DocumentTypeId: parseInt(doctypeId),
                DepartmentId: parseInt($(row).find("td:eq(2)").text()),
            };
            deptupload[rowData.documentTypeId] = rowData;
        });
        DeptViewcheckboxes.each(function (index, checkbox) {
            var row = checkbox.parentNode.parentNode;
            var rowData = {
                DocumentTypeId: parseInt(doctypeId),
                DepartmentId: parseInt($(row).find("td:eq(2)").text()),
            };
            deptView[rowData.documentTypeId] = rowData;
        });
        selectedRowsData = Object.values(deptupload);
        selectedRowsDataView = Object.values(deptView);
        if (selectedRowsData.length > 0) {

            $.ajax({
                type: "POST",
                url: '/DocumentManagement/PostDocUpload',
                contentType: "application/json; charset=utf-8",
                headers: { 'Content-Type': 'application/json' },
                data: JSON.stringify(selectedRowsData),
                dataType: "json",
                success: function (result) {
                    //console.log(result);
                    loadDocType();
                    //$('#doc-type-detail').modal('hide');

                }
            });
        }
        else {
           
        }
        if (selectedRowsDataView.length > 0) {

            $.ajax({
                type: "POST",
                url: '/DocumentManagement/PostDocView',
                contentType: "application/json; charset=utf-8",
                headers: { 'Content-Type': 'application/json' },
                data: JSON.stringify(selectedRowsDataView),
                dataType: "json",
                success: function (result) {
                    //console.log(result);
                    loadDocType();
                    $('#doc-type-detail').modal('hide');
                }
            });
        }
        else {
            alert("Select at least 1 more viewing Department");
        }

    });

    $('#FileExtnPopup').on('shown.bs.modal', () => {
        document.getElementById('doc-type-detail').style.filter = 'blur(5px)'; // adjust the blur value as needed
    });

    $('#FileExtnPopup').on('hidden.bs.modal', () => {
        document.getElementById('doc-type-detail').style.filter = 'none';
        gobalExtnName = "";
        $("#FileName-error").text("");
        $("#fileExtensionNameEdit").val('');
        $("#fileExtensionEditId").val(0);
    });
    $('#RefReasonPop').on('hidden.bs.modal', () => {
        var newNamevalidate = document.getElementById('DocReason');
        newNamevalidate.style.border = '';
        $("#DocReason").val('');
        $("#DocReasonId").val(0);
    });

    $('#RefReasonPop').on('shown.bs.modal', () => {
        api.getbulk("/DocumentManagement/GetAllRefReson").then((data) => {
            var tablebody = $("#RefDocGrid tbody");
            $(tablebody).html("");//empty tbody
            //console.log(data);
            for (i = 0; i < data.length; i++) {
                $(tablebody).append(AppUtil.ProcessTemplateData("RefDocGridRow", data[i]));
            }

        }).catch((error) => {

        });
    });

    $("#RefReasonSave").on("click", function () {
        var fileExtensionEditId = parseInt($("#DocReasonId").val());
        var fileExtensionNameEdit = $("#DocReason").val();

        if (fileExtensionNameEdit.length <= 0) {
            var newNamevalidate = document.getElementById('DocReason');
            newNamevalidate.style.border = '2px solid red';
            return false;
        } else {
            var newNamevalidate = document.getElementById('DocReason');
            newNamevalidate.style.border = '';
        }
        if (isNaN(fileExtensionEditId)) {
            fileExtensionEditId = 0;
        }
        var rowData = {
            refDocReasonListId: fileExtensionEditId,
            docReason: fileExtensionNameEdit
        };
        //api.getbulk("/DocumentManagement/CheckExtnName?extnName=" + fileExtensionNameEdit).then((data) => {
            //console.log(data);
            // if (DTDDocTypeId === 0) {
            /*if (!data) {*/
                $.ajax({
                    type: "POST",
                    url: '/DocumentManagement/PostDocReason',
                    contentType: "application/json; charset=utf-8",
                    headers: { 'Content-Type': 'application/json' },
                    data: JSON.stringify(rowData),
                    dataType: "json",
                    success: function (result) {
                        $("#DocReason").val('');
                        $("#DocReasonId").val(0);
                        api.getbulk("/DocumentManagement/GetAllRefReson").then((data) => {
                            var tablebody = $("#RefDocGrid tbody");
                            $(tablebody).html("");//empty tbody
                            //console.log(data);
                            for (i = 0; i < data.length; i++) {
                                $(tablebody).append(AppUtil.ProcessTemplateData("RefDocGridRow", data[i]));
                            }

                        }).catch((error) => {

                        });
                    }
                });
            //} else {
            //    $("#FileName-error").text("This File Extension Already Exists. Please Enter A Different File Extension");
            //    $("#fileExtensionEditId").val(0);
            //}
        //});

    });

});

function EditCDRP(element) {
    //SelectDocType();
    //$('#CDRP').modal('show');
    
    //var relatedTarget = $(element);
    //    var custretnid = relatedTarget.data("custretnid");
    //    var doctypename = relatedTarget.data("doctypename");
    //    var customerid = relatedTarget.data("customerid");
    //    var doctypenameid = relatedTarget.data("doctypenameid");
    //    var retperyear = relatedTarget.data("retperyear");
    //var retpermon = relatedTarget.data("retpermon");
    //$('#CDRP').on('shown.bs.modal', function (event) {
    //    if (custretnid) {
    //        $("#CDRPCustRetId").val(custretnid).trigger('change');
    //        $("#CDRPDocTypeName").val(doctypenameid).trigger('change');
    //        $("#CDRPCustomer").val(doctypenameid).trigger('change');
    //        $("#CDRPRetPerYear").val(retperyear).trigger('change');
    //        $("#CDRPRetPerMon").val(CDRPRetPerMon).trigger('change');
    //    }
    //});

}
function EditRefDoc(element) {
    var relatedTarget = $(element);
    var fileextnid = relatedTarget.data("refdocid");
    var fileextnname = relatedTarget.data("docreason");
    $("#DocReason").val(fileextnname);
    $("#DocReasonId").val(fileextnid);

}
function EditFileExtnsion(element) {
    var relatedTarget = $(element);
    var fileextnid = relatedTarget.data("fileextnid");
    var fileextnname = relatedTarget.data("fileextnname");
    gobalExtnName = fileextnname;
    $("#fileExtensionNameEdit").val(fileextnname);
    $("#fileExtensionEditId").val(fileextnid);

}


function DeleteCustRetData(custRetId) {
    let confirmval = confirm("Are your sure you want to delete this ?", "Yes", "No");
    if (confirmval) {
        api.get("/DocumentManagement/DeleteCustRetData?custRetId=" + custRetId).then((data) => {
            loadCustRetData();
        }).catch((error) => {

        });
    }
};
function DeleteDocType(doctypeId) {
    api.get("/masters/CheckDocTypeInDocList?docTypeid=" + doctypeId).then((data) => {
        // loadDocType(); 
        if (data) {
            let confirmval = confirm("Are your sure you want to delete this ?", "Yes", "No");
            if (confirmval) {
                api.get("/DocumentManagement/DeleteDocType?doctypeId=" + doctypeId).then((data) => {
                    loadDocType();
                }).catch((error) => {

                });
            }
        } else {
            alert("Deletion of File Extn can be done after files with the Extn are deleted from the System");
        }
    }).catch((error) => {

    });
};
function DeleteExtn(fileExtnName, extnId) {
    api.get("/DocumentManagement/NoFilesExtn?extn=" + fileExtnName).then((data) => {
        // loadDocType(); 
        if (!data) {
            let confirmval = confirm("Are your sure you want to delete this ?", "Yes", "No");
            if (confirmval) {
                api.get("/DocumentManagement/DeleteExtnInfo?extnId=" + extnId).then((data) => {
                    // loadDocType();
                    var tablebody = $("#DocTypePopupExtngrid tbody");
                    $(tablebody).html("");
                    loadFileExtnSelect();
                }).catch((error) => {

                });
            }
        } else {
            alert("Deletion of File Extn can be done after files with the Extn are deleted from the System");
        }
    }).catch((error) => {

    });
   
}